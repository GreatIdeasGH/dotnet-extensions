using Microsoft.AspNetCore.StaticFiles;

namespace GreatIdeas.Extensions.AspNetCore;

public static class ContentTypes
{
    private static readonly FileExtensionContentTypeProvider Provider = new();

    public static string GetContentType(this string fileName)
    {
        if (!Provider.TryGetContentType(fileName, out var contentType))
            contentType = "application/octet-stream";
        return contentType;
    }

    public static string GetImageContent(this string contentType, byte[] content) =>
        "data:" + contentType + ";base64," + Convert.ToBase64String(content);
}