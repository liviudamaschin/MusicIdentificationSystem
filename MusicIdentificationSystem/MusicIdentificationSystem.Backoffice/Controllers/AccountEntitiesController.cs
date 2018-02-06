using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicIdentificationSystem.DAL.Context;
using MusicIdentificationSystem.DAL.DbEntities;

namespace MusicIdentificationSystem.Backoffice.Controllers
{
    public class AccountEntitiesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: AccountEntities
        public async Task<ActionResult> Index()
        {
            return View(await db.Accounts.ToListAsync());
        }

        // GET: AccountEntities/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountEntity accountEntity = await db.Accounts.FindAsync(id);
            if (accountEntity == null)
            {
                return HttpNotFound();
            }
            return View(accountEntity);
        }

        // GET: AccountEntities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,AccountName,IsActive")] AccountEntity accountEntity)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(accountEntity);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(accountEntity);
        }

        // GET: AccountEntities/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountEntity accountEntity = await db.Accounts.FindAsync(id);
            if (accountEntity == null)
            {
                return HttpNotFound();
            }
            return View(accountEntity);
        }

        // POST: AccountEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,AccountName,IsActive")] AccountEntity accountEntity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountEntity).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(accountEntity);
        }

        // GET: AccountEntities/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountEntity accountEntity = await db.Accounts.FindAsync(id);
            if (accountEntity == null)
            {
                return HttpNotFound();
            }
            return View(accountEntity);
        }

        // POST: AccountEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AccountEntity accountEntity = await db.Accounts.FindAsync(id);
            db.Accounts.Remove(accountEntity);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
