using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LoneWolf.Migration.Core;

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
            var transformer = new Transformer();
            var text = File.ReadAllText(file.FullName).Replace("xmlns=\"http://www.w3.org/1999/xhtml\"", string.Empty);
            var document = XDocument.Parse(text);

            return transformer.Transform(document);
        }
    }
}
