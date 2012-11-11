using System.Collections.Generic;
using System.IO;

namespace LoneWolf.Migration.Files
{
    public class SectionMigration : BaseMigration
    {
        public SectionMigration(string input, string output)
            : base(input, output)
        {
        }

        protected override IEnumerable<string> GetFiles()
        {
            return Directory.GetFiles(input, "sect*.htm");
        }

        protected override ITransformer GetTransformer(FileInfo file)
        {
            // TODO: handle sect021.xml?

            return new SectionTransformer();
        }

        protected override string GetFilename(FileInfo file)
        {
            var number = file.Name.Replace("sect", string.Empty);
            number = number.Replace(".htm", string.Empty);

            return "sect" + number.PadLeft(3, '0') + ".xml";
        }
    }
}
