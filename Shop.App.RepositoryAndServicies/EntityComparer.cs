
using ShopApp.Core.Models.Models.Core.Abstract;

namespace RepositoryAndServicies
{
    internal class EntityComparer<T> : IComparer<T> where T : IEntity
    {
        public int Compare(T? x, T? y)
        {

            if ((x == null && y == null) || ((x?.Id == null) && (y?.Id == null)))
            {
                return 0;
            }
            else if (x == null || (x?.Id == null))
            {
                return -1;
            }
            else if (y == null || (y?.Id == null))
            {
                return 1;
            }
            else
            {
                return x.Id!.Value.CompareTo(y?.Id!.Value);
            }


        }
    }
}
