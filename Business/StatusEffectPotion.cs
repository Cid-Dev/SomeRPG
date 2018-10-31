using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class StatusEffectPotion : Item, IStackable, IUsable
    {
        public int Id { get; set; }
        /// <summary>
        /// Quantity of current stack
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Max quantity of stack
        /// </summary>
        public int MaxAmount { get; set; }
        public Buff buff { get; set; }

        public void Use(Character target)
        {
            buff.Apply(target);
            --Quantity;
        }

        public StatusEffectPotion(int id)
        {
            try
            {
                DataAccess.StatusEffectPotion DalStatusEffectPotion = new DataAccess.StatusEffectPotion();
                var temp = DalStatusEffectPotion.GetStatusEffectPotionById(id);
                if (temp != null)
                {
                    foreach (var dalStatusEffectPotion in temp)
                    {
                        Id = int.Parse(dalStatusEffectPotion?.GetType().GetProperty("Id")?.GetValue(dalStatusEffectPotion, null).ToString());
                        Name = dalStatusEffectPotion?.GetType().GetProperty("Name")?.GetValue(dalStatusEffectPotion, null).ToString();
                        Description = dalStatusEffectPotion?.GetType().GetProperty("Description")?.GetValue(dalStatusEffectPotion, null).ToString();
                        MaxAmount = int.Parse(dalStatusEffectPotion?.GetType().GetProperty("MaxAmount")?.GetValue(dalStatusEffectPotion, null).ToString());

                        var tempBuff = dalStatusEffectPotion?.GetType().GetProperty("Buff")?.GetValue(dalStatusEffectPotion, null);

                        buff = new Buff()
                        {
                            Id = int.Parse(tempBuff?.GetType().GetProperty("Id")?.GetValue(tempBuff, null).ToString()),
                            Name = tempBuff?.GetType().GetProperty("Name")?.GetValue(tempBuff, null).ToString(),
                            Duration = int.Parse(tempBuff?.GetType().GetProperty("Duration")?.GetValue(tempBuff, null).ToString()),
                            IsGood = bool.Parse(tempBuff?.GetType().GetProperty("IsGood")?.GetValue(tempBuff, null).ToString()),
                            HPModifier = int.Parse(tempBuff?.GetType().GetProperty("HPModifier")?.GetValue(tempBuff, null).ToString()),
                            StrenghModifier = int.Parse(tempBuff?.GetType().GetProperty("StrenghModifier")?.GetValue(tempBuff, null).ToString()),
                            DexterityModifier = int.Parse(tempBuff?.GetType().GetProperty("DexterityModifier")?.GetValue(tempBuff, null).ToString()),
                            VitalityModifier = int.Parse(tempBuff?.GetType().GetProperty("VitalityModifier")?.GetValue(tempBuff, null).ToString()),
                            AgilityModifier = int.Parse(tempBuff?.GetType().GetProperty("AgilityModifier")?.GetValue(tempBuff, null).ToString()),
                            PrecisionModifier = int.Parse(tempBuff?.GetType().GetProperty("PrecisionModifier")?.GetValue(tempBuff, null).ToString())
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        public StatusEffectPotion(string name)
        {
            try
            {
                DataAccess.StatusEffectPotion DalStatusEffectPotion = new DataAccess.StatusEffectPotion();
                var temp = DalStatusEffectPotion.GetStatusEffectPotionByName(name);
                if (temp != null)
                {
                    foreach (var dalStatusEffectPotion in temp)
                    {
                        Id = int.Parse(dalStatusEffectPotion?.GetType().GetProperty("Id")?.GetValue(dalStatusEffectPotion, null).ToString());
                        Name = dalStatusEffectPotion?.GetType().GetProperty("Name")?.GetValue(dalStatusEffectPotion, null).ToString();
                        Description = dalStatusEffectPotion?.GetType().GetProperty("Description")?.GetValue(dalStatusEffectPotion, null).ToString();
                        MaxAmount = int.Parse(dalStatusEffectPotion?.GetType().GetProperty("MaxAmount")?.GetValue(dalStatusEffectPotion, null).ToString());

                        var tempBuff = dalStatusEffectPotion?.GetType().GetProperty("Buff")?.GetValue(dalStatusEffectPotion, null);
                        
                        buff = new Buff()
                        {
                            Id = int.Parse(tempBuff?.GetType().GetProperty("Id")?.GetValue(tempBuff, null).ToString()),
                            Name = tempBuff?.GetType().GetProperty("Name")?.GetValue(tempBuff, null).ToString(),
                            Duration = int.Parse(tempBuff?.GetType().GetProperty("Duration")?.GetValue(tempBuff, null).ToString()),
                            IsGood = bool.Parse(tempBuff?.GetType().GetProperty("IsGood")?.GetValue(tempBuff, null).ToString()),
                            HPModifier = int.Parse(tempBuff?.GetType().GetProperty("HPModifier")?.GetValue(tempBuff, null).ToString()),
                            StrenghModifier = int.Parse(tempBuff?.GetType().GetProperty("StrenghModifier")?.GetValue(tempBuff, null).ToString()),
                            DexterityModifier = int.Parse(tempBuff?.GetType().GetProperty("DexterityModifier")?.GetValue(tempBuff, null).ToString()),
                            VitalityModifier = int.Parse(tempBuff?.GetType().GetProperty("VitalityModifier")?.GetValue(tempBuff, null).ToString()),
                            AgilityModifier = int.Parse(tempBuff?.GetType().GetProperty("AgilityModifier")?.GetValue(tempBuff, null).ToString()),
                            PrecisionModifier = int.Parse(tempBuff?.GetType().GetProperty("PrecisionModifier")?.GetValue(tempBuff, null).ToString())
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
