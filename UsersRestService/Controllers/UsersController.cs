using Microsoft.AspNetCore.Mvc;
using UsersRestService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UsersRestService.Controllers
{

    /// <summary>
    /// This allows operations on User objects
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authenticator]
    public class UsersController : ControllerBase
    {
        public static List<User> users = new List<User>();

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns>List of Users</returns>
        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(users);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var user = users.Where((user) => user.Id == id).First();
                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new ApiResponse(404, $"No User exists with id: {id}"));
            }
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if ((user.Email == null || user.Email == "")
                && (user.Password == null || user.Password == ""))
            {
                return BadRequest("Error: Email and Password fields are missing");
            }

            if (user.Email == null || user.Email == "")
            {
                return BadRequest("Error: Email field is missing");
            }

            if (user.Password == null || user.Password == "")
            {
                return BadRequest("Error: Password field is missing");
            }

            DateTime dateTime = DateTime.Now;
            user.DateAdded = dateTime;

            int newId;
            if(users.Count == 0)
            {
                newId = 1;
            }
            else
            {
                newId = users.Select(c => c.Id).ToList().Max() + 1;
            }

            user.Id = newId;
            users.Add(user);

            return CreatedAtAction(nameof(Get), new { id = newId }, user);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User updatedUser)
        {
            if (updatedUser.Id == 0 || updatedUser.Id == id)
            {

                var existingUser = users.FirstOrDefault(c => c.Id == id);

                if (existingUser == null)
                {
                    return NotFound();
                }

                existingUser.Email = updatedUser.Email;
                existingUser.Password = updatedUser.Password;

                return Ok(existingUser);
            }

            return Conflict();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int numberOfElementsRemoved = users.RemoveAll((user) => user.Id == id);
            if (numberOfElementsRemoved <= 0)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
