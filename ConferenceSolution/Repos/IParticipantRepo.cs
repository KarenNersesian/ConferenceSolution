using ConferenceSolution.Comparers;
using ConferenceSolution.DB;
using ConferenceSolution.Models;

namespace ConferenceSolution.Repos
{
    public interface IParticipantRepo
    {
        bool SaveChanges();
        Task<bool> SaveChangesAsync();
        IEnumerable<IModel> GetAll();
        IModel? GetById(string id);
        void Create(IModel model);
        void Create(IModel model, Action<ApplicationDbContext> validatonRule);
        public void Update(string id, IModel model);
        public void Delete(string id);
    }
}
