8006b018:
	j     0x74244
	lui   $v0, 0x8007

80074244:
	addiu $v1, $v0, 0x3cc0
	addu  $v0, $s6, $v1
	lbu   $v1,0x34f($v0)
	nop
	andi  $v0, $v1,0x8
	bne   $v0, $r0, 3
	lhu   $v0, 0x2b8($a0)
	j     0x6b020
	lh    $v1, 0x16($s4)
	j     0x6b050
	nop
