using Contacts.Api.Models;
using Contacts.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ContactService _contactService;

        public ContactController(ContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult<List<ContactModels>>> GetContacts()
        {
            var contacts = await _contactService.GetContactsAsync();
            return Ok(contacts);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ContactModels>> GetContactById(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> AddContact(ContactModels contact)
        {
            await _contactService.AddContactAsync(contact);
            return CreatedAtAction(nameof(GetContactById), new { id = contact.Id }, contact);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> UpdateContact([FromRoute] int id,[FromBody] ContactModels contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }
            await _contactService.UpdateContactAsync(contact);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteContact(int id)
        {
            await _contactService.DeleteContactAsync(id);
            return NoContent();
        }
    }
}
