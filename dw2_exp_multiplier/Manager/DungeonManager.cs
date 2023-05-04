using System.Collections.Generic;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Manager
{
    public class DungeonManager
    {
        private const int NUMBER_OF_FLOORS = 8;
        private const int TRAP_PTR_OFFSET = 0xC;
        private const int CHEST_TRAP_LEVEL_OFFSET = 52;
        private const int TRAP_BASE_LEVEL_OFFSET = 0x2E;
        
        private MemoryStream Data4000;

        public DungeonManager(ref FileStream dw2FileStream)
        {
            this.Data4000 = new MemoryStream(PsxSector.ReadSector(ref dw2FileStream, DW2Slus.GetLba(FileIndex.DATA4000_BIN), DW2Slus.GetSize(FileIndex.DATA4000_BIN)));
        }

        public void ApplyModifiers(ref FileStream dw2FileStream)
        {
            List<int> dungeonsList = this.GetDungeonList();

            foreach (var index in dungeonsList)
            {
                MemoryStream dungeonStream = new MemoryStream(PsxSector.ReadSector(ref dw2FileStream, DW2Slus.GetLba(index), DW2Slus.GetSize(index)));
                BinaryReader dungeonReader = new BinaryReader(dungeonStream);
                BinaryWriter dungeonWriter = new BinaryWriter(dungeonStream);

                List<int> floorList = this.GetFloorList(ref dungeonReader);

                foreach (var floor in floorList)
                {
                    for (int i = 0; i < NUMBER_OF_FLOORS; i++)
                    {
                        dungeonReader.BaseStream.Position = floor + NUMBER_OF_FLOORS + (i * 4);
                        int layout = dungeonReader.ReadInt32();
                        if (layout == 0) continue;
                        dungeonReader.BaseStream.Position = layout + TRAP_PTR_OFFSET;
                        dungeonWriter.BaseStream.Position = dungeonReader.ReadInt32();
                        dungeonWriter.Write(0x000000FF);

                        for (int j = 0; j < NUMBER_OF_FLOORS; j++)
                        {
                            dungeonWriter.BaseStream.Position = floor + CHEST_TRAP_LEVEL_OFFSET + (j * 4) + 1;
                            dungeonWriter.Write(false);
                        }

                        dungeonWriter.BaseStream.Position = floor + TRAP_BASE_LEVEL_OFFSET;
                        dungeonWriter.Write( (ushort) 0);
                    }
                }

                byte[] buffer = dungeonStream.ToArray();
                PsxSector.WriteSector(ref dw2FileStream, ref buffer, DW2Slus.GetLba(index), DW2Slus.GetSize(index));
            }
        }

        private List<int> GetDungeonList()
        {
            BinaryReader data4000Reader = new BinaryReader(Data4000);
            data4000Reader.BaseStream.Position = 0x28;
            uint start = data4000Reader.ReadUInt32();
            data4000Reader.BaseStream.Position = start;
            List<int> dungeonsList = new List<int>();
            while (true)
            {
                int dungeonIndex = data4000Reader.ReadUInt16();
                if (dungeonIndex == 0)
                    break;
                dungeonsList.Add(dungeonIndex);
                data4000Reader.BaseStream.Position += 18;
            }
            return dungeonsList;
        }

        private List<int> GetFloorList(ref BinaryReader dungeonReader)
        {
            List<int> floorList = new List<int>();
            while (true)
            {
                int floor = dungeonReader.ReadInt32();
                if (floor == 0) break;
                floorList.Add(floor);
            }
            return floorList;
        }
    }
    
}
