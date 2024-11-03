STAG4000.PRO
80066384:
	j 0x730B0
	lw  $ra, 0x18($sp)

800730B0:
	lw    $s0, 0x1b70($s4)    # R1 Button
	lb    $s1, 0x1d2b($s4)    # Floor

	blez   $s0, 4
	lw    $s0, 0x1b78($s4)    # L1 Button
	addiu $s1, $s1, 1
	j 0x730dc
	sb    $s1, 0x1d2b($s4)

	nop
	blez   $s0, 14
	addiu $s1, $s1, -1
	sb    $s1, 0x1d2b($s4)

	addiu $s1, $s1, 1     # update floor view
	addiu $s0, $r0, 10
	divu  $s1, $s0
	mflo  $s0
	mfhi  $s1
	beq   $s0, $r0, 3
	sb    $s0, 0x2b73($s4)    # floor 1st digit view
	j 0x7310c
	sb    $s1, 0x2b74($s4)    # floor 2nd digit view must be 0xf if empty : 01 0F FF

	sb    $s1, 0x2b73($s4)
	ori   $s0, $r0, 255
	sb    $s0, 0x2b74($s4)

	j 0x6638C
	lw    $s1, 0x14($sp)