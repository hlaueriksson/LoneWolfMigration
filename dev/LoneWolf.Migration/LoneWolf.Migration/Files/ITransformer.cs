using System.Xml.Linq;

namespace LoneWolf.Migration.Files
{
    public interface ITransformer
    {
        string Transform(XDocument document);
    }

    public interface ISubTransformer
    {
        XElement Transform(XElement document);
    }
}
