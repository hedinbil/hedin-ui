namespace Hedin.UI.Demo.Data
{
    public class CodeBlockService
    {
        public async Task<string> GetCodeExample(Type exampleComponentType)
        {
            var asm = exampleComponentType.Assembly;
            var all = asm.GetManifestResourceNames();

            // Build the *exact* resource name we expect:
            //   <ROOT_NS>.<folder>.<subfolder>....<ComponentFolder>.Examples.<ExampleName>.razor
            var candidate = $"{exampleComponentType.Namespace}.{exampleComponentType.Name}.razor";

            // Try an exact match first
            var resourceName = all
              .FirstOrDefault(rn =>
                 string.Equals(rn, candidate, StringComparison.OrdinalIgnoreCase)
              )
              // Fallback to the old EndsWith if somebody embedded with a different prefix:
              ?? all.FirstOrDefault(rn =>
                 rn.EndsWith($".{exampleComponentType.Name}.razor",
                             StringComparison.OrdinalIgnoreCase)
              );

            if (resourceName == null)
                return $"// No example source for {exampleComponentType.FullName}";

            await using var stream = asm.GetManifestResourceStream(resourceName)!;
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }

        public async Task<string> GetCodeExample(string componentName)
        {
            var codeExample = $"";
            try
            {
                var fileName = $"{componentName}.razor.txt";
                var workingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var hdDirectoryInWhichToSearch = new DirectoryInfo(Path.Combine(workingDirectory, "Pages", "Components"));

                var fileContent = ReadFileInDirectory(hdDirectoryInWhichToSearch, fileName);
                if (!string.IsNullOrEmpty(fileContent))
                    codeExample = fileContent;
                else
                {
                    var dirsInDir = hdDirectoryInWhichToSearch.GetDirectories();
                    foreach (var foundDir in dirsInDir)
                    {
                        fileContent = ReadFileInDirectory(foundDir, fileName);
                        if (!string.IsNullOrEmpty(fileContent))
                        {
                            codeExample = fileContent;
                            break;
                        }
                    }
                }

                return await Task.FromResult(codeExample);
            }
            catch (Exception)
            {
                return await Task.FromResult(codeExample);
            }
        }
        private string? ReadFileInDirectory(DirectoryInfo directory, string fileName)
        {
            var fileInMainDirectory = directory.GetFiles(fileName).FirstOrDefault();
            if (fileInMainDirectory != null)
            {
                return File.ReadAllText(fileInMainDirectory.FullName);
            }
            return null;
        }
    }
}
