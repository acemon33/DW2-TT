using System;
using MemcardRex;


namespace dw2_exp_multiplier.Entity
{
    public class MemoryCardFile : ISaveFile
    {
        private ps1card Ps1Card;
        private ushort SlotNumber;
        
        public override void Load(byte[] data)
        {
            this.Ps1Card = new ps1card();
            this.Ps1Card.openMemoryCardStream(data, true);
            byte[] buffer = new byte[16384];
            for (ushort i = 0; i < 15; i++)
            {
                if (Ps1Card.saveProdCode[i].Equals("SLUS-01193"))
                {
                    SlotNumber = i;
                    break;
                }
            }
            Buffer.BlockCopy(this.Ps1Card.getSaveBytes(SlotNumber), 128, buffer, 0, 16384);
            this.SaveFile = new SaveFile(buffer);
        }

        public override void Save(string filename)
        {
            int reqSlot = 0;
            byte[] buffer = this.Ps1Card.getSaveBytes(SlotNumber);
            Buffer.BlockCopy(this.SaveFile.ToArray(), 0, buffer, 128, 16384);
            this.Ps1Card.formatSave(SlotNumber);
            this.Ps1Card.setSaveBytes(SlotNumber, buffer, out reqSlot);
            this.Ps1Card.saveMemoryCard(filename, this.Ps1Card.cardType, true);
        }
    }
    
}