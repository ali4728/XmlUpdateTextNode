using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLUtils
{
    public class NodeInfo
    {
        public static void Execute(Stream inStream)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();


            using(var reader = XmlReader.Create(inStream))
            {
                while (reader.Read())
                {
                    XmlNodeType tp = reader.NodeType;

                    if (!dict.ContainsKey(tp.ToString()))
                    {
                        dict.Add(tp.ToString(), 1);
                    }
                    else
                    {
                        dict[tp.ToString()] = dict[tp.ToString()] + 1;
                    }
                }
            }

            foreach (KeyValuePair<string, int> kvp in dict)
            {
                Console.WriteLine("Key: {0}, Value: {1}", kvp.Key, kvp.Value);
            }
            

        }
    }
}
