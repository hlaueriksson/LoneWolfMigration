using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LoneWolf.Migration.Pretzel
{
    public class PageMigration : BaseMigration
    {
        public PageMigration(string input)
            : base(input)
        {
        }

        protected override IEnumerable<string> GetFiles()
        {
            return Directory.GetFiles(input, "*.xml").Where(Include);
        }

        protected override string GetYamlFrontMatter()
        {
            return "---\nlayout: charactercreation\n---\n\n";
        }

        private bool Include(string file)
        {
            var inclusion = new List<string>
                                {
                "gamerulz.xml",
                "discplnz.xml",
                "equipmnt.xml"
            };

            return inclusion.Any(file.EndsWith);
        }
    }
}
