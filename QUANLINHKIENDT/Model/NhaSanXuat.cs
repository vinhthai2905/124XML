using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QUANLINHKIENDT.Model
{
    class NhaSanXuat
    {
        XMLFile XmlFile = new XMLFile();
        public XmlNodeList getListNSX()
        {
            XmlDocument XDoc = XmlFile.getXmlDocument("NhaSanXuat.xml");
            XmlNodeList nodeList = XDoc.SelectNodes("/NhaSanXuats/NhaSanXuat");
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlNode a = nodeList[i];
            }
            return nodeList;
        }
    }
}
