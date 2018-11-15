using System;

namespace Business
{
    public class Buff : Status
    {
        public int Id { get; set; }
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
                var temp = DalBuff.GetBuff(id);
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
                throw ex;
            }
        }

        public override void Apply(Character target)
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
                ApplyTo<Buff>(target.Buffs);
            else
                ApplyTo<Buff>(target.DeBuffs);
        }

        public override void RemoveEffect(Character target)
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
    }
}
