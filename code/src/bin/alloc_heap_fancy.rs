#![allow(unused)]

use std::{
    alloc::{GlobalAlloc, Layout, System},
    time::Instant,
};

use show_me_what_u_got::perf;

static mut ALLOCS: heapless::Vec<Layout, 1024> = heapless::Vec::new();

struct MyAllocator;

unsafe impl GlobalAlloc for MyAllocator {
    unsafe fn alloc(&self, layout: Layout) -> *mut u8 {
        ALLOCS.push(layout.clone());
        System.alloc(layout)
    }

    unsafe fn dealloc(&self, ptr: *mut u8, layout: Layout) {
        System.dealloc(ptr, layout)
    }
}

#[global_allocator]
static GLOBAL: MyAllocator = MyAllocator;

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
    let bigs = perf!(
        "create",
        vec![Big {
            name: "it's-a-mee, Mario!".to_string(),
            data: BigVec::new(),
        }]
    );

    let a_name = perf!("map-name", bigs.into_iter().map(|item| item.name).next());
    println!("a name: {:?}", a_name);

    let bigs = perf!(
        "create",
        vec![Big {
            name: "it's-a-mee, Mario!".to_string(),
            data: BigVec::new(),
        }]
    );

    let a_number = perf!(
        "map-num",
        bigs.into_iter().map(|item| item.data.0[0] * 0).next()
    );
    println!("a number: {:?}", a_number);

    eprintln!();
    eprintln!("allocations:");
    let mut total = 0;
    unsafe {
        for alloc in ALLOCS.iter() {
            let size = alloc.size();
            total += size;
            eprintln!("{}", size);
        }
    }
    eprintln!("===========================");
    eprintln!(
        "alloc grand total: {:.2}MiB",
        total as f64 / (1024. * 1024.)
    );
}
