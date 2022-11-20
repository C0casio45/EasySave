using System;
using Xunit;
using EasySave.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text;

namespace EasySave_XUnit
{
    public class UnitTestACJsonFileHandler
    {

        public class Mock : ACFileHandler {  }

        [Fact]
        public void TestWriteJsonFile()
        {
            //Creating environnement
            string dir = "./testEasySave/";
            Directory.CreateDirectory(dir);
            using (FileStream fs = File.Create(dir + "MyProgression.json"))
            {
                // Init the array to put json in it  
                Byte[] init = new UTF8Encoding(true).GetBytes("[]");
                fs.Write(init, 0, init.Length);
            }
            //using (FileStream fs = File.Create(dir + "MyLogs.json"))
            //{
            //    // Init the array to put json in it  
            //    Byte[] init = new UTF8Encoding(true).GetBytes("[]");
            //    fs.Write(init, 0, init.Length);
            //}
            CProgression progression = new CProgression("Save 1", "Active", 500 , 400, 300, 200, "C:\\test", "C:\\test2");
            CLog log = new CLog("Save 1", "21:42 23/11/2021", "C:\\test", "C:\\test2", 1000, 0);

            //Execute the methods
            var json = new Mock();
            json.WriteJsonFile(progression, dir + "MyProgression.json");
            json.WriteJsonFile(log, dir);

            //Check for result
            DateTime now = DateTime.Now;
            string prog = File.ReadAllText(dir + "MyProgression.json");
            string logs = File.ReadAllText(dir + DateTime.Now.ToString("dd_MM_yyyy") + ".json");
            Assert.Equal($"[\r\n  {{\r\n    \"SName\": \"Save 1\",\r\n    \"DTimeStamp\": \"{now}\",\r\n    \"SState\": \"Active\",\r\n    \"LTotalFilesToCopy\": 500,\r\n    \"LTotalFilesSize\": 400,\r\n    \"LNumberFilesLeftToDo\": 300,\r\n    \"LSizeFilesLeftToDO\": 200,\r\n    \"SSourceFilePath\": \"C:\\\\test\",\r\n    \"STargetFilePath\": \"C:\\\\test2\"\r\n  }}\r\n]", prog);
            Assert.Equal($"[\r\n  {{\r\n    \"StartTime\": \"21:42 23/11/2021\",\r\n    \"EndTime\": \"{now}\",\r\n    \"SourceFilePath\": \"C:\\\\test\",\r\n    \"TargetFilePath\": \"C:\\\\test2\",\r\n    \"SName\": \"Save 1\",\r\n    \"FileSize\": 1000,\r\n    \"CryptTime\": 0\r\n  }}\r\n]", logs);

            //Wipe test
            File.Delete(dir + "MyProgression.json");
            File.Delete(dir + DateTime.Now.ToString("dd_MM_yyyy") + ".json");
            Directory.Delete(dir);
        }

        [Fact]
        public void TestRemoveJsonFile()
        {
            //Creating environnement
            string dir = "./testEasySave/";
            Directory.CreateDirectory(dir);
            using (FileStream fs = File.Create(dir + "MyProgression.json"))
            {
                // Add json object    
                Byte[] jsonProgress = new UTF8Encoding(true).GetBytes("[{\"SName\": \"Save 1\", \"DTimeStamp\": \"24 / 11 / 2021 00:08:22\", \"SState\": \"Active\", \"LTotalFilesToCopy\": 500,\"LTotalFilesSize\": 400,\"LNumberFilesLeftToDo\": 300,\"LSizeFilesLeftToDO\": 200,\"SSourceFilePath\": \"C:\\test\", \"STargetFilePath\": \"C:\\test2\"}]");
                fs.Write(jsonProgress, 0, jsonProgress.Length);
            }
            using (FileStream fs = File.Create(dir + "MyLogs.json"))
            {
                // Add json object  
                Byte[] jsonLog = new UTF8Encoding(true).GetBytes("[{\"StartTime\": \"21:42 23 / 11 / 2021\",\"EndTime\": \"24 / 11 / 2021 00:08:22\",\"SourceFilePath\": \"C:\\test\",\"TargetFilePath\": \"C:\\test2\",\"SName\": \"Save 1\",\"FileSize\": 1000}]");
                fs.Write(jsonLog, 0, jsonLog.Length);
            }
            CProgression progression = new CProgression("Save 1", "Active", 500, 400, 300, 200, "C:\\test", "C:\\test2");
            CLog log = new CLog("Save 1", "21:42 23/11/2021", "C:\\test", "C:\\test2", 1000, 0);

            //Execute the methods
            var json = new Mock();
            json.RemoveJsonFile(progression, dir + "MyProgression.json");
            json.RemoveJsonFile(log, dir + "MyLogs.json");

            //Check for result "[/r/n]"
            string prog = File.ReadAllText(dir + "MyProgression.json");
            string logs = File.ReadAllText(dir + "MyLogs.json");
            Assert.Equal("[]", prog);
            Assert.Equal("[]", logs);

            //Wipe test
            File.Delete(dir + "MyProgression.json");
            File.Delete(dir + "MyLogs.json");
            Directory.Delete(dir);
        }
    }
}