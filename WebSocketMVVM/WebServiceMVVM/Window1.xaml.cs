using Newtonsoft.Json;
using System.Windows;
using System.Net.Http;
using System;
using System.Text;

namespace WebServiceMVVM
{
    public partial class Window1 : Window
    {
        private static readonly HttpClient client = new HttpClient();
        public Window1()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string id = TB_ID.Text;
            TB_ID.Text = "";

            string contacte = await client.GetStringAsync("http://localhost:49781/api/contacte/" + id);

            Console.WriteLine(contacte);

            contacte = "[" + contacte + "]";

            var contacteData = JsonConvert.DeserializeObject<dynamic>(contacte);

            DG_Contactes.ItemsSource = contacteData;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string patro = TB_Patro.Text;
            TB_Patro.Text = "";

            string contacte = await client.GetStringAsync("http://localhost:49781/api/contactes/" + patro);

            contacte = "[" + contacte + "]";

            var contacteData = JsonConvert.DeserializeObject<dynamic>(contacte);

            DG_Contactes.ItemsSource = contacteData;
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string telefon = TB_Telefon.Text;
            TB_Telefon.Text = "";

            string contacte = await client.GetStringAsync("http://localhost:49781/api/contactes/telefon/" + telefon);

            Console.WriteLine(contacte);

            var contacteData = JsonConvert.DeserializeObject<dynamic>(contacte);

            DG_Contactes.ItemsSource = contacteData;
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string nom = TB_Nom.Text;
            string cognoms = TB_Cognom.Text;

            TB_Nom.Text = "";
            TB_Cognom.Text = "";


            string myJson =
                "{" +
                    "'nom': '"+ nom +"'," +
                    "'cognoms': '"+ cognoms +"'" +
                "}";
            var response = await client.PostAsync(
                "http://localhost:49781/api/contacte",
                new StringContent(myJson, Encoding.UTF8, "application/json"));
        }

        private async void Button_Click_5(object sender, RoutedEventArgs e)
        {
            string nom = TB_UNom.Text;
            string cognoms = TB_UCognom.Text;
            string idcontacte = TB_UID.Text;

            TB_UNom.Text = "";
            TB_UCognom.Text = "";
            TB_UID.Text = "";


            string myJson =
                "{" +
                    "'nom': '" + nom + "'," +
                    "'cognoms': '" + cognoms + "'" +
                "}";
            var response = await client.PutAsync(
                "http://localhost:49781/api/contacte/" + idcontacte,
                new StringContent(myJson, Encoding.UTF8, "application/json"));
        }

        private async void Button_Click_6(object sender, RoutedEventArgs e)
        {
            string idcontacte = TB_DID.Text;
            TB_DID.Text = "";

            var response = await client.DeleteAsync("http://localhost:49781/api/contactedel/" + idcontacte);

        }

        private async void Button_Click_7(object sender, RoutedEventArgs e)
        {
            string ID = TB_UID_Tel.Text;
            string telefon = TB_UTel.Text;
            string tipus = TB_UTipus.Text;
            string contacteID = TB_UcontacteID.Text;

            TB_UID_Tel.Text = "";
            TB_UTel.Text = "";
            TB_UTipus.Text = "";
            TB_UcontacteID.Text = "";


            string myJson =
                "{" +
                    "'telefon': '" + telefon + "'," +
                    "'tipus': '" + tipus + "'," +
                    "'contacteId': '" + contacteID + "'" +
                "}";
            var response = await client.PutAsync(
                "http://localhost:49781/api/telefon/" + ID,
                new StringContent(myJson, Encoding.UTF8, "application/json"));
        }

        private async void Button_Click_8(object sender, RoutedEventArgs e)
        {
            string telefon = TB_ITel.Text;
            string tipus = TB_ITipus.Text;
            string contacteID = TB_IcontacteID.Text;

            TB_ITel.Text = "";
            TB_ITipus.Text = "";
            TB_IcontacteID.Text = "";


            string myJson =
                "{" +
                    "'telefon': '" + telefon + "'," +
                    "'tipus': '" + tipus + "'," +
                    "'contacteId': '" + contacteID + "'" +
                "}";
            var response = await client.PostAsync(
                "http://localhost:49781/api/telefon",
                new StringContent(myJson, Encoding.UTF8, "application/json"));
        }

        private async void Button_Click_9(object sender, RoutedEventArgs e)
        {
            string idtelefon = TB_DID_Tel.Text;
            TB_DID.Text = "";

            var response = await client.DeleteAsync("http://localhost:49781/api/telefondel/" + idtelefon);
        }
    }
}
