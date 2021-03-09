using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace APIservice.Data
{
    public class JSONSchemaProvider
    {
        private static ILogger _logger;

        public JSONSchemaProvider(ILoggerFactory logFactory)
        {
            _logger = logFactory.CreateLogger<JSONSchemaProvider>();

        }
        public static void GetClassesFromJson(string _jsonFilePath)
        {
            string myJsonResponse = "";
            try
            {
                StreamReader sr = new StreamReader(_jsonFilePath);
                myJsonResponse = sr.ReadLine();
                while (myJsonResponse != null)
                {
                    Console.WriteLine(myJsonResponse);
                    myJsonResponse += sr.ReadLine();
                }
                sr.Close();
                object myDeserializedClass = JsonConvert.DeserializeObject(myJsonResponse);
            }
            catch (Exception e)
            {
              _logger.LogError("Exception: " + e.Message);
            }
        }
    }
}