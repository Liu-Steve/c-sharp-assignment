using Xunit;
using BusHelper.Service;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.IO;

namespace BusTest;

public class AuthTest1
{
    private AuthService service;

    public AuthTest1()
    {
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