﻿using Microsoft.Extensions.Configuration;

namespace CORE
{
    public class ConfigurationManager
    {
        private static IConfiguration _configuration;

        public ConfigurationManager(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        public static string Get(string key)
        {
            return _configuration[key];
        }

        public static string GetConnectionString(string key)
        {
            return _configuration.GetSection("DbConfig:DbConnectionStrings")[key];
        }

        public static string GetConnectionString()
        {
            return GetConnectionString(Get("ConnStrName"));
        }
    }
}
