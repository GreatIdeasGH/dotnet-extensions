namespace GreatIdeas.Extensions;

public static class ContentTypes
{

    public static string? GetContentType(this string fileName)
    {
        return string.Empty;
    }

    public static string GetImageContent(this string contentType, byte[] content) =>
        "data:" + contentType + ";base64," + Convert.ToBase64String(content);
}