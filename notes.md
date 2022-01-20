# Links
[Rust compiler explorer](https://rust.godbolt.org/)
- navigate between highlighted source + assembly fragments (context menu - "reveal linked code"/"scroll to source")
- change optimization level with e.g. `-C opt-level=0` or `-C opt-level=3` in "Compiler options…" field
    - this can also be used to select a different `--target=` than x86-64 assembly
- builtin hover + context menu help for x86-64 instructions
- functions need to be made `pub`, otherwise no output!

[PowerUp watcher](https://github.com/badamczewski/PowerUp)
- generates + updates `.asm` file with jump guides
- Windows only; I ported it to macos, vendored in `PowerUp/`
    - needs Microsoft .net SDK; build with `dotnet build`
    - see `watch.sh` for example usage after building
- does not demangle, helper tool to do that is in `demangle/`. Example watcher script that auto-updates: `demangle/run.sh` (needs `fswatch` installed)

[Cargo optimization documentation](https://doc.rust-lang.org/cargo/reference/profiles.html)
- optimize for size is a different goal from optimize for speed, those MAY conflict (but don't have to. You never know!)

[min-sized-rust](https://github.com/johnthagen/min-sized-rust) when you really need the smallest binary ever

you can tell Rust to [`[#inline]` functions as manual optimization](https://nnethercote.github.io/perf-book/inlining.html) (the stdlib makes ample use of this); despite their deceptive names, `#[inline(always)]` and `#[inline(never)]` are mere (strong) suggestions

## binary formats
- [ELF](https://en.wikipedia.org/wiki/Executable_and_Linkable_Format)
- [Apple Mach-O](https://web.archive.org/web/20090901205800/http://developer.apple.com/mac/library/documentation/DeveloperTools/Conceptual/MachORuntime/Reference/reference.html)

# some fun commands to try
prerequisites:
- [cargo-binutils](https://github.com/rust-embedded/cargo-binutils)
    - which in turn needs `llvm-tools-preview` (see their README)
- [cargo-bloat](https://github.com/RazrFalcon/cargo-bloat)

```bash
# try leaving out --release in all these commands
$ cargo objdump --release --bin alloc_static -- --disassemble-all --no-show-raw-insn
$ cargo size --release --bin alloc_static -- -A
$ cargo nm --release --bin alloc_static

# more interesting in a larger project with lots of dependencies 
# try leaving out --release here too
$ cargo bloat --release --bin alloc_static 
```
