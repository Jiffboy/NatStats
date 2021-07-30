﻿using System;
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
            mvm.SetSelectedCharacter(character);
        }

        private void NewCharacter_Click(object sender, RoutedEventArgs e)
        {
            Window window = new CharacterEditor(this.DataContext as MainViewModel);
            window.Show();
        }
    }
}
