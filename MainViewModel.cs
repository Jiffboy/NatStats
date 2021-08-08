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

        public ObservableCollection<CampaignViewModel> Campaigns { get; private set; }

        public CharacterViewModel SelectedCharacter { get; private set; }

        public CampaignViewModel SelectedCampaign { get; private set; }

        private DataBaseContext _database;

        public MainViewModel()
        {
            _database = new DataBaseContext();
            Campaigns = new ObservableCollection<CampaignViewModel>();
            

            var campaigns = _database.Campaign.ToList();
            foreach (var campaign in campaigns)
            {
                Campaigns.Add(new CampaignViewModel(campaign.Name, campaign.Id));
            }
            SelectedCampaign = Campaigns.First(); // temporary

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
            var characters = _database.Character.Where(c => c.CampaignId == SelectedCampaign.Id).ToList();
            foreach(var character in characters)
            {
                this.Characters.Add(new CharacterViewModel(character.Id));
            }
        }
    }
}
