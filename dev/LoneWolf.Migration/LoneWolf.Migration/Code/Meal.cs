namespace LoneWolf.Migration.Code
{
    public class Meal : GeneratorBase
    {
        public Meal()
            : base(
                @"^.*<p>.*Meal.*</p>$",
                @".add(new Meal(""Food""), TODO)")
        {
        }
    }
}
// one Meal
// two Meals
// must .* a Meal
