using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class TestUserController : ControllerBase
{
    private readonly ITestService _testService;

    public TestUserController(ITestService testService)
    {
        _testService = testService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var users = _testService.Get();
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TestUser testUser)
    {
        var createdUser = await _testService.Post(testUser);
        return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] TestUser updatedTestUser)
    {
        var updatedUser = await _testService.Put(id, updatedTestUser);
        if (updatedUser == null)
        {
            return NotFound();
        }
        return Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedUser = await _testService.Delete(id);
        if (deletedUser == null)
        {
            return NotFound();
        }
        return Ok(deletedUser);
    }
}

