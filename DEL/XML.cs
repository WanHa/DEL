using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DEL
{
    public class XML
    {
        /// <summary>  
        ///  实体类序列化成xml  
        /// </summary>  
        /// <param name="enitities">实体.</param>  
        /// <param name="headtag">xml保存路径</param>  
        public void ObjListToXml<T>(List<T> enitities, string headtag) where T : new()
        {//方法一
            XmlDocument xmldoc = new XmlDocument();
            XmlDeclaration xmldecl = xmldoc.CreateXmlDeclaration("1.0", "iso-8859-1", null);//生成<?xml version="1.0" encoding="iso-8859-1"?>
            xmldoc.AppendChild(xmldecl);
            XmlElement modelNode = xmldoc.CreateElement("XMLDATA");
            xmldoc.AppendChild(modelNode);

            foreach (T entity in enitities)
            {
                if (entity != null)
                {
                    XmlElement childNode = xmldoc.CreateElement(entity.GetType().Name);
                    modelNode.AppendChild(childNode);
                    foreach (PropertyInfo property in entity.GetType().GetProperties())
                    {
                        XmlElement attritude = xmldoc.CreateElement(property.Name);
                        if (property.GetValue(entity, null) != null)
                        {
                            attritude.InnerText = property.GetValue(entity, null).ToString();
                        }
                        else
                        {
                            attritude.InnerText = "[NULL]";
                        }
                        childNode.AppendChild(attritude);
                    }
                }
            }
            xmldoc.Save(headtag);
        }

        /// <summary>
        /// 更新xml数据
        /// </summary>
        /// <param name="Nodes">节点名称</param>
        /// <param name="Values">值</param>
        /// <param name="headtag">xml路径</param>
        /// <returns></returns>
        public bool UpdateXml(string Nodes,string Values, string headtag)
        {
            var xml = XDocument.Load(headtag);
            var firstNode = xml.Descendants(Nodes).FirstOrDefault();
            firstNode.Value = Values;
            FileStream fs = new FileStream(headtag, FileMode.Create);
            xml.Save(fs);
            fs.Close();
            return false;
        }
    }
}
