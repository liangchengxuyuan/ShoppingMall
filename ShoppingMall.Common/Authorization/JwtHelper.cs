﻿using Microsoft.IdentityModel.Tokens;
using ShoppingMall.Common.Cache;
using ShoppingMall.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShoppingMall.Common.Authorization
{
    /// <summary>
    /// JWT帮助类
    /// </summary>
    public class JwtHelper
    {
        /// <summary>
        /// 获取JWT字符串并存入缓存
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <param name="expiresSliding"></param>
        /// <returns></returns>
        public static string IssueJWT(JwtToken tokenModel, TimeSpan expiresSliding)
        {
            DateTime UTC = DateTime.UtcNow;
            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,tokenModel.Sub),//Subject,
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),//JWT ID,JWT的唯一标识
                new Claim(JwtRegisteredClaimNames.Iat, UTC.ToString(), ClaimValueTypes.Integer64),//Issued At，JWT颁发的时间，采用标准unix时间，用于验证过期
            };

            JwtSecurityToken jwt = new JwtSecurityToken(
            issuer: "ShoppingMall",//jwt签发者,非必须
            audience: tokenModel.Phone,//jwt的接收该方，非必须
            claims: claims,//声明集合
            expires: UTC.AddHours(12),//指定token的生命周期，unix时间戳格式,非必须
            signingCredentials: new Microsoft.IdentityModel.Tokens
                .SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ShoppingMall's Secret Key")), SecurityAlgorithms.HmacSha256));//使用私钥进行签名加密

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);//生成最后的JWT字符串

            MemoryCacheHelper.AddMemoryCache(encodedJwt, tokenModel, expiresSliding, TimeSpan.FromDays(1));//将JWT字符串和tokenModel作为key和value存入缓存
            return encodedJwt;
        }
    }
}
