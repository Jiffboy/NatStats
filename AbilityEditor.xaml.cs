using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
                HitCheckBase.Text = ability.HitCheckBase;
                HitCheckBonus.Text = Convert.ToString(ability.HitCheckBonus);
                Effect1Base.Text = ability.Effect1Base;
                Effect1Bonus.Text = Convert.ToString(ability.Effect1Bonus);
                Effect1Count.Text = Convert.ToString(ability.Effect1DiceCount);
                Effect1Sides.Text = Convert.ToString(ability.Effect1DiceSides);
                //ability.Effect1EffectType;
                Effect1DamageType.SelectedIndex = (int)ability.Effect1DamageTypeId - 1;
                Effect2Base.Text = ability.Effect2Base;
                Effect2Bonus.Text = Convert.ToString(ability.Effect2Bonus);
                Effect2Count.Text = Convert.ToString(ability.Effect2DiceCount);
                Effect2Sides.Text = Convert.ToString(ability.Effect2DiceSides);
                //ability.Effect2EffectType;
                Effect2DamageType.SelectedIndex = (int)ability.Effect2DamageTypeId - 1;
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            _abilityVM.CharacterId = _charVM.Id;
            _abilityVM.Name = Name.Text;
            _abilityVM.HitCheckBase = HitCheckBase.Text;
            _abilityVM.HitCheckBonus = Convert.ToInt32(HitCheckBonus.Text);
            _abilityVM.Effect1Base = Effect1Base.Text;
            _abilityVM.Effect1Bonus = Convert.ToInt32(Effect1Bonus.Text);
            _abilityVM.Effect1DiceCount = Convert.ToInt32(Effect1Count.Text);
            _abilityVM.Effect1DiceSides = Convert.ToInt32(Effect1Sides.Text);
            //_abilityVM.Effect1EffectType;
            _abilityVM.Effect1DamageTypeId = (uint)Convert.ToInt32(Effect1DamageType.SelectedIndex) + 1;
            _abilityVM.Effect2Base = Effect2Base.Text;
            _abilityVM.Effect2Bonus = Convert.ToInt32(Effect2Bonus.Text);
            _abilityVM.Effect2DiceCount = Convert.ToInt32(Effect2Count.Text);
            _abilityVM.Effect2DiceSides = Convert.ToInt32(Effect2Sides.Text);
            //_abilityVM.Effect2EffectType;
            _abilityVM.Effect2DamageTypeId = (uint)Convert.ToInt32(Effect2DamageType.SelectedIndex) + 1;

            if (!_charVM.Abilities.Contains(_abilityVM))
            {
                _charVM.Abilities.Add(_abilityVM);
            }
            this.Close();
        }
    }
}
