using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
using PlatformService.Entities;
using PlatformService.Interfaces;
using PlatformService.Models;

namespace PlatformService.Controllers;

[ApiController]
[Route("api/platform")]
public class PlatformController : ControllerBase
{
    private readonly IPlatformService _platformService;
    private readonly IMapper _mapper; 
    private readonly ICommandDataClient _commandDataClient;
    private readonly IMessageBusClient _messageBusClient;

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
    
    [HttpGet("{id}", Name = "GetPlatformById")]
    public ActionResult<PlatformReadModel> GetPlatformById(int id)
    {
        var platformItem = _platformService.GetPlatformById(id);
        if (platformItem != null)
        {
            return Ok(_mapper.Map<PlatformReadModel>(platformItem));
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<PlatformReadModel>> CreatePlatform(PlatformReadModel platformCreateDto)
    {
        var platformModel = _mapper.Map<Platform>(platformCreateDto);
        _platformService.CreatePlatform(platformModel);
        _platformService.SaveChanges();

        var platformReadDto = _mapper.Map<PlatformReadModel>(platformModel);

        // Send Sync Message
        try
        {
            await _commandDataClient.SendPlatformToCommand(platformReadDto);
        }
        catch(Exception ex)
        {
            Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
        }
        
        //Send Async Message
        try
        {
            var platformPublishedDto = _mapper.Map<PlatformPublishedModel>(platformReadDto);
            platformPublishedDto.Event = "Platform_Published";
            _messageBusClient.PublishNewPlatform(platformPublishedDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
        }
        // return the location in header, the result is baseURL and the /id that we have already create
        return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id}, platformReadDto);
    }
}
