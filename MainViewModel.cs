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

        public ObservableCollection<Skill> Skills { get; private set; }

        public CampaignViewModel SelectedCampaign { get; private set; }

        private CharacterViewModel _selectedCharacter;

        private DataBaseContext _database;

        public MainViewModel()
        {
            _database = new DataBaseContext();
            Campaigns = new ObservableCollection<CampaignViewModel>();
            Skills = new ObservableCollection<Skill>();
            

            var campaigns = _database.Campaign.ToList();
            foreach (var campaign in campaigns)
            {
                Campaigns.Add(new CampaignViewModel(campaign.Name, campaign.Id));
            }
            SelectedCampaign = Campaigns.First(); // temporary

            var skills = _database.Skill.ToList();
            foreach (var skill in skills)
            {
                Skills.Add(skill);
            }

            UpdateCampaignCharacters();
        }

        public CharacterViewModel SelectedCharacter
        {
            get
            {
                return _selectedCharacter;
            }
            set
            {
                if (SelectedCharacter != value)
                {
                    _selectedCharacter = value;
                    OnPropertyChanged("SelectedCharacter");
                }
            }
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
            SelectedCharacter = this.Characters[0];
        }
    }
}
