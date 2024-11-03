code_start 8006ae18

 BEQ v0, zr, check_buff
 LUI v0, 0x8007
 ADDIU v1, v0, 0x3CC0
 ADDU a0, s6, v1
 LBU v0, 0x0340(a0)
 NOOP
 BEQ v0, zr, check_buff
 SLL v0, s6, 0x01
 J logic
 SB zr, 0x0340(a0)

check_buff:
 ANDI v0, s3, 0x0200
 BEQ v0, zr, finish
 LUI v0, 0x8007
 ADDIU v1, v0, 0x3CC0
 ADDU a0, s6, v1
 LBU v0, 0x0346(a0)
 NOOP
 BEQ v0, zr, finish
 SLL v0, s6, 0x01
 SB zr, 0x0346(a0)

logic:
 ADDU v0, v0, v1
 LHU v1, 0x037A(v0)
 NOOP
 SH v1, 0x001C(s4)
 SH v1, 0x0356(v0)

 LHU v1, 0x0386(v0)
 NOOP
 SH v1, 0x001E(s4)
 SH v1, 0x0362(v0)

 LHU v1, 0x0392(v0)
 NOOP
 SH v1, 0x0020(s4)
 SH v1, 0x36E(v0)
 NOOP
 NOOP
 NOOP
 NOOP
 NOOP
 NOOP
finish:
