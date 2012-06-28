using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace LoneWolf.Migration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Log("Usage: lwm.exe <path-to-input> <path-to-output>");

                return;
            }

            var input = args.First();
            var output = args.Last();

            var result = Migrate(input, output);

            Log(result.Message);
        }

        private static void Log(string message)
        {
            Console.WriteLine(message);
        }

        private static Result Migrate(string input, string output)
        {
            if (!Directory.Exists(input)) return new Result("The path to input does not exist");
            if (!Directory.Exists(output)) return new Result("The path to output does not exist");

            try
            {
                CopyImages(input, output);
                CopySections(input, output);

                return new Result("Done");
            }
            catch (Exception ex)
            {
                return new Result(ex.GetType().Name + ": " + ex.Message + "\n" + ex.StackTrace);
            }
        }

        private static void CopyImages(string input, string output)
        {
            var files = Directory.GetFiles(input, "ill*.png").AsEnumerable();
            files = files.Union(Directory.GetFiles(input, "small*.png").AsEnumerable());
            files = files.Union(Directory.GetFiles(input, "*.png").Where(s => Include(s)));

            foreach (var path in files)
            {
                var file = new FileInfo(path);

                file.CopyTo(output + file.Name);
            }
        }

        private static bool Include(string file)
        {
            var inclusion = new List<string>()
            {
                "axe.png",
                "bsword.png",
                "dagger.png",
                "food.png",
                "helmet.png",
                "mace.png",
                "mail.png",
                "map.png",
                "potion.png",
                "pouch.png",
                "qstaff.png",
                "spear.png",
                "ssword.png",
                "sword.png",
                "warhammr.png",
                "weapons.png"
            };

            return inclusion.Any(f => file.EndsWith(f));
        }

        private static void CopySections(string input, string output)
        {
            var files = Directory.GetFiles(input, "sect*.htm");

            foreach (var path in files)
            {
                var file = new FileInfo(path);

                // TODO: ignore sect021.xml?

                var result = Transform(file);

                File.WriteAllText(output + GetFilename(file.Name), result, Encoding.UTF8);
            }
        }

        private static string GetFilename(string filename)
        {
            var number = filename.Replace("sect", string.Empty);
            number = number.Replace(".htm", string.Empty);

            return "sect" + number.PadLeft(3, '0') + ".xml";
        }

        private static string Transform(FileInfo file)
        {
            var text = File.ReadAllText(file.FullName).Replace("xmlns=\"http://www.w3.org/1999/xhtml\"", string.Empty);
            var doc = XDocument.Parse(text);

            // <div class="maintext">
            var source = doc.XPathSelectElement("//*[@class='maintext']");

            if (source == null) return string.Empty;

            // <h3>
            source.Element("h3").Name = "h1";

            // <p class="choice">
            foreach (var choice in source.XPathSelectElements("p[@class='choice']"))
            {
                // <a href="sectX.htm">
                var a = choice.Element("a");

                if (a == null) continue; // Your mission and your life end here.

                var number = a.Attribute("href").Value.Replace("sect", string.Empty).Replace(".htm", string.Empty);

                // <button type="button" onclick="javascript:Choice.turnTo(X);">
                a.Name = "button";
                a.RemoveAttributes();
                a.Add(new XAttribute("type", "button"));
                a.Add(new XAttribute("class", "choice"));
                a.Add(new XAttribute("onclick", string.Format("javascript:Choice.turnTo({0});", number)));

                choice.Add(new XAttribute("id", number));
            }

            // <a href="random.htm">Random Number Table</a>
            var randoms = source.XPathSelectElements("//a[@href='random.htm']").ToArray();
            if (randoms.Count() == 1)
            {
                var random = randoms.ElementAt(0);

                // <button type="button" class="random-number" onclick="javascript:RandomNumber.roll();">
                random.Name = "button";
                random.RemoveAttributes();
                random.Add(new XAttribute("type", "button"));
                random.Add(new XAttribute("class", "random-number"));
                random.Add(new XAttribute("onclick", "javascript:RandomNumber.roll();"));
            }
            else
            {
                for (var index = 0; index < randoms.Count(); index++)
                {
                    var random = randoms.ElementAt(index);

                    // <button type="button" class="random-number" onclick="javascript:RandomNumber.roll();">
                    random.Name = "button";
                    random.RemoveAttributes();
                    random.Add(new XAttribute("type", "button"));
                    random.Add(new XAttribute("class", "random-number"));
                    random.Add(new XAttribute("onclick", string.Format("javascript:RandomNumber.roll({0});", index)));
                }
            }

            // <p class="combat">
            var combats = source.XPathSelectElements("p[@class='combat']").ToArray();
            if (combats.Count() == 1)
            {
                var combat = combats.ElementAt(0);

                // <button type="button" class="combat" onclick="javascript:Combat.fight();">
                combat.Name = "button";
                combat.RemoveAttributes();
                combat.Add(new XAttribute("type", "button"));
                combat.Add(new XAttribute("class", "combat"));
                combat.Add(new XAttribute("onclick", "javascript:Combat.fight();"));
            }
            else
            {
                for (var index = 0; index < combats.Count(); index++)
                {
                    var combat = combats.ElementAt(index);

                    // <button type="button" class="combat" onclick="javascript:Combat.fight(X);">
                    combat.Name = "button";
                    combat.RemoveAttributes();
                    combat.Add(new XAttribute("type", "button"));
                    combat.Add(new XAttribute("class", "combat"));
                    combat.Add(new XAttribute("onclick", string.Format("javascript:Combat.fight({0});", index)));
                }
            }

            // <div class="illustration">
            foreach (var illustration in source.XPathSelectElements("div[@class='illustration']"))
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

            var result = new XDocument(
                new XElement("section",
                    source.Elements()
                )
            );

            return result.ToString();
        }
    }
}
