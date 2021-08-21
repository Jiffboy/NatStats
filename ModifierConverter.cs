using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using NatStats.Database;
using System.Linq;

namespace NatStats
{
    class ModifierConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var character = values[0] as CharacterViewModel;
            var skill = values[1] as Skill;
            var database = new DataBaseContext();
            var proficiency = database.Proficiency.Where(p => p.CharacterId == character.Id && p.SkillId == skill.Id).FirstOrDefault();
            int val = character.ValueFromShorthand(skill.Base);
            string rtnStr = skill.Name + "\n";

            if(proficiency != null)
            {
                val += 2; //TODO: Make proficiency weight modifiable
            }

            if(val > 0)
            {
                rtnStr += "+";
            }

            rtnStr += val;

            return rtnStr;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
