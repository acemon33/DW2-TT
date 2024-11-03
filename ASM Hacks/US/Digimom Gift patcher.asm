** Freeze enemy when giving a gift
8006a3c0:
	addiu $a2, $r0, 0x3

8006a3cc:
	sb    $a2, 0x5($s0)

========== old version ====================
8006a3c0: sb    $t9, 0x5($s0)
