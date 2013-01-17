using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace LoneWolf.Migration.Files
{
    public class RandomNumberTable : ISubTransformer
    {
        public XElement Transform(XElement element)
        {
            // <a href="random.htm">Random Number Table</a>
            var randoms = element.XPathSelectElements("//a[@href='random.htm']").ToArray();
            if (randoms.Count() == 1)
            {
                var random = randoms.ElementAt(0);

                // <button type="button" class="random-number" onclick="Section.roll();">
                random.Name = "button";
                random.RemoveAttributes();
                random.Add(new XAttribute("type", "button"));
                random.Add(new XAttribute("class", "random-number"));
                random.Add(new XAttribute("onclick", "Section.roll();"));
            }
            else
            {
                for (var index = 0; index < randoms.Count(); index++)
                {
                    var random = randoms.ElementAt(index);

                    // <button type="button" class="random-number" onclick="Section.roll();">
                    random.Name = "button";
                    random.RemoveAttributes();
                    random.Add(new XAttribute("type", "button"));
                    random.Add(new XAttribute("class", "random-number"));
                    random.Add(new XAttribute("onclick", string.Format("Section.roll({0});", index)));
                }
            }

            return element;
        }
    }
}
