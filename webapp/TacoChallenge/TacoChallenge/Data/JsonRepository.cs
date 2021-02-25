using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace TacoChallenge.Data
{
    public class JsonRepository<T> : IRepository<T> where T : class, IEntity
    {
        private static readonly string jsonSrcPath = @"Assets/SampleData.json";
        /*string jsonSrcPath = Configuration.GetConnectionString("JsonDbSrc");
        public JsonRepository(IConfiguration configuration){Configuration = configuration;
        public IConfiguration Configuration { get; }*/

        public List<T> GetAllRecords()
        {
            //TODO: Read json file path into appsettings.json
            //jsonSrcPath = Configuration.GetConnectionString("JsonDbSrc");
            var json = File.ReadAllText(jsonSrcPath);

            // for interface deserealisation
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.Converters.Add(new EntityJsonConverter()); 
            jsonSerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            jsonSerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

            List<T> allRecordsList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(json, jsonSerializerSettings);
            return allRecordsList;
        }

        public T GetItem(int id)
        {
            return GetAllRecords().FirstOrDefault(x => x.Id == id);
        }

        public void Add(T entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}