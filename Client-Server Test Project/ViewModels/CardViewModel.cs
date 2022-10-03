using Client_Server_Test_Project.Commands;
using Client_Server_Test_Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Client_Server_Test_Project.Services.Interfaces;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;
using System.Windows;

namespace Client_Server_Test_Project.ViewModels
{
    public  class CardViewModel : INotifyPropertyChanged
    {
        string apiUrl = "https://localhost:7296/Card/";

        private Card selectedCard = new Card();
        private IDialogService dialogService;
        public ObservableCollection<Card> Cards { get; set; }

        public CardViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            Cards = new ObservableCollection<Card>
            {
                new Card { Id = 0, MyBitmapImage = null, Name = "Empty card"}
            };
        }

        public Card SelectedCard
        {
            get { return selectedCard; }

            set
            {
                selectedCard = value;
                OnPropertyChanged("SelectedCard");
            }
        }

        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                    (openCommand = new RelayCommand(async obj =>
                    {
                        try
                        {
                            if (dialogService.OpenFileDialog() == true)
                            {
                                if (dialogService.FilePath.Split('.').Last() == "jpg")
                                {
                                    SelectedCard.ImageByte = File.ReadAllBytes(dialogService.FilePath);
                                    SelectedCard.MyBitmapImage = LoadBitmapImage(SelectedCard.ImageByte);
                                }
                                else
                                    MessageBox.Show("Choose jpg image");
                            }
                        }
                        catch (Exception e)
                        {
                            throw;
                        }
                    }));
            }
        }

        private RelayCommand sortCommand;
        public RelayCommand SortCommand
        {
            get
            {
                return sortCommand ??
                    (sortCommand = new RelayCommand(async obj =>
                    {
                        if (Cards != null)
                        {
                            List<Card> cardsToEdit = new List<Card>();
                            foreach(Card card in Cards)
                            {
                                cardsToEdit.Add(card);
                            }
                            cardsToEdit = cardsToEdit.OrderBy(n => n.Name).ToList();
                            Cards.Clear();
                            foreach(Card card in cardsToEdit)
                            {
                                Cards.Add(card);
                            }
                        }
                    }));
            }
        }

        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                    (editCommand = new RelayCommand(async obj =>
                    {
                        try
                        {
                            if (SelectedCard != null && SelectedCard.Name != "" && SelectedCard.ImageByte != null)
                            {
                                int[] imageInt = SelectedCard.ImageByte.Select(x => (int)x).ToArray();
                                CardToImportExport cardToExport = new CardToImportExport();
                                cardToExport.Id = SelectedCard.Id;
                                cardToExport.Name = SelectedCard.Name;
                                cardToExport.Image = imageInt;

                                using (HttpClient client = new HttpClient())
                                {
                                    string fullUrl = apiUrl + "Update/";
                                    client.Timeout = TimeSpan.FromSeconds(900);
                                    client.DefaultRequestHeaders.Accept.Clear();
                                    string stringToExport = JsonConvert.SerializeObject(cardToExport);
                                    var response = client.PutAsJsonAsync(fullUrl, stringToExport);
                                    response.Wait();
                                }
                            }
                            else
                                MessageBox.Show("Choose card and fill it");
                        }
                        catch (Exception e)
                        {
                            throw;
                        }
                    }));
            }
        }


        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(async obj =>
                    {
                        try
                        {
                            if (SelectedCard != null && SelectedCard.Name != "" && SelectedCard.ImageByte != null)
                            {
                                int[] imageInt = SelectedCard.ImageByte.Select(x => (int)x).ToArray();
                                CardToImportExport cardToExport = new CardToImportExport();
                                cardToExport.Name = SelectedCard.Name;
                                cardToExport.Image = imageInt;

                                using (HttpClient client = new HttpClient())
                                {
                                    string fullUrl = apiUrl + "Create/";
                                    client.Timeout = TimeSpan.FromSeconds(900);
                                    client.DefaultRequestHeaders.Accept.Clear();
                                    string stringToExport = JsonConvert.SerializeObject(cardToExport);
                                    var response = client.PostAsJsonAsync(fullUrl, stringToExport);
                                    response.Wait();
                                }
                            }
                            else
                                MessageBox.Show("Fill name form and choose image");
                        }
                        catch (Exception e)
                        {
                            throw;
                        }
                    }));
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(async obj =>
                    {
                        try
                        {
                            if (SelectedCard.ImageByte != null)
                            {
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                using (HttpClient client = new HttpClient())
                                {
                                    string fullUrl = $"{apiUrl}Delete?id={SelectedCard.Id}";
                                    client.Timeout = TimeSpan.FromSeconds(900);
                                    client.DefaultRequestHeaders.Accept.Clear();
                                    var response = client.DeleteAsync(fullUrl);
                                    response.Wait();
                                }
                            }
                            else
                                MessageBox.Show("Choose card");
                        }
                        catch (Exception e)
                        {
                            throw;
                        }
                    }));
            }
        }

        private RelayCommand addEmptyCardCommand;
        public RelayCommand AddEmptyCardCommand
        {
            get
            {
                return addEmptyCardCommand ??
                    (addEmptyCardCommand = new RelayCommand(async obj =>
                    {
                        Cards.Add(new Card { Id = 0, MyBitmapImage = null, Name = "Empty card"});
                    }));
            }
        }

        private RelayCommand getAllCommand;
        public RelayCommand GetAllCommand
        {
            get
            {
                return getAllCommand ??
                  (getAllCommand = new RelayCommand(async obj =>
                  {
                      try
                      {
                          ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                          using (HttpClient client = new HttpClient())
                          {
                              string fullUrl = $"{apiUrl}GetAll/";
                              client.Timeout = TimeSpan.FromSeconds(900);
                              client.DefaultRequestHeaders.Accept.Clear();
                              var response = client.GetAsync(fullUrl);
                              response.Wait();
                              if (response.Result.Content != null)
                              {
                                  var importedCards = JsonConvert.DeserializeObject<List<CardToImportExport>>(response.Result.Content.ReadAsStringAsync().Result);
                                  Cards.Clear();
                                  if (importedCards != null && importedCards.Count > 0)
                                  {
                                      foreach (var card in importedCards)
                                      {
                                          Card newCard = new Card();
                                          newCard.ImageByte = card.Image.Select(i => (byte)i).ToArray();
                                          newCard.MyBitmapImage = LoadBitmapImage(newCard.ImageByte);
                                          newCard.Id = card.Id;
                                          newCard.Name = card.Name;
                                          Cards.Add(newCard);
                                      }
                                  }
                              }
                              else
                                  MessageBox.Show("Something wrong, can't recive data");
                          }
                      }
                      catch (Exception ex)
                      {
                          throw;
                      }
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private static BitmapImage LoadBitmapImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}
