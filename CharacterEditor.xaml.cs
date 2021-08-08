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
using NatStats.Database;
using System.Linq;

namespace NatStats
{
    /// <summary>
    /// Interaction logic for CharacterEditor.xaml
    /// </summary>
    public partial class CharacterEditor : Window
    {
        private uint _campaignId;
        private MainViewModel _mvm;
        private CharacterViewModel _charVM;
        private bool _newChar;

        public CharacterEditor(MainViewModel context, uint campaignId, uint characterId = 0)
        {
            _mvm = context;
            _newChar = false;
            this.DataContext = _mvm.Characters.Where(c => c.Id == characterId).FirstOrDefault();
            
            if(this.DataContext == null)
            {
                this.DataContext = new CharacterViewModel(characterId);
                _newChar = true;
            }
            _charVM = this.DataContext as CharacterViewModel;
            _campaignId = campaignId;
            InitializeComponent();

            var db = new DataBaseContext();
            var character = db.Character.Where(c => c.Id == characterId).FirstOrDefault();
            if(character != null)
            {
                CharName.Text = character.Name;
                Class.SelectedIndex = (int)(character.ClassId - 1);
                Strength.Text = character.Strength.ToString();
                Dexterity.Text = character.Dexterity.ToString();
                Constitution.Text = character.Constitution.ToString();
                Intelligence.Text = character.Intelligence.ToString();
                Wisdom.Text = character.Wisdom.ToString();
                Charisma.Text = character.Charisma.ToString();
            }
        }

        private void CharacterSave_Click(object sender, RoutedEventArgs e)
        {
            if(_newChar)
            {
                _mvm.Characters.Add(this.DataContext as CharacterViewModel);
            }

            _charVM.Name = CharName.Text;
            //_charVM.CampaignId = _campaignId;
            _charVM.ClassId = (uint)Class.SelectedIndex + 1;
            _charVM.Strength = Convert.ToInt32(Strength.Text);
            _charVM.Dexterity = Convert.ToInt32(Dexterity.Text);
            _charVM.Constitution = Convert.ToInt32(Constitution.Text);
            _charVM.Intelligence = Convert.ToInt32(Intelligence.Text);
            _charVM.Wisdom = Convert.ToInt32(Wisdom.Text);
            _charVM.Charisma = Convert.ToInt32(Charisma.Text);

            _charVM.SaveToDb();

            this.Close();
        }

        private void CharacterCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ProficiencyAdd_Click(object sender, RoutedEventArgs e)
        {
            var skill = Skill_ComboBox.SelectedItem as Skill;
            _charVM.AddProficiency(skill);
        }

        private void ProficiencyRemove_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var skill = button.CommandParameter as Skill;
            _charVM.RemoveProficiency(skill);
        }
    }
}
