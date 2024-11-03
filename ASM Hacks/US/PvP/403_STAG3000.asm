	* Loading Digimons (Digilines Set):
4E40: 06
		800681CC: nop

		800681EC: nop

		80068210: nop


	* Music:
		80068008:
4CA8: 02 01

	* Disable AI:
		80068664: nop

	* 3D Field:
		8007023c: nop

	* Digimon Names Y Offsets:
		8007347a: 28
		8007347e: 28
		80073482: 28

	* Read HUD info:
		80071358: 06
		80071404: 06

	* CPU Menu Selection:
		80064a64: addiu $a0, $r0, 0x1

	* CPU Menu Selection 2nd Pad Controller:
		80071538:
			jr $ra
			nop

		80064c74:
			j 0x71540

		80071540:
			lui    $v0,0x8007
			addiu  $v0,$v0,0x3cc8
			lw     $v0,0x0($v0)
			nop
			ori    $v1,$v0,0x6
			beq    $v1,$v0,5
			nop
			slti   $v1,$v0,0x3
			bne    $v1,$r0, 2
			nop
			addiu  $a0,$a0,0x40
			lw     $v0,0x08($a0)
			j      0x64c7c
			nop

	* CPU Tech Selection 2nd Pad Controller:
		8006688c:
			lw $v0, 0x0($a2)

		80066840:
			j 0x71578
			lh $a1,0x3800($t1)
			lui $v0,0x8007
			addiu $v0,0x3808
			addu $a2,$r0,$a0

		80071578:
			addiu  $v0,$t1,0x3cc8
			lw     $v0,0x0($v0)
			nop
			slti   $v1,$v0,0x3
			beq    $v1,$r0, 2
			addiu  $a0,$s4,0xf730
			addiu  $a0,$s4,0xf6f0
			j      0x66848
			nop

	* CPU Target Selection 2nd Pad Controller:
		80066F5C:
			lui $v1, 0x8006

		80066f48:
			lui $v1, 0x8006
			j 0x7159c
			lui    $v0,0x8007

		8007159c:
			addiu  $v0, $v0, 0x3cc8
			lw     $v0, 0x0($v0)
			nop
			slti   $v1, $v0, 0x3
			bne    $v1, $r0, 4
			addiu  $v1, $s4, 0xf730 # 2nd team
			lw     $v0, 0x0($v1)
			j 0x66f58
			nop
			addiu  $v1, $s4, 0xf6f0 # 1st team
			lw     $v0, 0x4($v1)
			j 0x66f58
			nop

		80066fb8:
			j 0x715d0

		800715d0:
			lui    $v0,0x8007
			addiu  $v0,$v0,0x3cc8
			lw     $v0, 0x0($v0)
			nop
			slti   $v1, $v0, 0x3
			bne    $v1, $r0, 4
			addiu  $v1, $s4,0xf730
			lw     $v0, 0x4($v1)
			j 0x66fbc
			nop
			addiu  $v1 $s4, 0xf6f0
			lw     $v0, 0x0($v1)
			j 0x66fbc
			nop

		8006701c: x, /\
			addiu $v1,$r0,0x18
			lui    $v0,0x8007
			j 0x71608
			addiu  $v0,$v0,0x3cc8

		80071608:				=> make target selection hud
			lw     $v0,0x0($v0)
			nop
			slti   $v1,$v0,0x3
			beq    $v1,$r0, 2
			addiu  $v1,$s4,0xf730
			addiu  $v1,$s4,0xf6f0
			lw     $v0,0x14($v1)
			j      0x6702c
			nop

		80066eb0:
			sw $a0,0x1c($s1)

		80066e90:
			j 0x7162c
			lui    $a0,0x8007

		8007162c:
			addiu  $a0,$a0,0x3cc8
			lw     $a0,0x0(a0)
			nop
			slti   $a0,$a0,0x3
			beq    $a0,$r0, 2
			addiu  $a0,$r0,0
			addiu  $a0,$r0,1
			j      0x66e98
			addiu  $a1,$a0,0

		80066e84:
			sw $a0, 0x1c(s1)

		80066e68:
			j 0x71650
			lui    $a0,0x8007

		80071650:
			addiu  $a0,$a0,0x3cc8
			lw     $a0,0x0(a0)
			nop
			slti   $a0,$a0,0x3
			beq    $a0,$r0, 2
			addiu  $a0,$r0,1
			addiu  $a0,$r0,0
			j      0x66e70
			addiu  $a1,$a0,0

		80066ec4:
			j 0x71674
			lui    $v0,0x8007

		80071674:
			lw     $v0,0x3cc8(v0)
			nop
			slti   $v0,$v0,0x3
			beq    $v0,$r0, 2
			addiu  $v0,$r0,7
			addiu  $v0,$r0,8
			j 0x66ed0
			nop

		80066e88:
			j 0x71694
			lui    $v0,0x8007

		80071694:
			lw     $v0,0x3cc8(v0)
			nop
			slti   $v0,$v0,0x3
			beq    $v0,$r0, 2
			addiu  $v0,$r0,8
			addiu  $v0,$r0,7
			j 0x66ed0
			nop

		80067328:
			j 0x716b4
			lui v0,0x8007

		800716b4:
			lw     $v0,0x3cc8(v0)
			lui    $v1,0x8007
			slti   $v0,$v0,0x3
			beq    $v0,$r0, 3
			lui    v0,0x8007
			j 0x67330
			nop
			j 0x6737c
			nop
		80067374:
			j 0x716d8
			lui v0,0x8007

		800716d8:
			lw     $v0,0x3cc8(v0)
			lui    $v1,0x8007
			slti   $v0,$v0,0x3
			beq    $v0,$r0, 3
			lui    v0,0x8007
			j 0x6737c
			nop
			j 0x67330
			nop
		80067278:
			j 0x716fc
			lui    $a0,0x8007

		800716fc:
			lw     $a0,0x3cc8(a0)
			nop
			slti   $a0,$a0,0x3
			beq    $a0,$r0, 2
			addiu  $a0,$r0,9
			addiu  $a0,$r0,8
			j 0x6728c
			nop

		80067280:
			j 0x7171c
			lui    $a0,0x8007

		8007171c:
			lw     $a0,0x3cc8(a0)
			nop
			slti   $a0,$a0,0x3
			beq    $a0,$r0, 2
			addiu  $a0,$r0,8
			addiu  $a0,$r0,9
			j 0x6728c
			nop

		80064c74:
			j 0x7173c
			lui   $v0, 0x8007

		8007173c:
			addiu $v0, $v0, 0x3cc8
			lw    $v0, 0x0($v0)
			nop
			ori   $v1, $v0, 0x6
			beq   $v1, $v0, 5
			nop
			slti  $v1, $v0, 0x3
			bne   $v1, $r0, 2
			nop
			addiu $a0, 0x40
			lw    $v0, 0x8($a0)
			j 0x64c7c
			nop

		80066840:
			j 0x71770

		80071770:
			addiu $v0, $t1, 0x3cc8
			lw    $v0, 0x0($v0)
			nop
			slti  $v1, $v0, 0x3
			beq   $v1, $r0, 2
			addiu  $a0,$s4,0xf730
			addiu  $a0,$s4,0xf6f0
			j 0x66848
			nop

	* Allow Interrupt:
			8006d02c:
				j 0x6cfdc
				nop

	* Interrupt Prompt:
			8006fdd4:
				lui   $v0, 0x8007
				j 0x71794
				lh    $v0, 0x38a4($v0)

			80071794:
				addiu $v1, $r0, 0x13
				beq   $v0, $v1, 2
				  lui   $v0, 0x8006
				  addiu $v0, $v0, 0x40
				lw    $v1, 0xF6F0($v0)
				j 0x6fde0
				nop

	* Interrupt target selection pad:
			8006febc:
				lui   $v0, 0x8007
				j 0x717b0
				lh    $v0, 0x38a4($v0)

			800717b0:
				addiu $v1, $r0, 0x13
				beq   $v0, $v1, 2
				  lui   $v0, 0x8006
				  addiu $v0, $v0, 0x40
				lw    $v1, 0xF6F0($v0)
				j 0x6fec8
				nop

	* Interrupt target selection logic:
		8006fccc:
			j 0x7180c
			addiu  $a0, $v0 0x3cc0

		8007180c: # 1st team
			lw     $v0, 0x2cc($a0)
			addiu  $v1, $r0, 2
			bne    $v0, $v1, 2  # 3rd digimon
			lw     $v0, 0x2bc($a0)
			sw     $v1, 0x10($s0)

			addiu  $v1, $r0, 1
			bne    $v0, $v1, 2  # 2nd digimon
			lw     $v0, 0x2ac($a0)
			sw     $v1, 0x10($s0)

			addiu  $v1, $r0, 0
			bne    $v0, $v1, 2  # 1st digimon
			lui    $v0, 0x8007
			sw     $v1, 0x10($s0)

			lb     $v1, 0x3896($v0)
			nop
			slti   $v1, $v1, 3
			bne    $v1, $r0, 3
			nop
			j 0x6ffb4
			or     $a0, $s1, $r0
			j 0x6fcd4
			lw     $v0, 0x2fc($a0)

		8006fcd4: # 2nd team
			addiu  $v1, $r0, 5
			bne    $v0, $v1, 2  # 6th digimon
			lw     $v0, 0x2ec($a0)
			sw     $v1, 0x10($s0)

			addiu  $v1, $r0, 4
			bne    $v0, $v1, 2  # 5th digimon
			lw     $v0, 0x2dc($a0)
			sw     $v1, 0x10($s0)

			addiu  $v1, $r0, 3
			bne    $v0, $v1, 2  # 4th digimon
			nop
			sw     $v1, 0x10($s0)
			j 0x6ffb4
			or     $a0, $s1, $r0

		8006fefc:
			j 0x717cc
			lui   $v0, 0x8007
			beq   $v0, $v1, 0x1f

		800717cc:
			lh    $v0, 0x38a4($v0)
			addiu $v1, $r0, 0x13
			beq   $v0, $v1, 2
			  addiu $v1, $r0, 0
			  addiu $v1, $r0, 3
			lw    $v0, 0x10($s0)
			j 0x6ff04
			nop
		8006fed0:
			j 0x717ec
			lui   $v0, 0x8007

		800717ec:
			lh    $v0, 0x38a4($v0)
			addiu $v1, $r0, 0x13
			beq   $v0, $v1, 2
			  addiu $v0, $r0, 2
			  addiu $v0, $r0, 5
			lw    $v1, 0x10($s0)	
			j 0x6fed8
			nop

	* Return Previous State
		80067e90: addiu $v1, $r0, 0x701
		80067d6c: addiu $a0, $r0, 0x701

	* Winning Scene & Music
		80068c98: nop nop nop
		80067574: 06
		80067860: 06
		80067758:
			lb    $v0, 0x14($a0)
			nop
			slti  $v0, $v0, 0x4
			beq   $v0, $r0, 5
			addiu $s3, $r0, 0x19
			j 0x6777c
			addiu $s3, $r0, 0x17
		800677a0:
			addiu $a0, $s3, 0

	* Command Background
		command text position data
			800718ac (it was 8007348c)
				12 00 AB 00 75 00 AB 00 D8 00 AB 00 12 00 38 00 75 00 38 00 D8 00 38 00
			80070f50: ac 18
		command box
			80071338 nop

	* Interrupt Selecition Graphics (test)
		80070170:
			addiu $a2, $a1, 0x0
			addu  $a1, $v0
			j 0x718c4
			slti  $v0, $a2, 3

		800718c4:
			bne   $v0, $r0, 3
			nop
			addiu $a1, $a1, -3
			addiu $a0, $a0, 0x4C3C
			lw    $v0, 0x0($a0)
			j 0x70180
			nop

	* Skip Main Menu
		80064570:
			or $a0, $r0, 0
			or $a1, $r0, 0
			jal 0x6e31c
			or $a2, $r0, 0
			j 0x64a7c
			nop

	* Wireframe
		source-selection
			80064860: 05
		return back selection
			80064a2c: 05

		target-selection attack one
			80066e9c:
				ori   $v0, $r0, 0x0

		target-selection assist one
			80066e74:
				ori   $v0, $r0, 5

		target-selection Attack All
			80067184:
				j 0x71864

			80071864:
				lbu   $s0, 0x3cc8($v0)
				nop
				slti  $s0, $s0, 3
				bne   $s0, $r0, 3
				nop
				j 0x67130
				addiu $s0, $r0, 0x0
				j 0x6718c
				addiu $s0, $r0, 0x3

		target-selection Assist All
			80067128:
				j 0x71888

			80071888:
				lbu   $s0, 0x3cc8($v0)
				nop
				slti  $s0, $s0, 3
				beq   $s0, $r0, 3
				nop
				j 0x67130
				addiu $s0, $r0, 0x0
				j 0x6718c
				addiu $s0, $r0, 0x3

	* Wireframe Softlock with Necro-Magic Work-around
		8006f624:
			j 0x718e0
			lui   $a1, 0x8007
		800718e0:
			lw    $a1, 0x1900($a1)
			ori   $v0, $r0, 0xd2
			beq   $a1, $v0, 3
			addiu $a2, $sp, 0x10
			jal 0x20768
			ori   $a1, $r0, 0
			j 0x6f62c
			ori   $a1, $r0, 0
			nop          # for data 80071900
		80066e08:
			j 0x71904
		80071904:
			lui $v0, 0x8007
			jal 0x1ef3c
			sw  $a0, 0x1900($v0)
			j 0x66e10
			nop

	* Battle Enhance:
		Beast Fist King
		Debuff
		Enemy Boss
		SubzeroIcePunch
	* Battle Fixes:
		Zen-Re
		1.5 Damage vs guard
		Chrono Breaker
		Darkside Attack
		Gigabyte Wind
		Revive effect
		Shadow Scythe
		Venom Infusion
		Invinsibility