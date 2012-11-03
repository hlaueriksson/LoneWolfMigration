using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LoneWolf.Migration.Files
{
    public class PageMigration : BaseMigration
    {
        public PageMigration(string input, string output)
            : base(input, output)
        {
        }

        protected override IEnumerable<string> GetFiles()
        {
            return Directory.GetFiles(input, "*.htm").Where(s => Include(s));
        }

        protected override ITransformer GetTransformer(FileInfo file)
        {
            return new Transformer();
        }

        protected override string GetFilename(FileInfo file)
        {
            return file.Name.Replace(file.Extension, string.Empty) + ".xml";
        }

        private bool Include(string file)
        {
            var inclusion = new List<string>()
            {
                "gamerulz.htm", // TODO
                //"discplnz.htm",
                //"equipmnt.htm"
            };

            return inclusion.Any(f => file.EndsWith(f));
        }
    }
}
