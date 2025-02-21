using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace PresentationWebApi.Controllers;

[ApiController]
[Route("api/customer")]
public class CustomerController(ICustomerService customerService) : ControllerBase
{
    private readonly ICustomerService _customerService = customerService;

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CustomerRegistrationForm form)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _customerService.CreateCustomer(form);
        return result ? Ok() : Problem();
    }


    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        var contact = await _customerService.GetAllCustomers();
        if (contact == null)
        {
            return NotFound();
        }
        return Ok(contact);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id,CustomerUpdateForm form)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _customerService.UpdateCustomer(form);

        return result != null ? Ok(result) : NotFound();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }
        var result = await _customerService.DeleteCustomer(id);
        return result ? Ok() : NotFound();
    }
}
