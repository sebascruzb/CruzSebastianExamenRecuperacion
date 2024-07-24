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
        }
        private Uri imageUri;
        public Uri ImageUri
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
        public ICommand LoadDogImageCommand { get; }
        private async Task LoadDogImage()
        {
            SCDog DogImage = await Service.GetDogImage();
            if (DogImage != null && DogImage.message.Length > 0)
            {
                ImageUri = new Uri(DogImage.message[0]);
                status = DogImage.status;
            }
            else
            {
                ImageUri = new Uri("\"https://images.dog.ceo/breeds/hound-afghan/n02088094_1003.jpg\",\r\n" +
                    "\"https://images.dog.ceo/breeds/hound-afghan/n02088094_10263.jpg\",\r\n" +
                    "\"https://images.dog.ceo/breeds/hound-afghan/n02088094_10715.jpg\",");
                status = "Unknown";
            }
        }
    }
}
