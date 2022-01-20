# Instruction Count: 30; Code Size: 29
 main():
  0000: push   rbx                           
  0001: sub    rsp, 80                        # rsp -= 80 stack.push(80)
  0002: lea    rax, [rip + .L__unnamed_1]     # rax = rip + .L__unnamed_1
  0003: mov    qword ptr [rsp + 16], rax      # qword ptr [rsp + 16] = rax
  0004: mov    qword ptr [rsp + 24], 1        # qword ptr [rsp + 24] = 1
  0005: mov    qword ptr [rsp + 32], 0        # qword ptr [rsp + 32] = 0
  0006: lea    rax, [rip + .L__unnamed_2]     # rax = rip + .L__unnamed_2
  0007: mov    qword ptr [rsp + 48], rax      # qword ptr [rsp + 48] = rax
  0008: mov    qword ptr [rsp + 56], 0        # qword ptr [rsp + 56] = 0
  0009: mov    rbx, qword ptr [rip + std::io::stdio::_print::h1ac276ba56ca809c@GOTPCREL] # rbx = qword ptr [rip + std::io::stdio::_print::h1ac276ba56ca809c@GOTPCREL]
  000A: lea    rdi, [rsp + 16]                # rdi = rsp + 16
  000B: call   rbx                           
  000C: movabs rax, 30000000000              
  000D: mov    qword ptr [rsp + 8], rax       # qword ptr [rsp + 8] = rax
  000E: lea    rax, [rsp + 8]                 # rax = rsp + 8
  000F: mov    qword ptr [rsp + 64], rax      # qword ptr [rsp + 64] = rax
  0010: mov    rax, qword ptr [rip + core::fmt::num::imp::<impl core::fmt::Display for u64>::fmt::hd90a8e379e5d6e4b@GOTPCREL] # rax = qword ptr [rip + core::fmt::num::imp::<impl core::fmt::Display for u64>::fmt::hd90a8e379e5d6e4b@GOTPCREL]
  0011: mov    qword ptr [rsp + 72], rax      # qword ptr [rsp + 72] = rax
  0012: lea    rax, [rip + .L__unnamed_3]     # rax = rip + .L__unnamed_3
  0013: mov    qword ptr [rsp + 16], rax      # qword ptr [rsp + 16] = rax
  0014: mov    qword ptr [rsp + 24], 2        # qword ptr [rsp + 24] = 2
  0015: mov    qword ptr [rsp + 32], 0        # qword ptr [rsp + 32] = 0
  0016: lea    rax, [rsp + 64]                # rax = rsp + 64
  0017: mov    qword ptr [rsp + 48], rax      # qword ptr [rsp + 48] = rax
  0018: mov    qword ptr [rsp + 56], 1        # qword ptr [rsp + 56] = 1
  0019: lea    rdi, [rsp + 16]                # rdi = rsp + 16
  001A: call   rbx                           
  001B: add    rsp, 80                        # rsp += 80 stack.pop(80)
  001C: pop    rbx                           
  001D: ret                                   # return;
