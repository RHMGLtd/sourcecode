using System;
using blackpoolgigs.common.Models;
using blackpoolgigs.filedal.Providers;
using coolbunny.tests.common.contexts;
using Machine.Specifications;
using OpenFileSystem.IO;
using OpenFileSystem.IO.FileSystems.InMemory;

namespace blackpoolgigs.tests
{
    public abstract class with_file_system : with_service<FileSystemCache>
    {
        Establish filesystem = () => autoMocker.Inject(typeof(IFileSystem), new InMemoryFileSystem());
        protected static IFileSystem FileSystem
        {
            get { return autoMocker.Get<IFileSystem>(); }
        }
    }
}
