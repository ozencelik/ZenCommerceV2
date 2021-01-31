using Basket.Api.Data.Interfaces;
using Basket.Api.Entities;
using Basket.Api.Repositories.Interfaces;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Basket.Api.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IBasketContext _basketContext;

        public BasketRepository(IBasketContext basketContext)
        {
            _basketContext = basketContext;
        }

        public async Task<bool> DeleteBasket(string userName)
        {
            return await _basketContext.Redis.KeyDeleteAsync(userName);
        }

        public async Task<BasketCart> GetBasket(string userName)
        {
            // userName is the key for user's basket in Redis.
            var basket = await _basketContext.Redis.StringGetAsync(userName);

            if(basket.IsNullOrEmpty)
                return null;

            return JsonConvert.DeserializeObject<BasketCart>(basket);
        }

        public async Task<BasketCart> UpdateBasket(BasketCart basket)
        {
            if (basket is null)
                return null;

            var updated = await _basketContext.Redis
                                .StringSetAsync(basket.UserName, JsonConvert.SerializeObject(basket));

            return updated
                            ? await GetBasket(basket.UserName)
                            : null;
        }
    }
}
