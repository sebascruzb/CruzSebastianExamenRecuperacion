using CruzSebastianExamenRecuperacion.Models;
using CruzSebastianExamenRecuperacion.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CruzSebastianExamenRecuperacion.ViewModels
{
    public class SCDogViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public SCDogViewModel()
        {
            LoadDogImageCommand = new Command(async () => await LoadDogImage());
            service = new SCDogService();

            Breeds = new List<string> { "hound", "bulldog", "retriever" }; 
            SelectedBreed = Breeds.FirstOrDefault();
        }
        private List<Uri> imageUri;
        public List<Uri> ImageUri
        {
            get { return imageUri; }
            set
            {
                if (imageUri != value)
                {
                    imageUri = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string Status;
        public string status
        {
            get { return Status; }
            set
            {
                if (Status != value)
                {
                    Status = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private SCDogService service;
        public SCDogService Service
        {
            get { return service; }
        }
        public List<string> Breeds { get; set; }
        private string selectedBreed;
        public string SelectedBreed
        {
            get { return selectedBreed; }
            set
            {
                if (selectedBreed != value)
                {
                    selectedBreed = value;
                    NotifyPropertyChanged();
                    LoadDogImageCommand.Execute(null);
                }
            }
        }
        public ICommand LoadDogImageCommand { get; }
        private async Task LoadDogImage()
        {
            string requestUrl = $"https://dog.ceo/api/breed/{SelectedBreed}/images";

            SCDog DogImage = await Service.GetDogImage(requestUrl);
            if (DogImage != null && DogImage.message.Length > 3)
            {
                ImageUri = DogImage.message.Take(3).Select(url => new Uri(url)).ToList();
                status = DogImage.status;
            }
            else
            {
                ImageUri = new List<Uri>
                {
                    new Uri("https://images.dog.ceo/breeds/hound-afghan/n02088094_1003.jpg"),
                    new Uri("https://images.dog.ceo/breeds/hound-afghan/n02088094_10263.jpg"),
                    new Uri("https://images.dog.ceo/breeds/hound-afghan/n02088094_10715.jpg")
                };
                Status = "Unknown";
            }
        }
    }
}
