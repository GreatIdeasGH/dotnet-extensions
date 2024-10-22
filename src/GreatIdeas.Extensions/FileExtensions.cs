namespace GreatIdeas.Extensions;

public static class FileExtensions
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

    public static DirectoryInfo? GetSubDirectory(
        string directory,
        string subDirectory,
        string endsWith
    )
    {
        var currentDir = GetRootDirectory(endsWith)!.FullName;
        return new DirectoryInfo(Path.Combine(currentDir, directory, subDirectory));
    }

    private static readonly Dictionary<string, string> ContentTypes =
        new()
        {
            // Text
            { ".txt", "text/plain" },
            { ".csv", "text/csv" },
            { ".json", "application/json" },
            { ".xml", "application/xml" },
            // Documents
            { ".pdf", "application/pdf" },
            { ".doc", "application/msword" },
            { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
            { ".xls", "application/vnd.ms-excel" },
            { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
            { ".ppt", "application/vnd.ms-powerpoint" },
            {
                ".pptx",
                "application/vnd.openxmlformats-officedocument.presentationml.presentation"
            },
            // Images
            { ".png", "image/png" },
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".gif", "image/gif" },
            { ".bmp", "image/bmp" },
            { ".svg", "image/svg+xml" },
            { ".webp", "image/webp" },
            // Audio
            { ".mp3", "audio/mpeg" },
            { ".wav", "audio/wav" },
            { ".ogg", "audio/ogg" },
            { ".flac", "audio/flac" },
            { ".aac", "audio/aac" },
            { ".m4a", "audio/m4a" },
            { ".wma", "audio/x-ms-wma" },
            // Video
            { ".mp4", "video/mp4" },
            { ".avi", "video/x-msvideo" },
            { ".mov", "video/quicktime" },
            { ".wmv", "video/x-ms-wmv" },
            { ".flv", "video/x-flv" },
            { ".webm", "video/webm" },
            // Archives
            { ".zip", "application/zip" },
            { ".rar", "application/x-rar-compressed" },
            { ".7z", "application/x-7z-compressed" },
            { ".apk", "application/vnd.android.package-archive" },
        };

    public static string GetContentType(this string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        return ContentTypes.TryGetValue(extension, out var contentType)
            ? contentType
            : "application/octet-stream"; // Default to binary if type is unknown
    }

    public static string ImageContent(string contentType, byte[] content)
    {
        return $"data:{contentType};base64,{Convert.ToBase64String(content)}";
    }
}
