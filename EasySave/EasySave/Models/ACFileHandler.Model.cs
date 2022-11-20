using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace EasySave.Models
{
    //
    // Abstract Class to be implemented to classes handling json and xml files
    // 
    //
    public abstract class ACFileHandler
    {
        public void WriteJsonFile<T>(T obj, string path) where T : JsonSerializable
        {
            try
            {
                // If object is type of CLog, a today date json file is created if not exist 
                if (typeof(T) == typeof(CLog))
                {
                    path = path + DateTime.Today.ToString("dd_MM_yyyy") + ".json";
                    if (!File.Exists(path))
                    {
                        File.WriteAllText(path,"[]");
                    }
                }
                // The json file is overwritten if the object is an existing WorkSave or progression object else the object is written in
                // the file
                List<T> listObject = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(path));

                // If the arg object is not a CLog type and has the same SName attribute from an existing one
                if (typeof(T) != typeof(CLog) && listObject.FindIndex(x => x.SName == obj.SName) != -1)
                {
                    // The object overwrite the old object having the same SName attribute
                    listObject[listObject.FindIndex(x => x.SName == obj.SName)] = obj;
                }
                else
                {
                    // The object is added
                    listObject.Add(obj);
                }
                // The object lists is serialized as a json and written in the specific file
                File.WriteAllText(path, JsonConvert.SerializeObject(listObject, Formatting.Indented));
            }
            catch (Exception e)
            {
                // Writing json file Exception

            }
        }
        public void RemoveJsonFile<T>(T obj, string path) where T : JsonSerializable
        {
            try
            {
                // Remove an oject in a json file using the same attribute SName rewrite the json file
                List<T> listObject = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(path));
                listObject.Remove(listObject[listObject.FindIndex(x => x.SName == obj.SName)]);
                File.WriteAllText(path, JsonConvert.SerializeObject(listObject, Formatting.Indented));
            }
            catch (Exception e)
            {
                // Remove an item in the json file error 
            }
        }
        public void WriteXMLFile(CLog log, string path)
        {
            path = path + DateTime.Today.ToString("dd_MM_yyyy") + ".xml";
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "<?xml version=\"1.0\"?> <ArrayOfCLog> </ArrayOfCLog>");
            }
            // read file
            List<CLog> logs;
            using (var reader = new StreamReader(path))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<CLog>), new XmlRootAttribute("ArrayOfCLog"));
                logs = (List<CLog>)deserializer.Deserialize(reader);
            }

            logs.Add(log);
            XmlSerializer serializer = new XmlSerializer(typeof(List<CLog>));
            TextWriter txtWriter = new StreamWriter(path);

            serializer.Serialize(txtWriter, logs);

            txtWriter.Close();

        }
    }
}
