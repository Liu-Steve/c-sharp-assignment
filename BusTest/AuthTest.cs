using Xunit;
using BusHelper.Service;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using BusHelper.Models;

namespace BusTest;

public class AuthTest
{
    private AuthService service;

    public AuthTest()
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
    //输入关键字为正常时，可以正常生成token
    public void TokenTest(string name)
    {
        service.CreateToken(name);
    }

    [Theory]
    [InlineData(null)]
    public void NullTest(string name)
    {
        try{
            service.CreateToken(name);
        }
        catch(ArgumentNullException ex)
        {
            Assert.IsType<ArgumentNullException>(ex);
        }
    }

    [Theory]
    [InlineData(null)]
    //测试登录时空对象处理
    public void VaildTest(Manager manager)
    {
        try{
            service.Vaild(manager);
        }
        catch(InvalidOperationException ex)
        {
            Assert.IsType<InvalidOperationException>(ex);
        }     
    }

    [Theory]
    [InlineData("steve","2lp")]
    public void InvalidAccountTest(string account,string pwd)
    {
        Manager m=new Manager();
        m.ManagerId=account;
        m.Pwd=pwd;
        bool re=service.Vaild(m);
        Assert.Equal(false,re);
    }

    [Theory]
    [InlineData("bbg","123456")]
    public void ValidAccountTest(string account,string pwd)
    {
        Manager m=new Manager();
        m.ManagerId=account;
        m.Pwd=pwd;
        bool re=service.Vaild(m);
        Assert.Equal(true,re);
    }

    [Theory]
    [InlineData("bbg","2lg")]
    public void PwdErrorTest(string account,string pwd)
    {
        Manager m=new Manager();
        m.ManagerId=account;
        m.Pwd=pwd;
        bool re=service.Vaild(m);
        Assert.Equal(false,re);
    }
}