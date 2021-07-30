using System;
using System.Collections.Generic;
using System.Text;

namespace NatStats.Database
{
    public partial class Character
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public uint CampaignId { get; set; }
        public uint ClassId { get; set; }
    }
}
