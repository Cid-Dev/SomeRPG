using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class AttackReport
    {
        public AttackResult AttackResult { get; set; }
        public string AttackerName { get; set; }
        public string WeaponName { get; set; }
        public string DefenderName { get; set; }
        public int Damage { get; set; }
        public string BodyPart { get; set; }
        public int DefenderRemainingHP { get; set; }
    }
}
