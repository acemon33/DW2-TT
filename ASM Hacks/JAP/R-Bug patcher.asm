STAG4000.PRO
80070c7c:
	j 0x73120

80073120:
	addiu $v0, $r0, 0xb
	bne $t3, $v0, 2
	addiu $v0, $r0, 0
	sw  $v0, 0x0($t0)
	j 0x70c84
	sb  $t3, 0x8($t0)