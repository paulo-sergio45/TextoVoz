using TextoVoz.Interfaces;
using TextoVoz.Mvvm.Models;
using TextoVoz.Mvvm.ViewModels;

namespace TextoVoz.Mvvm.Views;

public partial class HomeView : ContentPage
{
  

    public HomeView(HomeViewModel homeViewModel)
    {
        InitializeComponent();
        BindingContext = homeViewModel;
    }







}