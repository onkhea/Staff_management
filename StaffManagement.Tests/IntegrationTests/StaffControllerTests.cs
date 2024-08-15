using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StaffManagementAPI.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Playwright;
using Staff_Management.Data;

namespace StaffManagementAPI.Tests.IntegrationTests
{
    public class StaffControllerTests
    {
        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;

        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.AddDbContext<StaffContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDb");
                        });
                    });
                });

            _client = _factory.CreateClient();
        }

        [Test]
        public async Task PostStaff_ValidData_ShouldReturnSuccess()
        {
            var staff = new Staff
            {
                StaffID = "12345678",
                FullName = "John Doe",
                BirthDay = new DateTime(1990, 1, 1),
                Gender = 1
            };
            var content = new StringContent(JsonSerializer.Serialize(staff), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/staff", content);

            response.EnsureSuccessStatusCode();
        }

        [TearDown]
        public void Teardown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
