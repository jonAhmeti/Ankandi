﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auction.BLL;
using Auction.Models;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Item")]
    public class ItemController : Controller
    {
        private readonly Items _bllItems;

        public ItemController(BLL.Items bllItems)
        {
            _bllItems = bllItems;
        }

        public async Task<IActionResult> Index()
        {
            return View(Mapper.ItemsMap(await _bllItems.GetAllAsync()));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Item obj)
        {
            var result = await _bllItems.AddAsync(new BO.Item()
            {
                Name = obj.Name,
                StartPrice = obj.StartPrice,
                MeasurementUnits = obj.MeasurementUnits,
                Amount = obj.Amount,
                SoldDate = obj.SoldDate,
                SoldPrice = obj.SoldPrice,
                Image = obj.Image,
                Details = obj.Details,
                InD = DateTime.Now
            });
            return RedirectToAction("Index");
        }

        [HttpGet("Edit")]
        public async Task<PartialViewResult> Edit(int id)
        {
            var obj = await _bllItems.GetAsync(id);
            return PartialView("ItemPartial/_ItemEditForm", Mapper.ItemsMap(new[] {obj}).FirstOrDefault());
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(Item obj)
        {
            var result = await _bllItems.UpdateAsync(new BO.Item()
            {
                Id = obj.Id,
                Name = obj.Name,
                StartPrice = obj.StartPrice,
                MeasurementUnits = obj.MeasurementUnits,
                Amount = obj.Amount,
                SoldDate = obj.SoldDate,
                SoldPrice = obj.SoldPrice,
                Image = obj.Image,
                Details = obj.Details
            });
            return RedirectToAction("Index");
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bllItems.DeleteAsync(id);
            return RedirectToAction("Index");
        } 
    }
}