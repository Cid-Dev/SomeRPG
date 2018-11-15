using System;
using System.Collections.Generic;

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
            foreach (var EventsName in EventsNames)
            {
                switch (EventsName)
                {
                    case "InRange":
                        if (events[EventsName].Trigger<bool>(battle.player))
                            result.Add(EventsName, (events[EventsName].ResultOk<List<AttackReport>>()));
                        else if (events[EventsName].resultko != null)
                            result.Add(EventsName, (events[EventsName].ResultKo<int>(battle.player, battle.monster.Character.BaseCooldown)));
                        break;
                }
            }
            return (result);
        }
    }
}
