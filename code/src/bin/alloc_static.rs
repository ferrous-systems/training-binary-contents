const HELLO: &'static str = "HELLOWORLD1234567890";

const SIZE: usize = 1024 * 1024;
static mut CHONKY: [u8; SIZE] = [0xfa; SIZE];

use show_me_what_u_got::SO_CHONKY;

pub fn main() {
    println!("hello, HELLO! {}", HELLO);
    unsafe {
        CHONKY[0] = 12;
        CHONKY[0] = 14;
        println!("hello, CHONKY! {}", CHONKY[0]);
    }

    //println!("hello, SO_CHONKY! {}", SO_CHONKY[0]);
}
