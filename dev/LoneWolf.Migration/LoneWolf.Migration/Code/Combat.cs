namespace LoneWolf.Migration.Code
{
    public class Combat : GeneratorBase
    {
        public Combat()
            : base(
                @"^.*<button type=""button"" class=""combat"".*>(.*):.*> (.*)   <.*> (.*)<.*$",
                @".set(new Combat().add(new Enemy(""{0}"", {1}, {2})))")
        {
        }
    }
}
