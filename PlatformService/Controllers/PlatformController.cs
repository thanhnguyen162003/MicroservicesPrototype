using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Interfaces;
using PlatformService.Models;

namespace PlatformService.Controllers;

[ApiController]
[Route("api/platform")]
public class PlatformController : ControllerBase
{
    private readonly IPlatformService _platformService;
    private readonly IMapper _mapper;
    public PlatformController(IPlatformService platformService, IMapper mapper)
    {
        _platformService = platformService;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadModel>> GetPlatforms()
    {
        Console.WriteLine("-->Getting platform--");
        var platforms = _platformService.GetAllPlatforms();
        var mapper = _mapper.Map<IEnumerable<PlatformReadModel>>(platforms);
        return Ok(mapper);
    }
}