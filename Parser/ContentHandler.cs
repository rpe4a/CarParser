using System.IO;

namespace Parser
{
    internal class ContentHandler : IContentHandler
    {
        public void Handle(string content)
        {
            File.WriteAllText(@"cars.txt", content);
        }
    }
}