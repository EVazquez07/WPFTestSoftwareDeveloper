using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace WPFTestSoftwareDeveloper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void GetData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44301/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Directorio").Result;
            if (response.IsSuccessStatusCode)
            {
                var users = response.Content.ReadAsAsync<IEnumerable<Persona>>().Result;
                //personagrid.ItemsSource = users;
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            //GetData();
        }

        private void crear_Click(object sender, RoutedEventArgs e)
        {
            //localhost:44301/
            if (nombre.Text == String.Empty || nombre.Text == null)
            {
                MessageBox.Show("Favor de ingresar un Nombre");
            } 
            else if(apellidoP.Text == String.Empty || apellidoP.Text == null)
            {
                MessageBox.Show("Favor de ingresar un Apellido Paterno");
            }
            else if (identificador.Text == String.Empty || identificador.Text == null)
            {
                MessageBox.Show("Favor de ingresar un identificador");
            }
            else
            {
                Persona objProduct = new Persona();
                objProduct.nombre = nombre.Text;
                objProduct.apellidoPaterno = apellidoP.Text;
                objProduct.apellidoMaterno = apellidoM.Text;
                objProduct.identificador = identificador.Text;
                string json = JsonConvert.SerializeObject(objProduct);
                var baseAddress = "http://localhost:44301/api/Directorio";
                var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.Method = "POST";
                string parsedContent = json;
                ASCIIEncoding encoding = new ASCIIEncoding();
                Byte[] bytes = encoding.GetBytes(parsedContent);
                Stream newStream = http.GetRequestStream();
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();
                var response = http.GetResponse();
                var stream = response.GetResponseStream();
                //GetData();

                if(stream != null)
                {
                    MessageBox.Show("Se agrego correctamente la persona");
                }    
            }
        }

        private void identificador_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}