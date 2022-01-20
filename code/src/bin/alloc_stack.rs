const SZ: usize = 8_000_000;

#[derive(Clone)]
struct Big {
    name: String,
    data: [u8; SZ],
}

impl Big {}
pub fn main() {
    println!("Hello, world!");
    let bigs = vec![Big {
        name: "it's-a-mee, Mario!".to_string(),
        data: [13; SZ],
    }];

    let a_name = bigs.into_iter().map(|item| item.name).next();
    println!("a name: {:?}", a_name);

    let bigs = vec![Big {
        name: "it's-a-mee, Mario!".to_string(),
        data: [13; SZ],
    }];

    let a_number = bigs.into_iter().map(|item| item.data[0] * 0).next();
    println!("a number: {:?}", a_number);
}
