800175cc:
	j 0x49Af0
	nop
-
80049Af0:
-v1
	blez v1,0x24
	nop
	lh v0,0x19e(s0)
	nop
	slti v1,v0,1
	bne v1,zero,0x12
	nop
	sh v0,0x19c(s0)
	j 0x175fc
	nop
	lui v1,0x8006
-v1
	j 0x175d4
