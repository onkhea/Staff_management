using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace StaffManagementAPI.Tests.EndToEndTests
{
    public class StaffManagementEndToEndTests : PageTest
    {
        [Test]
        public async Task AddNewStaff_ShouldReflectInTheStaffList()
        {
            // Replace with your frontend URL
            await Page.GotoAsync("http://localhost:5000");

            // Fill out the form
            await Page.FillAsync("#staffID", "12345678");
            await Page.FillAsync("#fullName", "John Doe");
            await Page.FillAsync("#birthDay", "1990-01-01");
            await Page.SelectOptionAsync("#gender", new[] { "1" });

            // Submit the form
            await Page.ClickAsync("#submitButton");

            // Verify the result
            var result = await Page.InnerTextAsync("#staffList");
            NUnit.Framework.Assert.IsTrue(result.Contains("John Doe"));
        }
    }
}
