using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersApp.Data;
using UsersApp.ViewModels;

namespace UsersApp.Controllers
{
    [Authorize]
    public class EController : Controller
    {

        private readonly AppDbContext _Db;

        public EController(AppDbContext db)
        {
            _Db = db;
        }

        // GET:

        public async Task<IActionResult> Index()
        {
            var emplees = _Db.Emplees.ToList();
            return View(emplees);
        }


        public IActionResult NewEmp() {




            return View();


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewEmp(empleeModel emplee)
        {
            _Db.Emplees.Add(emplee);

            try
            {
                _Db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Log the error
                ModelState.AddModelError("", "Error saving employee");
                return View(emplee);
            }

            return RedirectToAction("Index");


        }































    




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSalary(int id, int newSalary)
        {
            var emplee = await _Db.Emplees.FindAsync(id);
            if (emplee == null)
            {
                return NotFound();
            }

            emplee.salary = newSalary;
            _Db.Emplees.Update(emplee);
            await _Db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
       
     
      
        public IActionResult Create()
        {
            return View();
        }

        // POST: Emp/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(empleeModel emplee)
        {
            if (ModelState.IsValid)
            {
                _Db.Add(emplee);
                await _Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emplee);
        }

        // GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var emplee = await _Db.Emplees.FindAsync(id);
            if (emplee == null) return NotFound();

            return View(emplee);
        }

        // POST: Emp/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, empleeModel emplee)
        {
            if (id != emplee.id) return NotFound();

            if (ModelState.IsValid)
            {
                _Db.Update(emplee);
                await _Db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emplee);
        }

        // GET: Emp/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var emplee = await _Db.Emplees.FindAsync(id);
            if (emplee == null) return NotFound();

            return View(emplee);
        }

        // POST: Emp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emplee = await _Db.Emplees.FindAsync(id);
            _Db.Emplees.Remove(emplee);
            await _Db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    } }
