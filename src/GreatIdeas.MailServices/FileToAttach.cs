namespace GreatIdeas.MailServices;

public class FileToAttach
{
    public string? FileName { get; set; }
    public int FileSize { get; set; }
    public byte[]? Bytes { get; set; }
    public string? FilePath { get; set; }
}