8001ff60:
	j 0x498b4
	lw    $v1, 0x1b68($v1)    # [] Button

800498b4:
	nop
	blez  $v1, 9
	nop
	lh    $v0, 0x19e($s0)    # counter
	nop
	slti  $v1, $v0, 1
	bne   $v1, $r0, 4
	nop
	sh    $v0, 0x19c($s0)    # max counter
	j 0x1ff90
	nop
	lui   $v1, 0x8005
	lw    $v1, 0x1b60($v1)    # O Button
	j 0x1ff68
	nop

STAG4000:800637b0:
	nop