80069a44
80069a90
	s4
mock 80069ac0
to 80069bac

80069a90:
	lui   $v0, 0x8007
	j 0x200004
	addiu $s0, $v0, 0x3cc0

80200004:
	addu  $v0, $s4, $s0
	lbu   $v0, 0x34f($v0)
	addiu $s0, $a0, 0x31b8
	andi  $v0, $v0, 0x4
	bne   $v0, $r0, 3
	lui   $v0, 0x8007
	j 0x69a9c
	addiu $s1, $v0, 0x31c8
	j 0x69bac
	nop