using AutoMapper;
using ConferenceSolution.Comparers;
using ConferenceSolution.DB;
using ConferenceSolution.DTOS;
using ConferenceSolution.Erros;
using ConferenceSolution.Exceptions;
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
        public async Task<ActionResult<ParticipantReadDTO>> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            IModel? participant = repository.GetById(id);

            return Ok(mapper.Map<ParticipantReadDTO>(participant));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ParticipantWriteDTO participantCreateDTO)
        {
            IModel participant = mapper.Map<Participant>(participantCreateDTO);
            try
            {
                repository.Create(participant, (context) => 
                {
                    ValidateParticipantByFullName(participant as Participant, context);
                });
                await repository.SaveChangesAsync();

                return Ok();
            }
            catch (DuplicateParticipantEx ex)
            {
                return Ok(ex.ErrorInfo);
            }
            
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

        #region "Validations"
        private void ValidateParticipantByFullName(Participant participant, ApplicationDbContext context)
        {

            var participantFromDb = context.Participants
                        .Where(x => x.Name == (participant as Participant).Name && x.LastName == (participant as Participant).LastName)
                        .FirstOrDefault();
            if (participantFromDb != null)
                throw new DuplicateParticipantEx(new Erros.ErrorInfo(string.Empty, Enums.ErrorCodeType.DuplicateParticipantWithSameFullName),
                    "Duplicate participant exception.");


        }
        #endregion
    }
}
