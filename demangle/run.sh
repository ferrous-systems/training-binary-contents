#!/bin/zsh
file="../code/src/bin/loop.asm"
while sleep 1; do fswatch "$file" -1; cargo run --release "$file"; done
