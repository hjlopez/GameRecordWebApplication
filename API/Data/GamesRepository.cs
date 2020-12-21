using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class GamesRepository : IGamesRepository
    {
        private readonly IMapper mapper;
        private readonly DataContext context;
        public GamesRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GamesDto>> GetGames()
        {
            var games = await context.Games.ToListAsync();
            
            return mapper.Map<IEnumerable<GamesDto>>(games);
        }
    }
}