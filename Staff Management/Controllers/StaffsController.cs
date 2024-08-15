using ClosedXML.Excel;
using DinkToPdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Staff_Management.Data;
using StaffManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly StaffContext _context;

        public StaffsController(StaffContext context)
        {
            _context = context;
        }

        // GET: api/Staffs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetStaffs()
        {
            return await _context.Staffs.ToListAsync();
        }

        // GET: api/Staffs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetStaff(string id)
        {
            var staff = await _context.Staffs.FindAsync(id);

            if (staff == null)
            {
                return NotFound();
            }

            return staff;
        }

        // PUT: api/Staffs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaff(string id, Staff staff)
        {
            if (id != staff.StaffID)
            {
                return BadRequest();
            }

            _context.Entry(staff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Staffs
        [HttpPost]
        public async Task<ActionResult<Staff>> PostStaff(Staff staff)
        {
            _context.Staffs.Add(staff);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStaff", new { id = staff.StaffID }, staff);
        }
        // POST: api/Staff/Batch
        [HttpPost("Batch")]
        public IActionResult PostStaffBatch([FromBody] List<Staff> staffList)
        {
            if (staffList == null || staffList.Count == 0)
            {
                return BadRequest("No staff data provided.");
            }

            _context.Staffs.AddRange(staffList);
            _context.SaveChanges();

            return Ok(staffList);
        }
        // DELETE: api/Staffs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(string id)
        {
            var staff = await _context.Staffs.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            _context.Staffs.Remove(staff);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StaffExists(string id)
        {
            return _context.Staffs.Any(e => e.StaffID == id);
        }

        // GET: api/Staffs/Search?gender=1&startDate=2020-01-01&endDate=2021-01-01
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Staff>>> SearchStaffs(
            int? gender,
            DateTime? startDate,
            DateTime? endDate,
            string staffID = null)
        {
            var query = _context.Staffs.AsQueryable();

            if (!string.IsNullOrEmpty(staffID))
            {
                query = query.Where(s => s.StaffID.Contains(staffID));
            }

            if (gender.HasValue)
            {
                query = query.Where(s => s.Gender == gender.Value);
            }

            if (startDate.HasValue)
            {
                query = query.Where(s => s.BirthDay >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(s => s.BirthDay <= endDate.Value);
            }

            return await query.ToListAsync();
        }
        [HttpGet("export/excel")]
        public IActionResult ExportToExcel([FromQuery] string staffId, [FromQuery] int? gender, [FromQuery] DateTime? fromBirthday, [FromQuery] DateTime? toBirthday)
        {
            var staffs = SearchStaffs(gender, fromBirthday, toBirthday,staffId).Result.Value;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Staffs");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "Staff ID";
                worksheet.Cell(currentRow, 2).Value = "Full Name";
                worksheet.Cell(currentRow, 3).Value = "Birthday";
                worksheet.Cell(currentRow, 4).Value = "Gender";

                foreach (var staff in staffs)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = staff.StaffID;
                    worksheet.Cell(currentRow, 2).Value = staff.FullName;
                    worksheet.Cell(currentRow, 3).Value = staff.BirthDay.ToString("yyyy-MM-dd");
                    worksheet.Cell(currentRow, 4).Value = staff.Gender == 1 ? "Male" : "Female";
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Staffs.xlsx");
                }
            }
        }
        // Install-Package DinkToPdf
        // Configure in Startup.cs

        [HttpGet("export/pdf")]
        public IActionResult ExportToPdf([FromQuery] string staffId, [FromQuery] int? gender, [FromQuery] DateTime? fromBirthday, [FromQuery] DateTime? toBirthday)
        {
            var staffs = SearchStaffs(gender, fromBirthday, toBirthday, staffId).Result.Value;

            var htmlContent = "<h1>Staff Report</h1><table><tr><th>Staff ID</th><th>Full Name</th><th>Birthday</th><th>Gender</th></tr>";

            foreach (var staff in staffs)
            {
                htmlContent += $"<tr><td>{staff.StaffID}</td><td>{staff.FullName}</td><td>{staff.BirthDay:yyyy-MM-dd}</td><td>{(staff.Gender == 1 ? "Male" : "Female")}</td></tr>";
            }

            htmlContent += "</table>";

            var converter = new SynchronizedConverter(new PdfTools());
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = { ColorMode = ColorMode.Color, Orientation = Orientation.Portrait, PaperSize = PaperKind.A4 },
                Objects = { new ObjectSettings { HtmlContent = htmlContent } }
            };

            var pdf = converter.Convert(doc);
            return File(pdf, "application/pdf", "Staffs.pdf");
        }

    }
}
