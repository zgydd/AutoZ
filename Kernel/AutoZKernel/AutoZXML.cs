//////////////////
///Barton Joe
//////////////////
using System;
using System.Xml;

namespace AutoZKernel
{
    public class AutoZXML
    {
        public static string getInnerTextByName(String strName, XmlNode xmlNode)
        {
            foreach (XmlNode chdNode in xmlNode.ChildNodes)
            {
                if (strName.Equals(chdNode.Name))
                {
                    return chdNode.InnerText.Trim();
                }
            }
            return string.Empty;
        }
        public static XmlNode getXmlNodeByName(String strName, XmlNode xmlNode)
        {
            foreach (XmlNode chdNode in xmlNode.ChildNodes)
            {
                if (strName.Equals(chdNode.Name))
                {
                    return chdNode;
                }
            }
            return null;
        }
        public static XmlElement getXmlElementByName(String strName, XmlNode xmlNode)
        {
            foreach (XmlElement chdEle in xmlNode.ChildNodes)
            {
                if (strName.Equals(chdEle.Name))
                {
                    return chdEle;
                }
            }
            return null;
        }
    }
}
