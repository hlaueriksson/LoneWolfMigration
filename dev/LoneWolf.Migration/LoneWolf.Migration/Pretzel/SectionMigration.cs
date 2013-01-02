using System.Collections.Generic;
using System.IO;

namespace LoneWolf.Migration.Pretzel
{
    public class SectionMigration : BaseMigration
    {
        public SectionMigration(string input)
            : base(input)
        {
        }

        protected override IEnumerable<string> GetFiles()
        {
            return Directory.GetFiles(input, "sect*.xml");
        }

        protected override string GetYamlFrontMatter()
        {
            return "---\nlayout: section\n---\n\n";
        }
    }
}
