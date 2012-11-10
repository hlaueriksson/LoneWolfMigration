using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LoneWolf.Migration.Common;

namespace LoneWolf.Migration.Code
{
    public class SectionFactoryGenerator : IMigration
    {
        private string input;

        public SectionFactoryGenerator(string input)
        {
            this.input = input;
        }

        public void Execute()
        {
            var sb = new StringBuilder();

            var files = GetFiles();

            foreach (var path in files)
            {
                var file = new FileInfo(path);

                var number = Convert.ToInt32(file.Name.Replace("sect", string.Empty).Replace(".xml", string.Empty));

                var lines = File.ReadAllLines(file.FullName);

                sb.Append(SectionGenerator.Generate(number.ToString(), lines));
            }

            var template = GetTemplate();

            Write(template.Replace("{0}", sb.ToString()));
        }

        private string GetTemplate()
        {
            var path = Directory.GetCurrentDirectory() + @"\LoneWolf.Migration\Code\SectionFactory.java.template";

            return File.ReadAllText(path);
        }

        private IEnumerable<string> GetFiles()
        {
            return Directory.GetFiles(input, "sect*.xml");
        }

        protected virtual void Write(string result)
        {
            var path = input + "SectionFactory.java";

            File.WriteAllText(path, result, Encoding.UTF8);
        }
    }
}
