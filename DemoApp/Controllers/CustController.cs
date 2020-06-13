using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers
{
    public class CustController : Controller
    {

        public CustController()
        {
           
        }
        // GET: Cust
        public ActionResult Index()
        {

            DemoContext context = HttpContext.RequestServices.GetService(typeof(DemoContext)) as DemoContext;
            return View(context.GetCustomers());
        }

        // GET: Cust/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cust/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cust/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Cust/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cust/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Cust/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cust/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}