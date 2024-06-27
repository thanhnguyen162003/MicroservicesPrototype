using PlatformService.Entities;
using PlatformService.Interfaces;

namespace PlatformService.Services;

public class PlatformService : IPlatformService
{
    private readonly IPlatformRepository _platformRepository;

    public PlatformService(IPlatformRepository platformRepository)
    {
        _platformRepository = platformRepository;
    }

    public bool SaveChanges()
    {
        return _platformRepository.SaveChanges();
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        return _platformRepository.GetAllPlatforms();
    }

    public Platform GetPlatformById(int id)
    {
        return _platformRepository.GetPlatformById(id);
    }

    public void CreatePlatform(Platform plat)
    {
         _platformRepository.CreatePlatform(plat);
    }
}