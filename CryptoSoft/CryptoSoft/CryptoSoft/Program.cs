using System;
using System.Configuration;
using System.IO;
using System.Text;

namespace CryptoSoft
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 2) { return -2; }
            string key = ConfigurationManager.AppSettings["encryptionKey"];

            if (File.Exists(@args[0]))
            {
                string text = File.ReadAllText(@args[0]);
                var result = new StringBuilder();

                DateTime start = DateTime.Now;

                for (int c = 0; c < text.Length; c++)
                {
                    // take next character from string
                    char character = text[c];
                    // cast to a uint
                    uint charCode = (uint)character;
                    // figure out which character to take from the key
                    int keyPosition = c % key.Length; // use modulo to "wrap round"
                                                      // take the key character
                    char keyChar = key[keyPosition];
                    // cast it to a uint also
                    uint keyCode = (uint)keyChar;
                    // perform XOR on the two character codes
                    uint combinedCode = charCode ^ keyCode;
                    // cast back to a char
                    char combinedChar = (char)combinedCode;
                    // add to the result
                    result.Append(combinedChar);
                }
                File.WriteAllText(@args[1], result.ToString());
                return (int)(DateTime.Now - start).Milliseconds;
            }
            else { return -1; }
        }
    }
}
