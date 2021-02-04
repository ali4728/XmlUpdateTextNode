using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLUtils
{
    class Program
    {
        static void Main(string[] args)
        {
            string inFile = @"C:\temp\SmpleXmlDocument.xml";
            string outFile = @"C:\temp\SmpleXmlDocument_OUT.xml";

            FileStream SourceStream = File.Open(inFile, FileMode.Open);
            FileStream DestinationStream = File.Create(outFile);
            
            //NodeInfo.Execute(SourceStream);
            XOps.Execute(SourceStream, DestinationStream);

        }
    }
}
