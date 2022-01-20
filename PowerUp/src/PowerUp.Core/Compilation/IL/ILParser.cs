﻿using PowerUp.Core.Errors;
using System;
using System.Collections.Generic;

namespace PowerUp.Core.Compilation
{
    public class ILParser
    {
        public class Enviroment
        {
            public string Name { get; set; }
            public Enviroment Next { get; set; }

            public Enviroment(string name) { Name = name; }
        }

        private List<Error> errors = new List<Error>();
        private List<ILPToken> tokens = null;
        private int IP = 0;
        //
        // Poor mans enviroment made for simple error handling.
        //
        private Enviroment enviroment = null;

        public ILParseResult Parse(List<ILPToken> tokens)
        {
            ILTokenizer tokenizer = new ILTokenizer();
            this.tokens = tokens;
            this.errors.Clear();
            IP = 0;

            var currentEnv = new Enviroment("");
            enviroment = currentEnv;

            //
            // For now let's just parse methods;
            // @TODO: 30.08 BA Improve this.
            // 
            var result = new ILParseResult();
            ILClass @class = new ILClass();

            for(IP = 0; IP < tokens.Count; IP++)
            {
                var token = tokens[IP];
                if(token.Is(ILTokenKind.Word) && token.GetValue() == ".method")
                {
                    var parsedMethod = ParseMethod();
                    @class.Methods.Add(parsedMethod);
                }
            }
            result.Classes.Add(@class);
            result.Errors = errors;

            return result;
        }

        private ILMethod ParseMethod()
        {
            ILMethod method = new ILMethod();

            //
            // Parse Attributes:
            // Parse Accessor + HideBySig + Instance
            //
            var accessorToken = Consume();
            var maybeSigToken = Consume();

            if(maybeSigToken.GetValue() == "hidebysig")
            {
                var instanceToken   = Consume();
                method.InstanceType = instanceToken.GetValue();
            }
            //
            // Signature was empty it must be something else.
            //
            else
            {
                method.InstanceType = maybeSigToken.GetValue();
            }

            method.Accessor = accessorToken.GetValue();

            //
            // Parse Method Signatue
            //
            var returnType = Consume();
            var name = Consume(); 
            method.Returns = returnType.GetValue();
            method.Name = name.GetValue();
            method.Args = ParseMethodArguments();

            var currentEnv = new Enviroment(method.Name + $"({string.Join(",", method.Args)})");
            enviroment = currentEnv;
            //
            // Parse Method Code
            //
            if (Find(ILTokenKind.LBracket))
            {
                Find(ILTokenKind.Word);
                //
                // Possible maxstack opcode here.
                // This is probably required but I would like to avoid it.
                //
                var instructionToken = tokens[IP];
                if (instructionToken.GetValue() == ".maxstack")
                {
                    //Size
                    var size = Consume();
                    method.StackSize = int.Parse(size.GetValue());
                    //
                    // Move to code
                    //
                    instructionToken = Consume();
                }

                //
                // Opcodes can be labeled, so we need to parse two options:
                // 1) L0001: ret
                // 2) ret
                //
                for (; IP < tokens.Count;)
                {
                    var ilInstruction = new ILInst();
                    if (instructionToken.Is(ILTokenKind.RBracket)) break;
                    //
                    // Label
                    //
                    if (IsLabel(instructionToken))
                    {
                        ilInstruction.Label = instructionToken.GetValue();
                        ilInstruction.Label = ilInstruction.Label.Substring(0, ilInstruction.Label.Length - 1);

                        instructionToken = Consume();
                        ilInstruction.OpCode = instructionToken.GetValue();
                    }
                    // Value
                    else if (instructionToken.Is(ILTokenKind.Word))
                    {
                        ilInstruction.OpCode = instructionToken.GetValue();
                    }
                    //
                    // We might have arg list here.
                    //
                    instructionToken = Consume();

                    //
                    // Continue or Exit.
                    //
                    if (IsLabel(instructionToken))
                    {
                        //
                        // No Arguments, but we have another instruction in the set.
                        //
                        method.Code.Add(ilInstruction);
                        continue;
                    }
                    else if (instructionToken.Is(ILTokenKind.RBracket))
                    {
                        //
                        // End of method / block.
                        //
                        method.Code.Add(ilInstruction);
                        break;
                    }
                    //
                    // We should have some arguments to parse now.
                    // Here are some examples: 
                    // - 123
                    // - "abc"
                    // - [System.Private.CoreLib]System.Int32
                    // - (LABEL1, LABEL2)

                    //
                    // @TODO 31.08.21 BA Expand this.
                    //

                    // Array, Call, or Type
                    if (instructionToken.Is(ILTokenKind.LIndex))
                    {
                        //
                        // Parse the entire index and whatever is left.
                        // The index brackets should denote the library/namespace in this context.
                        //
                        if (Find(ILTokenKind.RIndex))
                        {
                            //
                            // Fetch the type.
                            //
                            instructionToken = Consume();
                            ilInstruction.Arguments = new[] { instructionToken.GetValue() };
                        }
                        else
                        {
                            Error("Found an opening bracket, but the instruction is missing the closing bracket");
                            return method;
                        }
                    }
                    //
                    // Arg List
                    //
                    else if (instructionToken.Is(ILTokenKind.LParen))
                    {
                        List<string> argumentList = new List<string>();
                        for (; IP < tokens.Count;)
                        {
                            var arg = Consume();
                            if (arg.Is(ILTokenKind.RParen)) break;

                            argumentList.Add(arg.GetValue());

                            var possibleCommaOrEnd = Peek();
                            if (possibleCommaOrEnd.Is(ILTokenKind.Comma))
                            {
                                Consume();
                                continue;
                            }
                            else if (possibleCommaOrEnd.Is(ILTokenKind.RParen))
                            {
                                Consume();
                                break;
                            }
                        }

                        ilInstruction.Arguments = argumentList.ToArray();
                    }
                    //
                    // Meh, we should detect that in tokenization; for not let's leave it like that.
                    // 
                    else if (ilInstruction.OpCode == "call" || ilInstruction.OpCode == "callvirt" || ilInstruction.OpCode == "calli")
                    {
                        ilInstruction = new ILCallInst() { Label = ilInstruction.Label, OpCode = ilInstruction.OpCode };

                        List<string> methodCallArgs = new List<string>();

                        for (; IP < tokens.Count;)
                        {
                            //
                            // This should be a return type or "instance" followed by return type.
                            //
                            if (instructionToken.GetValue() == "instance")
                                Consume(); //Move to return type.

                            //
                            // I don't know what the spec does say about the dll import name
                            // but I will make this optional since for most things this will not be needed.
                            //
                            if (PeekFind(ILTokenKind.LIndex))
                            {
                                //
                                // Fetch the assembly name (This is handly to create a fully specified name when fetching the type).
                                //
                                instructionToken = Consume();
                                ((ILCallInst)ilInstruction).AssemblyName = instructionToken.GetValue();

                                if (PeekFind(ILTokenKind.RIndex) == false)
                                {
                                    Error("Found an opening bracket in the argument list, but the closing bracket is missing");
                                    return method;
                                }
                            }
                            //
                            // This should be our method call.
                            //
                            instructionToken = Consume();

                            var typeAndCall = instructionToken.GetValue();
                            var typeAndCallPair = typeAndCall.Split("::");

                            ((ILCallInst)ilInstruction).TypeName = typeAndCallPair[0];
                            ((ILCallInst)ilInstruction).MethodCallName = typeAndCallPair[1];

                            instructionToken = Consume();
                            if (instructionToken.Is(ILTokenKind.LParen))
                            {
                                //
                                // Parse method arguments;
                                //
                                for (; IP < tokens.Count;)
                                {
                                    instructionToken = Consume();

                                    if (instructionToken.Is(ILTokenKind.RParen)) break;

                                    methodCallArgs.Add(instructionToken.GetValue());

                                    var possibleCommaOrEnd = Peek();
                                    if (possibleCommaOrEnd.Is(ILTokenKind.Comma))
                                    {
                                        Consume();
                                        continue;
                                    }
                                    else if (possibleCommaOrEnd.Is(ILTokenKind.RParen))
                                    {
                                        Consume();
                                        break;
                                    }
                                }
                                break;
                            }

                            Error("Found an opening bracket, but the instruction is missing the closing bracket");
                            return method;
                        }

                        ilInstruction.Arguments = methodCallArgs.ToArray();
                    }
                    else
                    {
                        ilInstruction.Arguments = new[] { instructionToken.GetValue() };
                    }

                    method.Code.Add(ilInstruction);
                    instructionToken = Consume();
                }
            }

            return method;    
        }

