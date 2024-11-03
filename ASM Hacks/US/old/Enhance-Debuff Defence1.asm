8006ad34:
	j 0x74320

80074320:
	sb    $v0, 0x4278($v1)
	sw    $a2, 0x4274($v1)
	sw    $a1, 0x4270($v1)
	j 0x6ad3c
	sw    $a0, 0x426C($v1)

8006b55c:
	j 0x74290
	mflo  $t1

80074290:
	lui   $a0, 0x8007
	lbu   $t0, 0x4278($a0)
	sw    $v0, 0x5c(sp)
	sw    $t1, 0x60(sp)
	beq   $t0, $r0, 5
	sb    $r0, 0x4278($a0)
	
	lw    $a2, 0x4274($a0)
	lw    $a1, 0x4270($a0)
	jal 0x6a968
	lw    $a0, 0x426C($a0)

	lw    $v0, 0x5c(sp)
	lw    $t1, 0x60(sp)
	j 0x6b564
	sll   $a0, $s2, 0x1
