using ConferenceSolution.Models;
using System.Diagnostics.CodeAnalysis;

namespace ConferenceSolution.Comparers
{
    public class ParticipantByFullNameComparer : IEqualityComparer<Participant>
    {
        public bool Equals(Participant? x, Participant? y)
        {
            if (x == null || y == null) return false;

            if (x.Name == y.Name && x.LastName == y.LastName) return true;

            return false;
        }
        public int GetHashCode([DisallowNull] Participant obj)
        {
            return obj.GetHashCode();
        }
    }
}
