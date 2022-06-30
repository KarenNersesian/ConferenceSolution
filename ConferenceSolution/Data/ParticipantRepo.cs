using ConferenceSolution.Comparers;
using ConferenceSolution.Models;
using System.Diagnostics.CodeAnalysis;

namespace ConferenceSolution.Data
{
    public class ParticipantRepo : IParticipantRepo
    {
        private readonly ApplicationDbContext context;
        public ParticipantRepo(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create([NotNull] IModel model)
        {
            ArgumentNullException.ThrowIfNull(nameof(model));

            Participant participant = model as Participant;

            context?.Participants.Add(participant);
        }
        public void Create(IModel model, IEqualityComparer<Participant> comparer)
        {
            ArgumentNullException.ThrowIfNull(nameof(model));

            Participant participant = model as Participant;

            if (context.Participants.Contains(participant, comparer))
                throw new NotImplementedException();

            context?.Participants.Add(participant);
        }

        public IEnumerable<IModel> GetAll()
        {
            return context.Participants.ToList();
        }

        public IModel? GetById(string id)
        {
            return context?.Participants.FirstOrDefault(x => x.Id == id);
        }

        public bool SaveChanges()
        {
            return (context?.SaveChanges() >= 0);
        }
    }
}
