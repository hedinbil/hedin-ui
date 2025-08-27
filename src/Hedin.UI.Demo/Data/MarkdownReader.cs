namespace Hedin.UI.Demo.Data
{
    public class MarkdownReader
    {
        public string GettingStartedText { get; private set; }
        public MarkdownReader() 
        {
            const string fileName = "getting-started.md";
            try
            {
                var workingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var completeFilePath = Path.Combine(workingDirectory, fileName);
                GettingStartedText = $"{File.ReadAllText(completeFilePath)}";
            }
            catch
            {
                GettingStartedText = $"Error, could not find {fileName}";
            }
        }
    }
}
