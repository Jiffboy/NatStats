using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace NatStats
{
    public class CampaignViewModel : INotifyPropertyChanged
    {
        public CampaignViewModel(string name, uint id)
        {
            _name = name;
            _id = id;
        }

        private String _name;
        private uint _id;

        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (Name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public uint Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (Id != value)
                {
                    _id = value;
                    OnPropertyChanged("Id");
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
    }
}
