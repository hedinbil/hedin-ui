using DocumentFormat.OpenXml;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace Hedin.UI.Demo.Shared.ApiDocs
{
    // this is needed for the api docs 
    public static partial class DocStrings
    {
        /* To speed up the method, run it in this way:
         *   string saveTypename = DocStrings.GetSaveTypename(type);  // calculate it only once
         *   DocStrings.GetMemberDescription(saveTypename, member);
         */
        public static string GetMemberDescription(string saveTypename, MemberInfo member)
        {
            XmlDocument doc = new XmlDocument();
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            doc.Load($"{baseDirectory}/Hedin.UI.xml");

            var xdoc = XDocument.Parse(doc.InnerXml);
            IEnumerable<XElement> xel;


            if (member is PropertyInfo property)
                xel = xdoc.Descendants("member").Where(xElement => xElement.Attribute("name")?.Value == $"P:Hedin.UI.{saveTypename}.{property.Name}");

            else if (member is MethodInfo method)
                xel = xdoc.Descendants("member").Where(xElement => xElement.Attribute("name")?.Value == $"M:Hedin.UI.{saveTypename}.{method.Name}");
            else
                throw new Exception("Implemented only for properties and methods.");

            return xel.Descendants("summary")?.FirstOrDefault()?.Value ?? "";
        }

        public static string GetSaveTypename(Type t) => Regex.Replace(t.ConvertToCSharpSource(), @"[\.]", "_").Replace("<T>", "").TrimEnd('_');
    }
}
