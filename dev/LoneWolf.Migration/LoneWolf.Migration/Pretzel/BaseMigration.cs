using System.Collections.Generic;
using System.IO;
using LoneWolf.Migration.Common;

namespace LoneWolf.Migration.Pretzel
{
    public abstract class BaseMigration : IMigration
    {
        protected string input;

        protected BaseMigration(string input)
        {
            this.input = input;
        }

        public virtual void Execute()
        {
            var files = GetFiles();

            foreach (var path in files)
            {
                var file = new FileInfo(path);

                Transform(file);
            }
        }

        protected abstract IEnumerable<string> GetFiles();
        protected abstract string GetYamlFrontMatter();

        protected virtual void Transform(FileInfo file)
        {
            var yaml = GetYamlFrontMatter();

            var text = File.ReadAllText(file.FullName);

            File.WriteAllText(file.FullName, yaml + text);
        }
    }
}
