using System.Collections.Generic;

namespace LoneWolf.Migration.Code
{
    public interface IGenerator
    {
        IEnumerable<string> Generate(IEnumerable<string> input);
    }
}
