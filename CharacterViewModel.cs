using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace NatStats
{
    public class CharacterViewModel : INotifyPropertyChanged
    {
        public CharacterViewModel(string name, string clss)
        {
            _name = name;
            _class = clss;
        }

        private String _name;
        private String _class;

        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                if(Name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public String Class
        {
            get
            {
                return _class ;
            }
            set
            {
                if (Class != value)
                {
                    _class = value;
                    OnPropertyChanged("Class");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(String info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
