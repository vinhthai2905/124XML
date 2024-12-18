using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace QUANLINHKIENDT.Model
{
    class ChucVu
    {
        XMLFile XmlFile = new XMLFile();
        public XmlNodeList getListChucVu()
        {
            XmlDocument XDoc = XmlFile.getXmlDocument("ChucVu.xml");
            XmlNodeList nodeList = XDoc.SelectNodes("/ChucVus/ChucVu");
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlNode a = nodeList[i];
            }
            return nodeList;

        }
    }
}
