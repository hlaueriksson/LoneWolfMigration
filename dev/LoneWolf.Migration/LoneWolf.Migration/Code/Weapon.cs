using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LoneWolf.Migration.Code
{
    public class Weapon : GeneratorBase
    {
        public Weapon()
            : base(
                @"^.*<p>.*(Axe)|(Broadsword)|(Dagger)|(Mace)|(Quarterstaff)|(Short Sword)|(Spear)|(Sword)|(Warhammer).*</p>$",
                @".add(new Weapon(""{0}""))")
        {
        }

        protected override IEnumerable<string> GetValues(GroupCollection groups)
        {
            for (var i = 1; i < groups.Count; i++)
            {
                if (groups[i].Success) return new[] {groups[i].Value};
            }

            return new string[0];
        }
    }
}
