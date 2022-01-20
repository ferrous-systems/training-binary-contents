	.text
	.intel_syntax noprefix
	.file	"loop.9e85ccde-cgu.0"
	.section	.text._ZN4loop4main17h05efe4255b55eb62E,"ax",@progbits
	.globl	_ZN4loop4main17h05efe4255b55eb62E
	.p2align	4, 0x90
	.type	_ZN4loop4main17h05efe4255b55eb62E,@function
_ZN4loop4main17h05efe4255b55eb62E:
.Lfunc_begin0:
	.file	1 "/Users/anatol.ulrich/work/ttt/binary_contents/code/src/bin/loop.rs"
	.loc	1 5 0
	.cfi_startproc
	push	rbx
	.cfi_def_cfa_offset 16
	sub	rsp, 80
	.cfi_def_cfa_offset 96
	.cfi_offset rbx, -16
	.file	2 "/rustc/f1edd0429582dd29cccacaf50fd134b05593bd9c/library/core/src/fmt/mod.rs"
	.loc	2 365 9 prologue_end
	lea	rax, [rip + .L__unnamed_1]
	mov	qword ptr [rsp + 16], rax
	mov	qword ptr [rsp + 24], 1
	mov	qword ptr [rsp + 32], 0
	lea	rax, [rip + .L__unnamed_2]
	mov	qword ptr [rsp + 48], rax
	mov	qword ptr [rsp + 56], 0
.Ltmp0:
	.loc	1 6 5
	mov	rbx, qword ptr [rip + _ZN3std2io5stdio6_print17h1ac276ba56ca809cE@GOTPCREL]
	lea	rdi, [rsp + 16]
	call	rbx
	movabs	rax, 30000000000
	.loc	1 0 0 is_stmt 0
	mov	qword ptr [rsp + 8], rax
	lea	rax, [rsp + 8]
	.loc	1 12 5 is_stmt 1
	mov	qword ptr [rsp + 64], rax
	mov	rax, qword ptr [rip + _ZN4core3fmt3num3imp52_$LT$impl$u20$core..fmt..Display$u20$for$u20$u64$GT$3fmt17hd90a8e379e5d6e4bE@GOTPCREL]
	mov	qword ptr [rsp + 72], rax
.Ltmp1:
	.loc	2 365 9
	lea	rax, [rip + .L__unnamed_3]
	mov	qword ptr [rsp + 16], rax
	mov	qword ptr [rsp + 24], 2
	mov	qword ptr [rsp + 32], 0
	lea	rax, [rsp + 64]
	mov	qword ptr [rsp + 48], rax
	mov	qword ptr [rsp + 56], 1
	lea	rdi, [rsp + 16]
.Ltmp2:
	.loc	1 12 5
	call	rbx
	.loc	1 13 2
	add	rsp, 80
	.cfi_def_cfa_offset 16
	pop	rbx
	.cfi_def_cfa_offset 8
	ret
.Ltmp3:
.Lfunc_end0:
	.size	_ZN4loop4main17h05efe4255b55eb62E, .Lfunc_end0-_ZN4loop4main17h05efe4255b55eb62E
	.cfi_endproc

	.type	.L__unnamed_4,@object
	.section	.rodata..L__unnamed_4,"a",@progbits
.L__unnamed_4:
	.ascii	"Hello, world!\n"
	.size	.L__unnamed_4, 14

	.type	.L__unnamed_1,@object
	.section	.data.rel.ro..L__unnamed_1,"aw",@progbits
	.p2align	3
.L__unnamed_1:
	.quad	.L__unnamed_4
	.asciz	"\016\000\000\000\000\000\000"
	.size	.L__unnamed_1, 16

	.type	.L__unnamed_2,@object
	.section	.rodata..L__unnamed_2,"a",@progbits
	.p2align	3
.L__unnamed_2:
	.size	.L__unnamed_2, 0

	.type	.L__unnamed_5,@object
	.section	.rodata..L__unnamed_5,"a",@progbits
.L__unnamed_5:
	.ascii	"Hello, done! "
	.size	.L__unnamed_5, 13

	.type	.L__unnamed_6,@object
	.section	.rodata..L__unnamed_6,"a",@progbits
.L__unnamed_6:
	.byte	10
	.size	.L__unnamed_6, 1

	.type	.L__unnamed_3,@object
	.section	.data.rel.ro..L__unnamed_3,"aw",@progbits
	.p2align	3
