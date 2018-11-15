namespace Business
{
    public class Battle
    {
        public CharacterInBattle player { get; set; }
        public CharacterInBattle monster { get; set; }

        public bool AreBothSidesAlive
        {
            get
            {
                return (player.Character.CurrentHP > 0 && monster.Character.CurrentHP > 0);
            }
        }
    }
}
