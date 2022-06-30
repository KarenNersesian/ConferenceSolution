using AutoMapper;
using ConferenceSolution.DTOS;
using ConferenceSolution.Models;

namespace ConferenceSolution.Profilers
{
    public class ParticipantProfiler : Profile
    {
        public ParticipantProfiler()
        {
            CreateMap<Participant, ParticipantReadDTO>();
            CreateMap<ParticipantWriteDTO, Participant>();
        }
    }
}
