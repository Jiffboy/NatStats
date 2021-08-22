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
    }
}
