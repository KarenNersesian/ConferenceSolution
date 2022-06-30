using ConferenceSolution.Comparers;
using ConferenceSolution.Models;

namespace ConferenceSolution.Data
{
    public interface IParticipantRepo
    {
        bool SaveChanges();
        IEnumerable<IModel> GetAll();
        IModel? GetById(string id);
        void Create(IModel model);
        void Create(IModel model, IEqualityComparer<Participant> comparer);
    }
}
