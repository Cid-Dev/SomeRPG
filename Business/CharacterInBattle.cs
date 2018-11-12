using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class CharacterInBattle
    {
        public Character Character { get; set; }
        /// <summary>
        /// Battlefield is linear
        /// Location is the place where the character is on an single axis
        /// </summary>
        public int Location { get; set; }

        /// <summary>
        /// Defines on the side of which target the character is
        /// null if none
        /// </summary>
        public Character OnTheSideOf = null;

        /// <summary>
        /// Defines on the back of which target the character is
        /// null if none
        /// </summary>
        public Character OnTheBackOf = null;

        public int GetDistance(CharacterInBattle other)
        {
            if (other != null)
                return (Math.Abs(Location - other.Location));
            return (-1);
        }

        public void MoveFrom(CharacterInBattle other, int distance)
        {
            OnTheSideOf = null;
            OnTheBackOf = null;
            if (Location < other.Location)
                distance *= -1;
            Location += distance;
        }

        public void MoveTo(CharacterInBattle other, int distance)
        {
            OnTheSideOf = null;
            OnTheBackOf = null;
            var realDistance = GetDistance(other);
            if (realDistance < Math.Abs(distance))
                distance = realDistance;
            if (Location > other.Location)
                distance *= -1;
            Location += distance;
        }

        public bool IsInRange(CharacterInBattle target)
        {
            int range = 0;
            if (Character.RightHand != null
                && Character.RightHand is Weapon)
            {
                var RightWeapon = Character.RightHand as Weapon;
                range = RightWeapon.Range;
                if (Character.LeftHand != null
                && Character.LeftHand is Weapon)
                {
                    var LeftWeapon = (Character.LeftHand as Weapon);
                    if (LeftWeapon.Range < range)
                        range = LeftWeapon.Range;
                }
            }

            return (GetDistance(target) <= range);
        }

        public bool IsStrafePossible(CharacterInBattle target)
        {
            return (IsInRange(target)
                    && ((Character.RightHand == null)
                        || (Character.RightHand is Weapon
                            && (Character.RightHand as Weapon).WeaponClass == WeaponClass.Melee)));
        }

        public void MoveInFront(CharacterInBattle target)
        {
            if (IsStrafePossible(target))
            {
                OnTheSideOf = null;
                OnTheBackOf = null;
            }
        }

        public void MoveOnTheSide(CharacterInBattle target)
        {
            if (IsStrafePossible(target))
            {
                OnTheSideOf = target.Character;
                OnTheBackOf = null;
                target.OnTheSideOf = null;
                target.OnTheBackOf = null;
            }
        }

        public void MoveOnTheBack(CharacterInBattle target)
        {
            if (IsStrafePossible(target)
                && OnTheSideOf == target.Character)
            {
                OnTheBackOf = target.Character;
                OnTheSideOf = null;
                target.OnTheSideOf = null;
                target.OnTheBackOf = null;
            }
        }
    }
}
