#!/bin/zsh
dir="/Users/anatol.ulrich/work/ttt/binary_contents/code/src/bin"
bin=$1
src/PowerUp.Watcher/bin/Debug/net6.0/PowerUp.Watcher -rs "${dir}/${bin}.rs" "${dir}/${bin}.asm"
