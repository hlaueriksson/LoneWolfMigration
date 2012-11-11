using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace LoneWolf.Migration.Files
{
    public class Combat : ISubTransformer
    {
        public XElement Transform(XElement element)
        {
            // <p class="combat">
            var combats = element.XPathSelectElements("p[@class='combat']").ToArray();
            if (combats.Count() == 1)
            {
                var combat = combats.ElementAt(0);

                // <button type="button" class="combat" onclick="javascript:Section.fight();">
                combat.Name = "button";
                combat.RemoveAttributes();
                combat.Add(new XAttribute("type", "button"));
                combat.Add(new XAttribute("class", "combat"));
                combat.Add(new XAttribute("onclick", "javascript:Section.fight();"));
            }
            else
            {
                for (var index = 0; index < combats.Count(); index++)
                {
                    var combat = combats.ElementAt(index);

                    // <button type="button" class="combat" onclick="javascript:Section.fight(X);">
                    combat.Name = "button";
                    combat.RemoveAttributes();
                    combat.Add(new XAttribute("type", "button"));
                    combat.Add(new XAttribute("class", "combat"));
                    combat.Add(new XAttribute("onclick", string.Format("javascript:Section.fight({0});", index)));
                }
            }

            return element;
        }
    }
}
