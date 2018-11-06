using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Weapon : HandGear
    {
        public int MinDamageBonus { get; set; }
        public int MaxDamageBonus { get; set; }
        public bool isTwoHand { get; set; }
        public WeaponType TypeName { get; set; }

        public bool IsTargetingRightHand = true;

        public Weapon(int id)
        {
            try
            {
                DataAccess.Weapon DalWeapon = new DataAccess.Weapon();
                var temp = DalWeapon.GetWeaponById(id);
                if (temp != null)
                {
                    foreach (var dalWeapon in temp)
                    {
                        Id = int.Parse(dalWeapon?.GetType().GetProperty("Id")?.GetValue(dalWeapon, null).ToString());
                        Name = dalWeapon?.GetType().GetProperty("Name")?.GetValue(dalWeapon, null).ToString();
                        Description = dalWeapon?.GetType().GetProperty("Description")?.GetValue(dalWeapon, null).ToString();
                        MinDamageBonus = int.Parse(dalWeapon?.GetType().GetProperty("MinDamageBonus")?.GetValue(dalWeapon, null).ToString());
                        MaxDamageBonus = int.Parse(dalWeapon?.GetType().GetProperty("MaxDamageBonus")?.GetValue(dalWeapon, null).ToString());
                        isTwoHand = bool.Parse(dalWeapon?.GetType().GetProperty("isTwoHand")?.GetValue(dalWeapon, null).ToString());
                        if (Enum.TryParse(dalWeapon?.GetType().GetProperty("TypeName")?.GetValue(dalWeapon, null).ToString(), out WeaponType weaponType))
                            TypeName = weaponType;
                        else
                            throw new Exception("Invalid weapon type");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Weapon(string name)
        {
            try
            {
                DataAccess.Weapon DalWeapon = new DataAccess.Weapon();
                var temp = DalWeapon.GetWeaponByName(name);
                if (temp != null)
                {
                    foreach (var dalWeapon in temp)
                    {
                        Id = int.Parse(dalWeapon?.GetType().GetProperty("Id")?.GetValue(dalWeapon, null).ToString());
                        Name = dalWeapon?.GetType().GetProperty("Name")?.GetValue(dalWeapon, null).ToString();
                        Description = dalWeapon?.GetType().GetProperty("Description")?.GetValue(dalWeapon, null).ToString();
                        MinDamageBonus = int.Parse(dalWeapon?.GetType().GetProperty("MinDamageBonus")?.GetValue(dalWeapon, null).ToString());
                        MaxDamageBonus = int.Parse(dalWeapon?.GetType().GetProperty("MaxDamageBonus")?.GetValue(dalWeapon, null).ToString());
                        isTwoHand = bool.Parse(dalWeapon?.GetType().GetProperty("isTwoHand")?.GetValue(dalWeapon, null).ToString());
                        if (Enum.TryParse(dalWeapon?.GetType().GetProperty("TypeName")?.GetValue(dalWeapon, null).ToString(), out WeaponType weaponType))
                            TypeName = weaponType;
                        else
                            throw new Exception("Invalid weapon type");
                    }
                }

            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        public override void TakeOff(Character target)
        {
            if (IsTargetingRightHand)
            {
                if (target.RightHand != null)
                {
                    if (target is Player)
                    {
                        var player = target as Player;
                        player.Inventory.Add(player.RightHand);
                        if (isTwoHand && player.LeftHand != null)
                            player.Inventory.Add(player.LeftHand);
                    }
                    target.RightHand = null;
                    if (isTwoHand)
                        target.LeftHand = null;
                }
            }
            else
            {
                if (target.LeftHand != null)
                {
                    if (target is Player)
                    {
                        var player = target as Player;
                        player.Inventory.Add(player.LeftHand);
                        if (isTwoHand && player.RightHand != null)
                            player.Inventory.Add(player.RightHand);
                    }
                    target.LeftHand = null;
                    if (isTwoHand)
                        target.RightHand = null;
                }
            }
        }

        public override void TakeOn(Character target)
        {
            TakeOff(target);
            if (target is Player)
            {
                var player = target as Player;
                player.Inventory.Remove(this);
            }
            if (IsTargetingRightHand)
                target.RightHand = this;
            else
                target.LeftHand = this;
        }
    }
}
