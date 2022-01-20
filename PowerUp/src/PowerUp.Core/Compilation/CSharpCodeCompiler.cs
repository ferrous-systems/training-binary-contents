﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Text;
using PowerUp.Core.Decompilation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace PowerUp.Core.Compilation
{
    public class CSharpCodeCompiler
    {

        public const string BaseClassName = "CompilerGen";

        public string DotNetCoreDirPath { get; set; }
        public LanguageVersion LanguageVersion { get; private set; }

        public CSharpCodeCompiler(string dotNetCoreDirPath, LanguageVersion languageVersion = LanguageVersion.Latest)
        {
            DotNetCoreDirPath = dotNetCoreDirPath;
            LanguageVersion = languageVersion;
        }

        public CompilationUnit Compile(string code)
        {
            CompilationOptions options = new CompilationOptions();
            var sourceCode  = RewriteCode(code, options);
            var compilation = CSharpCompilation.Create("assembly_" + DateTime.Now.Ticks.ToString())
            .WithOptions(
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                    .WithOptimizationLevel(OptimizationLevel.Release)
                    .WithAllowUnsafe(true))
            .AddReferences(ResolveReferences(DotNetCoreDirPath))
            .AddSyntaxTrees(CSharpSyntaxTree.ParseText(sourceCode, 
            CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion)));

            EmitOptions emitOptions = new EmitOptions(debugInformationFormat: DebugInformationFormat.PortablePdb);
            DecompilationUnit unit = new DecompilationUnit();
            
            var assemblyStream = new MemoryStream();
            var pdbStream = new MemoryStream();
            var compilationResult = compilation.Emit(assemblyStream, pdbStream: pdbStream, options: emitOptions);

            return new CompilationUnit()
            {
                SourceCode = sourceCode,
                AssemblyStream = assemblyStream,
                PDBStream = pdbStream,
                CompilationResult = compilationResult,
                LanguageVersion = compilation.LanguageVersion.ToDisplayString(),
                Options = options
            };
        }

        //
        // Parse and rewrite code to provide some extra utility when like
        // Running / Benchmark and Aliases on C# Methods and Classes.
        //
        private string RewriteCode(string code, CompilationOptions options)
        {
            var ast  = CSharpSyntaxTree.ParseText(code);
            var root = ast.GetRoot();

            CodeRewriter rewriter = new CodeRewriter(options);
            root           = rewriter.Visit(root);
            code           = root.ToFullString();
            var benchCode  = rewriter.GetBenchCodeOrEmpty();
            var usingCode  = rewriter.GetUsingsOrEmpty();
            var sizeOfCode = rewriter.GetStructSizeOrEmpty();
            var sourceCode = $@"
                    using System.Linq;
                    using System.Diagnostics;
                    using System;
                    using System.Runtime.CompilerServices;
                    using System.Collections.Generic;
                    using System.Collections;
                    using System.Linq.Expressions;
                    using System.Runtime.InteropServices;
                    using System.Threading.Tasks;

                    {usingCode}

                    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
                    sealed class JITAttribute : Attribute
                    {{
                        private Type[] _types;

                        public JITAttribute(params Type[] types)
                        {{
                            _types = types;
                        }}

                        public Type[] Types
                        {{
                            get {{ return _types; }}
                        }}
                    }}

                    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct, Inherited = false, AllowMultiple = true)]
                    sealed class ConstructorAttribute : Attribute
                    {{
                        private Object[] _parameters;

                        public ConstructorAttribute(params Object[] parameters)
                        {{
                            _parameters = parameters;
                        }}

                        public Object[] Parameters
                        {{
                            get {{ return _parameters; }}
                        }}
                    }}

                    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
                    sealed class BenchAttribute : Attribute
                    {{
                        public BenchAttribute(params object[] arguments)
                        {{
                            Arguments = arguments;
                        }}

                        public int WarmUpCount {{ get; set; }} = 1000;
                        public int RunCount {{ get; set; }} = 1000;
                        public object[] Arguments {{get; set;}} 

                    }}

                    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
                    sealed class RunAttribute : Attribute
                    {{
                        public RunAttribute()
                        {{
                        }}
                    }}

                    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
                    sealed class HideAttribute : Attribute
                    {{
                        public HideAttribute()
                        {{
                        }}
                    }}

                    public class _Log 
                    {{
                        public static List<string> print = new();
                    }}

                    public class {BaseClassName}
                    {{
                        {code}
                        {benchCode}
                        {sizeOfCode}

                        static void Print(object o, string name = ""<expr>"") 
                        {{
                            if(o is string str)
                            {{
                              _Log.print.Add(name + "" => "" + o.ToString());
                            }}
                            else if(o is IEnumerable arr)
                            {{
                              string message = name + "" => "" + ""["";
                              foreach (var value in arr)
                              {{
                                  message += value + "", "";
                              }}
                              message = message.Remove(message.Length - 2);
                              message += ""]"";
                              _Log.print.Add(message);
                           }}
                           else
                             _Log.print.Add(name + "" => "" + o.ToString());
                        }}

                    }}";

            return sourceCode;
        }

        public static PortableExecutableReference[] ResolveReferences(string dotNetCoreDirPath)
        {
            return new PortableExecutableReference[]
            {
                 MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location),
                 MetadataReference.CreateFromFile(typeof(System.Console).GetTypeInfo().Assembly.Location),
                 MetadataReference.CreateFromFile(Path.Combine(dotNetCoreDirPath, "mscorlib.dll")),
                 MetadataReference.CreateFromFile(Path.Combine(dotNetCoreDirPath, "netstandard.dll")),
                 MetadataReference.CreateFromFile(Path.Combine(dotNetCoreDirPath, "System.Runtime.dll")),
                 MetadataReference.CreateFromFile(Path.Combine(dotNetCoreDirPath, "System.Linq.dll")),
                 MetadataReference.CreateFromFile(Path.Combine(dotNetCoreDirPath, "System.Linq.Expressions.dll")),
                 MetadataReference.CreateFromFile(Path.Combine(dotNetCoreDirPath, "System.Runtime.CompilerServices.Unsafe.dll"))
            };
        }
    }
}
