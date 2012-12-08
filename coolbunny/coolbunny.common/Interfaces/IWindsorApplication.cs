using Castle.Windsor;

namespace coolbunny.common.Interfaces
{
    public interface IWindsorApplication
    {
        IWindsorContainer GetContainer();
        IWindsorContainer CreateContainer();
        void DoInstallation(ApplicationEnvironment environment);
    }
}