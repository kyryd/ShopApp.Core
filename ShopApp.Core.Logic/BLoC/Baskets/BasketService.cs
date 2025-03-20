using RepositoryAndServicies.Repositories.Abstract;
using ShopApp.Core.Logic.BLoC.Baskets.Abstract;
using ShopApp.Core.Models.Models.Baskets.Abstract;
using ShopApp.Core.Models.Models.Client.Abstract;
using ShopApp.Core.Models.Models.Core.Abstract;

namespace ShopApp.Core.Logic.BLoC.Baskets
{
    public class BasketService(IFrontendRepository<Basket> repository) : IBasketService
    {
        public async Task<IBasket?> CreateBasket(IClient client)
        {
            var res = await repository.SaveAsync(new Basket(client, []));
            if (res.IsError)
            {
                return null;
            }
            return res.Value!;

        }

        public async Task<IBasket?> RemoveBasket(IClient client)
        {
            var id = ((IEntity)client).Id;
            
            if (id == null)
            {
                return null;
            }

            var res = await repository.DeleteAsync(id.Value);

            if (res.IsError)
            {
                return null;
            }

            return res.Value!;
        }
    }
}
