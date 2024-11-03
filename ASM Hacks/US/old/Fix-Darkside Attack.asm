8006b79c:
	lui       $v0,0x8007
	addiu      $a1 ,$v0,0x3cc0
	j 0x200000
	addu       $v0,$s6,$a1

80200000:
	lbu        $v1,0x34f($v0)
	nop
	andi      $v0,$v1,0x2
	bne        $v0, $r0,4

	lh   $v1, 0x14($s4)
	lhu  $v0, 0x14($s4)
	j 0x6b094
	subu $s2, $v1, $a0

	lhu  $v0, 0x16($s4)
	lh   $v1, 0x16($s4)
	j 0x6b094
	subu $s2, $v1, $a0