namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

    public record class GetBasketResult(ShoppingCart Cart);

    public class GetBasketQueryHandler(IBasketRepository repository)
        : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            // get basket from DB
            var basket = await repository.GetBasket(query.UserName, cancellationToken);

            return new GetBasketResult(basket);
        }
    }
}
