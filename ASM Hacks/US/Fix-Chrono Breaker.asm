Data:
	flag 0x8007435c 8007 435c

8006c21c:
  j 0x74220

80074220:
  lui   $v1, 0x8007
  addiu $v1, $v1, 0x435c
  sb    $s6, 0x0($v1)
  j 0x6c220
  sll   $v0, $s6, 0x10

80068890:
  j 0x74334
  lui   $v0, 0x8007

80074334:
  addiu $a0, $v0, 0x435c
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
