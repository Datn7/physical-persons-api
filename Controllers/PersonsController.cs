using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using physical_persons_api.DTOs;
using physical_persons_api.Entities;
using physical_persons_api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace physical_persons_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IFileStorageService fileStorageService;

        public PersonsController(ApplicationDbContext context, IMapper mapper, IFileStorageService fileStorageService)
        {
            this.context = context;
            this.mapper = mapper;
            this.fileStorageService = fileStorageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = context.Persons.AsQueryable();
            await HttpContext.InsertParametersPaginationInHeader(queryable);
            //var persons = await context.Persons.ToListAsync();

            var persons = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();

            return mapper.Map <List<PersonDTO>>(persons);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PersonDTO>> Get(int id)
        {
            var person = await context.Persons.FirstOrDefaultAsync(x => x.Id == id);

            if(person == null)
            {
                return NotFound();
            }

            return mapper.Map<PersonDTO>(person);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] PersonCreationDTO personCreationDTO)
        {
            var person = mapper.Map<Person>(personCreationDTO);

            if(personCreationDTO.Picture != null)
            {
                person.Picture = await fileStorageService.SaveFile("persons", personCreationDTO.Picture);
            }

            context.Add(person);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] PersonCreationDTO personCreationDTO)
        {
            var person = await context.Persons.FirstOrDefaultAsync(x => x.Id == id);

            if(person == null)
            {
                return NotFound();
            }

            person = mapper.Map(personCreationDTO, person);

            if(personCreationDTO.Picture != null)
            {
                person.Picture = await fileStorageService.EditFile("persons", personCreationDTO.Picture, person.Picture);
            }

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var person = await context.Persons.FirstOrDefaultAsync(x => x.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            context.Remove(person);
            await context.SaveChangesAsync();
            await fileStorageService.DeleteFile(person.Picture, "persons");
            return NoContent();
        }
    }
}
