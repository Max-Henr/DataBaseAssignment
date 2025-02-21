using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Data.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace PresentationWebApi.Controllers;

[ApiController]
[Route("api/contact")]
public class ContactController(IContactService contactService) : ControllerBase
{
    private readonly IContactService _contactService = contactService;

    [HttpPost]
    public async Task<IActionResult> CreateContact(ContactRegistrationForm form)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _contactService.CreateContact(form);
        return result ? Ok() : Problem();
    }


    [HttpGet]
    public async Task<IActionResult> GetContacts()
    {
        var contact = await _contactService.GetAllContacts();
        if (contact == null)
        {
            return NotFound();
        }
        return Ok(contact);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContact(int id,ContactUpdateForm form)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _contactService.UpdateContact(form);

        return result != null ? Ok(result) : NotFound();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact(int id)
    {
        if(id <= 0)
        {
            return BadRequest();
        }
        var result = await _contactService.DeleteContact(id);
        return result ? Ok() : NotFound();
    }
}
