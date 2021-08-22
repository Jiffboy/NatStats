using System;
using System.Collections.Generic;
using System.Text;

namespace NatStats.Database
{
    public partial class Campaign
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public bool InCombat { get; set; }
    }
}
