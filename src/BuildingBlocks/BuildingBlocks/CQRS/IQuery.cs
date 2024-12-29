using MediatR;

namespace BuildingBlocks.CQRS
{
    //For read operation
    public interface IQuery<out TResponse> : IRequest<TResponse>
        where TResponse : notnull // make sure TResponse not the null value while compiling eg: int?, string?.
                                  // otherwise it will return compile-time error
    {

    }
}
