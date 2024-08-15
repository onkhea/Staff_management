
// //The controller will manage the CRUD operations and the advanced search functionality
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Staff_Management.Models;
// using Staff_ManagementAPI.Models;
// using System.Linq;

// namespace Staff_ManagementAPI.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class StaffsController : ControllerBase
//     {
//         private readonly StaffContext _staffContext;
//         public StaffsController(StaffContext staffContext)
//         {
//             _staffContext = staffContext;
//         }
//         //get : api/staff
//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<Staff>>> GetStaff()
//         {
//             return await _staffContext.Staffs.ToListAsync();
//         }
//         [HttpGet]
//         public async Task<ActionResult<Staff>> GetStaff(string id)
//         {
//             var staff = await _staffContext.Staffs.FindAsync(id);

//             if (staff == null)
//             {
//                 return NotFound();
//             }
//             return staff;
//         }
//         [HttpPut]
//         public async Task<IActionResult> PutStaff(string id, Staff staff)
//         {
//             if (id != staff.StaffID)
//             {
//                 return BadRequest();
//             }
//             _staffContext.Entry(staff).State = EntityState.Modified;
//             try
//             {
//                 await _staffContext.SaveChangesAsync();
//             }
//             catch (DbUpdateConcurrencyException)
//             {
//                 if (!StaffExists(id))
//                 {
//                     return NotFound();
//                 }
//                 else
//                 {
//                     throw;
//                 }

//             }

//             return NoContent();

//         }
//         private bool StaffExists(string id)
//         {
//             return _staffContext.Staffs.Any(st => st.StaffID == id);
//         }
//         // Post : api/ staff
//         [HttpPost]
//         public async Task<ActionResult<Staff>> PostStaff(Staff staff)
//         {
//             _staffContext.Staffs.Add(staff);
//             await _staffContext.SaveChangesAsync();
//             return CreatedAtAction("GateStaff", new { id = staff.StaffID }, staff);
//         }
//         // Delete: api / staff
//         [HttpDelete("{id}")]
//         public async Task<IActionResult> DeleteStaff(string id)
//         {
//             var staff = await _staffContext.Staffs.FindAsync(id);
//             if (staff == null)
//             {
//                 return NotFound();
//             }
//             _staffContext.Staffs.Remove(staff);
//             await _staffContext.SaveChangesAsync();
//             return NoContent();
//         }
//         [HttpGet("Search")]

//         public async Task<ActionResult<IEnumerable<Staff>>> SearchStaff(
//             int? gender,
//             DateTime? StartDate,
//             DateTime? EndDate,
//             string StaffID = null)
//         {
//             var query = _staffContext.Staffs.AsQueryable();

//             if (!string.IsNullOrEmpty(StaffID))
//             {
//                 query = query.Where(s => s.StaffID.Contains(StaffID));
//             }
//             if (gender.HasValue)
//             {
//                 query = query.Where(s => s.Gender == gender.Value);
//             }
//             if (StartDate.HasValue)
//             {
//                 query = query.Where(s => s.Birthday >= StartDate.Value);
//             }
//             if (EndDate.HasValue)
//             {
//                 query = query.Where(s => s.Birthday <= EndDate.Value);
//             }
//             return await query.ToListAsync();

//         }
//     }
// }
    

        