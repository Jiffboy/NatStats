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
    }
}
