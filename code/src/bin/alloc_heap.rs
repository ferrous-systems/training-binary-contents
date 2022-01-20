#![allow(unused)]

use std::{
    alloc::{GlobalAlloc, Layout, System},
    time::Instant,
};

#[derive(Clone)]
struct BigVec(Vec<u8>);

impl BigVec {
    fn new() -> Self {
        let mut inner = vec![];
        for _ in 0..8_000_000 {
            inner.push(1)
        }
        BigVec(inner)
    }
}

#[derive(Clone)]
struct Big {
    name: String,
    data: BigVec,
}

impl Big {}
pub fn main() {
    println!("Hello, world!");
    let bigs = vec![Big {
        name: "it's-a-mee, Mario!".to_string(),
        data: BigVec::new(),
    }];

    let a_name = bigs.into_iter().map(|item| item.name).next();
    println!("a name: {:?}", a_name);

    let bigs = vec![Big {
        name: "it's-a-mee, Mario!".to_string(),
        data: BigVec::new(),
    }];

    let a_number = bigs.into_iter().map(|item| item.data.0[0] * 0).next();
    println!("a number: {:?}", a_number);
}
