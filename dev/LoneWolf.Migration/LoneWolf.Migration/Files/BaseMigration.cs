using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using LoneWolf.Migration.Common;

namespace LoneWolf.Migration.Files
{
    public abstract class BaseMigration : IMigration
    {
        protected string input;
        protected string output;

        public BaseMigration(string input, string output)
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

                var result = Transform(file);

                Write(file, result);
            }
        }

        protected abstract IEnumerable<string> GetFiles();
        protected abstract ITransformer GetTransformer(FileInfo file);
        protected abstract string GetFilename(FileInfo file);

        protected virtual string Transform(FileInfo file)
        {
            var transformer = GetTransformer(file);
            var text = File.ReadAllText(file.FullName).Replace("xmlns=\"http://www.w3.org/1999/xhtml\"", string.Empty);
            var document = XDocument.Parse(text);

            return transformer.Transform(document);
        }

        protected virtual void Write(FileInfo file, string result)
        {
            File.WriteAllText(output + GetFilename(file), result, Encoding.UTF8);
        }
    }
}
