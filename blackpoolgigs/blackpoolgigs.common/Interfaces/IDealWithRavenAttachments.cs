using Raven.Abstractions.Data;

namespace blackpoolgigs.common.Interfaces
{
    public interface IDealWithRavenAttachments
    {
        Attachment MainImage(string id);
    }
}