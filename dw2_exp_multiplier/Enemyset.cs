using System;

/*
 * @author acemon33
 */

namespace dw2_exp_multiplier
{
    public class Enemyset
    {
        public static readonly int LENGTH = 100;

        public byte Id;
        public byte Move;
        public ushort Model;
        public byte GiftType;
        public byte EncType;
        public ushort GiftRate;
        public Enemy[] Enemy = new Enemy[3];
        public ushort Padding;

        public Enemyset(byte[] data)
        {
            this.Id = data[0];
            this.Move = data[1];
            this.Model = BitConverter.ToUInt16(data, 2);
            this.GiftType = data[4];
            this.EncType = data[5];
            this.GiftRate = BitConverter.ToUInt16(data, 6);

            byte[] temp = new byte[dw2_exp_multiplier.Enemy.LENGTH];
            for (int i = 0; i < this.Enemy.Length; i++)
            {
                Buffer.BlockCopy(data, i * temp.Length + 8, temp, 0, temp.Length);
                this.Enemy[i] = new Enemy(temp);
            }
        }

        public Byte[] ToArray()
        {
            byte[] data = new byte[Enemyset.LENGTH];
            data[0] = this.Id;
            data[1] = this.Move;
            Buffer.BlockCopy(BitConverter.GetBytes(this.Model), 0, data, 2, 2);
            data[4] = this.GiftType;
            data[5] = this.EncType;
            Buffer.BlockCopy(BitConverter.GetBytes(this.GiftRate), 0, data, 6, 2);

            for (int i = 0; i < this.Enemy.Length; i++)
            {
                Buffer.BlockCopy(this.Enemy[i].ToArray(), 0, data, i * dw2_exp_multiplier.Enemy.LENGTH + 8, dw2_exp_multiplier.Enemy.LENGTH);
            }
            return data;
        }
    }
    
    public class Enemy
    {
        public const int LENGTH = 30;
        
        public byte Id;
        public byte Padding0;
        public ushort Hp;
        public ushort Mp;
        public ushort Exp;
        public ushort Bits;
        public byte Level;
        public byte Attack;
        public byte Defence;
        public byte Padding1;
        public byte Speed;
        public byte[] SkillList = new byte[3];
        public EnemySkill[] EnemySkillList = new EnemySkill[4];
        
        public Enemy(byte[] data)
        {
            this.Id = data[0];
            this.Hp = BitConverter.ToUInt16(data, 2);
            this.Mp = BitConverter.ToUInt16(data, 4);
            this.Exp = BitConverter.ToUInt16(data, 6);
            this.Bits = BitConverter.ToUInt16(data, 8);
            this.Level = data[10];
            this.Attack = data[11];
            this.Defence = data[12];
            this.Speed = data[14];

            for (int i = 0; i < this.SkillList.Length; i++) this.SkillList[i] = data[i + 15];

            byte[] temp = new byte[EnemySkill.LENGTH];
            for (int i = 0; i < this.EnemySkillList.Length; i++)
            {
                Buffer.BlockCopy(data, i * temp.Length + 18, temp, 0, temp.Length);
                this.EnemySkillList[i] = new EnemySkill(temp);
            }
        }

        public byte[] ToArray()
        {
            byte[] data = new byte[Enemy.LENGTH];
            data[0] = this.Id;
            Buffer.BlockCopy(BitConverter.GetBytes(this.Hp), 0, data, 2, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(this.Mp), 0, data, 4, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(this.Exp), 0, data, 6, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(this.Bits), 0, data, 8, 2);
            data[10] = this.Level;
            data[11] = this.Attack;
            data[12] = this.Defence;
            data[14] = this.Speed;
            for (int i = 0; i < this.SkillList.Length; i++) data[i + 15] = this.SkillList[i];
            for (int i = 0; i < this.EnemySkillList.Length; i++) 
                Buffer.BlockCopy(this.EnemySkillList[i].ToArray(), 0, data, i * EnemySkill.LENGTH + 18, EnemySkill.LENGTH);
            
            return data;
        }
    }

    public class EnemySkill
    {
        public const int LENGTH = 3;
        
        public byte Condition;
        public byte Skill;
        public byte Target;

        public EnemySkill(byte[] data)
        {
            this.Condition = data[0];
            this.Skill = data[1];
            this.Target = data[2];
        }

        public byte[] ToArray()
        {
            byte[] data = new byte[EnemySkill.LENGTH];
            data[0] = this.Condition;
            data[1] = this.Skill;
            data[2] = this.Target;
            return data;
        }
    }
    
}
