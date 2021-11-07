using System;
using System.Windows;
using NatStats.Database;
using System.Linq;

namespace NatStats
{
    /// <summary>
    /// Interaction logic for AbilityEditor.xaml
    /// </summary>
    public partial class AbilityEditor : Window
    {
        AbilityViewModel _abilityVM;
        CharacterViewModel _charVM;
        public AbilityEditor(CharacterViewModel charVM, AbilityViewModel ability = null)
        {
            _charVM = charVM;

            if (ability != null)
            {
                this.DataContext = ability;
            }
            else
            {
                this.DataContext = new AbilityViewModel(_charVM.Id, 0);
            }
            _abilityVM = this.DataContext as AbilityViewModel;
            InitializeComponent();
            if(ability != null)
            {
                Name.Text = ability.Name;
                Description.Text = ability.Description;

                if (ability.HasHitCheck)
                {
                    HitCheckCheckBox.IsChecked = true;
                    HitCheckBase.Text = _abilityVM.BaseList.Where(a => a.Id == ability.HitCheckBaseId).FirstOrDefault().Name;
                    HitCheckBonus.Text = Convert.ToString(ability.HitCheckBonus);
                    HitCheckCrit.Text = Convert.ToString(ability.HitCheckCrit);
                }

                if (ability.HasEffect)
                {
                    EffectCheckBox.IsChecked = true;
                    EffectBase.Text = _abilityVM.BaseList.Where(a => a.Id == ability.EffectBaseId).FirstOrDefault().Name;
                    EffectBonus.Text = Convert.ToString(ability.EffectBonus);
                    EffectCount.Text = Convert.ToString(ability.EffectDiceCount);
                    EffectSides.Text = Convert.ToString(ability.EffectDiceSides);
                    if (ability.EffectHeals)
                        EffectDamageType.SelectedItem = "Healing";
                    else
                        EffectDamageType.SelectedIndex = (int)ability.EffectDamageTypeId - 1;
                    CritCheckBox.IsChecked = _abilityVM.EffectCanCrit;
                    CritCount.Text = Convert.ToString(ability.EffectCritDiceCount);
                    CritSides.Text = Convert.ToString(ability.EffectCritDiceSides);
                    CritBonus.Text = Convert.ToString(ability.EffectCritBonus);
                }

                ConditionCheckBox.IsChecked = ability.ConditionId != 0;
                Condition.SelectedIndex = (int)ability.ConditionId;

                if (_abilityVM.HasSavingThrow)
                {
                    SavingThrowCheckBox.IsChecked = true;
                    SavingThrowSave.SelectedItem = _abilityVM.BaseList.Where(a => a.Id == ability.SavingThrowBaseId).FirstOrDefault();
                    if (ability.DCSaveId != 0)
                        SavingThrowBaseMod.SelectedItem = _abilityVM.BaseList.Where(a => a.Id == ability.DCSaveId).FirstOrDefault().Name;
                    else if(ability.FlatDC != 0)
                    {
                        SavingThrowBaseMod.SelectedItem = "Flat Value";
                        SavingThrowFlatDC.Text = Convert.ToString(ability.FlatDC);
                    }
                    PassDamageMod.Text = Convert.ToString(ability.PassDamageMod);
                    PassApplyCondition.IsChecked = _abilityVM.PassApplyCondition;
                    FailDamageMod.Text = Convert.ToString(ability.FailDamageMod);
                    FailApplyCondition.IsChecked = _abilityVM.FailApplyCondition;
                }
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            _abilityVM.CharacterId = _charVM.Id;
            _abilityVM.Name = Name.Text;
            _abilityVM.Description = Description.Text;

            _abilityVM.HasHitCheck = HitCheckCheckBox.IsChecked.GetValueOrDefault();
            if (_abilityVM.HasHitCheck)
            {
                _abilityVM.HitCheckBaseId = ((Skill)HitCheckBase.SelectedItem).Id;
                _abilityVM.HitCheckBonus = Convert.ToInt32(HitCheckBonus.Text);
                _abilityVM.HitCheckCrit = Convert.ToInt32(HitCheckCrit.Text);
            }

            _abilityVM.HasEffect = EffectCheckBox.IsChecked.GetValueOrDefault();
            if (_abilityVM.HasEffect)
            {
                _abilityVM.EffectBaseId = ((Skill)EffectBase.SelectedItem).Id;
                _abilityVM.EffectBonus = Convert.ToInt32(EffectBonus.Text);
                _abilityVM.EffectDiceCount = Convert.ToInt32(EffectCount.Text);
                _abilityVM.EffectDiceSides = Convert.ToInt32(EffectSides.Text);
                if ((String)EffectDamageType.SelectedItem == "Healing")
                {
                    _abilityVM.EffectHeals = true;
                    _abilityVM.EffectDamageTypeId = 0;
                }
                else
                {
                    _abilityVM.EffectHeals = false;
                    _abilityVM.EffectDamageTypeId = (uint)Convert.ToInt32(EffectDamageType.SelectedIndex) + 1;
                }
                _abilityVM.EffectCanCrit = CritCheckBox.IsChecked.GetValueOrDefault();
                _abilityVM.EffectCritDiceCount = Convert.ToInt32(CritCount.Text);
                _abilityVM.EffectCritDiceSides = Convert.ToInt32(CritSides.Text);
                _abilityVM.EffectCritBonus = Convert.ToInt32(CritBonus.Text);
            }

            if (ConditionCheckBox.IsChecked.GetValueOrDefault())
            {
                _abilityVM.ConditionId = (uint)Convert.ToInt32(Condition.SelectedIndex) + 1; ;
            }
            else
            {
                _abilityVM.ConditionId = 0;
            }

            _abilityVM.HasSavingThrow = SavingThrowCheckBox.IsChecked.GetValueOrDefault();
            if (_abilityVM.HasSavingThrow)
            {
                _abilityVM.SavingThrowBaseId = ((Skill)SavingThrowSave.SelectedItem).Id;
                if ((String)SavingThrowBaseMod.SelectedItem == "Casting Mod")
                {
                    _abilityVM.UsesCastingDC = true;
                    _abilityVM.FlatDC = 0;
                    _abilityVM.DCSaveId = 0;
                }
                else if ((String)SavingThrowBaseMod.SelectedItem == "Flat Value")
                {
                    _abilityVM.UsesCastingDC = false;
                    _abilityVM.FlatDC = Convert.ToInt32(SavingThrowFlatDC.Text);
                    _abilityVM.DCSaveId = 0;
                }
                else
                {
                    _abilityVM.UsesCastingDC = false;
                    _abilityVM.FlatDC = 0;
                    _abilityVM.DCSaveId = _abilityVM.BaseList.Where(a => a.Name == (String)SavingThrowBaseMod.SelectedItem).FirstOrDefault().Id;
                }
                _abilityVM.PassDamageMod = Convert.ToDouble(PassDamageMod.Text);
                _abilityVM.PassApplyCondition = PassApplyCondition.IsChecked.GetValueOrDefault();
                _abilityVM.FailDamageMod = Convert.ToDouble(FailDamageMod.Text);
                _abilityVM.FailApplyCondition = FailApplyCondition.IsChecked.GetValueOrDefault();
            }

            if (!_charVM.Abilities.Contains(_abilityVM))
            {
                _charVM.Abilities.Add(_abilityVM);
            }
            this.Close();
        }
    }
}
