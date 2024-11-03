8006773c:
	j 0x732D8
	lw  $ra, 0x18($sp)

800732D8:
	lw    $s0, 0xF710($s4)    # R1 Button
	lb    $s1, 0xD5A3($s4)    # Floor 8005d5a3

	blez   $s0, 4
	lw    $s0, 0xF718($s4)    # L1 Button
	addiu $s1, $s1, 1
	j 0x73304
	sb    $s1, 0xD5A3($s4)

	nop
	blez   $s0, 14
	addiu $s1, $s1, -1
	sb    $s1, 0xD5A3($s4)

	addiu $s1, $s1, 1     # update floor view
	addiu $s0, $r0, 10
	divu  $s1, $s0
	mflo  $s0
	mfhi  $s1
	beq   $s0, $r0, 3
	sb    $s0, 0xE3ED($s4)    # floor 1st digit view
	j 0x73334
	sb    $s1, 0xE3EE($s4)    # floor 2nd digit view must be 0xf if empty : 01 0F FF

	sb    $s1, 0xE3ED($s4)
	ori   $s0, $r0, 255
	sb    $s0, 0xE3EE($s4)

	j 0x67744
	lw    $s1, 0x14($sp)
