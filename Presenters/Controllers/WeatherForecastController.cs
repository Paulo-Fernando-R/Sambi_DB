using db.Index.Enums;
using db.Models;
using Microsoft.AspNetCore.Mvc;

namespace db.Presenters.Controllers
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

            string json = @"{ 'Name': 'Alice', 'Age': 25 }";
            string json2 = @"{ 'Name': 'Bob', 'Age': 30 }";
            string json3 = @"{ 'Name': 'Charlie', 'Age': 35 }";

           // var sTree = new SearchTree();
            /* sTree.Insert(json);
             sTree.Insert(json2);
             sTree.Insert(json3);
             sTree.Insert(json);
             sTree.Insert(json2);
             sTree.Insert(json3);*/

            // sTree.SearchById("8691aebe-768e-4d93-8558-ad8f7f475e42");


            //sTree.GetAll();
            //sTree.GetKeys();
           // sTree.SearchByProperty("Name", "Alice", OperatorsEnum.Equal);

            //  sTree.DeleteById("10ebad1c-3c5d-4f14-a8a7-c5dde1e53828");
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
