using System;
using coolbunny.common;
using coolbunny.common.Interfaces;

namespace blackpoolgigs.web.Configuration
{
    public abstract class BaseConfig : IEnvironmentConfiguration
    {
        public abstract ApplicationEnvironment Environment { get; }

        public string WebPath
        {
            get { throw new NotImplementedException(); }
        }
    }
    public class Live : BaseConfig
    {
        public override ApplicationEnvironment Environment
        {
            get { return ApplicationEnvironment.Live; }
        }
    }
    public class Local : BaseConfig
    {
        public override ApplicationEnvironment Environment
        {
            get { return ApplicationEnvironment.Local; }
        }
    }
    public class Development : BaseConfig
    {
        public override ApplicationEnvironment Environment
        {
            get { return ApplicationEnvironment.Development; }
        }
    }
}