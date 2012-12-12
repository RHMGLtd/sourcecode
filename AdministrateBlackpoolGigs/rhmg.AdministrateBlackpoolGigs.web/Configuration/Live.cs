using System;
using coolbunny.common;

namespace rhmg.AdministrateBlackpoolGigs.web.Configuration
{
    public class Live : BaseConfig
    {
        public override ApplicationEnvironment Environment
        {
            get { return ApplicationEnvironment.Live; }
        }

        public override string WebPath
        {
            get { return "http://admin.blackpoolgigs.co.uk"; }
        }
    }
}