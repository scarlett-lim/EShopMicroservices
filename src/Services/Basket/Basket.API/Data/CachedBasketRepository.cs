
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    // Creation of cachedbasketrepository is decorator pattern + proxy pattern 
    // Proxy pattern : CachedBasketRepository acts as proxy and forward the call to underlying BasketRepository
    // Decorator pattern : Get/Delete/StoreBasket methods are decorated with caching capabilities (extend the functionalities of BasketRepository)
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache)
        : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken)
        {
            var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);

            if (!string.IsNullOrEmpty(cachedBasket))
            {
                // ! = null forgiving operator
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            }

            var basket = await repository.GetBasket(userName, cancellationToken);

            // after retrieving from DB, update to cache db
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken)
        {
            await repository.StoreBasket(basket,cancellationToken);
            
            // update the distibuted cache after storing into db
            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
            
            return basket;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken)
        {
            await repository.DeleteBasket(userName, cancellationToken);

            await cache.RemoveAsync(userName, cancellationToken);

            return true;
        }

    }
}
