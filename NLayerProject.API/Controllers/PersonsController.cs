using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerProject.API.DTOs;
using NLayerProject.Core.Models;
using NLayerProject.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IService<Person> _personService;
        private readonly IMapper _mapper;
        public PersonsController(IService<Person> service, IMapper mapper)
        {
            _personService = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var persons = await _personService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<PersonDto>>(persons));
        }

        [HttpPost]
        public async Task<IActionResult> Save(PersonDto person)
        {
            var newPerson = await _personService.AddAsync(_mapper.Map<Person>(person));

            return Created(string.Empty, _mapper.Map<PersonDto>(newPerson));
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var person = _personService.GetByIdAsync(id).Result;
            _personService.Remove(person);

            return NoContent();
        }
    }
}
