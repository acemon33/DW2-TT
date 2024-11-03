8006ad28:
	nop
	nop
	nop
	nop
	nop

8006b568:
	j 0x74130
	nop

80074130:
	div   $t1, $a0

	add   $t1, $r0, $v0
	lw    $a0, 0x60($sp)
	jal   0x1f130
	nop

	andi  $v0, $v0, 0x4
	beq   $v0, $r0, 0x9
	nop

	sll   $v0, $s6, 0x1
	lui   $s2, 0x8007
	addiu $s2, $s2, 0x4046
	addu  $a0, $v0, $s2
	addiu $a1, s4, 0x1e
	addiu $a2, $s2,-0x24
	jal   0x6a968
	addu  $a2, $v0, $a2

	mflo  $s2
	j     0x6b570
	add   $v0, $r0, $t1
