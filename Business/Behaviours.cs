using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Behaviours
    {
        public Battle battle;
        public Dictionary<string, Behaviour> events;
        private List<string> EventsNames;

        public Behaviours(Battle _battle, List<string> eventsNames)
        {
            battle = _battle;
            EventsNames = new List<string>(eventsNames);

            events = new Dictionary<string, Behaviour>();
            for (int i = EventsNames.Count - 1; i >= 0; --i)
            {
                switch (EventsNames[i])
                {
                    case ("InRange"):
                        events.Add(EventsNames[i], new Behaviour
                        {
                            trigger = (Func<CharacterInBattle, bool>)battle.monster.IsInRange,
                            resultok = (Func<List<AttackReport>>)battle.monster.Character.Attack,
                            resultko = (Func<CharacterInBattle, int, int>)battle.monster.MoveTo
                        });
                        break;

                    default:
                        EventsNames.RemoveAt(i);
                        break;
                }
            }
        }

        public Dictionary<string, object> SelectBehaviour()
        {
            var result = new Dictionary<string, object>();
            foreach (var Event in events)
            {
                var name = Event.Key;
                switch (name)
                {
                    case "InRange":
                        if (events[name].Trigger<bool>(battle.player))
                            result.Add(name, (events[name].ResultOk<List<AttackReport>>()));
                        else if (events[name].resultko != null)
                            result.Add(name, (events[name].ResultKo<int>(battle.player, battle.monster.Character.BaseCooldown)));
                        break;
                }
            }
            return (result);
        }
    }
}
