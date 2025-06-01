using Domain.Aggregates;
using Application.Interfaces;
using MediatR;
using Unit = Domain.Aggregates.Unit;

namespace Application.CQRS.Queries.UnitQueries
{
    public record GetAllUnits: IRequest<List<Unit>>
    {
    }

    public class GetAllUnitsHandler: IRequestHandler<GetAllUnits, List<Unit>>
    {
        private readonly IUnitRepository _unitRepository;

        public GetAllUnitsHandler(IUnitRepository unitRepository) => _unitRepository = unitRepository;

        public async Task<List<Unit>> Handle(GetAllUnits request, CancellationToken cancellationToken)
        {
            return (List<Unit>)await _unitRepository.GetAllUnits();
        }
    }
}
