* Skip Animation:
80064f98:
	j 0x49b28

; 80049b28 + 3C
; 80049B64 + 150
; 80049CB4
; Key Bindings
80049b28:
LUI   $s0, 0x8007
LBU   $s1, 0x3cc8($s0)
ADDIU $s2, zr, 0x6
BNE   $s1, $s2, 9
LUI   $s2, 0x8006
LW    $s1, 0xF708($s2)
LW    $s3, 0xf70c($s2)
BEQ   $s1, zr, 2
  ADDIU $s1, zr, 0x1
  SB    $s1, 0x40B0($s0)    ;800740B0 flag activation 0x40B0
BEQ   $s3, zr, 2
  NOOP
  SB  zr ,0x40B0($s0)
J 0x64fa0
LW    $ra, 0x60($sp)

8006f254:
	j 0x49B64
	lui   $v1,0x8007

80049B64:
LBU   $v1, 0x40B0($v1)
NOOP
BEQ   $v1, zr, 3
LW    $v1, 0x18($s2)
J 0x6f088
NOOP
J 0x6f25c
NOOP

8006d2c4:
	j 0x49b84
	lui   $s0,0x8007

; Logic
80049b84:
LUI   $s0, 0x8007
LBU   $s2, 0x40B0($s0)
LBU   $s4, 0x38a0($s0)

; Check for skip activation
BEQ   $s2, zr, finish
  ADDIU $s6, zr, 0x18  ; End Phase
  ADDIU $t0, zr, 0x1   ; Force start Animation
  ADDIU $t1, zr, 0xb   ; Force Impact Animation
  ADDIU $t8, zr, 0x10   ; Impact Duration

interrupt:
  BNE   $s4, zr, frames_to_wait
    LBU  $t4, 0x3896($s0)
    NOOP
    SLTI  $t7, $t4, 3
    ; Player Interrupt
    BNE  $t7, zr, 2
    ADDIU $t7, zr, 0x13
    SB   $t7, 0x38a0($s0)

    ADDIU $t6, $s0, 6
    LBU   $s3, 0x38B0($s0)    ; no. target
    J check_number_of_targets
    SB    $s6, 0x38a2($s0)

frames_to_wait:
  LBU   $s7, 0x40B1($s0)    ; 800040B1 frame counter 0x40B1
  NOOP
  SLTI  $s3, $s7, 0x9    ; no. frames
  BNE  $s3, zr, counter
    ADDIU $s2, zr ,0x16
    ADDI  $s7, zr, -1

; i.cannon
  LBU   $s5, 0x3898($s0)
  ADDIU $s2, zr ,0x9
  BNE   $s5, $s2, attack_and_assist
    ADDIU $s2, zr ,0x16
    ADDI $t6, $s0, -8
    LBU   $s3, 0x38ac($s0)    ; no. target
    J check_number_of_targets
    SB    $t8, 0x38b0($s0)

attack_and_assist:
  BNE   $s4, $s2, missed
    ADDIU $s2, zr, 0xb4
    LBU    $s5, 0x0($s1)
    ADDIU $t3, zr, 0xa0
    BNE   $s5, $t3, 2
      ADDIU $s3, zr, 0xb6
      SB    $s3, 0x0($s1)

    ADDIU $t6, $s0, 0
    LBU   $s3, 0x38AA($s0)    ; no. target

check_number_of_targets:
  ADDIU $t5, zr, 0x0
  ADDIU $t4, $t6, 0x38c2

loop:
  ;SB    $t0, 0x0($t4)    ; forct start impact
  SB    $t0, 0x2($t4)    ; s.i.duration
  SB    $t1, 0xC($t4)    ; force impact anim
  
  BEQ   $s3, $t0, 2
    ADDIU $t5, $t5, 0x1
    SB    $t8, 0x14($t4)

  SLT   $t9, $t5, $s3
  BNE   $t9, zr, loop
  ADDIU $t4, $t4, 0x1E

; nth
  BEQ   $t0, $s3, 1st
    ADDIU $t4, zr, 0x1e
    MULT  $t4, $s3
    MFLO  $t4
    ADDIU $t4, $t4, 0x38BA
    ADDU $t4, $t4, $t6
    SB    $s6, 0x0($t4)    ; end turn n

1st:
  BNE   $t0, $s3, missed
    ADDIU $s3, zr, 3
    BNE   $s5, $s3, 2
    LH    $t1, 0x38C8($s0)
    ADDIU $t6, $t6, -4
	NOOP
	BNE   $t1, zr, 2
	NOOP
    ADDIU $t6, $t6, -4
    SB    $t8, 0x38da($t6)    ; end turn 1

missed:
  BNE   $s4, $s2, 0x2
    ADDIU $s2, zr, 0x78
    SB    $t8, 0x38a0($s0)  ;SB    s6,0x389e(s0)

; Guard
  BNE   $s4, $s2, 0x2
    NOOP
    SB    $t8, 0x38a0($s0) ;SB    $s6,0x389e(s0)

counter:
  ADDIU $s7, $s7, 0x1
  SB    $s7, 0x40B1($s0)

; For Impact After Interrupt Prompt
ADDIU $t7, zr, 0x13
BNE   $s4, $t7, finish
  LBU   $s5, 0x0($s1)
  ADDIU $s3, zr, 0xa2
  BNE   $s5, $s3, finish
    ADDIU $s3, zr, 0xbc
    SB    $s3, 0x0($s1)

finish:
LW    $ra, 0x9c($sp)
J 0x6d2c8
LW    $s6, 0x98($sp)
