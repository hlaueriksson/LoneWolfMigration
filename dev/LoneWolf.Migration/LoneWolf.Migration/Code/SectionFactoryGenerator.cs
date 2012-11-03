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

                var result = Generate(file);

                if(!string.IsNullOrEmpty(result))
                {
                    var number = Convert.ToInt32(file.Name.Replace("sect", string.Empty).Replace(".xml", string.Empty));

                    sb.Append("\t\t");
                    sb.AppendFormat("manager.add(new Section(\"{0}\")", number);

                    sb.Append(result);

                    sb.Append(");");

                    sb.AppendLine();
                    sb.AppendLine();
                }
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

        private string Generate(FileInfo file)
        {
            var sb = new StringBuilder();

            var lines = File.ReadAllLines(file.FullName);

            foreach (var generator in GetGenerators())
            {
                var result = generator.Generate(lines);

                foreach (var line in result)
                {
                    sb.Append(line);
                }
            }

            return sb.ToString();
        }

        protected virtual void Write(string result)
        {
            var path = input + "SectionFactory.cs";

            File.WriteAllText(path, result, Encoding.UTF8);
        }

        private IEnumerable<IGenerator> GetGenerators()
        {
            return new List<IGenerator> {
                new KaiDiscipline(),
                new RandomNumberTable(),
                new Combat(),
                new CombatSkill()
            };
        }
    }
}
