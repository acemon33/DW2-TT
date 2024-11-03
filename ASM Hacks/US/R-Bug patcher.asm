8006d554:
	j 0x73348

80073348:
	addiu $v0, $r0, 0xb
	bne $t3, $v0, 2
	addiu $v0, $r0, 0
	sw  $v0, 0x0($t0)
	j 0x6d55c
	sb  $t3, 0x8($t0)
