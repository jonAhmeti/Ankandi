using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auction.BLL;
using Auction.Models;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Event")]
    public class EventController : Controller
    {
        private readonly Events _bllEvents;

        public EventController(BLL.Events bllEvents)
        {
            _bllEvents = bllEvents;
        }

        public async Task<IActionResult> Index()
        {
            return View(Mapper.EventsMap(await _bllEvents.GetAllAsync()));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Event obj)
        {
            return RedirectToAction("Index");
        }
    }
}