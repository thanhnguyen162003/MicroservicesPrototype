using System.Threading.Tasks;
using PlatformService.Models;

namespace PlatformService.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadModel plat); 
    }
}