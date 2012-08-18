using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoneWolf.Migration.Core
{
    public class ImageMigration : IMigration
    {
        protected string input;
        protected string output;

        public ImageMigration(string input, string output)
        {
            this.input = input;
            this.output = output;
        }

        public virtual void Execute()
        {
            var files = GetFiles();

            foreach (var path in files)
            {
                var file = new FileInfo(path);

                Write(file);
            }
        }

        protected virtual IEnumerable<string> GetFiles()
        {
            var files = Directory.GetFiles(input, "ill*.png").AsEnumerable();
            files = files.Union(Directory.GetFiles(input, "small*.png").AsEnumerable());
            files = files.Union(Directory.GetFiles(input, "*.png").Where(s => Include(s)));

            return files;
        }

        protected virtual void Write(FileInfo file)
        {
            file.CopyTo(output + file.Name);
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
    }
}
