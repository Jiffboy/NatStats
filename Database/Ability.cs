using System;
using System.Collections.Generic;
using System.Text;

namespace NatStats.Database
{
    public partial class Ability
    {
        public uint Id { get; set; }
        public uint CharacterId { get; set; }
        public string Name { get; set; }
        public string HitCheckBase { get; set; }
        public int HitCheckBonus { get; set; }
        public string Effect1EffectType { get; set; }
        public int Effect1DiceCount { get; set; }
        public int Effect1DiceSides { get; set; }
        public string Effect1Base { get; set; }
        public int Effect1Bonus { get; set; }
        public uint Effect1DamageTypeId { get; set; }
        public bool HasSecondaryEffect { get; set; }
        public string Effect2EffectType { get; set; }
        public int Effect2DiceCount { get; set; }
        public int Effect2DiceSides { get; set; }
        public string Effect2Base { get; set; }
        public int Effect2Bonus { get; set; }
        public uint Effect2DamageTypeId { get; set; }
    }
}
