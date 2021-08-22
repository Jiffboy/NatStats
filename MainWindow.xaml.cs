using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NatStats.Database;

namespace NatStats
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new MainViewModel();
            InitializeComponent();
        }

        private void CharacterTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainViewModel mvm = DataContext as MainViewModel;
            CharacterViewModel character = e.AddedItems[0] as CharacterViewModel;
            mvm.SelectedCharacter = character;
        }

        private void NewCharacter_Click(object sender, RoutedEventArgs e)
        {
            var mvm = this.DataContext as MainViewModel;
            Window window = new CharacterEditor(this.DataContext as MainViewModel, mvm.SelectedCampaign.Id);
            window.Show();
        }

        private void CharacterTab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if( e.ChangedButton == MouseButton.Right)
            {
                var charVM = ((sender as TextBlock).DataContext as CharacterViewModel);
                var mvm = this.DataContext as MainViewModel;
                Window window = new CharacterEditor(this.DataContext as MainViewModel, mvm.SelectedCampaign.Id, charVM.Id);
                window.Show();
            }
        }

        private void SkillButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel mvm = DataContext as MainViewModel;
            Button button = sender as Button;
            string btnText = (button.Content as TextBlock).Text;

            string roll = btnText.Split("\n")[0];
            string mod = btnText.Split("\n")[1];

            CurrRollName.Text = roll;
            mvm.CurrRollCharacter = mvm.SelectedCharacter;

            if (mod.Contains("+"))
            {
                CurrRollModifierSign.Text = "+";
                CurrRollModifier.Text = mod.Replace("+", "");
            }
            else if(mod.Contains("-"))
            {
                CurrRollModifierSign.Text = "-";
                CurrRollModifier.Text = mod.Replace("-", "");
            }
            else
            {
                CurrRollModifierSign.Text = "";
                CurrRollModifier.Text = "";
            }

            FocusRollbox();
        }

        private void FocusRollbox()
        {
            MainViewModel mvm = DataContext as MainViewModel;
            if(mvm.RollEntryDice)
            {
                CurrRollValue.Focus();
            }
            else if(mvm.RollEntryTotal)
            {
                CurrRollTotal.Focus();
            }
        }

        private void RollEntryDice_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel mvm = this.DataContext as MainViewModel;
            mvm.RollEntryDice = true;
        }

        private void RollEntryTotal_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel mvm = this.DataContext as MainViewModel;
            mvm.RollEntryTotal = false;
        }

        private void Roll_Click(object sender, RoutedEventArgs e)
        {
            SaveRoll();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                SaveRoll();
            }
        }

        private void SaveRoll()
        {
            var db = new DataBaseContext();
            MainViewModel mvm = this.DataContext as MainViewModel;
            int roll = 0;
            int mod = 0;
            int bonusMod = 0;
            int total = 0;
            int sides = 20; // Temporary

            string name = CurrRollName.Text;

            if (CurrRollModifier.Text != "")
            {
                if (CurrRollModifierSign.Text == "+")
                {
                    mod += Convert.ToInt32(CurrRollModifier.Text);
                }
                if (CurrRollModifierSign.Text == "-")
                {
                    mod -= Convert.ToInt32(CurrRollModifier.Text);
                }
            }

            if (CurrRollBonus.Text != "")
            {
                bonusMod += Convert.ToInt32(CurrRollBonus.Text);
            }

            if (CurrRollValue.Text != "")
            {
                roll = Convert.ToInt32(CurrRollValue.Text);
                total = roll + mod + bonusMod;
            }
            else if (CurrRollTotal.Text != "")
            {
                total = Convert.ToInt32(CurrRollTotal.Text);
                roll = total - mod - bonusMod;
            }

            var result = db.Add(new RollHeader { CharacterId = mvm.CurrRollCharacter.Id, Name = name, FinalValue = total, RollType = "Check" });
            // Save to assign an ID
            db.SaveChanges();
            RollHeader header = result.Entity;

            db.Add(new Roll { HeaderId = header.Id, DiceRoll = roll, Modifier = mod, BonusModifier = bonusMod, DiceSides = sides, Total = total, IsFinal = true });
            db.SaveChanges();

            CurrRollValue.Text = "";
            CurrRollTotal.Text = "";
            CurrRollBonus.Text = "";
        }
    }
}
