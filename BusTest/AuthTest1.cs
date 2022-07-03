using Xunit;
using BusHelper.Service;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.IO;

namespace BusTest;

public class AuthTest1
{
    private Mock<IConfigurationSection> confSection;
    private AuthService service;

    public AuthTest1()
    {
        confSection = new Mock<IConfigurationSection>();
        //实际上线需更换SecretKey
        confSection.SetupGet(_ => _["SecretKey"]).Returns("b037b073ceb0c08fb3bce5f9c740d2a9a867696a");
        confSection.SetupGet(_ => _["Issuer"]).Returns("bus-safengine");
        confSection.SetupGet(_ => _["JWT:Expires"]).Returns("10");
        confSection.SetupGet(_ => _["Audience"]).Returns("admin");

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        string path = System.IO.Directory.GetCurrentDirectory();

        var configuration = new ConfigurationBuilder()
            .AddJsonFile($"./appsettings.json")
            .Build();
        service = new AuthService(configuration);
    }

    //[Fact]
    [Theory]
    [InlineData("apple")]
    [InlineData("")]
    [InlineData(null)]
    public void Test1(string name)
    {
        service.CreateToken(name);
    }


}