using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using NatStats.Database;
using System.Linq;
using System.Collections.ObjectModel;


namespace NatStats
{
    public class AbilityViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<String> BaseList { get; private set; }
        public ObservableCollection<String> DamageTypeList { get; private set; }

        private Ability _ability;
        private DataBaseContext _database;

        public AbilityViewModel(uint characterId, uint abilityId)
        {
            _database = new DataBaseContext();
            BaseList = new ObservableCollection<string>();
            DamageTypeList = new ObservableCollection<string>();

            _ability = _database.Ability.Where(c => c.Id == abilityId).FirstOrDefault();
            if (_ability == null)
            {
                _ability = new Ability {};
            }

            foreach( var skill in _database.Skill.Where(s => s.Name == s.Base).ToList())
            {
                BaseList.Add(skill.Name);
            }

            foreach( var damage in _database.DamageType.ToList())
            {
                DamageTypeList.Add(damage.Name);
            }
        }

        public void SaveToDb()
        {
            if (_database.Ability.Where(c => c.Id == _ability.Id).FirstOrDefault() == null)
            {
                var result = _database.Add(_ability);
                // Saving early to let the database handle the Ability ID
                _database.SaveChanges();

                // The entity in the database will be a bit different, so copy it over
                _ability = result.Entity;
            }

            _database.SaveChanges();
        }

        public String Name
        {
            get
            {
                return _ability.Name;
            }
            set
            {
                if (Name != value)
                {
                    _ability.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public uint Id
        {
            get
            {
                return _ability.Id;
            }
            set
            {
                if (Id != value)
                {
                    _ability.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public uint CharacterId
        {
            get
            {
                return _ability.CharacterId;
            }
            set
            {
                if (CharacterId != value)
                {
                    _ability.CharacterId = value;
                    OnPropertyChanged("CharacterId");
                }
            }
        }

        public String HitCheckBase
        {
            get
            {
                return _ability.HitCheckBase;
            }
            set
            {
                if (HitCheckBase != value)
                {
                    _ability.HitCheckBase = value;
                    OnPropertyChanged("HitCheckBase");
                }
            }
        }

        public int HitCheckBonus
        {
            get
            {
                return _ability.HitCheckBonus;
            }
            set
            {
                if (HitCheckBonus != value)
                {
                    _ability.HitCheckBonus = value;
                    OnPropertyChanged("HitCheckBonus");
                }
            }
        }

        public String Effect1EffectType
        {
            get
            {
                return _ability.Effect1EffectType;
            }
            set
            {
                if (Effect1EffectType != value)
                {
                    _ability.Effect1EffectType = value;
                    OnPropertyChanged("Effect1EffectType");
                }
            }
        }

        public int Effect1DiceCount
        {
            get
            {
                return _ability.Effect1DiceCount;
            }
            set
            {
                if (Effect1DiceCount != value)
                {
                    _ability.Effect1DiceCount = value;
                    OnPropertyChanged("Effect1DiceCount");
                }
            }
        }

        public int Effect1DiceSides
        {
            get
            {
                return _ability.Effect1DiceSides;
            }
            set
            {
                if (Effect1DiceSides != value)
                {
                    _ability.Effect1DiceSides = value;
                    OnPropertyChanged("Effect1DiceSides");
                }
            }
        }

        public String Effect1Base
        {
            get
            {
                return _ability.Effect1Base;
            }
            set
            {
                if (Effect1Base != value)
                {
                    _ability.Effect1Base = value;
                    OnPropertyChanged("Effect1Base");
                }
            }
        }

        public int Effect1Bonus
        {
            get
            {
                return _ability.Effect1Bonus;
            }
            set
            {
                if (Effect1Bonus != value)
                {
                    _ability.Effect1Bonus = value;
                    OnPropertyChanged("Effect1Bonus");
                }
            }
        }

        public uint Effect1DamageTypeId
        {
            get
            {
                return _ability.Effect1DamageTypeId;
            }
            set
            {
                if (Effect1DamageTypeId != value)
                {
                    _ability.Effect1DamageTypeId = value;
                    OnPropertyChanged("Effect1DamageTypeId");
                }
            }
        }

        public bool HasSecondaryEffect
        {
            get
            {
                return _ability.HasSecondaryEffect;
            }
            set
            {
                if (HasSecondaryEffect != value)
                {
                    _ability.HasSecondaryEffect = value;
                    OnPropertyChanged("HasSecondaryEffect");
                }
            }
        }

        public String Effect2EffectType
        {
            get
            {
                return _ability.Effect2EffectType;
            }
            set
            {
                if (Effect2EffectType != value)
                {
                    _ability.Effect2EffectType = value;
                    OnPropertyChanged("Effect2EffectType");
                }
            }
        }

        public int Effect2DiceCount
        {
            get
            {
                return _ability.Effect2DiceCount;
            }
            set
            {
                if (Effect2DiceCount != value)
                {
                    _ability.Effect2DiceCount = value;
                    OnPropertyChanged("Effect2DiceCount");
                }
            }
        }

        public int Effect2DiceSides
        {
            get
            {
                return _ability.Effect2DiceSides;
            }
            set
            {
                if (Effect2DiceSides != value)
                {
                    _ability.Effect2DiceSides = value;
                    OnPropertyChanged("Effect2DiceSides");
                }
            }
        }

        public String Effect2Base
        {
            get
            {
                return _ability.Effect2Base;
            }
            set
            {
                if (Effect2Base != value)
                {
                    _ability.Effect2Base = value;
                    OnPropertyChanged("Effect2Base");
                }
            }
        }

        public int Effect2Bonus
        {
            get
            {
                return _ability.Effect2Bonus;
            }
            set
            {
                if (Effect2Bonus != value)
                {
                    _ability.Effect2Bonus = value;
                    OnPropertyChanged("Effect2Bonus");
                }
            }
        }

        public uint Effect2DamageTypeId
        {
            get
            {
                return _ability.Effect2DamageTypeId;
            }
            set
            {
                if (Effect2DamageTypeId != value)
                {
                    _ability.Effect2DamageTypeId = value;
                    OnPropertyChanged("Effect2DamageTypeId");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(String info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
