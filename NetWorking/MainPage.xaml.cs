using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NetWorking
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string classId = button.ClassId;

            switch (classId)
            {
                case "Picture":
                    await Navigation.PushAsync(new PicOD());
                    break;
                default:
                    break;
            }
        }
    }
}