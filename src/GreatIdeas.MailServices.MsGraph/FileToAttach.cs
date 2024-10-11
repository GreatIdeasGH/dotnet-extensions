namespace GreatIdeas.MailServices.MsGraph;

public record FileToAttach(string FileName, int FileSize, byte[] Bytes, string FilePath);
