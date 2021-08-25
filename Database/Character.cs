using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace NatStats.Database
{
    public partial class Character
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public uint CampaignId { get; set; }
        public uint ClassId { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public int ProficiencyBonus { get; set; }
        public int Level { get; set; }
    }
}
