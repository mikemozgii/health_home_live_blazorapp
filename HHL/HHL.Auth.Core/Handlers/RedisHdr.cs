using HHL.Auth.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.Auth.Core.Handlers
{
    public static class RedisHdr
    {
        public static string Redis_Server = "192.168.1.25";
        public static int Redis_Port = 6379;
        public static string Redis_Password = "";

        public static RedisSvc redisStorage { get; set; } =

        redisStorage = new RedisSvc($"{Redis_Server}:{Redis_Port}", Redis_Password, 0, TimeSpan.FromDays(1));
    }
}
