using System.Xml.Linq;
using System.Xml.XPath;

namespace LoneWolf.Migration.Files
{
    public class Choice : ISubTransformer
    {
        public XElement Transform(XElement element)
        {
            // <p class="choice">
            foreach (var choice in element.XPathSelectElements("p[@class='choice']"))
            {
                // <a href="sectX.htm">
                var a = choice.Element("a");

                if (a == null) continue; // Your mission and your life end here.

                var number = a.Attribute("href").Value.Replace("sect", string.Empty).Replace(".htm", string.Empty);

                // <button type="button" onclick="Section.turnTo(X);">
                a.Name = "button";
                a.RemoveAttributes();
                a.Add(new XAttribute("type", "button"));
                a.Add(new XAttribute("class", "choice"));
                a.Add(new XAttribute("onclick", string.Format("Section.turnTo({0});", number)));

                choice.Add(new XAttribute("id", number));
            }

            return element;
        }
    }
}
