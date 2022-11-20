using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave.Models
{
    public class CLog : JsonSerializable
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string SourceFilePath { get; set; }
        public string TargetFilePath { get; set; }
        public string SName { get; set; }
        public long FileSize { get ;set; }
        public int CryptTime { get; set; }

        public CLog() { }
        public CLog(string name, string startTime, string sourceFilePath, string targetFilePath, long fileSize, int cryptTime)
        {
            this.SName = name;
            this.StartTime = startTime;
            this.EndTime = DateTime.Now.ToString();
            this.SourceFilePath = sourceFilePath;
            this.TargetFilePath = targetFilePath;
            this.FileSize = fileSize;
            this.CryptTime = cryptTime;
        }
    }
}
