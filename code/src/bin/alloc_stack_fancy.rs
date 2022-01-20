use std::time::Instant;

use show_me_what_u_got::perf;

const SZ: usize = 8_000_000;

#[derive(Clone)]
struct Big {
    name: String,
    data: [u8; SZ],
}

macro_rules! perf {
    ($name:expr, $e:expr) => {{
        let then = Instant::now();
        let result = $e;
        println!(concat!("[", $name, "] {:.2?}"), then.elapsed());
        result
    }};
}

impl Big {}
pub fn main() {
    println!("Hello, world!");
    let bigs = perf!(
        "create",
        vec![Big {
            name: "it's-a-mee, Mario!".to_string(),
            data: [13; SZ],
        }]
    );

    let a_name = perf!("map-name", bigs.into_iter().map(|item| item.name).next());
    println!("a name: {:?}", a_name);

    let bigs = perf!(
        "create",
        vec![Big {
            name: "it's-a-mee, Mario!".to_string(),
            data: [13; SZ],
        }]
    );

    let a_number = perf!(
        "map-num",
        bigs.into_iter().map(|item| item.data[0] * 0).next()
    );
    println!("a number: {:?}", a_number);
}