        private bool IsLabel(ILPToken token)
        {
            return token.Kind == ILTokenKind.Word && token.GetValue().EndsWith(":");
        }

        private bool Find(ILTokenKind kind)
        {
            for (; IP < tokens.Count; IP++)
                if (tokens[IP].Kind == kind)
                    return true;

            return false;
        }

        private bool PeekFind(ILTokenKind kind)
        {
            int ip = IP;
            for (; ip < tokens.Count; ip++)
            {
                if (tokens[ip].Kind == kind)
                {
                    IP = ip;
                    return true;
                }
            }

            return false;
        }

        private List<ILMethodArg> ParseMethodArguments()
        {
            List<ILMethodArg> args = new List<ILMethodArg>();
            //
            // Skip over LParen
            //
            Consume();
            //
            // Arg
            //
            for(;IP < tokens.Count;)
            {
                var argType = Consume();
                var argName = Consume();

                args.Add(new ILMethodArg() { Name = argName.GetValue(), Type = argType.GetValue() });

                var possibleCommaOrEnd = Peek();
                if (possibleCommaOrEnd.Is(ILTokenKind.Comma))
                {
                    Consume();
                    continue;
                }
                else if (possibleCommaOrEnd.Is(ILTokenKind.RParen))
                {
                    Consume();
                    break;
                }

            }

            return args;
        }

        private ILPToken Peek()
        {
            return tokens[IP + 1];
        }

        private ILPToken Consume()
        {
            return tokens[++IP];
        }


        //
        // This method will create all of the needed information about the error
        // we just need to provide a good message and decide if we return or continue going.
        //
        private void Error(string message) 
        {
            //
            // Get the current IP; if we're overflowed then get the previrous IP and extract
            // line information.
            //
            ILPToken currentToken = null;
            int position = -1;
            if (IP < tokens.Count)
            {
                currentToken = tokens[IP];
                position = currentToken.Position;
            }
            else if (IP - 1 < tokens.Count)
            {
                currentToken = tokens[IP - 1];
                position = currentToken.Position;
            }

            errors.Add(new Error() { 
                Trace = enviroment.Name, 
                Position = position, 
                Message = $"[PARSING ERROR]{Environment.NewLine}{message}" });
        }
    }
}
