using ConferenceShareModel.Db;
using ConferenceShareModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceShareModel.Repos
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
