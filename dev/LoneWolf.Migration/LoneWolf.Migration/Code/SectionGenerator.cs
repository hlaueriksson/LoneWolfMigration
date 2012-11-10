using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoneWolf.Migration.Code
{
    public static class SectionGenerator
    {
        private const string Indentation = "    ";

        public static string Generate(string section, IEnumerable<string> input)
        {
            var lines = Generate(input);

            if (!lines.Any()) return string.Empty;

            var sb = new StringBuilder();

            sb.Append(Indentation);
            sb.Append(Indentation);
            sb.AppendFormat("manager.add(new Section(\"{0}\")", section);

            foreach (var line in lines)
            {
                if (lines.Count() > 1)
                {
                    sb.AppendLine();
                    sb.Append(Indentation);
                    sb.Append(Indentation);
                    sb.Append(Indentation);
                    sb.Append(Indentation);
                }

                sb.Append(line);
            }

            sb.Append(");");
            sb.AppendLine();
            sb.AppendLine();

            return sb.ToString();
        }

        private static IEnumerable<string> Generate(IEnumerable<string> input)
        {
            var result = new List<string>();

            foreach (var generator in GetGenerators())
            {
                result.AddRange(generator.Generate(input));
            }

            return result;
        }

        private static IEnumerable<IGenerator> GetGenerators()
        {
            return new List<IGenerator> {
                new KaiDiscipline(),
                new RandomNumberTable(),
                new Combat(),
                new CombatSkill(),
                new GoldCrowns(),
                new Meal(),
                new Weapon()
            };
        }
    }
}
// TODO: If you possess a Vordak Gem