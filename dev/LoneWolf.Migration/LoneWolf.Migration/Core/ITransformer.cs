using System.Xml.Linq;

namespace LoneWolf.Migration.Core
{
    public interface ITransformer
    {
        string Transform(XDocument document);
    }
}
