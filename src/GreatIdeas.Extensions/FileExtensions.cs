namespace GreatIdeas.Extensions;

public class FileExtensions
{
    /// <summary>
    /// Returns a FileInfo with the full path of the requested file
    /// </summary>
    /// <param name="directory">A subdirectory</param>
    /// <param name="file"></param>
    /// <param name="endsWith"></param>
    /// <returns></returns>
    public static FileInfo GetFileInfo(string directory, string file, string endsWith)
    {
        var rootDir = GetRootDirectory(endsWith)!.FullName;
        return new FileInfo(Path.Combine(rootDir, directory, file));
    }

    private static DirectoryInfo? GetRootDirectory(string endWith)
    {
        var currentDir = AppDomain.CurrentDomain.BaseDirectory;
        while (currentDir != null && !currentDir.EndsWith(endWith))
        {
            currentDir = Directory.GetParent(currentDir)?.FullName.TrimEnd('\\');
        }
        return new DirectoryInfo(currentDir!).Parent;
    }

    public static DirectoryInfo? GetSubDirectory(string directory, string subDirectory, string endsWith)
    {
        var currentDir = GetRootDirectory(endsWith)!.FullName;
        return new DirectoryInfo(Path.Combine(currentDir, directory, subDirectory));
    }
}