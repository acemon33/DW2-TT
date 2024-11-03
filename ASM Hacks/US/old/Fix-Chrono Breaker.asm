8006c21c:
  j 0x0200134

80200134:
  lui   $v1, 0x8020
  addiu $v1, $v1, 0x0130
  sb    $s6, 0x0($v1)
  j 0x6c220
  sll   $v0, $s6, 0x10

80068890:
  j 0x200148
  lui   $v0, 0x8020

80200148:
  addiu $a0, $v0, 0x0130
  lbu   $v0, 0x0($a0)
  addiu $v1, $r0, 0xa5
  bne   $v1, $v0, 3
  nop
  j 0x68bfc
  sb    $r0, 0x0($a0)
  jal   0x6e674
  addiu $a0, $r0, 0x0
  j 0x68898
  nop

-------------------- old ---------------
8006c21c:
  j 0x0200134

~80200130
80200134:
  addu $t7, $r0, $a0
  jal 0x6e674
  addiu $a0, $r0, 1
  addu $a0, $r0, $t7
  lui   $v1, 0x8020
  addiu $v1, $v1, 0x0130
  sb    $s6, 0x0($v1)
  sb    $v0, 0x1($v1)
  j 0x6c220
  sll   $v0, $s6, 0x10

800687bc:
  j 0x20015c
  lui   $v0, 0x8020

8020015c:
  addiu $v1, $v0, 0x0130
  lbu   $v0, 0x0($v1)
  addiu $t7, $r0, 0xa5
  bne   $t7, $v0, 11
  lui   $v0, 0x8007
  addu $t7, $r0, $a0
  jal 0x6e674
  addiu $a0, $r0, 0
  lbu   $t6, 0x1($v1)
  addu $a0, $r0, $t7
  bne   $t6, $v0, 4
  lui   $v0, 0x8007
  sb    $r0, 0x0($v1)
  j 0x68740
  sb    $r0, 0x1($v1)
  
  j 0x687c4
  addiu $s0, $v0, 0x3cc0

-------------------- very old ---------------
8006c20c:
	sw    $r0, 0x24($t1)

8006e5f0:
	j     0x74180
	lw    $v1, 0x24($a1)

80074180:
	addiu $v0, $r0, 0x1
	bne   $v1, $r0, 0x3
	lw    $v1, 0x28($a1)
	sb    $v0, 0x18($s6)
	sw    $v1, 0x24($a1)
	jr    $ra
	nop

