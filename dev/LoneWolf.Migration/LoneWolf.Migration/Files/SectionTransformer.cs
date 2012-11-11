using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

namespace LoneWolf.Migration.Files
{
    public class SectionTransformer : ITransformer
    {
        public string Transform(XDocument document)
        {
            // <div class="maintext">
            var element = document.XPathSelectElement("//*[@class='maintext']");

            if (element == null) return string.Empty;

            element = Transform(element);

            var result = new XDocument(
                new XElement("section",
                    element.Elements()
                )
            );

            return result.ToString();
        }

        private XElement Transform(XElement element)
        {
            foreach (var generator in GetTransformers())
            {
                generator.Transform(element);
            }

            return element;
        }

        private IEnumerable<ISubTransformer> GetTransformers()
        {
            return new List<ISubTransformer>
            {
                new Heading(),
                new Choice(),
                new RandomNumberTable(),
                new Combat(),
                new Illustration(),
                new ActionChart()
            };
        }
    }
}
