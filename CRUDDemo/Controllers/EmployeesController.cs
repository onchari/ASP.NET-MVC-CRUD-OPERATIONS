using CRUDDemo.Extensions;
using CRUDDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CRUDDemo.Controllers
{
    public class EmployeesController : Controller
    {
       private readonly CrudContext _context = new CrudContext();
        // GET: Employees
        public ActionResult Index()
        {
            var employees = _context.Employees.ToList();
            return View(employees);
        }

        //GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST : Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                this.AddNotification("Employees Added Successfully", NotificationType.INFO);
                return RedirectToAction("Index");
            }
            this.AddNotification("Model State is invalid", NotificationType.ERROR);
            return View(employee);
        }

        //GET : Employees/Edit/3
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var findById = _context.Employees.SingleOrDefault(e => e.EmployeeId == id);
            if(findById == null)
            {
                return HttpNotFound();
            }
            return View(findById);
        }

        //POST : Employees/Edit/3
        [HttpPost]
    
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(employee);
        }

        //GET : Employees/Details/5
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var findById = _context.Employees.SingleOrDefault(c => c.EmployeeId == id);
            if(findById == null)
            {
                return HttpNotFound();
            }
            return View(findById);
        }

        //
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var findById = _context.Employees.SingleOrDefault(e => e.EmployeeId == id);
            if(findById == null)
            {
                return HttpNotFound();
            }
            return View(findById);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
           if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var findById = _context.Employees.SingleOrDefault(x => x.EmployeeId == id);
            if(findById == null)
            {
                return HttpNotFound();
            }
            _context.Employees.Remove(findById ?? throw new InvalidOperationException());
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}