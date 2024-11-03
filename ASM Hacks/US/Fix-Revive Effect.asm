8006a8b0:
	j 0x74270
	lhu  $v1, 0x2e($v0)

80074270:
	nop
	slti $s6, $v1, 0x2
	beq  $s6, $r0, 3
	lhu  $v1, 0x2c($v0)
	j 0x6a8b8
	ori  $s6, $r0, 0x1
	j 0x6a8cc
	nop
