using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LoneWolf.Migration.Code
{
    public abstract class GeneratorBase : IGenerator
    {
        private string Pattern { get; set; }
        private string Output { get; set; }

        protected GeneratorBase(string pattern, string output)
        {
            Pattern = pattern;
            Output = output;
        }

        public IEnumerable<string> Generate(string[] input)
        {
            var rx = new Regex(Pattern, RegexOptions.Compiled);

            return (from line in input
                    from Match match in rx.Matches(line)
                    select match.Groups
                        into groups
                        select string.Format(Output, GetValues(groups).ToArray())).ToList();
        }

        protected virtual IEnumerable<string> GetValues(GroupCollection groups)
        {
            for (var i = 1; i < groups.Count; i++)
            {
                yield return groups[i].Value;
            }
        }
    }
}
