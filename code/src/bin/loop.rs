//up:showGuides
//up:optimization level = 0
//up:showSource
//up:showASMDocs
pub fn main() {
    println!("Hello, world!");

    let mut i = 0u64;
    for _ in 0..1_000_000_000 {
        i = i.wrapping_add(30);
    }
    println!("Hello, done! {}", i);
}
