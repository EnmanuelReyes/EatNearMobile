using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace EatNearMobile.View
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public async void Login(object sender, EventArgs e)
        {
           
            string token = await new RestService().Login(Username.Text, Password.Text);

            if (token == null ){
                await DisplayAlert("Datos incorrectos", "Favor intentar con datos correctos.", "OK");
            } else
            {
                RestService.token = token;
                Device.BeginInvokeOnMainThread(() => ((App)App.Current).LoginSuccess());
            }
        }
    }
}
