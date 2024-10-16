﻿using Contacts.Api.Exceptions;
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
        private readonly ILogger<ContactController> _logger;

        public ContactController(ContactService contactService, ILogger<ContactController> logger)
        {
            _contactService = contactService;
            _logger = logger;
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
                //throw new NotFoundException($"Product ID {id} not found.");
                return NotFound();
            }
            _logger.LogInformation($"Returning product with ID: {contact.Id}.");
            return Ok(contact);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> AddContact(ContactModels contact)
        {
            var status = new ErrorResponse();
            await _contactService.AddContactAsync(contact);
            if (contact.Id > 0)
            {
                status.Message = "Saved successfully.";
                status.StatusCode = StatusCodes.Status201Created;
            }
            return Ok(status);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> UpdateContact([FromRoute] int id,[FromBody] ContactModels contact)
        {
            var status = new ErrorResponse();
            if (id != contact.Id)
            {
                return BadRequest();
            }

            await _contactService.UpdateContactAsync(contact);
            if (contact.Id > 0)
            {
                status.Message = "Updated successfully.";
                status.StatusCode = StatusCodes.Status200OK;
            }
            //return NoContent();
            return Ok(status);
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
