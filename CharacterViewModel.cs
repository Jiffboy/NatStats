using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using NatStats.Database;
using System.Linq;

namespace NatStats
{
    public class CharacterViewModel : INotifyPropertyChanged
    {
        private String _name;
        private uint _id;
        private String _class;

        private int _strength;
        private int _dexterity;
        private int _constitution;
        private int _intelligence;
        private int _wisdom;
        private int _charisma;

        public CharacterViewModel(Character character)
        {
            UpdateFromDb(character);
        }

        public void UpdateFromDb(Character character)
        {
            var db = new DataBaseContext();
            Name = character.Name;
            Id = character.Id;
            Class = db.Class.Where(c => c.Id == character.ClassId).FirstOrDefault().Name;

            Strength = character.Strength;
            Dexterity = character.Dexterity;
            Constitution = character.Constitution;
            Intelligence = character.Intelligence;
            Wisdom = character.Wisdom;
            Charisma = character.Charisma;
        }

        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                if(Name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public uint Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (Id != value)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public String Class
        {
            get
            {
                return _class ;
            }
            set
            {
                if (Class != value)
                {
                    _class = value;
                    OnPropertyChanged("Class");
                }
            }
        }

        public int Strength
        {
            get
            {
                return _strength;
            }
            set
            {
                if (Strength != value)
                {
                    _strength = value;
                    OnPropertyChanged("Strength");
                }
            }
        }
        public int Dexterity
        {
            get
            {
                return _dexterity;
            }
            set
            {
                if (Dexterity != value)
                {
                    _dexterity = value;
                    OnPropertyChanged("Dexterity");
                }
            }
        }

        public int Constitution
        {
            get
            {
                return _constitution;
            }
            set
            {
                if (Constitution != value)
                {
                    _constitution = value;
                    OnPropertyChanged("Constitution");
                }
            }
        }

        public int Intelligence
        {
            get
            {
                return _intelligence;
            }
            set
            {
                if (Intelligence != value)
                {
                    _intelligence = value;
                    OnPropertyChanged("Intelligence");
                }
            }
        }

        public int Wisdom
        {
            get
            {
                return _wisdom;
            }
            set
            {
                if (Wisdom != value)
                {
                    _wisdom = value;
                    OnPropertyChanged("Wisdom");
                }
            }
        }

        public int Charisma
        {
            get
            {
                return _charisma;
            }
            set
            {
                if (Charisma != value)
                {
                    _charisma = value;
                    OnPropertyChanged("Charisma");
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
