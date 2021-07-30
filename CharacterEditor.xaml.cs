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

namespace NatStats
{
    /// <summary>
    /// Interaction logic for CharacterEditor.xaml
    /// </summary>
    public partial class CharacterEditor : Window
    {
        public CharacterEditor(MainViewModel context)
        {
            this.DataContext = context;
            InitializeComponent();
        }

        private void CharacterSave_Click(object sender, RoutedEventArgs e)
        {
            var mvm = this.DataContext as MainViewModel;
            var db = new DataBaseContext();
            var character = new Character();

            character.Name = Name.Text;
            character.CampaignId = mvm.SelectedCampaign.Id;
            character.ClassId = (uint)Class.SelectedIndex + 1;
            character.Strength = Convert.ToInt32(Strength.Text);
            character.Dexterity = Convert.ToInt32(Dexterity.Text);
            character.Constitution = Convert.ToInt32(Constitution.Text);
            character.Intelligence = Convert.ToInt32(Intelligence.Text);
            character.Wisdom = Convert.ToInt32(Wisdom.Text);
            character.Charisma = Convert.ToInt32(Charisma.Text);
            db.Add(character);
            db.SaveChanges();
            
            mvm.Characters.Add(new CharacterViewModel(character));
            this.Close();
        }
    }
}
