using ConferenceSolution.Comparers;
using ConferenceSolution.DB;
using ConferenceSolution.Exceptions;
using ConferenceSolution.Models;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ConferenceSolution.Repos
{
    public class ParticipantRepo : IParticipantRepo
    {
        private readonly ApplicationDbContext context;
        public ParticipantRepo(ApplicationDbContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Create([NotNull] IModel model)
        {
            ArgumentNullException.ThrowIfNull(nameof(model));

            Participant participant = model as Participant;

            context?.Participants.Add(participant);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="validatonRule"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Create(IModel model, Action<ApplicationDbContext> validatonRule)
        {
            ArgumentNullException.ThrowIfNull(nameof(model));

            validatonRule(context);

            context?.Participants.Add(model as Participant);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Delete(string id)
        {
            Participant participant = context.Participants.First(x => x.Id == id);

            context.Participants.Remove(participant);
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
            return (context.SaveChanges() >= 0);
        }

        public async Task<bool> SaveChangesAsync()
        {
            var result = await context.SaveChangesAsync();

            return result >= 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Update(string id, IModel model)
        {
            Participant participant = context.Participants.First(x => x.Id == id);

            context.Entry(participant).CurrentValues.SetValues(model);

        }
    }
}
