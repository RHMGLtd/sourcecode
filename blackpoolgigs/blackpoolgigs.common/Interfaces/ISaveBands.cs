using blackpoolgigs.common.Models;

namespace blackpoolgigs.common.Interfaces
{
    public interface ISaveBands
    {
        BandMetadata Save(BandMetadata band);
    }
}