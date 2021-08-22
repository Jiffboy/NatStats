using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using NatStats.Database;
using System.Linq;

enum RollEntryMode
{ 
    Dice,
    Total
}

namespace NatStats
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<CharacterViewModel> Characters { get; private set; }

        public ObservableCollection<CampaignViewModel> Campaigns { get; private set; }

        public ObservableCollection<Skill> Skills { get; private set; }

        public ObservableCollection<Skill> SavingThrows { get; private set; }

        public CampaignViewModel SelectedCampaign { get; private set; }

        private RollEntryMode _rollEntryMode;

        private CharacterViewModel _selectedCharacter;

        private CharacterViewModel _currRollCharacter;

        private DataBaseContext _database;

        public CharacterViewModel SelectedCharacter
        {
            get
            {
                return _selectedCharacter;
            }
            set
            {
                _selectedCharacter = value;
                OnPropertyChanged("SelectedCharacter");
            }
        }

        public CharacterViewModel CurrRollCharacter
        {
            get
            {
                return _currRollCharacter;
            }
            set
            {
                _currRollCharacter = value;
                OnPropertyChanged("CurrRollCharacter");
            }
        }

        public bool RollEntryDice
        {
            get
            {
                return _rollEntryMode == RollEntryMode.Dice;
            }
            set
            {
                _rollEntryMode = RollEntryMode.Dice;
                OnPropertyChanged("RollEntryDice");
                OnPropertyChanged("RollEntryTotal");
            }
        }

        public bool RollEntryTotal
        {
            get
            {
                return _rollEntryMode == RollEntryMode.Total;
            }
            set
            {
                _rollEntryMode = RollEntryMode.Total;
                OnPropertyChanged("RollEntryTotal");
                OnPropertyChanged("RollEntryDice");
            }
        }


        public MainViewModel()
        {
            _database = new DataBaseContext();
            Campaigns = new ObservableCollection<CampaignViewModel>();
            Skills = new ObservableCollection<Skill>();
            SavingThrows = new ObservableCollection<Skill>();

            _rollEntryMode = RollEntryMode.Dice;

            var campaigns = _database.Campaign.ToList();
            foreach (var campaign in campaigns)
            {
                Campaigns.Add(new CampaignViewModel(campaign.Name, campaign.Id));
            }
            SelectedCampaign = Campaigns.First(); // temporary

            var skills = _database.Skill.ToList();
            foreach (var skill in skills)
            {
                if (skill.Name == skill.Base)
                {
                    SavingThrows.Add(skill);
                }
                else
                {
                    Skills.Add(skill);
                }
            }

            UpdateCampaignCharacters();
        }

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
                this.Characters.Add(new CharacterViewModel(SelectedCampaign.Id, character.Id));
            }

            if (this.Characters.Count > 0)
            {
                SelectedCharacter = this.Characters[0];
            }
        }
    }
}
