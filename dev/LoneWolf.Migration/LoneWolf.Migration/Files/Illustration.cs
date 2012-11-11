using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace LoneWolf.Migration.Files
{
    public class Illustration : ISubTransformer
    {
        public XElement Transform(XElement element)
        {
            // <div class="illustration">
            foreach (var illustration in element.XPathSelectElements("div[@class='illustration']"))
            {
                // <img alt="" align="middle" height="32" width="32" src="brdrX.png" />
                var img = illustration.Descendants("img").Where(e => !e.Attribute("src").Value.StartsWith("brdr")).First();
                var src = img.Attribute("src").Value;

                // <figure><img alt="" src="X.png" /></figure>
                var figure =
                    new XElement("figure",
                        new XElement("img",
                            new XAttribute("alt", ""),
                            new XAttribute("src", src)));

                illustration.ReplaceWith(figure);
            }

            return element;
        }
    }
}
