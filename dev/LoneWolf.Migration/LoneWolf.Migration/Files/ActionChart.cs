using System.Xml.Linq;
using System.Xml.XPath;

namespace LoneWolf.Migration.Files
{
    public class ActionChart : ISubTransformer
    {
        public XElement Transform(XElement element)
        {
            // <a href="action.htm">Action Chart</a>
            foreach (var action in element.XPathSelectElements("//a[@href='action.htm']"))
            {
                // <button type="button" class="action-chart" onclick="javascript:Section.inventory();">
                action.Name = "button";
                action.RemoveAttributes();
                action.Add(new XAttribute("type", "button"));
                action.Add(new XAttribute("class", "action-chart"));
                action.Add(new XAttribute("onclick", "javascript:Section.inventory();"));
            }

            return element;
        }
    }
}
