using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Schema;
using Gaston.Pages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Gaston.Models
{
    public class Level
    {
        public List<Example> Examples;
        public int LevelScore;

        public static Level GetLevelFromJson()
        {
            List<Example> examples = new List<Example>();
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;

            #region MultipleChoiceRegion

            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.MultipleChoice.json");

            if (stream != null)
                using (var reader = new JsonTextReader(new StreamReader(stream)))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    JArray jArray = (JArray) serializer.Deserialize(reader);

                    foreach (var item in jArray)
                    {
                        examples.Add(new MultipleChoiceExample(
                            new Verb(item["answers"].ToObject<Dictionary<string, bool>>()),
                            item["sentence"].Value<string>()
                        ));
                    }
                }
            #endregion

            #region FillInTheBlankRegion

            stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.FillInTheBlank.json");
            
            if (stream != null)
                using (var reader = new JsonTextReader(new StreamReader(stream)))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    JArray jArray = (JArray) serializer.Deserialize(reader);
                   
                    foreach (var item in jArray)
                    {
                        var sentence = item["sentence"].Value<string>();
                        var letters = item["letters"].ToObject<List<Letter>>();
                        var word = item["word"].Value<string>();
                        examples.Add(new FillBlankExample(
                            item["sentence"].Value<string>(),
                            item["word"].Value<string>(),
                            item["letters"].ToObject<List<Letter>>()
                        ));
                    }
                }


            #endregion

            Level level = new Level(examples);
            return level;
        }

        public Level(List<Example> examples)
        {
            Examples = examples;
        }
    }
}