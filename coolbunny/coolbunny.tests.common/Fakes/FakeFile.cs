using System;
using System.IO;
using OpenFileSystem.IO;
using Path = OpenFileSystem.IO.Path;

namespace coolbunny.tests.common.Fakes
{
    public class FakeFile :IFile
    {

        public IFile Create()
        {
            return new FakeFile();
        }

        public IFile Create(string name)
        {
            return new FakeFile
                       {
                           Name = name,
                           NameWithoutExtension = name.Split('.')[0],
                           LastModifiedTimeUtc = DateTime.Today
                       };
        }

        public void Delete()
        {

        }

        public void CopyTo(IFileSystemItem item)
        {
            throw new NotImplementedException();
        }

        public void MoveTo(IFileSystemItem item)
        {
            throw new NotImplementedException();
        }

        public Path Path { get; private set; }
        public IDirectory Parent { get; private set; }
        public IFileSystem FileSystem { get; private set; }
        public bool Exists { get; private set; }
        public string Name { get; private set; }

        
        public Stream Open(FileMode fileMode, FileAccess fileAccess, FileShare fileShare)
        {
            throw new NotImplementedException();
        }

        public string NameWithoutExtension { get; private set; }
        public string Extension { get; private set; }
        public long Size { get; private set; }
        public DateTime? LastModifiedTimeUtc { get; private set; }
    }
}