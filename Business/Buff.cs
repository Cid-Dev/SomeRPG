using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Buff : Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RemainingDuration { get; set; }
        public int Duration { get; set; }
        public bool IsGood { get; set; }
        public int HPModifier { get; set; }
        public int StrenghModifier { get; set; }
        public int VitalityModifier { get; set; }
        public int AgilityModifier { get; set; }
        public int DexterityModifier { get; set; }
        public int PrecisionModifier { get; set; }

        public Buff()
        {
            Duration = 0;
            HPModifier = 0;
            StrenghModifier = 0;
            VitalityModifier = 0;
            AgilityModifier = 0;
            DexterityModifier = 0;
            PrecisionModifier = 0;
        }

        public Buff(int id)
        {
            try
            {
                DataAccess.Buff DalBuff = new DataAccess.Buff();
                var temp = DalBuff.GetBuffById(id);
                if (temp != null)
                {
                    foreach (var dalBuff in temp)
                    {
                        Id = int.Parse(dalBuff?.GetType().GetProperty("Id")?.GetValue(dalBuff, null).ToString());
                        Name = dalBuff?.GetType().GetProperty("Name")?.GetValue(dalBuff, null).ToString();
                        Duration = int.Parse(dalBuff?.GetType().GetProperty("Duration")?.GetValue(dalBuff, null).ToString());
                        IsGood = bool.Parse(dalBuff?.GetType().GetProperty("IsGood")?.GetValue(dalBuff, null).ToString());
                        HPModifier = int.Parse(dalBuff?.GetType().GetProperty("HPModifier")?.GetValue(dalBuff, null).ToString());
                        StrenghModifier = int.Parse(dalBuff?.GetType().GetProperty("StrenghModifier")?.GetValue(dalBuff, null).ToString());
                        DexterityModifier = int.Parse(dalBuff?.GetType().GetProperty("DexterityModifier")?.GetValue(dalBuff, null).ToString());
                        VitalityModifier = int.Parse(dalBuff?.GetType().GetProperty("VitalityModifier")?.GetValue(dalBuff, null).ToString());
                        AgilityModifier = int.Parse(dalBuff?.GetType().GetProperty("AgilityModifier")?.GetValue(dalBuff, null).ToString());
                        PrecisionModifier = int.Parse(dalBuff?.GetType().GetProperty("PrecisionModifier")?.GetValue(dalBuff, null).ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        private int IsInList(List<Buff> buffs)
        {
            for (int i = 0; i < buffs.Count; ++i)
                if (Name == buffs[i].Name)
                    return (i);
            return (-1);
        }

        private void ApplyToBuff(Character target)
        {
            int buffIndex = IsInList(target.Buffs);
            if (buffIndex >= 0)
                target.Buffs.RemoveAt(buffIndex);
            target.Buffs.Add(this);
        }

        private void ApplyToDeBuff(Character target)
        {
            int buffIndex = IsInList(target.DeBuffs);
            if (buffIndex >= 0)
                target.DeBuffs.RemoveAt(buffIndex);
            target.DeBuffs.Add(this);
        }

        public void Apply(Character target)
        {
            RemainingDuration = Duration;
            target.HPBonus += HPModifier;
            if (HPModifier >= 0)
                target.CurrentHP += HPModifier;
            else if (target.HP < target.CurrentHP)
                target.CurrentHP = target.HP;
            target.BonusStrengh += StrenghModifier;
            target.BonusVitality += VitalityModifier;
            target.BonusAgility += AgilityModifier;
            target.BonusDexterity += DexterityModifier;
            target.BonusPrecision += PrecisionModifier;

            if (IsGood)
            {
                ApplyToBuff(target);
            }
            else
            {
                ApplyToDeBuff(target);
            }
        }

        public void RemoveEffect(Character target)
        {
            target.HPBonus -= HPModifier;
            target.CurrentHP -= HPModifier;
            if (target.HP < target.CurrentHP)
                target.CurrentHP = target.HP;
            target.BonusStrengh -= StrenghModifier;
            target.BonusVitality -= VitalityModifier;
            target.BonusAgility -= AgilityModifier;
            target.BonusDexterity -= DexterityModifier;
            target.BonusPrecision -= PrecisionModifier;

            if (IsGood)
                target.Buffs.Remove(this);
            else
                target.DeBuffs.Remove(this);
        }

        public string Description(int numberOfTab = 1)
        {
            string tabs = "";
            for (int i = 0; i < numberOfTab; ++i)
                tabs += "\t";
            string result = tabs + Name + " (Duration : " + RemainingDuration + ") :\n";
            if (HPModifier != 0)
                result += tabs + "\t" + "HP : " + HPModifier + "\n";
            if (StrenghModifier != 0)
                result += tabs + "\t" + "Strengh : " + StrenghModifier + "\n";
            if (VitalityModifier != 0)
                result += tabs + "\t" + "Vitality : " + VitalityModifier + "\n";
            if (AgilityModifier != 0)
                result += tabs + "\t" + "Agility : " + AgilityModifier + "\n";
            if (DexterityModifier != 0)
                result += tabs + "\t" + "Dexterity : " + DexterityModifier + "\n";
            if (PrecisionModifier != 0)
                result += tabs + "\t" + "Precision : " + PrecisionModifier + "\n";
            return (result);
        }
    }
}
