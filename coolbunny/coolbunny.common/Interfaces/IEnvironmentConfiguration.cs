namespace coolbunny.common.Interfaces
{
    public interface IEnvironmentConfiguration
    {
        ApplicationEnvironment Environment { get; }
        string WebPath { get; }
    }
}