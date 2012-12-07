using System.Collections.Generic;

namespace blackpoolgigs.common.Interfaces
{
    public interface IAccessTheFileSystem
    {
        T ReadOut<T>(string fileName, params T[] errorReturn) where T : new();
        IEnumerable<T> ReadOutList<T>(string fileName) where T : new();
    }
}