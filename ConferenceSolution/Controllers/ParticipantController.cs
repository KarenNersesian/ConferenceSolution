using AutoMapper;
using ConferenceShareModel.Db;
using ConferenceShareModel.Models;
using ConferenceShareModel.Repos;
using ConferenceSolution.DTOS;
using ConferenceSolution.Erros;
using ConferenceSolution.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly ILogger<ParticipantController> logger;
        private readonly IMapper mapper;
        private readonly IParticipantRepo repository;
        public ParticipantController(IParticipantRepo repository, IMapper mapper, ILogger<ParticipantController> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
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
        //We could add some validation logic as an Attribute that will run before our action.
        public async Task<ActionResult> Post([FromBody] ParticipantWriteDTO participantCreateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            IModel participant = mapper.Map<Participant>(participantCreateDTO);
            try
            {
                repository.Create(participant, (context) => 
                {
                    ValidateParticipantByFullName(participant as Participant, context);
                });
                await repository.SaveChangesAsync();

                return Ok(mapper.Map<ParticipantReadDTO>(participant));
            }
            catch (DuplicateParticipantEx ex)
            {
                logger.LogInformation(ex.Message);

                return Ok(ex.ErrorInfo);
            }
            
        }

        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] ParticipantWriteDTO participantCreateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Participant participant = mapper.Map<Participant>(participantCreateDTO);

            repository.Update(id, participant);
            repository.SaveChanges();

            return Ok();

        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            repository.Delete(id);
            repository.SaveChanges();

            return Ok();
        }

        #region "Validations"
        //For specific smart type validation.
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
