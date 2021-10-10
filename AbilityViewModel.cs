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
        public ObservableCollection<String> ConditionList { get; private set; }

        private Ability _ability;
        private DataBaseContext _database;

        public AbilityViewModel(uint characterId, uint abilityId)
        {
            _database = new DataBaseContext();
            BaseList = new ObservableCollection<string>();
            DamageTypeList = new ObservableCollection<string>();
            ConditionList = new ObservableCollection<string>();

            _ability = _database.Ability.Where(c => c.Id == abilityId).FirstOrDefault();
            if (_ability == null)
            {
                _ability = new Ability { };
            }

            foreach (var skill in _database.Skill.Where(s => s.Name == s.Base).ToList())
            {
                BaseList.Add(skill.Name);
            }

            foreach (var damage in _database.DamageType.ToList())
            {
                DamageTypeList.Add(damage.Name);
            }

            foreach (var condition in _database.Condition.ToList())
            {
                ConditionList.Add(condition.Name);
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

        public string Name
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

        public string Description
        {
            get
            {
                return _ability.Description;
            }
            set
            {
                if (Description != value)
                {
                    _ability.Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public bool HasHitCheck
        {
            get
            {
                return _ability.HasHitCheck;
            }
            set
            {
                if (HasHitCheck != value)
                {
                    _ability.HasHitCheck = value;
                    OnPropertyChanged("HasHitCheck");
                }
            }
        }

        public string HitCheckBase
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

        public int HitCheckCrit
        {
            get
            {
                return _ability.HitCheckCrit;
            }
            set
            {
                if (HitCheckCrit != value)
                {
                    _ability.HitCheckCrit = value;
                    OnPropertyChanged("HitCheckCrit");
                }
            }
        }

        public bool HasEffect
        {
            get
            {
                return _ability.HasEffect;
            }
            set
            {
                if (HasEffect != value)
                {
                    _ability.HasEffect = value;
                    OnPropertyChanged("HasEffect");
                }
            }
        }

        public int EffectDiceCount
        {
            get
            {
                return _ability.EffectDiceCount;
            }
            set
            {
                if (EffectDiceCount != value)
                {
                    _ability.EffectDiceCount = value;
                    OnPropertyChanged("EffectDiceCount");
                }
            }
        }

        public int EffectDiceSides
        {
            get
            {
                return _ability.EffectDiceSides;
            }
            set
            {
                if (EffectDiceSides != value)
                {
                    _ability.EffectDiceSides = value;
                    OnPropertyChanged("EffectDiceSides");
                }
            }
        }

        public string EffectBase
        {
            get
            {
                return _ability.EffectBase;
            }
            set
            {
                if (EffectBase != value)
                {
                    _ability.EffectBase = value;
                    OnPropertyChanged("EffectBase");
                }
            }
        }

        public int EffectBonus
        {
            get
            {
                return _ability.EffectBonus;
            }
            set
            {
                if (EffectBonus != value)
                {
                    _ability.EffectBonus = value;
                    OnPropertyChanged("EffectBonus");
                }
            }
        }

        public uint EffectDamageTypeId
        {
            get
            {
                return _ability.EffectDamageTypeId;
            }
            set
            {
                if (EffectDamageTypeId != value)
                {
                    _ability.EffectDamageTypeId = value;
                    OnPropertyChanged("EffectDamageTypeId");
                }
            }
        }

        public bool EffectCanCrit
        {
            get
            {
                return _ability.EffectCanCrit;
            }
            set
            {
                if (EffectCanCrit != value)
                {
                    _ability.EffectCanCrit = value;
                    OnPropertyChanged("EffectCanCrit");
                }
            }
        }

        public int EffectCritDiceCount
        {
            get
            {
                return _ability.EffectCritDiceCount;
            }
            set
            {
                if (EffectCritDiceCount != value)
                {
                    _ability.EffectCritDiceCount = value;
                    OnPropertyChanged("EffectCritDiceCount");
                }
            }
        }

        public int EffectCritDiceSides
        {
            get
            {
                return _ability.EffectCritDiceSides;
            }
            set
            {
                if (EffectCritDiceSides != value)
                {
                    _ability.EffectCritDiceSides = value;
                    OnPropertyChanged("EffectCritDiceSides");
                }
            }
        }

        public int EffectCritBonus
        {
            get
            {
                return _ability.EffectCritBonus;
            }
            set
            {
                if (EffectCritBonus != value)
                {
                    _ability.EffectCritBonus = value;
                    OnPropertyChanged("EffectCritBonus");
                }
            }
        }

        public uint ConditionId
        {
            get
            {
                return _ability.ConditionId;
            }
            set
            {
                if (ConditionId != value)
                {
                    _ability.ConditionId = value;
                    OnPropertyChanged("ConditionId");
                }
            }
        }

        public bool HasSavingThrow
        {
            get
            {
                return _ability.HasSavingThrow;
            }
            set
            {
                if (HasSavingThrow != value)
                {
                    _ability.HasSavingThrow = value;
                    OnPropertyChanged("HasSavingThrow");
                }
            }
        }

        public double PassDamageMod
        {
            get
            {
                return _ability.PassDamageMod;
            }
            set
            {
                if (PassDamageMod != value)
                {
                    _ability.PassDamageMod = value;
                    OnPropertyChanged("PassDamageMod");
                }
            }
        }

        public bool PassApplyCondition
        {
            get
            {
                return _ability.PassApplyCondition;
            }
            set
            {
                if (PassApplyCondition != value)
                {
                    _ability.PassApplyCondition = value;
                    OnPropertyChanged("PassApplyCondition");
                }
            }
        }

        public double FailDamageMod
        {
            get
            {
                return _ability.FailDamageMod;
            }
            set
            {
                if (FailDamageMod != value)
                {
                    _ability.FailDamageMod = value;
                    OnPropertyChanged("FailDamageMod");
                }
            }
        }

        public bool FailApplyCondition
        {
            get
            {
                return _ability.FailApplyCondition;
            }
            set
            {
                if (FailApplyCondition != value)
                {
                    _ability.FailApplyCondition = value;
                    OnPropertyChanged("FailApplyCondition");
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
