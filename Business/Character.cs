using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Character
    {
        protected Random seed = new Random();
        protected Random seedEvasion = new Random();
        protected Random seedParry = new Random();
        protected Random seedBodyPart = new Random();
        private int _baseHP;
        protected int _baseCooldown;
        private int _level = 1;
        public int _currentExp = 0;
        private int _baseExp = 16;
        private int _givenExp = 16;
        private float expMultiplier = 1.9F;
        public float hpMultiplier = 1.1F;
        private float minAttackMultiplier = 1F;
        private float maxAttackMultiplier = 1F;
        private HandGear _rightHand;
        private HandGear _leftHand;

        private int RightMinDamageBonus = 0;
        private int RightMaxDamageBonus = 0;
        private int LeftMinDamageBonus = 0;
        private int LeftMaxDamageBonus = 0;
        public int HPBonus = 0;

        private double headChance = 5;
        private double handsChance = 10;
        private double feetChance = 10;
        private double armsChance = 15;
        private double legsChance = 25;
        private double chestChance = 35;

        /// <summary>
        /// The base strengh of the character
        /// </summary>
        public int BaseStrengh { get; set; }

        /// <summary>
        /// The amount of points added by the player to the strengh
        /// </summary>
        public int AddedStrengh { get; set; }

        /// <summary>
        /// The amount of points added by buffs and items to the strengh 
        /// </summary>
        public int BonusStrengh { get; set; }

        /// <summary>
        /// The strengh of the character.
        /// Adds 0.01 to both minAttackMultiplier and maxAttackMultiplier
        /// </summary>
        public int Strengh
        {
            get
            {
                return (BaseStrengh + AddedStrengh + BonusStrengh);
            }
        }

        /// <summary>
        /// The base vitality of the character
        /// </summary>
        public int BaseVitality { get; set; }

        /// <summary>
        /// The amount of points added by the player to the vitality
        /// </summary>
        public int AddedVitality { get; set; }

        /// <summary>
        /// The amount of points added by buffs and items to the vitality 
        /// </summary>
        public int BonusVitality { get; set; }

        /// <summary>
        /// The vitality of the character.
        /// Adds 0.01 to the hpMultiplier
        /// </summary>
        public int Vitality
        {
            get
            {
                return (BaseVitality + AddedVitality + BonusVitality);
            }
        }

        /// <summary>
        /// The base agility of the character
        /// </summary>
        public int BaseAgility { get; set; }

        /// <summary>
        /// The amount of points added by the player to the agility
        /// </summary>
        public int AddedAgility { get; set; }

        /// <summary>
        /// The amount of points added by buffs and items to the agility 
        /// </summary>
        public int BonusAgility { get; set; }

        /// <summary>
        /// The agility of the character.
        /// Used to determine the ability of evading
        /// </summary>
        public int Agility
        {
            get
            {
                return (BaseAgility + AddedAgility + BonusAgility);
            }
        }

        /// <summary>
        /// The base agility of the character
        /// </summary>
        public int BaseDexterity { get; set; }

        /// <summary>
        /// The amount of points added by the player to the agility
        /// </summary>
        public int AddedDexterity { get; set; }

        /// <summary>
        /// The amount of points added by buffs and items to the agility 
        /// </summary>
        public int BonusDexterity { get; set; }

        /// <summary>
        /// The dexterity of the character.
        /// User to determine the ability of parrying and blocking
        /// </summary>
        public int Dexterity
        {
            get
            {
                return (BaseDexterity + AddedDexterity + BonusDexterity);
            }
        }

        /// <summary>
        /// The base agility of the character
        /// </summary>
        public int BasePrecision { get; set; }

        /// <summary>
        /// The amount of points added by the player to the agility
        /// </summary>
        public int AddedPrecision { get; set; }

        /// <summary>
        /// The amount of points added by buffs and items to the agility 
        /// </summary>
        public int BonusPrecision { get; set; }

        /// <summary>
        /// The precision of the character.
        /// Works against target's Agility and Dexterity
        /// </summary>
        public int Precision
        {
            get
            {
                return (BasePrecision + AddedPrecision + BonusPrecision);
            }
        }

        public List<Buff> Buffs = new List<Buff>();
        public List<Buff> DeBuffs = new List<Buff>();

        #region gear

        public HandGear RightHand
        {
            get => _rightHand;
            set
            {
                if (value != null
                    && value is Weapon)
                {
                    var weapon = value as Weapon;
                    RightMinDamageBonus += weapon.MinDamageBonus;
                    RightMaxDamageBonus += weapon.MaxDamageBonus;
                }

                if (_rightHand != null
                    && _rightHand is Weapon)
                {
                    var oldWeapon = _rightHand as Weapon;
                    RightMinDamageBonus -= oldWeapon.MinDamageBonus;
                    RightMaxDamageBonus -= oldWeapon.MaxDamageBonus;
                }
                _rightHand = value;
            }
        }

        public HandGear LeftHand
        {
            get => _leftHand;
            set
            {
                if (value != null
                    && value is Weapon)
                {
                    var weapon = value as Weapon;
                    LeftMinDamageBonus += weapon.MinDamageBonus;
                    LeftMaxDamageBonus += weapon.MaxDamageBonus;
                }

                if (_leftHand != null
                    && _leftHand is Weapon)
                {
                    var oldWeapon = _leftHand as Weapon;
                    LeftMinDamageBonus -= oldWeapon.MinDamageBonus;
                    LeftMaxDamageBonus -= oldWeapon.MaxDamageBonus;
                }
                _leftHand = value;
            }
        }

        public ChestArmor ChestArmor { get; set; }
        public LegsArmor LegsArmor { get; set; }
        public SleevesArmor SleevesArmor { get; set; }
        public FeetArmor FeetArmor { get; set; }
        public HandsArmor HandsArmor { get; set; }
        public HeadArmor HeadArmor { get; set; }

        #endregion gear

        public string Name { get; set; }
        public int Level{ get => _level; set => _level = value; }
        public int GivenExp { get => _givenExp; set => _givenExp = value; }
        public int CurrentHP { get; set; }
        public int HP
        {
            get
            {
                var curHP = (1 + (0.01 * Vitality)) * BaseHP * Level + HPBonus;
                return ((int)Math.Round(curHP));
            }
        }
        public int BaseHP
        {
            get => _baseHP;
            set
            {
                _baseHP = value;
                CurrentHP = value;
            }
        }
        public int BaseRightMinAttack { get; set; }
        public int BaseLeftMinAttack { get; set; }
        public int CurrentRightMinAttack
        {
            get
            {
                var curMinAtk = (1 + (0.01 * Strengh)) * (BaseRightMinAttack + RightMinDamageBonus);
                return ((int)Math.Round(curMinAtk));
            }
        }
        public int CurrentLeftMinAttack
        {
            get
            {
                var curMinAtk = (1 + (0.01 * Strengh)) * (BaseLeftMinAttack + LeftMinDamageBonus);
                return ((int)Math.Round(curMinAtk));
            }
        }
        public int BaseRightMaxAttack { get; set; }
        public int BaseLeftMaxAttack { get; set; }
        public int CurrentRightMaxAttack
        {
            get
            {
                var curMaxAtk = (1 + (0.01 * Strengh)) * (BaseRightMaxAttack + RightMaxDamageBonus);
                return ((int)Math.Round(curMaxAtk));
            }
        }
        public int CurrentLeftMaxAttack
        {
            get
            {
                var curMaxAtk = (1 + (0.01 * Strengh)) * (BaseLeftMaxAttack + LeftMaxDamageBonus);
                return ((int)Math.Round(curMaxAtk));
            }
        }

        public int CurrentCooldown { get; set; }
        public int BaseCooldown
        {
            get => _baseCooldown;
            set
            {
                _baseCooldown = value;
                CurrentCooldown = value;
            }
        }
        public Character Target { get; set; }

        public int BaseExp { set => _baseExp = value; }

        public int getRequiredExp
        {
            get
            {
                return ((int)Math.Round((double)_baseExp * Math.Pow((double)_level, (double)expMultiplier)));
            }
        }

        public int getGivenExp
        {
            get
            {
                return ((int)(_givenExp * _level));
            }
        }

        protected void LevelUp()
        {
            ++_level;
            ++BaseStrengh;
            if (_level % 2 == 0)
                ++BaseVitality;
            if (_level % 5 == 0)
                ++BaseAgility;
            if (_level % 5 == 0)
                ++BasePrecision;
            if (_level % 3 == 0)
                ++BaseDexterity;
            _currentExp = 0;
            CurrentHP = HP;
        }

        protected string setExp(int exp)
        {
            string result = "";
            int levels = 0;
            while (exp > 0)
            {
                _currentExp += exp;
                exp = _currentExp - getRequiredExp;
                if (exp >= 0)
                {
                    LevelUp();
                    ++levels;
                }
            }
            if (levels > 0)
                result = "You've earned " + levels + " level" + ((levels < 2) ? ("") : ("s")) + " ! You are now level " + _level + "\n";
            return (result);
        }

        public void Heal(int amount)
        {
            CurrentHP = ((CurrentHP + amount > HP) ? (HP) : (CurrentHP + amount));
        }

        public int Defend(ref int damage, HandGear handGear, out string bodyPart)
        {
            bodyPart = "";

            int aim = seedBodyPart.Next(1, 101);
            Armor armorPart = null;
            switch (aim)
            {
                case int n when (n <= headChance):
                    bodyPart = "head";
                    armorPart = HeadArmor;
                    break;

                case int n when (n <= headChance + handsChance):
                    bodyPart = "hands";
                    armorPart = HandsArmor;
                    break;

                case int n when (n <= headChance + handsChance + feetChance):
                    bodyPart = "feet";
                    armorPart = FeetArmor;
                    break;

                case int n when (n <= headChance + handsChance + feetChance + armsChance):
                    bodyPart = "arms";
                    armorPart = SleevesArmor;
                    break;

                case int n when (n <= headChance + handsChance + feetChance + armsChance + legsChance):
                    bodyPart = "legs";
                    armorPart = LegsArmor;
                    break;

                default:
                    bodyPart = "chest";
                    armorPart = ChestArmor;
                    break;
            }
            if (armorPart != null)
            {
                damage = ((damage - armorPart.Defense >= 0) ? (damage - armorPart.Defense) : (0));
                damage = (int)Math.Round((double)damage * ((100.0 - armorPart.ArmorType.Absorbency) / 100.0));
                if (handGear != null && handGear is Weapon)
                {
                    var weaponType = (handGear as Weapon).TypeName;

                    switch (weaponType)
                    {
                        case WeaponType.Slash:
                            switch (armorPart.ArmorType.Name)
                            {
                                case "Plate":
                                    damage -= (int)(damage * 25.0 / 100.0);
                                    break;

                                case "Studded leather":
                                    damage += (int)(damage * 50.0 / 100.0);
                                    break;

                                case "Leather":
                                    damage += (int)(damage * 50.0 / 100.0);
                                    break;

                                default:
                                    break;
                            }
                            break;

                        case WeaponType.Blunt:
                            switch (armorPart.ArmorType.Name)
                            {
                                case "Plate":
                                    damage += (int)(damage * 50.0 / 100.0);
                                    break;

                                case "Mail":
                                    damage -= (int)(damage * 25.0 / 100.0);
                                    break;

                                default:
                                    break;

                            }
                            break;

                        case WeaponType.Thrust:
                            switch (armorPart.ArmorType.Name)
                            {
                                case "Mail":
                                    damage += (int)(damage * 50.0 / 100.0);
                                    break;

                                case "Studded leather":
                                    damage -= (int)(damage * 25.0 / 100.0);
                                    break;

                                case "Leather":
                                    damage -= (int)(damage * 25.0 / 100.0);
                                    break;

                                default:
                                    break;
                            }
                            break;
                    }
                }
            }
            CurrentHP -= damage;
            if (CurrentHP <= 0)
                CurrentHP = 0;
            return (CurrentHP);
        }

        protected bool IsAttackEvaded()
        {
            int baseEvasion = 10;
            int factor = Target.Agility - Precision;
            if (factor < 0)
                factor = 0;
            int finalEvasion = baseEvasion + factor;
            if (finalEvasion > 90)
                finalEvasion = 90;
            int evadResult = seedEvasion.Next(101);
            if (evadResult <= finalEvasion)
                return (true);
            return (false);
        }
        //BaseDexterity

        protected bool IsAttackParried()
        {
            int baseParry = 10;
            int factor = Target.Agility - Precision;
            if (factor < 0)
                factor = 0;
            int finalParry = baseParry + factor;
            if (finalParry > 90)
                finalParry = 90;
            int parryResult = seedParry.Next(101);
            if (parryResult <= finalParry)
                return (true);
            return (false);
        }

        protected virtual string AnyHandAttack(HandGear handGear, int CurrentMinAttack, int CurrentMaxAttack)
        {
            string report = "";

            if (IsAttackEvaded())
            {
                report = Name + " attacked " + Target.Name + " with " + ((handGear != null) ? (handGear.Name) : ("bare hands")) + " but " + Target.Name + " evaded the blow.";
            }
            else if (IsAttackParried())
            {
                report = Name + " attacked " + Target.Name + " with " + ((handGear != null) ? (handGear.Name) : ("bare hands")) + " but " + Target.Name + " parried the blow.";
            }
            else
            {
                int damage = seed.Next(CurrentMinAttack, CurrentMaxAttack + 1);
                string bodyPart;
                int TargetHP = Target.Defend(ref damage, handGear, out bodyPart);
                report = Name + " attacked " + Target.Name + " with " + ((handGear != null) ? (handGear.Name) : ("bare hands")) + " on the " + bodyPart + " and dealt " + damage + " damage.\n";
                report += Target.Name + " has " + TargetHP + " HP remaining.\n";
                if (TargetHP <= 0)
                    report += Name + " killed " + Target.Name;
            }

            return (report);
        }

        public virtual string Attack()
        {
            string report = "";
            CurrentCooldown = _baseCooldown;
            if (CurrentRightMinAttack > 0
                || CurrentRightMaxAttack > 0)
                report += AnyHandAttack(RightHand, CurrentRightMinAttack, CurrentRightMaxAttack);

            if (Target.CurrentHP > 0
                && (CurrentLeftMinAttack > 0
                || CurrentLeftMaxAttack > 0))
                report += AnyHandAttack(LeftHand, CurrentLeftMinAttack, CurrentLeftMaxAttack);
            return (report);
        }

        public virtual string Stats()
        {
            string result = "=== Name : " + Name + " === HP : " + CurrentHP + "/" + HP + " === Damages : Right hand [" + CurrentRightMinAttack + " - " + CurrentRightMaxAttack + "]";
            if (CurrentLeftMinAttack > 0
                || CurrentLeftMaxAttack > 0)
                result += " Left hand [" + CurrentLeftMinAttack + " - " + CurrentLeftMaxAttack + "]";
            result += " === Level : " + _level + " ===\n";

            return (result);
        }
    }
}
