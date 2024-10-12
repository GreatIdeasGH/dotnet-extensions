using GreatIdeas.MailServices;
using GreatIdeas.MailServices.MsGraph;
using NSubstitute;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace GreatIdeas.Extensions.Tests.MailServices;

public class MsGraphTests(ITestOutputHelper testOutputHelper)
{
    private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;
    private readonly IMsGraphService _msGraphService = Substitute.For<IMsGraphService>();


    [Fact]
    public async Task Send_Email_Succeed()
    {
        var emailModel = new EmailModel { FromAddress = "testmail@email.com", To = "receipt@email.com", Body = "Hello there", Subject = "Greetings!" };
        var result = await _msGraphService.SendEmailAsync(emailModel);

        _testOutputHelper.WriteLine(JsonSerializer.Serialize(emailModel));

        Assert.True(result);
    }
}
