using Newtonsoft.Json;
using System.Text;

namespace deskTAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void getALL()
        {
            string URI = "https://localhost:7157/api/Produtos";
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(URI))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var LivrosJsonString = await response.Content.ReadAsStringAsync();
                        dataGridView1.DataSource = "";
                        dataGridView1.DataSource = JsonConvert.DeserializeObject<Produto[]>(value: LivrosJsonString).ToList();

                    }
                    else
                    {
                        label1.Text = "Não foi possível obter o produto : " + response.StatusCode;
                    }
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getALL();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Produto produto = new Produto();
            produto.id = 1;
            produto.descricao= "Teste";
            produto.quantidade = 15;
            produto.valor = 35; 
            string json = JsonConvert.SerializeObject(produto);

            string URI = "https://localhost:7157/api/Produtos";

            using (var client = new HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    ResponseMessage = await httpClient.PostAsync(adaptiveUri, httpConent);
                }
                catch (Exception ex)
                {
                    ResponseMessage.StatusCode = HttpStatusCode.InternalServerError;
                    ResponseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
                }


                var result = await client.PostAsync(URI, content);
                MessageBox.Show(result.StatusCode.ToString());

            }
            getALL();


        }
    }
}