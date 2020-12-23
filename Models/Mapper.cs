using System;
using System.Collections.Generic;
using System.Linq;
using Auction.BO;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Auction.Models
{
    public static class Mapper
    {
        public static IEnumerable<AuctionData> AuctionDataMap(IEnumerable<BO.AuctionData> list)
        {
            var mappedList = list.Select(obj => new AuctionData()
            {
                Id = obj.Id, StartDate = DateTime.Parse(obj.StartDate.ToString()),
                EndDate = DateTime.Parse(obj.EndDate.ToString())
            });

            return mappedList;
        }
        public static IEnumerable<BidHistory> BidsMap(IEnumerable<BO.BidHistory> list)
        {
            var mappedList = list.Select(obj => new BidHistory()
            {
                Id = obj.Id,
                UserId = obj.UserId,
                BidDate = DateTime.Parse(obj.BidDate.ToString()),
                BidAmount = obj.BidAmount,
                EventId = int.Parse(obj.EventId.ToString()),
                AuctionId = int.Parse(obj.AuctionId.ToString())
            });
            return mappedList;
        }
        public static IEnumerable<Event> EventsMap(IEnumerable<BO.Event> list)
        {
            var mappedList = list.Select(obj => new Event()
            {
                Id = obj.Id,
                AuctionId = obj.AuctionId,
                ItemId = obj.ItemId,
                StartDate = obj.StartDate,
                EndDate = obj.EndDate,
                Lun = obj.Lun,
                Lud = obj.Lud,
                TopBidder = obj.TopBidder,
                CurrentPrice = obj.CurrentPrice,
                MinPriceIncrementAmount = obj.MinPriceIncrementAmount
            });
            return mappedList;
        }
        public static IEnumerable<Item> ItemsMap(IEnumerable<BO.Item> list)
        {
            var mappedList = list.Select(obj => new Item()
            {
                Id = obj.Id,
                Name = obj.Name,
                Details = obj.Details,
                MeasurementUnits = obj.MeasurementUnits,
                Amount = obj.Amount,
                StartPrice = obj.StartPrice,
                Image = obj.Image,
                InD = obj.InD,
                Lud = obj.Lud,
                Lun = obj.Lun,
                SoldPrice = obj.SoldPrice,
                SoldDate = obj.SoldDate
            });

            return mappedList;
        }
        public static IEnumerable<Users> UsersMap(IEnumerable<BO.Users> list)
        {
            var mappedList = list.Select(obj => new Users()
            {
                Id = obj.Id,
                RoleId = obj.RoleId,
                Username = obj.Username,
                Password = obj.Password,
                Name = obj.Name,
                Dob = DateTime.Parse(obj.Dob.ToString()),
                InD = obj.InD,
            });

            return mappedList;
        }
        public static IEnumerable<WithdrawHistory> WithdrawsMap(IEnumerable<BO.WithdrawHistory> list)
        {
            var mappedList = list.Select(obj => new WithdrawHistory()
            {
                Id = obj.Id,
                AuctionId = obj.AuctionId,
                EventId = obj.EventId,
                UserId = obj.UserId,
                WithdrawDate = obj.WithdrawDate,
                WithdrawAmount = obj.WithdrawAmount
            });

            return mappedList;
        }
    }
}
