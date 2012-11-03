using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LoneWolf.Migration.Code
{
    public class KaiDiscipline : GeneratorBase
    {
        public KaiDiscipline()
            : base(
                @"^.*<p class=""choice"" id=""(.*)"">.*Kai Discipline of (.*),.*$",
                @".when(new KaiDisciplineIsNotAcquired(KaiDiscipline.{1}).then(new DisableChoice(""{0}"")))")
        {
        }

        protected override IEnumerable<string> GetValues(GroupCollection groups)
        {
            return new[] { groups[1].Value, groups[2].Value.Replace(" ", string.Empty) };
        }
    }
}
