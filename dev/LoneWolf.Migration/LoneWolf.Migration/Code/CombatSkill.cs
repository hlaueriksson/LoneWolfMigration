namespace LoneWolf.Migration.Code
{
    public class CombatSkill : GeneratorBase
    {
        public CombatSkill()
            : base(
                @"^.*([0-9]) point.*<span class=""smallcaps"">COMBAT SKILL</span>.*$",
                @".add(new ModifyCombatSkill({0}))")
        {
        }
    }
}
