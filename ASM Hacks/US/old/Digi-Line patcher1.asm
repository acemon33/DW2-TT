800175cc:
	j 0x49Af0

80049Af0:
	lw    $v1, -0x8f8($v1)
	nop
	blez  $v1, 9
	nop
	lh    $v0, 0x19e($s0)
	nop
	slti  $v1, $v0, 1
	bne   $v1, $r0, 4
	nop
	sh    $v0, 0x19c($s0)
	j 0x175fc
	nop
	lui   $v1, 0x8006
	lw    $v1, -0x8fc($v1)
	j 0x175d4
	nop

STAG4000:800639d8:
	nop
