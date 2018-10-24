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
        private int _baseHP;
        protected int _baseCooldown;
        private int _level = 1;
        protected int _currentExp = 0;
        private int _baseExp = 16;
        private int _givenExp = 16;
        private float expMultiplier = 1.9F;
        protected float hpMultiplier = 1.1F;
        private float minAttackMultiplier = 1.1F;
        private float maxAttackMultiplier = 1.1F;
        private RightHand _rightHand;

        private int MinDamageBonus = 0;
        private int MaxDamageBonus = 0;

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

        public int Defend(int damage)
        {
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
            int TargetHP = Target.Defend(damage);
            report = Name + " attacked " + Target.Name + " and dealt " + damage + " damage.\n";
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
