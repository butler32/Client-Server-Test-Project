using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Client_Server_Test_Project.Models
{
    public  class Card : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private byte[] imageByte;
        private BitmapImage myBitmapImage;

        public BitmapImage MyBitmapImage
        {
            get { return myBitmapImage; }
            set
            {
                myBitmapImage = value;
                OnPropertyChanged("MyBitmapImage");
            }
        }

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public byte[] ImageByte
        {
            get { return imageByte; }
            set
            {
                imageByte = value;
                OnPropertyChanged("ImageName");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
