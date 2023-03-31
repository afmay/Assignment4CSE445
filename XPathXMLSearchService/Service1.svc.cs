using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace XPathXMLSearchService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string[] XPathSearch(string xml, string pathExpression)
        {
            try
            {
                List<string> results = new List<string>();

                using (XmlReader reader = XmlReader.Create(xml))
                {
                    XPathDocument document = new XPathDocument(reader);
                    XPathNavigator navigator = document.CreateNavigator();

                    XPathNodeIterator nodeIterator = navigator.Select(pathExpression);

                    while (nodeIterator.MoveNext())
                    {
                        results.Add(nodeIterator.Current.Value);
                    }
                }

                if (results.Count > 0)
                {
                    return results.ToArray();
                }
                else
                {
                    return new string[] { "No matching nodes found." };
                }
            }
            catch (Exception ex)
            {
                return new string[] { $"Error: {ex.Message}" };
            }
        }
    }
}
