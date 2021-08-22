using System;
using System.Collections.Generic;
using System.Text;

namespace NatStats.Database
{
    public partial class Combat
    {
        public uint Id { get; set; }
        public uint CampaignId { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
    }
}
