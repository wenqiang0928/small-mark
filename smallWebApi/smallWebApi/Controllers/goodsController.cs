using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using smallWebApi.Models;

namespace smallWebApi.Controllers
{
    public class goodsController : ApiController
    {
        private SmallModelContainer db = new SmallModelContainer();

        // GET: api/goods
        public IQueryable<goods> GetgoodsSet()
        {
            return db.goodsSet;
        }

        // GET: api/goods/5
        [ResponseType(typeof(goods))]
        public IHttpActionResult Getgoods(int id)
        {
            goods goods = db.goodsSet.Find(id);
            if (goods == null)
            {
                return NotFound();
            }

            return Ok(goods);
        }

        // PUT: api/goods/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putgoods(int id, goods goods)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != goods.Id)
            {
                return BadRequest();
            }

            db.Entry(goods).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!goodsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/goods
        [ResponseType(typeof(goods))]
        public IHttpActionResult Postgoods(goods goods)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.goodsSet.Add(goods);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = goods.Id }, goods);
        }

        // DELETE: api/goods/5
        [ResponseType(typeof(goods))]
        public IHttpActionResult Deletegoods(int id)
        {
            goods goods = db.goodsSet.Find(id);
            if (goods == null)
            {
                return NotFound();
            }

            db.goodsSet.Remove(goods);
            db.SaveChanges();

            return Ok(goods);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool goodsExists(int id)
        {
            return db.goodsSet.Count(e => e.Id == id) > 0;
        }
    }
}