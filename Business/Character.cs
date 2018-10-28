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
        protected Random seedBodyPart = new Random();
        private int _baseHP;
        protected int _baseCooldown;
        private int _level = 1;
        public int _currentExp = 0;
        private int _baseExp = 16;
        private int _givenExp = 16;
        private float expMultiplier = 1.9F;
        public float hpMultiplier = 1.1F;
        private float minAttackMultiplier = 1.1F;
        private float maxAttackMultiplier = 1.1F;
        private RightHand _rightHand;

        private int MinDamageBonus = 0;
        private int MaxDamageBonus = 0;

        private double headChance = 5;
        private double handsChance = 10;
        private double feetChance = 10;
        private double armsChance = 15;
        private double legsChance = 25;
        private double chestChance = 35;

        /// <summary>
        /// The strengh of the character.
        /// Adds 0.01 to both minAttackMultiplier and maxAttackMultiplier
        /// </summary>
        public int Strengh { get; set; }

        /// <summary>
        /// The vitality of the character.
        /// Adds 0.01 to the hpMultiplier
        /// </summary>
        public int Vitality { get; set; }

        /// <summary>
        /// The agility of the character.
        /// Used to determine the ability of evading
        /// </summary>
        public int Agility { get; set; }

        /// <summary>
        /// The dexterity of the character.
        /// User to determine the ability of parrying and blocking
        /// </summary>
        public int Dexterity { get; set; }

        /// <summary>
        /// The precision of the character.
        /// Works against target's Agility and Dexterity
        /// </summary>
        public int Precision { get; set; }

        #region gear

        public RightHand RightHand
        {
            get => _rightHand;
            set
            {
                if (value != null)
                {
                    MinDamageBonus += value.MinDamageBonus;
                    MaxDamageBonus += value.MaxDamageBonus;
                }

                if (_rightHand != null)
                {
                    MinDamageBonus -= _rightHand.MinDamageBonus;
                    MaxDamageBonus -= _rightHand.MaxDamageBonus;
                }
                _rightHand = value;
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
        public int BaseHP
        {
            get => _baseHP;
            set
            {
                _baseHP = value;
                CurrentHP = value;
            }
        }
        public int BaseMinAttack { get; set; }
        public int CurrentMinAttack
        {
            get
            {
                float curMinAtk = (float)BaseMinAttack + (float)MinDamageBonus;
                for (int i = 1; i < _level; ++i)
                    curMinAtk = curMinAtk * minAttackMultiplier;

                return ((int)Math.Round(curMinAtk));
            }
        }
        public int BaseMaxAttack { get; set; }
        public int CurrentMaxAttack
        {
            get
            {
                float curMaxAtk = (float)BaseMaxAttack + (float)MaxDamageBonus;
                for (int i = 1; i < _level; ++i)
                    curMaxAtk = curMaxAtk * maxAttackMultiplier;
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
                    ++levels;
                    ++_level;
                    _currentExp = 0;
                    BaseHP = (int)(BaseHP * hpMultiplier);
                }
            }
            if (levels > 0)
                result = "You've earned " + levels + " levels ! You are now level " + _level + "\n";
            return (result);
        }

        public void Heal(int amount)
        {
            CurrentHP = ((CurrentHP + amount > BaseHP) ? (BaseHP) : (CurrentHP + amount));
        }

        public int Defend(ref int damage, out string bodyPart)
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
            }
            CurrentHP -= damage;
            if (CurrentHP <= 0)
                CurrentHP = 0;
            return (CurrentHP);
        }

        public virtual string Attack()
        {
            string report;
            CurrentCooldown = _baseCooldown;
            int damage = seed.Next(CurrentMinAttack, CurrentMaxAttack + 1);
            string bodyPart;
            int TargetHP = Target.Defend(ref damage, out bodyPart);
            report = Name + " attacked " + Target.Name + " on the " + bodyPart + " and dealt " + damage + " damage.\n";
            report += Target.Name + " has " + TargetHP + " HP remaining.\n";
            if (TargetHP <= 0)
                report += Name + " killed " + Target.Name;
            return (report);
        }

        public virtual string Stats()
        {
            string result = "=== Name : " + Name + " === HP : " + CurrentHP + "/" + BaseHP + " === Damages : " + CurrentMinAttack + " - " + CurrentMaxAttack + " === Level : " + _level + " ===\n";

            return (result);
        }
    }
}
