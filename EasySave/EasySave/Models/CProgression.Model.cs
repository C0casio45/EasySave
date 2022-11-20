using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace EasySave.Models
{
    public class CProgression : JsonSerializable
    {
        public string SName { get; set; }
        public string DTimeStamp { get; }
        public string SState { get; set; }
        public long LTotalFilesToCopy { get; set; }
        public long LTotalFilesSize { get; set; }
        public long LNumberFilesLeftToDo { get; set; }
        public long LSizeFilesLeftToDO { get; set; }
        public string SSourceFilePath { get; set; }
        public string STargetFilePath { get; set; }
        
        public CProgression(string sSaveName, string sSaveState, long lSaveTotalFilesToCopy, long lSaveTotalFilesSize, long lSaveNumberFilesLeftToDo, long lSizeFilesLeftToDo, string sSaveSourceFilePath, string sSaveTargetFilePath)
        {
            //Set all attributes by using the constructor
            SName = sSaveName;
            DTimeStamp = DateTime.Now.ToString();
            SState = sSaveState;
            LTotalFilesToCopy = lSaveTotalFilesToCopy;
            LTotalFilesSize = lSaveTotalFilesSize;
            LNumberFilesLeftToDo = lSaveNumberFilesLeftToDo;
            LSizeFilesLeftToDO = lSizeFilesLeftToDo;
            SSourceFilePath = sSaveSourceFilePath;
            STargetFilePath = sSaveTargetFilePath;
        }
    }
}
