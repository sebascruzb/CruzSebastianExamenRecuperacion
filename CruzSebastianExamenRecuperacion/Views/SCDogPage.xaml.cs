using CruzSebastianExamenRecuperacion.ViewModels;

namespace CruzSebastianExamenRecuperacion.Views;

public partial class SCDogPage : ContentPage
{
	public SCDogPage()
	{
		InitializeComponent();
        BindingContext = new SCDogViewModel();
    }
}