use std::{
    env::args,
    fs::File,
    io::{Read, Write},
};

use regex::{Captures, Regex};
use rustc_demangle::demangle;
fn main() -> anyhow::Result<()> {
    let file_name = &args().nth(1).ok_or(anyhow::anyhow!("missing file name"))?;

    let mut contents = String::new();
    File::open(file_name)?.read_to_string(&mut contents)?;

    let mangle_re = Regex::new(r" (_ZN.*?)[@\s\n]")?;

    let demangled = mangle_re.replace_all(&contents, |captures: &Captures| {
        let hit = &captures[1];
        let all = &captures[0];
        all.replace(hit, &demangle(hit).to_string())
    });

    File::create(file_name)?.write_all(&demangled.as_bytes())?;
    Ok(())
}
