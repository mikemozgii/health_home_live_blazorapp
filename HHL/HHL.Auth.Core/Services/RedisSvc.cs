using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace HHL.Auth.Core.Services
{
   public  class RedisSvc
    {
        private readonly ConnectionMultiplexer _redisConnection;
        private readonly IDatabase _redisDB;
        private readonly TimeSpan _redisDefaultExpiry;
        private readonly EndPoint[] _endPoints;

        public RedisSvc(string connectionString, string password, int dataBaseNum, TimeSpan defaultExpiry)
        {
            var options = ConfigurationOptions.Parse(connectionString);
            //TODO reduce it
            var minute = 60000;

            options.AllowAdmin = true;
            options.AbortOnConnectFail = false;

            options.SyncTimeout = minute;
            options.ConnectTimeout = minute;

            if (!string.IsNullOrWhiteSpace(password))
            {
                options.Password = password;
            }


            _redisConnection = ConnectionMultiplexer.Connect(options);
            _redisDB = _redisConnection.GetDatabase(dataBaseNum, true);
            _redisDefaultExpiry = defaultExpiry;
            _endPoints = _redisConnection.GetEndPoints(true);
        }


        public async Task<bool> SetValueAsync(string key, string value, TimeSpan? expiryTime = null)
        {
            try
            {

                return await _redisDB.StringSetAsync(key, value, expiryTime ?? _redisDefaultExpiry);
            }
            catch (Exception)
            {

                return false;
            }


        }

        public bool SetValue(string key, string value)
        {
            try
            {

                return _redisDB.StringSet(key, value, _redisDefaultExpiry);
            }
            catch (Exception ex)
            {

                return false;
            }


        }

        public async Task<bool> SetValueAsync(string key, string value, TimeSpan expiryTime)
        {
            try
            {

                return await _redisDB.StringSetAsync(key, value, expiryTime);
            }
            catch (Exception)
            {

                return false;
            }


        }
        public bool SetValue(string key, string value, TimeSpan expiryTime)
        {
            try
            {

                return _redisDB.StringSet(key, value, expiryTime);
            }
            catch (Exception)
            {

                return false;
            }


        }

        public async Task<string> GetValueAsync(string key)
        {
            try
            {
                return await _redisDB.StringGetAsync(key);
                //return RedisDB.StringGetAsync(key).Result;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public string GetValue(string key)
        {
            try
            {
                return _redisDB.StringGet(key);
            }
            //todo 401's bugs. catch Sytstem.TimeoutException, set timer, try again.
            catch (Exception)
            {
                throw;
                return null;
            }
        }

        public bool DeleteValue(string key)
        {

            try
            {
                _redisDB.KeyDelete(key);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<bool> DeleteValueAsync(string key)
        {

            try
            {
                await _redisDB.KeyDeleteAsync(key);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool DeleteValue_StartWith(string pattern)
        {
            try
            {

                foreach (var endpoint in _endPoints)
                {
                    var server = _redisConnection.GetServer(endpoint);
                    var keys = server.Keys(pattern: pattern + "*");
                    if (keys != null && keys.Any())
                    {
                        foreach (var key in keys)
                        {
                            _redisDB.KeyDelete(key);
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
