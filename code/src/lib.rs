pub const SO_CHONKY: [u8; 8_000_000] = [0; 8_000_000];

#[macro_export]
macro_rules! perf {
    ($name:expr, $e:expr) => {{
        let then = Instant::now();
        let result = $e;
        println!(concat!("[", $name, "] {:.2?}"), then.elapsed());
        result
    }};
}
