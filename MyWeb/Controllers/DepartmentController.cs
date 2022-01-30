using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Models;
using MyWeb.Repository.IRepository;

namespace MyWeb.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _depRepo;

        public DepartmentController(IDepartmentRepository depRepo)
        {
            _depRepo = depRepo;
        }

        private string getToken() {
            return HttpContext.Session.GetString("JWToken");
        }

        // GET: DepartmentController
        public async Task<IActionResult> Index()
        {
           return View(await _depRepo.GetAllAsync(Constants.ApiDepartment, getToken()));
        }

        // GET: DepartmentController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var obj = await _depRepo.GetAsync(Constants.ApiDepartment, id, getToken());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // GET: DepartmentController/Create
        public async Task<IActionResult> Upsert(int? id)
        {
            Department obj = new Department();

            if (id == null)
            {
                //this will be true for Insert/Create
                return View(obj);
            }

            //Flow will come here for update
            obj = await _depRepo.GetAsync(Constants.ApiDepartment, id??0, getToken());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Department obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    await _depRepo.CreateAsync(Constants.ApiDepartment, obj, getToken());
                }
                else
                {
                    await _depRepo.UpdateAsync(Constants.ApiDepartment + "/" + obj.Id, obj, getToken());
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }

        // GET: DepartmentController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var obj = await _depRepo.GetAsync(Constants.ApiDepartment, id, getToken());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: DepartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Department obj)
        {
            try
            {
                if (id == obj.Id)
                {
                    await _depRepo.DeleteAsync(Constants.ApiDepartment, id, getToken());
                }
                else {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
