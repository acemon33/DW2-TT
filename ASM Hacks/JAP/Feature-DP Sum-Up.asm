STAG2000.PRO
80066d24:
	lui    $v0, 0x8005
	lw     $t1, 0x1bf0($v0)        # get location previous_game_state_id 8005f790
	addiu  $v0, $r0, 0x315
	lbu    $a0, 0xe($s6)    # 1st Digimon DP
	j 0x70bb0
	lbu    $v1, 0xe($s5)    # 2nd Digimon DP
	nop

80070bb0:
	beq    $t1, $v0, 7    # Check if Device Dome
	nop
	sltu   $v0, $v1, $a0
	bne    $v0, $r0, 2
	addiu  $v0, $a0, 1       # Add dp
	addiu  $v0, $v1, 1
	j  0x66d40
	nop
	bne    $v1, $r0, 3
	nop
	j  0x66d40
	addiu  $v0, $a0, 1     # Add dp in case of 0
	bne    $a0, $r0, 2
	nop
	addiu  $a0, $r0, 1      # Add dp in case of 0
	j  0x66d40
	addu   $v0, $a0, $v1      # Sum dp