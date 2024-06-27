using PlatformService.Entities;

namespace PlatformService.Interfaces;

public interface IPlatformService
{
    bool SaveChanges();

    IEnumerable<Platform> GetAllPlatforms();
    Platform GetPlatformById(int id);
    void CreatePlatform(Platform plat);
}