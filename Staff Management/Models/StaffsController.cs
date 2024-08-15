// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Staff_Management.Models;
// using Staff_ManagementAPI.Controllers;
// using Staff_ManagementAPI.Models;
// using Moq;
// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Xunit;

// namespace StaffManagementAPI.Tests
// {
//     public class StaffsControllerTests
//     {
//         private readonly DbContextOptions<StaffContext> _options;

//         public StaffsControllerTests()
//         {
//             _options = new DbContextOptionsBuilder<StaffContext>()
//                 .UseInMemoryDatabase(databaseName: "StaffDatabase")
//                 .Options;

//             SeedDatabase();
//         }

//         private void SeedDatabase()
//         {
//             using (var context = new StaffContext(_options))
//             {
//                 context.Staffs.Add(new Staff { StaffID = "ST000001", FullName = "John Doe", Birthday = new DateTime(1980, 1, 1), Gender = 1 });
//                 context.Staffs.Add(new Staff { StaffID = "ST000002", FullName = "Jane Smith", Birthday = new DateTime(1990, 2, 2), Gender = 2 });
//                 context.SaveChanges();
//             }
//         }

//         [Fact]
//         public async Task GetStaffs_ReturnsAllStaffs()
//         {
//             using (var context = new StaffContext(_options))
//             {
//                 var controller = new StaffsController(context);

//                 var result = await controller.GetStaff();

//                 var actionResult = Assert.IsType<ActionResult<IEnumerable<Staff>>>(result);
//                 var returnValue = Assert.IsType<List<Staff>>(actionResult.Value);

//                 Assert.Equal(2, returnValue.Count);
//             }
//         }

//         [Fact]
//         public async Task GetStaff_ReturnsNotFound_ForInvalidId()
//         {
//             using (var context = new StaffContext(_options))
//             {
//                 var controller = new StaffsController(context);

//                 var result = await controller.GetStaff("InvalidID");

//                 Assert.IsType<NotFoundResult>(result.Result);
//             }
//         }

//         [Fact]
//         public async Task PostStaff_AddsNewStaff()
//         {
//             using (var context = new StaffContext(_options))
//             {
//                 var controller = new StaffsController(context);
//                 var newStaff = new Staff { StaffID = "ST000003", FullName = "Sam Wilson", BirthDay = new DateTime(1985, 3, 3), Gender = 1 };

//                 var result = await controller.PostStaff(newStaff);

//                 var actionResult = Assert.IsType<ActionResult<Staff>>(result);
//                 var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);

//                 var returnValue = Assert.IsType<Staff>(createdAtActionResult.Value);
//                 Assert.Equal(newStaff.StaffID, returnValue.StaffID);
//             }
//         }

//         [Fact]
//         public async Task SearchStaffs_ReturnsMatchingResults()
//         {
//             using (var context = new StaffContext(_options))
//             {
//                 var controller = new StaffsController(context);

//                 var result = await controller.SearchStaffs(gender: 1, startDate: new DateTime(1979, 1, 1), endDate: new DateTime(1981, 1, 1));

//                 var actionResult = Assert.IsType<ActionResult<IEnumerable<Staff>>>(result);
//                 var returnValue = Assert.IsType<List<Staff>>(actionResult.Value);

//                 Assert.Single(returnValue);
//                 Assert.Equal("John Doe", returnValue[0].FullName);
//             }
//         }
//     }
// }
