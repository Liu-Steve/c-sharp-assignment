using IdentityModel;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace BusHelper.Service;

//验证token
public class BusService
{
    private readonly IConfiguration configuration;

    public BusService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string CreateToken(string name)
    {
        // 1. 定义需要使用到的Claims
        var claims = new[]
        {
            new Claim("Name", name)
        };

        // 2. 从 appsettings.json 中读取SecretKey
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));

        // 3. 选择加密算法
        var algorithm = SecurityAlgorithms.HmacSha256;

        // 4. 生成Credentials
        var signingCredentials = new SigningCredentials(secretKey, algorithm);

        // 5. 从 appsettings.json 中读取Expires
        var expires = Convert.ToDouble(configuration["JWT:Expires"]);

        // 6. 根据以上，生成token
        var token = new JwtSecurityToken(
            configuration["JWT:Issuer"],     //Issuer
            configuration["JWT:Audience"],   //Audience
            claims,                          //Claims,
            DateTime.Now,                    //notBefore
            DateTime.Now.AddDays(expires),   //expires
            signingCredentials               //Credentials
        );

        // 7. 将token变为string
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        return jwtToken;
    }
}