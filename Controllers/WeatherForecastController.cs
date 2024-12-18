using db.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace db.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<string> Get()
        {

            Zip();
            return Enumerable.Range(1, 1).Select(index => "created");
            

            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            string folderNmae = "Database";
            string folderPath = Path.Combine(currentDir, folderNmae);

            Console.WriteLine(folderPath);



            return Enumerable.Range(1, 1).Select(index => "created");


     
        }


        private void Zip()
        {
           // var degree = 3; // Grau da árvore B
          //  var bTree = new BTree(degree);

            // Inserindo dados na árvore
           /* bTree.Insert("Value1");
            bTree.Insert("Value2");
            bTree.Insert("Value3");
            bTree.Insert("Value4");
            bTree.Insert("Value5");*/

            // Simulação de reinício do programa
          //  Console.WriteLine("Carregando a árvore após reiniciar...");
           // var newBTree = new BTree(degree); // Carrega automaticamente os dados do arquivo

            // Teste após reiniciar
            // O estado da árvore será restaurado e as operações podem continuar
           // newBTree.Insert("Value6");
          //  newBTree.Insert("Value7");

            var sTree = new SearchTree();
            /*  sTree.Insert("value tal");
              sTree.Insert("value tal");
              sTree.Insert("value tal");
              sTree.Insert("value tal");
              sTree.Insert("value tal");
              sTree.Insert("value tal");*/

            // sTree.SearchById("8691aebe-768e-4d93-8558-ad8f7f475e42");


            //sTree.GetAll();
            sTree.GetKeys();
        }


        private void WriteFile(string path, BTree bTree)
        {

              //bTree.SaveToFile(path);
           // var tree = BTree.LoadFromFile(path, 3);
          
           // string text = "Hello world";

           // var sw = new StreamWriter(path + @"\file1.json");

              //sw.Write(bTree.ToString();

           // sw.Close();

        }
    }
}
