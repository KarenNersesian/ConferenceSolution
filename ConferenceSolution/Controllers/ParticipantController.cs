using AutoMapper;
using ConferenceSolution.Comparers;
using ConferenceSolution.Data;
using ConferenceSolution.DTOS;
using ConferenceSolution.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConferenceSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IParticipantRepo repository;
        public ParticipantController(IParticipantRepo repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public ActionResult<IEnumerable<ParticipantReadDTO>> GetAll()
        {
            var participants = repository.GetAll();

            return Ok(mapper.Map<IEnumerable<ParticipantReadDTO>>(participants));
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ActionResult<ParticipantReadDTO> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            IModel? participant = repository.GetById(id);

            return Ok(mapper.Map<ParticipantReadDTO>(participant));
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] ParticipantCreateDTO participantCreateDTO)
        {
            IModel participant = mapper.Map<Participant>(participantCreateDTO);

            repository.Create(participant, new ParticipantByFullNameComparer());
            repository.SaveChanges();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ParticipantCreateDTO participantCreateDTO)
        {
            Participant participant = mapper.Map<Participant>(participantCreateDTO);

        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
