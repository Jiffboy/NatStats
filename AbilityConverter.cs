using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using NatStats.Database;
using System.Linq;

namespace NatStats
{
    class AbilityModConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var ability = value as AbilityViewModel;
            var database = new DataBaseContext();
            var skill = database.Skill.Where(s => s.Name == ability.HitCheckBase).FirstOrDefault();
            var character = database.Character.Where(c => c.Id == ability.CharacterId).FirstOrDefault();
            var proficiency = database.Proficiency.Where(p => p.CharacterId == ability.CharacterId && p.SkillId == skill.Id).FirstOrDefault();
            int val = 0;
            string rtnStr = "";

            switch (skill.Name.ToLower())
            {
                case "strength":
                    val = character.Strength;
                    break;
                case "dexterity":
                    val = character.Dexterity;
                    break;
                case "constitution":
                    val = character.Constitution;
                    break;
                case "intelligence":
                    val = character.Intelligence;
                    break;
                case "wisdom":
                    val = character.Wisdom;
                    break;
                case "charisma":
                    val = character.Charisma;
                    break;
            }

            if (proficiency != null)
            {
                val += character.ProficiencyBonus;
            }

            val += ability.HitCheckBonus;

            if (val > 0)
            {
                rtnStr += "+";
            }

            rtnStr += val;

            return rtnStr;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class AbilityRollConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var ability = value as AbilityViewModel;
            var database = new DataBaseContext();
            var skill = database.Skill.Where(s => s.Name == ability.HitCheckBase).FirstOrDefault();
            var character = database.Character.Where(c => c.Id == ability.CharacterId).FirstOrDefault();
            var damage = database.DamageType.Where(d => d.Id == ability.Effect1DamageTypeId).FirstOrDefault();
            int bonus = 0;
            string rtnStr = ability.Effect1DiceCount + "d" + ability.Effect1DiceSides;

            switch (skill.Name.ToLower())
            {
                case "strength":
                    bonus = character.Strength;
                    break;
                case "dexterity":
                    bonus = character.Dexterity;
                    break;
                case "constitution":
                    bonus = character.Constitution;
                    break;
                case "intelligence":
                    bonus = character.Intelligence;
                    break;
                case "wisdom":
                    bonus = character.Wisdom;
                    break;
                case "charisma":
                    bonus = character.Charisma;
                    break;
            }

            if (bonus > 0)
            {
                rtnStr += "+";
            }

            rtnStr += bonus + " " + damage.Name;

            return rtnStr;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
