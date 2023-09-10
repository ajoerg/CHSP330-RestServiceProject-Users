using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using UsersRestService.Models;

namespace UserRestService.Tests
{
    public class UserTests
    {
        HttpClient client;

        [SetUp]
        public void Setup()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5189/api/");
        }

        //POST

        [Test]
        public async Task AddUser()
        {
            var newUser = new User
            {
                Email = "test@gmail.com",
                Password = "ABC123DRM"
            };

            var newUserAsJSON = JsonSerializer.Serialize(newUser);

            var postContent = new StringContent(newUserAsJSON,
                Encoding.UTF8, "application/json");

            var postResult = await client.PostAsync("users", postContent);

            Assert.AreEqual(HttpStatusCode.Created, postResult.StatusCode);
        }

        [Test]
        public async Task AddUser_NoEmail()
        {
            var newUser = new User
            {
                Password = "ABC123DRM"
            };

            var newUserAsJSON = JsonSerializer.Serialize(newUser);

            var postContent = new StringContent(newUserAsJSON,
                Encoding.UTF8, "application/json");

            var postResult = await client.PostAsync("users", postContent);

            Assert.AreEqual(HttpStatusCode.BadRequest, postResult.StatusCode);
        }

        [Test]
        public async Task AddUser_NoPassword()
        {
            var newUser = new User
            {
                Email = "test@gmail.com"
            };

            var newUserAsJSON = JsonSerializer.Serialize(newUser);

            var postContent = new StringContent(newUserAsJSON,
                Encoding.UTF8, "application/json");

            var postResult = await client.PostAsync("users", postContent);

            Assert.AreEqual(HttpStatusCode.BadRequest, postResult.StatusCode);
        }

        //PUT

        [Test]
        public async Task UpdateUser()
        {
            var newUser = new User
            {
                Email = "test@gmail.com",
                Password = "ABC123DRM"
            };

            var newUserAsJSON = JsonSerializer.Serialize(newUser);

            var postContent = new StringContent(newUserAsJSON,
                Encoding.UTF8, "application/json");

            var postResult = await client.PostAsync("users", postContent);

            var postResultString = await postResult.Content.ReadAsStringAsync();

            var ob = JsonSerializer.Deserialize<User>(postResultString);
            var id = ob.Id;

            //////
            var update = new User
            {
                Id = id,
                Email = "testupdate@gmail.com",
                Password = "ABC123DRM"
            };

            var updateAsJSON = JsonSerializer.Serialize(update);

            var putContent = new StringContent(updateAsJSON,
                Encoding.UTF8, "application/json");

            var putResult = await client.PutAsync($"users/{id}", putContent);

            Assert.AreEqual(HttpStatusCode.OK, putResult.StatusCode);
        }

        [Test]
        public async Task UpdateUser_NotFound()
        {
            var update = new User
            {
                Id = 100,
                Email = "testupdate@gmail.com",
                Password = "ABC123DRM"
            };

            var updateAsJSON = JsonSerializer.Serialize(update);

            var putContent = new StringContent(updateAsJSON,
                Encoding.UTF8, "application/json");

            var putResult = await client.PutAsync("users/100", putContent);

            Assert.AreEqual(HttpStatusCode.NotFound, putResult.StatusCode);
        }

        [Test]
        public async Task UpdateUser_Conflict()
        {
            var update = new User
            {
                Id = 100,
                Email = "testupdate@gmail.com",
                Password = "ABC123DRM"
            };

            var updateAsJSON = JsonSerializer.Serialize(update);

            var putContent = new StringContent(updateAsJSON,
                Encoding.UTF8, "application/json");

            var putResult = await client.PutAsync("users/150", putContent);

            Assert.AreEqual(HttpStatusCode.Conflict, putResult.StatusCode);
        }

        //GET

        [Test]
        public async Task GetAllUsers()
        {

            var getResult = await client.GetAsync("users");

            Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);
        }

        [Test]
        public async Task GetUserWithValidId()
        {
            var newUser = new User
            {
                Email = "test@gmail.com",
                Password = "ABC123DRM"
            };

            var newUserAsJSON = JsonSerializer.Serialize(newUser);

            var postContent = new StringContent(newUserAsJSON,
                Encoding.UTF8, "application/json");

            var postResult = await client.PostAsync("users", postContent);

            var postResultString = await postResult.Content.ReadAsStringAsync();

            var ob = JsonSerializer.Deserialize<User>(postResultString);
            var id = ob.Id;

            var getResult = await client.GetAsync($"users/{id}");

            Assert.AreEqual(HttpStatusCode.OK, getResult.StatusCode);
        }

        [Test]
        public async Task GetUserWithInvalidId()
        {

            var getResult = await client.GetAsync("users/100");

            Assert.AreEqual(HttpStatusCode.NotFound, getResult.StatusCode);
        }

        [Test]
        public async Task DeleteUserWithValidId()
        {
            var newUser = new User
            {
                Email = "test@gmail.com",
                Password = "ABC123DRM"
            };

            var newUserAsJSON = JsonSerializer.Serialize(newUser);

            var postContent = new StringContent(newUserAsJSON,
                Encoding.UTF8, "application/json");

            var postResult = await client.PostAsync("users", postContent);

            var postResultString = await postResult.Content.ReadAsStringAsync();

            var ob = JsonSerializer.Deserialize<User>(postResultString);
            var id = ob.Id;

            var deleteResult = await client.DeleteAsync($"users/{id}");

            Assert.AreEqual(HttpStatusCode.OK, deleteResult.StatusCode);
        }

        [Test]
        public async Task DeleteUserWithInvalidId()
        {

            var deleteResult = await client.DeleteAsync("users/100");

            Assert.AreEqual(HttpStatusCode.NotFound, deleteResult.StatusCode);
        }
    }
}