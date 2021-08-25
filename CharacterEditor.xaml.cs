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
        private MainViewModel _mvm;
        private CharacterViewModel _charVM;
        private bool _newChar;

        private List<Skill> _addedSkills;
        private List<Skill> _removedSkills;

        public CharacterEditor(MainViewModel context, uint campaignId, uint characterId = 0)
        {
            _mvm = context;
            _newChar = false;
            this.DataContext = _mvm.Characters.Where(c => c.Id == characterId).FirstOrDefault();
            _addedSkills = new List<Skill>();
            _removedSkills = new List<Skill>();
            
            if(this.DataContext == null)
            {
                this.DataContext = new CharacterViewModel(campaignId, characterId);
                _newChar = true;
            }
            _charVM = this.DataContext as CharacterViewModel;
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
                Level.Text = character.Level.ToString();
                ProfBonus.Text = character.ProficiencyBonus.ToString();
            }
        }

        private void CharacterSave_Click(object sender, RoutedEventArgs e)
        {
            if(_newChar)
            {
                _mvm.Characters.Add(this.DataContext as CharacterViewModel);
            }

            _charVM.Name = CharName.Text;
            _charVM.ClassId = (uint)Class.SelectedIndex + 1;
            _charVM.Strength = Convert.ToInt32(Strength.Text);
            _charVM.Dexterity = Convert.ToInt32(Dexterity.Text);
            _charVM.Constitution = Convert.ToInt32(Constitution.Text);
            _charVM.Intelligence = Convert.ToInt32(Intelligence.Text);
            _charVM.Wisdom = Convert.ToInt32(Wisdom.Text);
            _charVM.Charisma = Convert.ToInt32(Charisma.Text);
            _charVM.Level = Convert.ToInt32(Level.Text);
            _charVM.ProficiencyBonus = Convert.ToInt32(ProfBonus.Text);

            _charVM.SaveToDb();
            _addedSkills.Clear();
            _removedSkills.Clear();
            _mvm.SelectedCharacter = _charVM;

            this.Close();
        }

        private void CharacterCancel_Click(object sender, RoutedEventArgs e)
        {
            CancelChanges();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CancelChanges();
        }

        private void CancelChanges()
        {
            // Undo changes made to the collections for viewing skills/proficiencies
            foreach (var skill in _addedSkills)
            {
                _charVM.RemoveProficiency(skill);
            }

            foreach (var skill in _removedSkills)
            {
                _charVM.AddProficiency(skill);
            }
        }

        private void ProficiencyAdd_Click(object sender, RoutedEventArgs e)
        {
            var skill = Skill_ComboBox.SelectedItem as Skill;
            _charVM.AddProficiency(skill);
            _addedSkills.Add(skill);
        }

        private void ProficiencyRemove_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var skill = button.CommandParameter as Skill;
            _charVM.RemoveProficiency(skill);
            _removedSkills.Add(skill);
        }
    }
}
