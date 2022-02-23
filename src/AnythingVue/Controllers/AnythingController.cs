using Anything.Data;
using Anything.Models;
using Microsoft.AspNetCore.Mvc;

namespace Anything.Controllers
{
    [ApiController]
    public class AnythingController : Controller
    {
        private readonly AnythingDb _db;

        public AnythingController(AnythingDb anythingDb)
        {
            _db = anythingDb;
        }

        [Route("/thing")]
        [HttpGet]
        public IActionResult Get()
        {
            var model = _db.Things;
            return Ok(model);
        }

        [Route("/thing/{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var model = _db.Things.Find(id);
            return Ok(model);
        }

        [Route("/thing")]
        [HttpPost]
        public IActionResult Post(AnyThing model)
        {
            _db.Things.Add(model);
            _db.SaveChanges();

            return Ok(model);
        }

        [Route("/thing")]
        [HttpPut]
        public IActionResult Put(AnyThing model)
        {
            var thingFromDb = _db.Things.Find(model.Id);
            if (thingFromDb is null)
            {
                return NotFound($"Id '{model.Id}' not found.");
            }

            thingFromDb.Name = model.Name;
            thingFromDb.Description = model.Description;

            _db.SaveChanges();

            return Ok(model);
        }

        [Route("/thing")]
        [HttpDelete]
        public IActionResult Delete(AnyThing model)
        {
            var thing = _db.Things.Find(model.Id);
            if (thing is null)
            {
                return NotFound($"Id '{model.Id}' not found.");
            }

            _db.Things.Remove(thing);
            _db.SaveChanges();

            return Ok(model);
        }
    }
}