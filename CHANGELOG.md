**********************************************************************
                            CHANGELOG
**********************************************************************



v4.5:
-----
 - File-Manager: supports importing files larger than the original
 - Dialog-Fast-Forward extends Battle Menus
 - Fix Enemy Boss State Multiplier
 - Fix Turn Skipping when using Cannon shows random Text


v4.1:
-----
 - FPS Unlocker: run the game at 60 FPS (Experimental).
 - Remove validation when opening the game file.
 - Selection Options for US / Japanese versions.
 - Update Faithful & Resurrection configuration data files.
 - Add some patches for the Japanese version.
 - Fix minor issues.

**Note:**
- For FPS Unlock patcher:
   - Usage:
      - FPS activate:   press L1 + R1 buttons at any time.
      - FPS deactivate: press L2 + R2 buttons at any time.



v4.0-Final:
-----
 - Support Japanese version (Some Patches).
 - Support VCD image (PS2 Pops images).
 - Separate DNA Sum-up & DNA Labs Restriction patchers.
 - Max Level Limit patcher: remove the max level limit from Digimon.
 - Dialog Fast-Forward patcher: progressing Dialogs by holding X button.
 - Zudokorn Mons patcher: obtain Zodukorn's Digimons after the training mission and get 30,000 Bits.
 - Domain Free Obstacles patcher: prevent Bugs, Mines, Spores & Rocks to be spot on the Map.
 - Floor Skipping patcher: Allow the Player to jump to the specified floor in the domain.
 - R-Bug Removal patcher: prevent R-Bug to be spot on the Map.
 - PvP Reimagined patcher: PvP Battle System similar to Player 1 walkthrough.
 - Turn Skipping patcher: Skip animations & voice during battle combat.
 - Tech Ordering patcher: Allow the Player to arrange Digimon's Techs by accessing the Learn Tech Menu after the battle.
 - Next Level Limit patcher: Allow a Digimon to Level up more than once according to the experience during battle.
 - Battle Enhancement:
   - Enemy Boss Confusion: enemy Boss can be affected by Confusion Effect.
   - Enemy Boss Affection: enemy boss can be affected or taken damage by Venom Infusion, motivation down Techs, giga-scissor-claw.
   - Necro Magic Tech: get 200 MP of Faint Digimon.
 - Battle Fix:
   - Chrono Breaker Tech: correct the behavior when some (Attack / Assist / Counter-Attack) Techs following Chrono Breaker.
   - Darkside Attack Tech: target can't recover HP (all healing Techs).
   - Giga Byte Attack Tech: target can't Recover Status even the random chance to cure the status.
   - 1.5 damage vs guard Effect: correct the behavior 1.5 damage vs guarding.
   - Zen Recovery / Parameter Patch / Re-Initilize / Re-Format Tech: reset state changes permanently.
 - Fix various minor issues.

**Note:**
- For Floor Skipping patcher:
   - There is no boundary checking, exceeding will softlock the game (be very cautious)
   - Usage:
      - press x on the floor gate, when dialog appeares, don't close it, press as the following:
      - R1 to increase floor
      - L1 to decrease floor
- For Turn Skipping patcher:
   - Usage:
      - Press a button on Main Menu (Give Orders / Cannon / Run Away) as the following:
      - [] square: to enable Skip Turns (Skip Animation)
      - /\ triangle: to disable Skip Turns (Default Animation)
- For Tech Ordering patcher:
   - Press R1 on Exp / Bits Earning Menu



v3.0:
-----
 - Backup and Restore Options.
 - File-Manager
   - Export File by its index based on the Game Look-Up Table (multi files also supported).
   - Import File by its index based on the Game Look-Up Table (multi files also supported).
   - Exporting / Importing by complete Sectors (a data sector is 2024 bytes).
 - Name-Extension Patcher: expand the Hero's Name upto 8 characters and Digi-Beetle upto 10 characters.
 - Battle Enhancement:
   - Demi Dart and Evil Touch Techs: increasing lowering MP effect.
   - Poison Effect Damage: poison damage increased.
   - Debuff Defence Effect: the defense reduction is applied after it actually deals damage.
 - Battle Fix:
   - Shadow Scythe Tech: correct the behavior when being Interrupted
   - Invincibility Tech: correct the behavior when being Attacked by Beast King Fist Tech.
   - Fix Zen Recovery Tech Patch of previous version.

**Note:**
 - Perform a backup from the Menu Bar before attempting anything, so you can restore later if anything goes wrong.



v2.4:
-----
 - DNA-Labs Patcher: remove restriction on 3 Guard team labs which will allow dna of any type and Device Dome's lab will sum-up the DP(s) instead of adding 1.
 - Digimon Gift Patcher: make a wild Digimon stop moving when it accepts a gift.
 - Digi-Line Patcher: allow to 1 or 2 Digimons to be in digi-line (instead of always to be 3).
 - No Enemy Encounter Patcher: the wild Digimons will not move from their places.
 - Battle Enhancement:
   - MP-on-Guard: ratio of increasing MP during Guard is 25%.
   - Beast King Fist Tech: the damage is 2.1 on Counter-Attack.
   - Subzero Ice Punch: every hit increases 7 AP.
 - Battle Fix:
   - Venom Infusion Tech: guarantee to disable target's turn (no more crash or soft-lock).
   - Zen Recovery Tech: reset all state changes permanently.

**Note:**
 - For Digi-Line Patcher:
    - After selecting the 1st or 2nd Digimon, press [] square to finish the setting.



v2.3:
-----
 - Save Editor:
    - Support PSX Memory Card formats (mcr, gme, bin, mcd, vmc...) loading and saving (Thanks to MemcardRex Project).
	- Load data according to the selected Mod (select appropriate Mod before loading save file).



v2.0:
-----
 - Exp Multiplier support decimal number.
 - Validation on DW2 image file.
 - Unhide AAA Folder.
 - Digi-Beetle Patcher (set a Digi-Beetle for entire Game Walkthrough, can be restored to default).
 - Save Editor:
    - Open and Save Raw save file (BASLUS-01193 DMW2).
    - Edit last save point, rank, bits, name.
    - Edit Digi-Beetle data.
    - Edit Items, Important Items and Server Items.
    - Edit Digimon data.
    - Edit Game Flags.
    - Set Game story progress.
 - Enemy Boss Stats Multiplier (HP, MP, Attack, Defense, Speed) (only speed will be half x multiplier).
 - Extreme Mode Multiplier will double x multiply only for **Defence** stat.

**Note:**
 - For Digi-Beetle Patcher:
    - Setting the same NPC Digi-Beetle that will appear during the dialog will cause **Softlock**.
 - For Save Editor when working with Raw Save-File:
    - Save the filename to "BASLUS-01193 DMW2" or rename it before importing to Memory Card File (.mcd / .mcr),
      because MemcardRex cannot import properly with different name (in case of Raw Save File).



v1.0:
-----
 - Increases exp & bits of enemyset.
 - Support any DW2 version.
 - Export & Import ENEMYSET.BIN for backup & restore.
 - User Friendly interface can drag files to their respective fields.
