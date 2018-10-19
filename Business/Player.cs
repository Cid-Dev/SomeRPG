using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Player : Character
    {
        public override string Stats()
        {
            string result = "=== Name : " + Name + " === HP : " + CurrentHP + "/" + BaseHP + " === Damages : " + CurrentMinAttack + " - " + CurrentMaxAttack + " === Level : " + Level + " === Exp : " + _currentExp + "/" + getRequiredExp + " ===\n";

            return (result);
        }
    }
}
