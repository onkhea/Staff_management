using Microsoft.AspNetCore.Mvc;
using StaffManagementAPI.Models;

public class StaffController : Controller
{
    private readonly HttpClient _client;

    public StaffController()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri("https://localhost:7052/api/");
    }

    public async Task<IActionResult> Index()
    {
        var response = await _client.GetAsync("Staffs");
        var staffList = await response.Content.ReadAsAsync<IEnumerable<Staff>>();
        return View(staffList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Staff staff)
    {
        var response = await _client.PostAsJsonAsync("Staffs", staff);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        return View(staff);
    }

    public async Task<IActionResult> Edit(string id)
    {
        var response = await _client.GetAsync($"Staffs/{id}");
        var staff = await response.Content.ReadAsAsync<Staff>();
        return View(staff);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Staff staff)
    {
        var response = await _client.PutAsJsonAsync($"Staffs/{staff.StaffID}", staff);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        return View(staff);
    }

    public async Task<IActionResult> Delete(string id)
    {
        var response = await _client.DeleteAsync($"Staffs/{id}");
        return RedirectToAction("Index");
    }
}
