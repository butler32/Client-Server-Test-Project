using Client_Server_Test_Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Client_Server_Test_Project.ViewModels
{
    public  class CardViewModel : INotifyPropertyChanged
    {
        private Card selectedCard;
        public ObservableCollection<Card> Cards { get; set; }

        public Card SelectedCard
        {
            get { return selectedCard; }

            set 
            {
                selectedCard = value;
                OnPropertyChanged("SelectedCards");
            }
        }

        public CardViewModel()
        {
            Cards = new ObservableCollection<Card>
            {
                new Card { Name = "Банан", Id = 0, ImageName = "C:\\Users\\plus8\\source\\repos\\Client-Server Test Project\\Server Test Project\\Data\\Images\\banana.jpg" },
                new Card { Name = "Апельсин", Id = 1, ImageName = "orange"}
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
