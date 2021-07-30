using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using NatStats.Database;
using System.Linq;

namespace NatStats
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<CharacterViewModel> Characters { get; private set; }

        public CharacterViewModel SelectedCharacter { get; private set; }

        private DataBaseContext _database;

        private uint _selectedCampaign;

        public MainViewModel()
        {
            _selectedCampaign = 1;
            _database = new DataBaseContext();
            UpdateCampaignCharacters();
        }

        public void SetSelectedCharacter(CharacterViewModel character)
        {
            SelectedCharacter = character;
            OnPropertyChanged("SelectedCharacter");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(String info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }

        private void UpdateCampaignCharacters()
        {
            this.Characters = new ObservableCollection<CharacterViewModel>();
            var characters = _database.Character.Where(c => c.CampaignId == _selectedCampaign).ToList();
            foreach(var character in characters)
            {
                var charClass = _database.Class.Where(c => c.Id == character.ClassId).FirstOrDefault();
                this.Characters.Add(new CharacterViewModel(character.Name, charClass.Name));
            }
        }
    }
}
