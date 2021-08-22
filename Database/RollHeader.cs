using System;
using System.Collections.Generic;
using System.Text;

namespace NatStats.Database
{
    public partial class RollHeader
    {
        public uint Id { get; set; }
        public uint CharacterId { get; set; }
        public uint CombatId { get; set; }
        public uint SessionId { get; set; }
        public string Name { get; set; }
        public string RollType { get; set; }
        public int FinalValue { get; set; }
    }
}
