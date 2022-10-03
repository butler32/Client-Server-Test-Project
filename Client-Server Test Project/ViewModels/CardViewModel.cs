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
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Imaging;
using System.IO;

namespace Client_Server_Test_Project.ViewModels
{
    public  class CardViewModel : INotifyPropertyChanged
    {
        string apiUrl = "https://localhost:7296/Card/";

        private Card selectedCard = new Card();
        private IDialogService dialogService;
        public ObservableCollection<Card> Cards { get; set; }

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
                                SelectedCard.ImageName = dialogService.FilePath;
                            }
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
                            var imageBytes = File.ReadAllBytes(SelectedCard.ImageName);
                            int[] imageInt = imageBytes.Select(x => (int)x).ToArray();
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
                        catch (Exception e)
                        {
                            throw;
                        }
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
                              client.Timeout = TimeSpan.FromSeconds(900);
                              client.DefaultRequestHeaders.Accept.Clear();
                              var response = client.GetAsync("https://localhost:7296/Card/GetAll/");
                              response.Wait();
                              var a = response.Result.Content.ReadAsStringAsync().Result;
                              var b = JsonConvert.DeserializeObject<List<Card>>(a);
                              Cards.Clear();
                              foreach(var card in b)
                              {
                                  Cards.Add(card);
                              }
                              

                          }
                      }
                      catch (Exception ex)
                      {
                          throw;
                      }
                  }));
            }
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

        public CardViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
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
