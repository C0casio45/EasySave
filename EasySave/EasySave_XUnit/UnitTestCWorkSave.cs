using System;
using System.Collections.Generic;
using Xunit;
using EasySave.Models;
using System.IO;
using System.Text;
using System.Linq;

namespace EasySave_XUnit
{
    public class UnitTestCWorkSave
    {
        [Fact]
        public void TestDirSize()
        {
            Directory.CreateDirectory("./testDir");
            using (FileStream fs = File.Create("./testDir/testFile.txt"))
            {
                // Add some text to file    
                Byte[] title = new UTF8Encoding(true).GetBytes("New Text File");
                fs.Write(title, 0, title.Length);
                byte[] author = new UTF8Encoding(true).GetBytes("Armand Debesse");
                fs.Write(author, 0, author.Length);
            }
            long size = CWorkSave.DirSize(new DirectoryInfo("./testDir"));
            Assert.Equal(27, size);
            File.Delete("./testDir/testFile.txt");
            Directory.Delete("./testDir");
        }

        [Fact]
        public void TestGetDirectoryTotalFilesAndSize()
        {
            Directory.CreateDirectory("./testDir");
            using (FileStream fs = File.Create("./testDir/testFile.txt"))
            {
                // Add some text to file    
                Byte[] title = new UTF8Encoding(true).GetBytes("New Text File");
                fs.Write(title, 0, title.Length);
                byte[] author = new UTF8Encoding(true).GetBytes("Armand Debesse");
                fs.Write(author, 0, author.Length);
            }

            CWorkSave workSave = new CWorkSave("name", "./testDir", "./testDir", false);
            Dictionary<string, long> dictReturned = workSave.GetDirectoryTotalFilesAndSize();
            Dictionary<string, long> dictExpected = new Dictionary<string, long>
            {
                {"totalFilesToCopy", 1},
                {"totalFilesSize", 27}
            };
            Assert.Equal(dictExpected, dictReturned);
            File.Delete("./testDir/testFile.txt");
            Directory.Delete("./testDir");
        }

        [Fact]
        public void TestClearFolder()
        {
            // Create directory and sub-Directory
            Directory.CreateDirectory("./testDir");
            Directory.CreateDirectory("./testDir/testDir2");

            //Create files into directories
            using (FileStream fs = File.Create("./testDir/testFile.txt"))
            {
                // Add some text to file    
                Byte[] title = new UTF8Encoding(true).GetBytes("New Text File");
                fs.Write(title, 0, title.Length);
                byte[] author = new UTF8Encoding(true).GetBytes("Armand Debesse");
                fs.Write(author, 0, author.Length);
            }
            using (FileStream fs = File.Create("./testDir/testDir2/testFile2.txt"))
            {
                // Add some text to file    
                Byte[] title = new UTF8Encoding(true).GetBytes("New Text File");
                fs.Write(title, 0, title.Length);
                byte[] author = new UTF8Encoding(true).GetBytes("Armand Debesse");
                fs.Write(author, 0, author.Length);
            }

            //Test ClearFolder Method
            CWorkSave workSave = new CWorkSave("name", "./testDir", "./testDir", false);
            workSave.ClearFolder();
            bool isEmpty = !Directory.EnumerateFileSystemEntries("./testDir").Any();
            Assert.True(isEmpty);

            //Delete temporary Folder
            Directory.Delete("./testDir");
        }
    }
}
