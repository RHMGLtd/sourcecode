using System;
using System.Configuration;

namespace coolbunny.common
{
    public static class EnvironmentConfigurationHelper
    {
        public static ApplicationEnvironment GetApplicationEnvironment()
        {
            var setting = ConfigurationManager.AppSettings["ApplicationEnvironment"];
            if (string.IsNullOrEmpty(setting))
                return ApplicationEnvironment.Local;

            ApplicationEnvironment environment;

            return Enum.TryParse(setting.Trim(), true, out environment) ? environment : ApplicationEnvironment.Local;
        }

        public static int GetPortNumber()
        {
            var setting = ConfigurationManager.AppSettings["PortNumber"];
            return string.IsNullOrEmpty(setting) ? 3137 : int.Parse(setting);
        }
    }
}