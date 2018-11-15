using System;
using System.Collections.Generic;

namespace Business
{
    public class StatusEffectPotion : Item, IStackable, IUsable
    {
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

        private void BuildStatusEffectPotion(List<object> StatusEffectPot)
        {
            if (StatusEffectPot != null)
            {
                foreach (var dalStatusEffectPotion in StatusEffectPot)
                {
                    Id = int.Parse(dalStatusEffectPotion?.GetType().GetProperty("Id")?.GetValue(dalStatusEffectPotion, null).ToString());
                    Name = dalStatusEffectPotion?.GetType().GetProperty("Name")?.GetValue(dalStatusEffectPotion, null).ToString();
                    Description = dalStatusEffectPotion?.GetType().GetProperty("Description")?.GetValue(dalStatusEffectPotion, null).ToString();
                    MaxAmount = int.Parse(dalStatusEffectPotion?.GetType().GetProperty("MaxAmount")?.GetValue(dalStatusEffectPotion, null).ToString());

                    var tempBuff = dalStatusEffectPotion?.GetType().GetProperty("Buff")?.GetValue(dalStatusEffectPotion, null);
                    var BuffId = int.Parse(tempBuff?.GetType().GetProperty("Id")?.GetValue(tempBuff, null).ToString());
                    buff = new Buff(BuffId);
                }
            }
        }

        public StatusEffectPotion(int id)
        {
            try
            {
                DataAccess.StatusEffectPotion DalStatusEffectPotion = new DataAccess.StatusEffectPotion();
                BuildStatusEffectPotion(DalStatusEffectPotion.GetStatusEffectPotion(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public StatusEffectPotion(string name)
        {
            try
            {
                DataAccess.StatusEffectPotion DalStatusEffectPotion = new DataAccess.StatusEffectPotion();
                BuildStatusEffectPotion(DalStatusEffectPotion.GetStatusEffectPotion(name));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
