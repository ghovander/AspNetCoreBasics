using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetBasics.Controllers.Resources;
using AspNetBasics.Models;
using AspNetBasics.Persistance;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetBasics.Controllers
{
    public class MakesController : Controller
    {
        private readonly VegaDbContext _context;
        private readonly IMapper _mapper;
        public MakesController(VegaDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes(){
            var makes = await _context.Makes.Include(m => m.Models).ToListAsync();
            return _mapper.Map<List<Make>,List<MakeResource>>(makes);
        }
    }
}