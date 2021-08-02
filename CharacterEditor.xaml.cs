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
        private uint _id;

        public CharacterEditor(MainViewModel context, uint campaignId, uint characterId = 0)
        {
            this.DataContext = context;
            _campaignId = campaignId;
            _id = characterId;
            InitializeComponent();

            if(characterId != 0)
            {
                var db = new DataBaseContext();
                var character = db.Character.Where(c => c.Id == characterId).FirstOrDefault();
                if(character != null)
                {
                    Name.Text = character.Name;
                    Class.SelectedIndex = (int)(character.ClassId - 1);
                    Strength.Text = character.Strength.ToString();
                    Dexterity.Text = character.Dexterity.ToString();
                    Constitution.Text = character.Constitution.ToString();
                    Intelligence.Text = character.Intelligence.ToString();
                    Wisdom.Text = character.Wisdom.ToString();
                    Charisma.Text = character.Charisma.ToString();
                }
            }
        }

        private void CharacterSave_Click(object sender, RoutedEventArgs e)
        {
            var mvm = this.DataContext as MainViewModel;
            var db = new DataBaseContext();
            Character character;

            if(_id != 0)
            {
                character = db.Character.Where(c => c.Id == _id).FirstOrDefault();
            }
            else
            {
                character = new Character();
            }

            character.Name = Name.Text;
            character.CampaignId = _campaignId;
            character.ClassId = (uint)Class.SelectedIndex + 1;
            character.Strength = Convert.ToInt32(Strength.Text);
            character.Dexterity = Convert.ToInt32(Dexterity.Text);
            character.Constitution = Convert.ToInt32(Constitution.Text);
            character.Intelligence = Convert.ToInt32(Intelligence.Text);
            character.Wisdom = Convert.ToInt32(Wisdom.Text);
            character.Charisma = Convert.ToInt32(Charisma.Text);

            if (_id != 0)
            {
                var charVM = mvm.Characters.Where(c => c.Id == _id).FirstOrDefault();
                charVM.UpdateFromDb(character);
            }
            else
            {
                db.Add(character);
                mvm.Characters.Add(new CharacterViewModel(character));
            }

            db.SaveChanges();
            this.Close();
        }
    }
}
