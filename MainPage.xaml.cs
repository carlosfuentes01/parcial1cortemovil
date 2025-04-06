﻿

namespace llamada
{
    public partial class MainPage : NavigationPage
    {

       
        public MainPage()
        {
            InitializeComponent();
            //Flyout = new Maestro();
            //NavigationPage nav = new(new Detalle())
            //{
            //    BarBackgroundColor = Color.Parse("#9C27B3"),
            //    BarTextColor = Color.Parse("#ffffff")
            //};

            //Detail = nav;


            App.navegacion = this;
            App.navegacion.Navigation.PushAsync(new Detalle());
            App.navegacion.BarBackgroundColor = Color.Parse("#9C27B3");
            App.navegacion.BarTextColor = Color.Parse("white");

            var label = new Label()
            {
                HorizontalOptions = LayoutOptions.Center,
                Text = "Phonebook",
                TextColor = Color.Parse("white"),
                FontAttributes = FontAttributes.Bold,
                Padding = 10,
                FontSize = 25
            };
            //una chida creacion de label
        }


    }

}
