using AutoMapper;
using ConferenceShareModel.Models;
using ConferenceSolution.DTOS;

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
