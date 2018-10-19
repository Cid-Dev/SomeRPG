using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Character
    {
        private Random seed = new Random();
        private int _baseHP;
        private int _baseCooldown;
        private int _level = 1;
        protected int _currentExp = 0;
        private int _baseExp = 16;
        private int _givenExp = 4;
        private float expMultiplier = 1.5F;
        private float hpMultiplier = 1.1F;
        private float minAttackMultiplier = 1.1F;
        private float maxAttackMultiplier = 1.1F;

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
                int curMinAtk = BaseMinAttack;
                for (int i = 1; i < _level; ++i)
                    curMinAtk = (int)(curMinAtk * minAttackMultiplier);
                return (curMinAtk);
            }
        }
        public int BaseMaxAttack { get; set; }
        public int CurrentMaxAttack
        {
            get
            {
                int curMaxAtk = BaseMaxAttack;
                for (int i = 1; i < _level; ++i)
                    curMaxAtk = (int)(curMaxAtk * maxAttackMultiplier);
                return (curMaxAtk);
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

        public int getRequiredExp
        {
            get
            {
                int givenXP = _baseExp;
                for (int i = 1; i < _level; ++i)
                    givenXP = (int)(givenXP * expMultiplier);
                return (givenXP);
            }
        }

        public int getGivenExp
        {
            get
            {
                return ((int)(getRequiredExp / _givenExp));
            }
        }

        private string setExp(int exp)
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

        public int Defend(int damage)
        {
            CurrentHP -= damage;
            if (CurrentHP <= 0)
                CurrentHP = 0;
            return (CurrentHP);
        }

        public string Attack()
        {
            string report;
            CurrentCooldown = _baseCooldown;
            int damage = seed.Next(CurrentMinAttack, CurrentMaxAttack + 1);
            int TargetHP = Target.Defend(damage);
            report = Name + " attacked " + Target.Name + " and dealt " + damage + " damage.\n";
            report += Target.Name + " has " + TargetHP + " HP remaining.\n";
            if (TargetHP <= 0)
            {
                report += "You killed " + Target.Name + " and have earned " + Target.getGivenExp + " exp\n";
                report += setExp(Target.getGivenExp);
            }
            return (report);
        }

        public virtual string Stats()
        {
            string result = "=== Name : " + Name + " === HP : " + BaseHP + " === Damages : " + CurrentMinAttack + " - " + CurrentMaxAttack + " === Level : " + _level + " ===\n";

            return (result);
        }
    }
}
