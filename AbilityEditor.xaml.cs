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
        public AbilityEditor(CharacterViewModel charVM, uint abilityId = 0)
        {
            _charVM = charVM;
            this.DataContext = _charVM.Abilities.Where(c => c.Id == abilityId).FirstOrDefault();

            if (this.DataContext == null)
            {
                this.DataContext = new AbilityViewModel(_charVM.Id, abilityId);
            }
            _abilityVM = this.DataContext as AbilityViewModel;
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _abilityVM.SaveToDb();
        }
    }
}
