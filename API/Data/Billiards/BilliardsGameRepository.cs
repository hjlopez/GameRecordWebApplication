using API.Interfaces.Billiards;
using AutoMapper;

namespace API.Data.Billiards
{
    public class BilliardsGameRepository : IBilliardsGameRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public BilliardsGameRepository(DataContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }
    }
}