using PlatformService.Models;

namespace PlatformService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewPlatform(PlatformPublishedModel platformPublishedDto);
    }
}