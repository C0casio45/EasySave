using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using EasySave.Exceptions;
using Newtonsoft.Json.Linq;

namespace EasySave.Models
{
    public class CWorkSave : ACFileHandler, JsonSerializable
    {
        // Work saves attributs
        public string SName { get; set; }
        public string SSourcePath { get; set; }
        public string STargetPath { get; set; }
        public bool BDiscriminating { get; set; }

        //create a Sha256 object which will be use to hash files
        private SHA256 Sha256 = SHA256.Create();

        public CWorkSave(string sName, string sSourcePath, string sTargetPath, bool bDiscriminating)
        {
            this.SName = sName;
            this.SSourcePath = sSourcePath;
            this.STargetPath = sTargetPath;
            this.BDiscriminating = bDiscriminating;
        }

        public static long DirSize(DirectoryInfo directoryInfo)
        {
            long size = 0;
            try
            {
                // Add file sizes.
                FileInfo[] files = directoryInfo.GetFiles();
                foreach (FileInfo file in files)
                {
                    size += file.Length;
                }
                // Add subdirectory sizes.
                DirectoryInfo[] directories = directoryInfo.GetDirectories();
                foreach (DirectoryInfo directory in directories)
                {
                    size += DirSize(directory);
                }
                return size;
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                
                return new long();
            }
        }
        public Dictionary<string, long> GetDirectoryTotalFilesAndSize()
        {
            //Create a dictionnary with the number of files in the directory and the size of the directory
            Dictionary<string,long> dLogsData = new Dictionary<string, long>();
            
            dLogsData.Add("totalFilesToCopy", Directory.GetFiles(this.SSourcePath, "*", SearchOption.AllDirectories).Length);
            dLogsData.Add("totalFilesSize", DirSize(new DirectoryInfo(this.SSourcePath)));
            return dLogsData;
        }
        public void ClearFolder()
        {
            try
            {
                System.IO.DirectoryInfo directory = new DirectoryInfo(this.STargetPath);
                //Delete files in the directory
                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                //Delete directories in the directory
                foreach (DirectoryInfo dir in directory.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                
            }
        }
        public byte[] GetHashSha256(string fileName)
        {
            //Get the hash of a file
            using (FileStream stream = File.OpenRead(fileName))
            {
            return Sha256.ComputeHash(stream);
            }
        }
        public int StartProcess(string process, string arg)
        {
            ProcessStartInfo myProcessInfo = new ProcessStartInfo(process, arg);
            myProcessInfo.UseShellExecute = false;
            myProcessInfo.CreateNoWindow = true;
            Process myProcess = new Process();
            myProcess.StartInfo = myProcessInfo;
            myProcess.Start();
            myProcess.WaitForExit();
            return myProcess.ExitCode;
        }
        public bool FileNeedEncrypt(string file)
        {
            //Get worksaves of the user in UserSettings/extToEncrypt.json
            List<CExtension> extensionList;
            string fileExt = file.Substring(file.IndexOf('.'));
            try
            {
                // Using the installer this path will be used 
                extensionList = JsonConvert.DeserializeObject<List<CExtension>>(File.ReadAllText("./UserSettings/extToEncrypt.json"));
            }
            catch (Exception)
            {
                // Else during developpement this path will be used
                extensionList = JsonConvert.DeserializeObject<List<CExtension>>(File.ReadAllText(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\UserSettings\extToEncrypt.json"));
            }
            //Check if the extension of the file is in the list
            foreach (CExtension extension in extensionList)
            {
                if (extension.SName == fileExt)
                {
                    return true;
                }
            }
            return false;
        }
        public void SaveFolder()
        {
            try
            {
                // Check if Business Software is running
                if (CheckProcessRunning())
                {
                    // If business software is running the save is not done
                    throw new BusinessSoftwareRunningException();
                }
                //Clear the Folder for the complete saves
                if (!this.BDiscriminating)
                {
                    this.ClearFolder();
                }

                //Get Files info
                Dictionary<string, long> filesInfo = this.GetDirectoryTotalFilesAndSize();
                long lSaveNumberFilesLeftToDo = filesInfo["totalFilesToCopy"];
                long lSizeFilesLeftToDo = filesInfo["totalFilesSize"];

                int cryptTime = 0;

                //Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(this.SSourcePath, "", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(this.SSourcePath, this.STargetPath));
                }

                //Copy all the files
                foreach (string newPath in Directory.GetFiles(this.SSourcePath, "*", SearchOption.AllDirectories))
                {
                    string sStartTime = DateTime.Now.ToString();
                    // If the worksave is discriminating
                    if (this.BDiscriminating)
                    {
                        //Check if file exist
                        if (File.Exists(newPath.Replace(this.SSourcePath, this.STargetPath)))
                        {
                            byte[] sourceHash = GetHashSha256(newPath);
                            byte[] targetHash = GetHashSha256(newPath.Replace(this.SSourcePath, this.STargetPath));
                            //Compare sourceFile hash and targetFile hash
                            if (sourceHash.SequenceEqual(targetHash))
                            {
                                //Skip copying if the files are same
                                continue;
                            }
                        }
                    }
                    //Copy the file
                    if (FileNeedEncrypt(newPath))
                    {
                        string cryptoSoftPath = Environment.GetEnvironmentVariable("CryptoSoft.exe");

                        if (cryptoSoftPath != null)
                        {
                            try
                            {
                                cryptTime = this.StartProcess(cryptoSoftPath, "\"" + newPath + "\"" + " \"" + newPath.Replace(this.SSourcePath, this.STargetPath) + "\"");
                            }
                            catch (Exception)
                            {

                                throw new CantFindCryptoSoftException();
                            }
                        }
                        else
                        {
                            //CMenu.ShowExceptionMessage("Can't open CryptoSoft, please install it");
                        }
                    }
                    else
                    {
                        File.Copy(newPath, newPath.Replace(this.SSourcePath, this.STargetPath), true);
                    }

                    // Update FilesLeftToDo Info and create Progression and Log Object
                    FileInfo file = new FileInfo(newPath);
                    lSaveNumberFilesLeftToDo--;
                    lSizeFilesLeftToDo -= file.Length;
                    string sSourcePathFile = file.FullName;
                    string sTargetPathFile = file.FullName.Replace(this.SSourcePath, this.STargetPath);
                    CProgression progression = new CProgression(this.SName, "Active", filesInfo["totalFilesToCopy"], filesInfo["totalFilesSize"], lSaveNumberFilesLeftToDo, lSizeFilesLeftToDo, sSourcePathFile, sTargetPathFile);
                    if (lSaveNumberFilesLeftToDo == 0)
                    {
                        progression.SState = "END";
                    }
                    CLog log = new CLog(this.SName, sStartTime, sSourcePathFile, sTargetPathFile, file.Length, cryptTime);

                    // Error handling for files in deployment version
                    if (File.Exists(@".\UserSettings\logSettings.json"))
                    {
                        // Get the state of the logSettings file (either JSON or XML)
                        string extension = JObject.Parse(File.ReadAllText("./UserSettings/logSettings.json"))["logSettings"].ToString();
                        // Write logs
                        if (extension == "JSON")
                        {
                            WriteJsonFile(log, @".\ApplicationLogs\");
                        }
                        else { WriteXMLFile(log, @".\ApplicationLogs\"); }
                        // Write progression
                        WriteJsonFile(progression, @".\Progression\progression.json");
                    }
                    else
                    {
                        // Get the state of the logSettings file (either JSON or XML)
                        string extension = JObject.Parse(File.ReadAllText(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\UserSettings\logSettings.json"))["logSettings"].ToString();
                        // Write logs
                        if (extension == "JSON")
                        {
                            WriteJsonFile(log, Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ApplicationLogs\");
                        }
                        else 
                        { 
                            WriteXMLFile(log, Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\ApplicationLogs\");
                        }
                        // Write progression
                        WriteJsonFile(progression, Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Progression\progression.json");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void CreateJsonWorkSave()
        {
            //Call JsonFileHandler method to write
            if (File.Exists(@".\UserSettings\WorkSaves.json"))
            {
                this.WriteJsonFile(this, @".\UserSettings\WorkSaves.json");
            }
            else
            {
                this.WriteJsonFile(this, Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\UserSettings\WorkSaves.json");
            }
        }
        public void DeleteJsonWorkSave()
        {
            //Call JsonFileHandler method to delete
            if (File.Exists(@".\UserSettings\WorkSaves.json"))
            {
                this.RemoveJsonFile(this, @".\UserSettings\WorkSaves.json");
            }
            else
            {
                this.RemoveJsonFile(this, Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\UserSettings\WorkSaves.json");
            }
        }
        public bool CheckProcessRunning()
        {
            CBusinessSoftware businessSoftware;
            try
            {
                // Using the installer this path will be used 
                businessSoftware = JsonConvert.DeserializeObject<CBusinessSoftware>(File.ReadAllText("./UserSettings/businessSoftware.json"));
            }
            catch (Exception)
            {
                // Else during developpement this path will be used
                businessSoftware = JsonConvert.DeserializeObject<CBusinessSoftware>(File.ReadAllText(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\UserSettings\businessSoftware.json"));
            }
            Process[] proc = Process.GetProcessesByName(businessSoftware.SName);
            if (proc.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
