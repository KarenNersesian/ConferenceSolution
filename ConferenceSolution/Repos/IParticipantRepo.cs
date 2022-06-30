using ConferenceSolution.Comparers;
using ConferenceSolution.Models;

namespace ConferenceSolution.Repos
{
    public interface IParticipantRepo
    {
        bool SaveChanges();
        IEnumerable<IModel> GetAll();
        IModel? GetById(string id);
        void Create(IModel model);
        void Create(IModel model, IEqualityComparer<Participant> comparer);
        public void Update(string id, IModel model);
        public void Delete(string id);
    }
}
