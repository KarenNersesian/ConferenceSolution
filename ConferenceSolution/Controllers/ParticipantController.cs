using AutoMapper;
using ConferenceSolution.Comparers;
using ConferenceSolution.DTOS;
using ConferenceSolution.Models;
using ConferenceSolution.Repos;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public ActionResult<IEnumerable<ParticipantReadDTO>> GetAll()
        {
            var participants = repository.GetAll();

            return Ok(mapper.Map<IEnumerable<ParticipantReadDTO>>(participants));
        }

        [HttpGet("{id}")]
        public ActionResult<ParticipantReadDTO> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            IModel? participant = repository.GetById(id);

            return Ok(mapper.Map<ParticipantReadDTO>(participant));
        }

        [HttpPost]
        public void Post([FromBody] ParticipantWriteDTO participantCreateDTO)
        {
            IModel participant = mapper.Map<Participant>(participantCreateDTO);

            repository.Create(participant, new ParticipantByFullNameComparer());
            repository.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ParticipantWriteDTO participantCreateDTO)
        {
            Participant participant = mapper.Map<Participant>(participantCreateDTO);

            repository.Update(id, participant);
            repository.SaveChanges();

        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            repository.Delete(id);
            repository.SaveChanges();
        }
    }
}
