using System;
using System.Collections.Generic;
using System.Text;

namespace NatStats.Database
{
    public partial class Roll
    {
        public uint Id { get; set; }
        public uint HeaderId { get; set; }
        public string Description { get; set; }
        public int Modifier { get; set; }
        public int BonusModifier { get; set; }
        public int DiceSides { get; set; }
        public int DiceRoll { get; set; }
        public int Total { get; set; }
        public bool IsFinal { get; set; }
    }
}
