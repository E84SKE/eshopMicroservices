
using MediatR;

namespace BuildingBlocks.CQRS
{



    public interface Icommand : ICollection<Unit>
    {

    }
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
