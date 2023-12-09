using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Forms.Internals.Profile;

namespace NetWorking
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PicOD : ContentPage
    {
        private readonly static string My_API = "efNjLdEED5GcZdRvaEtObuFR91CEdhhfumCuv9dL";
        static readonly HttpClient client = new HttpClient();
        public PicOD()
        {
            InitializeComponent();
        }
        public async void MakeRequest()
        {
            string url = $"https://api.nasa.gov/planetary/apod?api_key={My_API}";

            DateTime dtNeedDay = dateP.Date;
            DateTime dtStartDay = start_date.Date;
            DateTime dtEndDay = end_date.Date;
            string start_datePdatePicked = $"{dtStartDay.Year}-{dtStartDay.Month}-{dtStartDay.Day}";//= start_date.te;
            string end_datePdatePicked = $"{dtEndDay.Year}-{dtEndDay.Month}-{dtEndDay.Day}";
            string datePicked = $"{dtNeedDay.Year}-{dtNeedDay.Month}-{dtNeedDay.Day}";

            if (chkbx.IsChecked)    // посылаем период
                url += $"&start_date={start_datePdatePicked}&end_date={end_datePdatePicked}";
            else                    // посылаем один день
                url += $"&date={datePicked}";

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();


                string myPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "pictureoftheday.txt");
                File.WriteAllText(myPath, responseBody);


                string json = File.ReadAllText(myPath);
                List<Amod> lists = new List<Amod>();
                try
                {
                    lists = JsonConvert.DeserializeObject<List<Amod>>(json);
                }
                catch
                {
                    Amod amod = JsonConvert.DeserializeObject<Amod>(json);
                    lists.Add(amod);
                }
                taskList.ItemsSource = lists;
            }
            catch (HttpRequestException e) { }
        }


        private void Button_Clicked(object sender, EventArgs e) => MakeRequest();

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (chkbx.IsChecked)
            {
                labeltxt.Text = "Укажите нужный период";
                dateP.IsVisible = false;
                start_date.IsVisible = true;
                end_date.IsVisible = true;
            }
            else
            {
                labeltxt.Text = "Укажите нужный день";
                dateP.IsVisible = true;
                start_date.IsVisible = false;
                end_date.IsVisible = false;
            }
        }
    }
}
    
