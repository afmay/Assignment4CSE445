using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Xml.Schema;
using System.Xml;

namespace XMLVerifyService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        List<string> validationErrors = new List<string>();
        public string[] xml_verification(string xsd, string xml)
        {
            
            try
            {
                XmlSchemaSet schemaSet = new XmlSchemaSet();
                schemaSet.Add(null, xsd);

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas = schemaSet;
                settings.ValidationEventHandler += ValidationEventHandler;

                List<string> errors = new List<string>();

                using (XmlReader reader = XmlReader.Create(xml, settings))
                {
                    while (reader.Read()) { }
                }

                if (validationErrors.Count > 0)
                {
                    errors.AddRange(validationErrors);
                }

                if (errors.Count > 0)
                {
                    return errors.ToArray();
                }
                else
                {
                    return new string[] { "No validation errors found." };
                }
            }
            catch (Exception ex)
            {
                return new string[] { $"Error: {ex.Message}" };
            }
        }
        private void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            validationErrors.Add(e.Message);
        }
    }
}