.L__unnamed_3:
	.quad	.L__unnamed_5
	.asciz	"\r\000\000\000\000\000\000"
	.quad	.L__unnamed_6
	.asciz	"\001\000\000\000\000\000\000"
	.size	.L__unnamed_3, 32

	.section	.debug_abbrev,"",@progbits
	.byte	1
	.byte	17
	.byte	1
	.byte	37
	.byte	14
	.byte	19
	.byte	5
	.byte	3
	.byte	14
	.byte	16
	.byte	23
	.byte	27
	.byte	14
	.ascii	"\264B"
	.byte	25
	.byte	17
	.byte	1
	.byte	18
	.byte	6
	.byte	0
	.byte	0
	.byte	2
	.byte	57
	.byte	1
	.byte	3
	.byte	14
	.byte	0
	.byte	0
	.byte	3
	.byte	46
	.byte	0
	.byte	110
	.byte	14
	.byte	3
	.byte	14
	.byte	58
	.byte	11
	.byte	59
	.byte	5
	.byte	32
	.byte	11
	.byte	0
	.byte	0
	.byte	4
	.byte	46
	.byte	1
	.byte	17
	.byte	1
	.byte	18
	.byte	6
	.byte	64
	.byte	24
	.byte	110
	.byte	14
	.byte	3
	.byte	14
	.byte	58
	.byte	11
	.byte	59
	.byte	11
	.byte	63
	.byte	25
	.byte	0
	.byte	0
	.byte	5
	.byte	29
	.byte	0
	.byte	49
	.byte	19
	.byte	17
	.byte	1
	.byte	18
	.byte	6
	.byte	88
	.byte	11
	.byte	89
	.byte	11
	.byte	87
	.byte	11
	.byte	0
	.byte	0
	.byte	0
	.section	.debug_info,"",@progbits
.Lcu_begin0:
	.long	.Ldebug_info_end0-.Ldebug_info_start0
.Ldebug_info_start0:
	.short	4
	.long	.debug_abbrev
	.byte	8
	.byte	1
	.long	.Linfo_string0
	.short	28
	.long	.Linfo_string1
	.long	.Lline_table_start0
	.long	.Linfo_string2

	.quad	.Lfunc_begin0
	.long	.Lfunc_end0-.Lfunc_begin0
	.byte	2
	.long	.Linfo_string3
	.byte	2
	.long	.Linfo_string4
	.byte	2
	.long	.Linfo_string5
	.byte	3
	.long	.Linfo_string6
	.long	.Linfo_string7
	.byte	2
	.short	361
	.byte	1
	.byte	0
	.byte	0
	.byte	0
	.byte	2
	.long	.Linfo_string8
	.byte	4
	.quad	.Lfunc_begin0
	.long	.Lfunc_end0-.Lfunc_begin0
	.byte	1
	.byte	87
	.long	.Linfo_string9
	.long	.Linfo_string10
	.byte	1
	.byte	5

	.byte	5
	.long	57
	.quad	.Lfunc_begin0
	.long	.Ltmp0-.Lfunc_begin0
	.byte	1
	.byte	6
	.byte	5
	.byte	5
	.long	57
	.quad	.Ltmp1
	.long	.Ltmp2-.Ltmp1
	.byte	1
	.byte	12
	.byte	5
	.byte	0
	.byte	0
	.byte	0
.Ldebug_info_end0:
	.section	.text._ZN4loop4main17h05efe4255b55eb62E,"ax",@progbits
.Lsec_end0:
	.section	.debug_aranges,"",@progbits
	.long	44
	.short	2
	.long	.Lcu_begin0
	.byte	8
	.byte	0
	.zero	4,255
	.quad	.Lfunc_begin0
	.quad	.Lsec_end0-.Lfunc_begin0
	.quad	0
	.quad	0
	.section	.debug_str,"MS",@progbits,1
.Linfo_string0:
	.asciz	"clang LLVM (rustc version 1.57.0 (f1edd0429 2021-11-29))"
.Linfo_string1:
	.asciz	"/Users/anatol.ulrich/work/ttt/binary_contents/code/src/bin/loop.rs"
.Linfo_string2:
	.asciz	"/Users/anatol.ulrich/play/4k/PowerUp"
.Linfo_string3:
	.asciz	"core"
.Linfo_string4:
	.asciz	"fmt"
.Linfo_string5:
	.asciz	"Arguments"
.Linfo_string6:
	.asciz	"_ZN4core3fmt9Arguments6new_v117h873555a14ca7ae8cE"
.Linfo_string7:
	.asciz	"new_v1"
.Linfo_string8:
	.asciz	"loop"
.Linfo_string9:
	.asciz	"_ZN4loop4main17h05efe4255b55eb62E"
.Linfo_string10:
	.asciz	"main"
	.section	.debug_pubnames,"",@progbits
	.long	.LpubNames_end0-.LpubNames_start0
.LpubNames_start0:
	.short	2
	.long	.Lcu_begin0
	.long	146
	.long	57
	.asciz	"new_v1"
	.long	78
	.asciz	"main"
	.long	52
	.asciz	"Arguments"
	.long	47
	.asciz	"fmt"
	.long	42
	.asciz	"core"
	.long	73
	.asciz	"loop"
	.long	0
.LpubNames_end0:
	.section	.debug_pubtypes,"",@progbits
	.long	.LpubTypes_end0-.LpubTypes_start0
.LpubTypes_start0:
	.short	2
	.long	.Lcu_begin0
	.long	146
	.long	0
.LpubTypes_end0:
	.section	".note.GNU-stack","",@progbits
	.section	.debug_line,"",@progbits
.Lline_table_start0:
