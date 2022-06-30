using System.Diagnostics.CodeAnalysis;

namespace ConferenceSolution.Comparers
{
    public abstract class ModelComparer<IModel> : IEqualityComparer<IModel>
    {
        public abstract bool Equals(IModel? x, IModel? y);


        public abstract int GetHashCode([DisallowNull] IModel obj);
    }
}
