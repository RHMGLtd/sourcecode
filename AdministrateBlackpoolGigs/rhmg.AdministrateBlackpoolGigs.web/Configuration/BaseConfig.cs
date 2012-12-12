using coolbunny.common;
using coolbunny.common.Interfaces;

namespace rhmg.AdministrateBlackpoolGigs.web.Configuration
{
    public abstract class BaseConfig : IEnvironmentConfiguration
    {
        public abstract ApplicationEnvironment Environment { get; }
        public abstract string WebPath { get; }
    }
}