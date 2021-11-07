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
            var character = database.Character.Where(c => c.Id == ability.CharacterId).FirstOrDefault();
            string rtnStr = "";

            if (ability.HasHitCheck)
            {
                var skill = database.Skill.Where(s => s.Id == ability.HitCheckBaseId).FirstOrDefault();
                var proficiency = database.Proficiency.Where(p => p.CharacterId == ability.CharacterId && p.SkillId == skill.Id).FirstOrDefault();
                int val = 0;

                val = CommonFuncs.GetBaseStat(character, skill.Name);

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
            }
            else if(ability.HasSavingThrow)
            {
                if(ability.UsesCastingDC)
                {
                    var skill = database.Skill.Where(s => s.Id == character.CastingId).FirstOrDefault();
                    if (skill != null)
                    {
                        var proficiency = database.Proficiency.Where(p => p.CharacterId == ability.CharacterId && p.SkillId == skill.Id).FirstOrDefault();
                        int dc = 8;
                        if(proficiency != null)
                        {
                            dc += character.ProficiencyBonus;
                        }
                        dc += CommonFuncs.GetBaseStat(character, skill.Name);
                        rtnStr = "DC " + dc;
                    }
                }
                else if(ability.DCSaveId != 0)
                {
                    var skill = database.Skill.Where(s => s.Id == ability.DCSaveId).FirstOrDefault();
                    if (skill != null)
                    {
                        var proficiency = database.Proficiency.Where(p => p.CharacterId == ability.CharacterId && p.SkillId == skill.Id).FirstOrDefault();
                        int dc = 8;
                        if (proficiency != null)
                        {
                            dc += character.ProficiencyBonus;
                        }
                        dc += CommonFuncs.GetBaseStat(character, skill.Name);
                        rtnStr = "DC " + dc;
                    }
                }
                else if(ability.FlatDC != 0)
                {
                    rtnStr = "DC " + ability.FlatDC;
                }
            }

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
            var character = database.Character.Where(c => c.Id == ability.CharacterId).FirstOrDefault();
            string rtnStr = "";

            if (ability.HasEffect && character != null)
            {
                var skill = database.Skill.Where(s => s.Id == ability.EffectBaseId).FirstOrDefault();
                var damage = database.DamageType.Where(d => d.Id == ability.EffectDamageTypeId).FirstOrDefault();
                int bonus = 0;
                rtnStr = ability.EffectDiceCount + "d" + ability.EffectDiceSides;

                bonus = CommonFuncs.GetBaseStat(character, skill.Name);

                if (bonus > 0)
                {
                    rtnStr += "+";
                }

                if (bonus != 0)
                {
                    rtnStr += bonus;
                }

                if(!ability.EffectHeals)
                    rtnStr += " " + damage.Name;
                else
                    rtnStr += " Healing";
            }

            return rtnStr;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
