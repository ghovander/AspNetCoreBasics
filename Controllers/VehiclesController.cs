using System;
using System.Threading.Tasks;
using AspNetBasics.Controllers.Resources;
using AspNetBasics.Models;
using AspNetBasics.Persistance;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetBasics.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly VegaDbContext context;
        private readonly IVehicleRepository repository;

        public VehiclesController(IMapper mapper, VegaDbContext context, IVehicleRepository repository)
        {
            this.context = context;
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            //Validation Logic
            // var model = await context.Models.FindAsync(vehicleResource.ModelId);
            // if(model == null)
            // {
            //     ModelState.AddModelError("ModelId", "Invalid modelId.");
            //     return BadRequest(ModelState);
            // }

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            repository.Add(vehicle);
            await context.SaveChangesAsync();

            vehicle = await repository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var vehicle = await repository.GetVehicle(id);

            if(vehicle == null) return NotFound();

            mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            await context.SaveChangesAsync();

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id, includeRelated: false);

            if(vehicle == null) return NotFound();
            repository.Remove(vehicle);
            await context.SaveChangesAsync();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id);

            if(vehicle == null) return NotFound();

            var vehicleResource = mapper.Map<Vehicle,VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }
    }
}