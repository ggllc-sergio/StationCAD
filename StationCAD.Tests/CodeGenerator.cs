using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace StationCAD.Tests
{
    [TestClass]
    public class CodeGenerator
    {
        [TestMethod]
        public void BuildMunicipalities()
        {
            StringBuilder sb = new StringBuilder();

            using (StreamReader sr = new StreamReader(@"TestData\MuniCodelist-20160621.csv"))
            {
                int lineCnt = 0;
                bool header = true;
                string fileText = sr.ReadToEnd();
                string[] fileArray = fileText.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string currentLine in fileArray)
                {

                    if (header)
                    {
                        lineCnt++;
                        header = false;
                    }
                    else
                    {
                        string[] parts = currentLine.Split(',');
                        string line = string.Format("                    _munic.Add(new Municipality {{ Name=\"{0}\", Abbreviation=\"{1}\", Code=\"{2}\" }});", parts[0], parts[1], parts[3]);
                        sb.AppendLine(line);
                        lineCnt++;
                    }
                }

                Console.WriteLine(sb.ToString());
            }
        }

        [TestMethod]
        public void BuildEventCodes()
        {

            StringBuilder sb = new StringBuilder();

            using (StreamReader sr = new StreamReader(@"TestData\event_type.csv"))
            {
                int lineCnt = 0;
                bool header = true;
                string fileText = sr.ReadToEnd();
                string[] fileArray = fileText.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string currentLine in fileArray)
                {

                    if (header)
                    {
                        lineCnt++;
                        header = false;
                    }
                    else
                    {
                        string[] parts = currentLine.Split(',');
                        string line = string.Format("                   _evtCode.Add(new EventCode {{ Group=EventGroup.{0}, TypeCode = \"{1}\", SubTypeCode = \"{2}\", Description = \"{3}\" }});",
                                                    parts[0],
                                                    parts[1],
                                                    parts[3],
                                                    parts[4].Replace('"', new char()));
                        sb.AppendLine(line);
                        lineCnt++;
                    }
                }
            }
            Console.WriteLine(sb.ToString());
        }
    }
}
