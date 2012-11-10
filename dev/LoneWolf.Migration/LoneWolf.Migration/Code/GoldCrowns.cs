namespace LoneWolf.Migration.Code
{
    public class GoldCrowns : GeneratorBase
    {
        public GoldCrowns()
            : base(
                @"^.*<p>.* ([0-9]{1,2}) Gold Crowns.*</p>$",
                @".add(new GoldCrowns({0}))")
        {
        }
    }
}
