using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetAllTypesHandlers : IRequestHandler<GetAllTypesQuery, IList<TypesResponse>>
    {
        private readonly ITypesRepository _typesRepository;
        public GetAllTypesHandlers(ITypesRepository typesRepository)
        {
            _typesRepository = typesRepository;
        }
        public async Task<IList<TypesResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _typesRepository.GetAllTypes();
            return ProductMapper.Mapper.Map<IList<TypesResponse>>(types.ToList());
        }

     }
}
