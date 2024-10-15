using Microsoft.AspNetCore.Mvc;
using GDPRTask.Service.Services;
using GDPRTask.Data.Model;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] User user)
    {
        if (user == null)
        {
            return BadRequest("User cannot be null.");
        }

        // Validate the model (if you're using model validation)
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _userService.AddUser(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user); // Ensure GetUser is a valid method
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(string id)
    {
        var user = _userService.GetUser(id);
        if (user == null)
        {
            return NotFound();
        }
        return user;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAllUsers()
    {
        var users = _userService.GetAllUsers(); // Ensure this method exists in your IUserService
        return Ok(users); // Return the list of users with a 200 OK status
    }

    // DELETE: api/User/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(string id)
    {
        var user = _userService.GetUser(id);
        if (user == null)
        {
            return NotFound(); // User not found
        }

        _userService.DeleteUser(id); // Call the service to delete the user
        return NoContent(); // Return 204 No Content on successful deletion
    }

    // PUT: api/User/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateUser(string id, [FromBody] User user)
    {
        if (user == null)
        {
            return BadRequest("User cannot be null.");
        }

        // Validate the model (if you're using model validation)
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Check if the user exists
        var existingUser = _userService.GetUser(id);
        if (existingUser == null)
        {
            return NotFound(); // User not found
        }

        // Update the user
        user.Id = id; // Ensure the ID is set correctly for the update
        _userService.UpdateUser(user); // Call the service to update the user

        return NoContent(); // Return 204 No Content on successful update
    }
    // Other action methods...
}
