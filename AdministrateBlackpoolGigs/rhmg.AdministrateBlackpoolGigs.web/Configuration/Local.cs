using coolbunny.common;

namespace rhmg.AdministrateBlackpoolGigs.web.Configuration
{
    public class Local : BaseConfig
    {
        public override ApplicationEnvironment Environment
        {
            get { return ApplicationEnvironment.Local; }
        }

        public override string WebPath
        {
            get { return "http://local.administrateblackpoolgigs"; }
        }
    }
}