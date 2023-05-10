using Microsoft.AspNetCore.Mvc;
using ppedv.CyanBayCars.Models;
using ppedv.CyanBayCars.Models.Contracts;

namespace ppedv.CyanBayCars.UI.ASPMVC.Controllers
{
    public class CarController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CarController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: CarController
        public ActionResult Index()
        {
            return View(unitOfWork.CarRepo.Query().ToList());
        }

        // GET: CarController/Details/5
        public ActionResult Details(int id)
        {
            return View(unitOfWork.CarRepo.GetById(id));
        }

        // GET: CarController/Create
        public ActionResult Create()
        {
            return View(new Car() { Model = "NEU" });
        }

        // POST: CarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Car car)
        {
            try
            {
                unitOfWork.CarRepo.Add(car);
                unitOfWork.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(unitOfWork.CarRepo.GetById(id));

        }

        // POST: CarController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Car car)
        {
            try
            {
                unitOfWork.CarRepo.Update(car);
                unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarController/Delete/5
        public ActionResult Delete(int id)
        {
            return BadRequest("Schade");

            return View(unitOfWork.CarRepo.GetById(id));

        }

        // POST: CarController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Car car)
        {
            try
            {
                unitOfWork.CarRepo.Delete(car);
                unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //return BadRequest("Schade");
                return View();
            }
        }
    }
}
