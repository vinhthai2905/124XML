using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QUANLINHKIENDT.Model
{
    class XMLFile
    {
        //get XmlDocument 
        public XmlDocument getXmlDocument(String fileName)
        {
            XmlDocument Xd = new XmlDocument();
            Xd.Load(fileName);
            return Xd;
        }
    }
}
