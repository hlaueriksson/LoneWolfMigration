using System.Xml.Linq;

namespace LoneWolf.Migration.Files
{
    public class Heading : ISubTransformer
    {
        public XElement Transform(XElement element)
        {
            // <h3>
            var heading = element.Element("h3");
            if(heading != null) heading.Name = "h1";

            // <h2>
            heading = element.Element("h2");
            if (heading != null) heading.Name = "h1";

            return element;
        }
    }
}
