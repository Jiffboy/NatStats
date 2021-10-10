using System;
using System.Collections.Generic;
using System.Text;
using NatStats.Database;

namespace NatStats
{
    static class CommonFuncs
    {
        public static int GetBaseStat(Character character, string stat)
        {
            switch (stat.ToLower())
            {
                case "strength":
                    return character.Strength;
                case "dexterity":
                    return character.Dexterity;
                case "constitution":
                    return character.Constitution;
                case "intelligence":
                    return character.Intelligence;
                case "wisdom":
                    return character.Wisdom;
                case "charisma":
                    return character.Charisma;
            }
            return 0;
        }
    }
}
