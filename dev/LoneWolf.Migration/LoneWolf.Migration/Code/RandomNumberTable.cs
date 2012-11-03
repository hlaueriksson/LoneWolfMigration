namespace LoneWolf.Migration.Code
{
    public class RandomNumberTable : GeneratorBase
    {
        public RandomNumberTable()
            : base(
                @"^.*<p class=""choice"" id=""(.*)"">.*([0-9])–([0-9]).*$",
                @".when(new RandomNumberIsNotBetween({1}, {2}).then(new DisableChoice(""{0}"")))")
        {
        }
    }
}
