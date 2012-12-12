using coolbunny.common;

namespace rhmg.AdministrateBlackpoolGigs.web.Configuration
{
    public class Development : BaseConfig
    {
        public override ApplicationEnvironment Environment
        {
            get { return ApplicationEnvironment.Development; }
        }

        public override string WebPath
        {
            get { return "http://local.administrateblackpoolgigs"; }
        }
    }
}