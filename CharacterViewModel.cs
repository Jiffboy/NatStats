using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using NatStats.Database;
using System.Linq;
using System.Collections.ObjectModel;

namespace NatStats
{
    public class CharacterViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Class> ClassList { get; private set; }
        public ObservableCollection<Skill> UnusedSkillList { get; private set; }
        public ObservableCollection<Skill> ProficiencyList { get; private set; }
        public ObservableCollection<Skill> CastingModifierList { get; private set; }
        public ObservableCollection<AbilityViewModel> Abilities { get; private set; }

        private Character _character;
        private DataBaseContext _database;

        public CharacterViewModel(uint campaignId, uint characterId)
        {
            _database = new DataBaseContext();
            ClassList = new ObservableCollection<Class>();
            UnusedSkillList = new ObservableCollection<Skill>();
            ProficiencyList = new ObservableCollection<Skill>();
            CastingModifierList = new ObservableCollection<Skill>();
            Abilities = new ObservableCollection<AbilityViewModel>();

            var classes = _database.Class.ToList();
            foreach (var clss in classes)
            {
                ClassList.Add(clss);
            }

            var skills = _database.Skill.ToList();
            foreach (var skill in skills)
            {
                UnusedSkillList.Add(skill);
                if(skill.Name == skill.Base)
                {
                    CastingModifierList.Add(skill);
                }
            }

            _character = _database.Character.Where(c => c.Id == characterId).FirstOrDefault();
            if (_character == null)
            {
                _character = new Character { CampaignId = campaignId };
            }

            var proficiencies = _database.Proficiency.Where(p => p.CharacterId == characterId).ToList();
            foreach (var proficiency in proficiencies)
            {
                var skill = _database.Skill.Where(s => s.Id == proficiency.SkillId).FirstOrDefault();
                AddProficiency(skill);
            }

            var abilites = _database.Ability.Where(c => c.CharacterId == characterId).ToList();
            foreach(var ability in abilites)
            {
                Abilities.Add(new AbilityViewModel(characterId, ability.Id));
            }    
        }

        public void SaveToDb()
        {
            var proficiencies = _database.Proficiency.Where(p => p.CharacterId == _character.Id).ToList();

            if (_database.Character.Where(c => c.Id == _character.Id).FirstOrDefault() == null)
            {
                var result = _database.Add(_character);
                // Saving early to let the database handle the Character ID
                _database.SaveChanges();

                // The entity in the database will be a bit different, so copy it over
                _character = result.Entity;
            }

            // Clear out and reset proficiencies so we don't have to worry about duplicates
            foreach (var proficiency in proficiencies)
            {
                _database.Remove(proficiency);
            }

            foreach(var skill in ProficiencyList)
            {
                _database.Add(new Proficiency { CharacterId = _character.Id, SkillId = skill.Id });
            }

            foreach(var ability in Abilities)
            {
                // Just in case we made this ability before the Character Id was assigned
                ability.CharacterId = _character.Id;
                ability.SaveToDb();
            }

            _database.SaveChanges();
        }

        public void AddProficiency(Skill skill)
        {
            if (skill != null && !ProficiencyList.Contains(skill))
            {
                ProficiencyList.Add(skill);
                UnusedSkillList.Remove(skill);
            }
        }

        public void RemoveProficiency(Skill skill)
        {
            if(skill != null && ProficiencyList.Contains(skill))
            {
                ProficiencyList.Remove(skill);
                UnusedSkillList.Add(skill);
            }
        }

        // Returns the associated stat from the shorthand for the base stats
        public int ValueFromShorthand(string abr)
        {
            abr = abr.ToLower();
            switch(abr)
            {
                case "strength":
                    return Strength;
                case "dexterity":
                    return Dexterity;
                case "constitution":
                    return Constitution;
                case "intelligence":
                    return Intelligence;
                case "wisdom":
                    return Wisdom;
                case "charisma":
                    return Charisma;
                default:
                    return 0;
            }
        }

        public String Name
        {
            get
            {
                return _character.Name;
            }
            set
            {
                if(Name != value)
                {
                    _character.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public uint Id
        {
            get
            {
                return _character.Id;
            }
            set
            {
                if (Id != value)
                {
                    _character.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public uint ClassId
        {
            get
            {
                return _character.ClassId;
            }
            set
            {
                if (ClassId != value)
                {
                    _character.ClassId = value;
                    OnPropertyChanged("ClassId");
                }
            }
        }

        public int Strength
        {
            get
            {
                return _character.Strength;
            }
            set
            {
                if (Strength != value)
                {
                    _character.Strength = value;
                    OnPropertyChanged("Strength");
                }
            }
        }
        public int Dexterity
        {
            get
            {
                return _character.Dexterity;
            }
            set
            {
                if (Dexterity != value)
                {
                    _character.Dexterity = value;
                    OnPropertyChanged("Dexterity");
                }
            }
        }

        public int Constitution
        {
            get
            {
                return _character.Constitution;
            }
            set
            {
                if (Constitution != value)
                {
                    _character.Constitution = value;
                    OnPropertyChanged("Constitution");
                }
            }
        }

        public int Intelligence
        {
            get
            {
                return _character.Intelligence;
            }
            set
            {
                if (Intelligence != value)
                {
                    _character.Intelligence = value;
                    OnPropertyChanged("Intelligence");
                }
            }
        }

        public int Wisdom
        {
            get
            {
                return _character.Wisdom;
            }
            set
            {
                if (Wisdom != value)
                {
                    _character.Wisdom = value;
                    OnPropertyChanged("Wisdom");
                }
            }
        }

        public int Charisma
        {
            get
            {
                return _character.Charisma;
            }
            set
            {
                if (Charisma != value)
                {
                    _character.Charisma = value;
                    OnPropertyChanged("Charisma");
                }
            }
        }

        public int ProficiencyBonus
        {
            get
            {
                return _character.ProficiencyBonus;
            }
            set
            {
                if (ProficiencyBonus != value)
                {
                    _character.ProficiencyBonus = value;
                    OnPropertyChanged("ProficiencyBonus");
                }
            }
        }

        public int Level
        {
            get
            {
                return _character.Level;
            }
            set
            {
                if (Level != value)
                {
                    _character.Level = value;
                    OnPropertyChanged("Level");
                }
            }
        }

        public uint CastingId
        {
            get
            {
                return _character.CastingId;
            }
            set
            {
                if (CastingId != value)
                {
                    _character.CastingId = value;
                    OnPropertyChanged("CastingId");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(String info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
