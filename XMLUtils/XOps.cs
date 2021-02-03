using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLUtils
{
    public class XOps
    {
        public static void PostProcess(Stream inStream, Stream outStream)
        {
            var settings = new XmlWriterSettings() { Indent = true, IndentChars = " " };
            string[] badChars = {"*","~",":","^" };
            using (var reader = XmlReader.Create(inStream))
            using (var writer = XmlWriter.Create(outStream, settings))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            writer.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);


                            //writer.WriteAttributes(reader, true);
                            
                            if (reader.HasAttributes) {
                                for (int i = 0; i < reader.AttributeCount; i++)
                                {
                                    reader.MoveToAttribute(i);                                    
                                    writer.WriteAttributeString(reader.Prefix, reader.LocalName, null, "updated|" + reader.Value);
                                }
                                reader.MoveToElement();
                            }


                            if (reader.IsEmptyElement)
                            {
                                writer.WriteEndElement();
                            }
                            break;

                        case XmlNodeType.Text:
                            string updatedValue = reader.Value;
                            foreach (string item in badChars)
                            {
                                if (reader.Value.Contains(item))
                                {
                                    updatedValue = reader.Value.Replace(item, "");
                                }
                            }                            
                            writer.WriteString(updatedValue);
                            break;

                        case XmlNodeType.EndElement:
                            writer.WriteFullEndElement();
                            break;

                        case XmlNodeType.XmlDeclaration:
                        case XmlNodeType.ProcessingInstruction:
                            writer.WriteProcessingInstruction(reader.Name, reader.Value);
                            break;

                        case XmlNodeType.SignificantWhitespace:
                            writer.WriteWhitespace(reader.Value);
                            break;
                        case XmlNodeType.Comment:
                            writer.WriteComment(reader.Value);
                            break;
                    }
                }
            }
        }

    }
}
