--- Logic ---
800679e0:
	ori $t9, $v0, 0x0
80067bac:
	j 0x200000
80200000:
	bne   $fp, $r0, 5
	  sltiu $a3, $t9, 0x2
	  beq   $a3, $r0, 3
	  nop
	j 0x67d7c
	nop
	j 0x67bb4
	nop

--- level up hud ---
80071cac:
	j 0x200020
	andi  $a0, $v0, 0x3
80200020:
	bne $a0, $r0, 3
	nop
	j 0x71cb4
	nop
	j 0x71cc0
	ori $a0, $s0, 0x0

--- pad ---
80067d84:
	j 0x200038
80200038:
	lui   $s4,0x8006
	lw    $s1,0xF710($s4)
	lui   $s4,0x8007
	beq   $s1, $r0, 13
	addiu $s2, $r0, 0
	lbu   $s3, 0x400c($s4)
	nop
	sltiu $s6, $s3, 0x2
	beq   $s6, $r0, 9
	lbu   $s3, 0x400c($s4)
	addiu $s2, $s2, 0x1
	bne   $s3, $r0, 2
	addiu $s3, $r0, 0x2
	addiu $s3, $r0, 0x4
	sb    $s3, 0x400c($s4)
	sltiu $s5, $s2, 3
	bne   $s5, $r0, -8
	addiu $s4, $s4, 0x1
	j 0x67d8c
	lw    $ra, 0x9c($sp)