8006b020:    # x 2.1
	addiu  $a0, $r0, 0x10      # $v1 = hp
	divu   $v0, $a0            # $v0 = damage
	sll    $v0, $v0, 0x1
	mflo   $a0
	nop

===============================

8006b020:    # x 1.8
	li $a0, 18
    mult $v0, $a0
	li  $s2, 10
	mflo $a0
    div $a0, $s2
    mflo $s2
