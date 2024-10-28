using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GarageMVC.Data;
using GarageMVC.Models.Vehicles;

namespace GarageMVC.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly GarageMVCContext _context;

        public VehiclesController(GarageMVCContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index(string? searchTerm = null, string sortColumn = "RegistrationNumber", string sortOrder = "asc")
        {
            IQueryable<IVehicle> vehicles = _context.Vehicles;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                vehicles = vehicles.Where(v =>
                    v.RegistrationNumber.Contains(searchTerm) ||
                    v.VehicleType.Contains(searchTerm) ||
                    v.Color.Contains(searchTerm) ||
                    v.FuelType.Contains(searchTerm) ||
                    v.NumberOfWheels.ToString().Contains(searchTerm));
            }

            vehicles = sortColumn switch
            {
                "RegistrationNumber" => sortOrder == "asc" ? vehicles.OrderBy(v => v.RegistrationNumber) : vehicles.OrderByDescending(v => v.RegistrationNumber),
                "VehicleType" => sortOrder == "asc" ? vehicles.OrderBy(v => v.VehicleType) : vehicles.OrderByDescending(v => v.VehicleType),
                "Color" => sortOrder == "asc" ? vehicles.OrderBy(v => v.Color) : vehicles.OrderByDescending(v => v.Color),
                "FuelType" => sortOrder == "asc" ? vehicles.OrderBy(v => v.FuelType) : vehicles.OrderByDescending(v => v.FuelType),
                "NumberOfWheels" => sortOrder == "asc" ? vehicles.OrderBy(v => v.NumberOfWheels) : vehicles.OrderByDescending(v => v.NumberOfWheels),
                _ => vehicles.OrderBy(v => v.RegistrationNumber)
            };

            ViewData["CurrentSort"] = $"{sortColumn}_{sortOrder}";

            return View(await vehicles.ToListAsync());
        }


        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleCreateViewModel vehicle)
        {
            if (ModelState.IsValid)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                Vehicle newVehicle = vehicle.VehicleType switch
                {
                    "Car" => new Car(),
                    "Bus" => new Bus(),
                    "Airplane" => new Airplane(),
                    "Boat" => new Boat(),
                    "Motorcycle" => new Motorcycle(),
                    _ => null
                };
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                newVehicle.RegistrationNumber = vehicle.RegistrationNumber;
                newVehicle.Color = vehicle.Color;
                newVehicle.FuelType = vehicle.FuelType;
                newVehicle.NumberOfWheels = vehicle.NumberOfWheels;

                _context.Vehicles.Add(newVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegistrationNumber,Color,FuelType,NumberOfWheels")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
    }
}
