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
            if (list == null) return null;

            var mappedList = list.Select(obj => new AuctionData()
            {
                Id = obj.Id,
                StartDate = DateTime.Parse(obj.StartDate.ToString()),
                EndDate = DateTime.Parse(obj.EndDate.ToString())
            });

            return mappedList;
        }
        public static IEnumerable<BidHistory> BidsMap(IEnumerable<BO.Bids> list)
        {
            if (list == null) return null;

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
            if (list == null) return null;

            var mappedList = list.Select(obj => new Event()
            {
                Id = obj.Id,
                AuctionId = obj.AuctionId,
                ItemId = obj.ItemId,
                StartDate = obj.StartDate,
                EndDate = obj.EndDate,
                Lun = obj.Lun,
                Lud = obj.Lud,
                CurrentPrice = obj.CurrentPrice,
                MinPriceIncrementAmount = obj.MinPriceIncrementAmount
            });
            return mappedList;
        }

        public static Event Event(BO.Event obj)
        {
            if (obj == null) return null;

            return new Event()
            {
                Id = obj.Id,
                ItemId = obj.ItemId,
                AuctionId = obj.AuctionId,
                StartDate = obj.StartDate,
                EndDate = obj.EndDate,
                CurrentPrice = obj.CurrentPrice,
                MinPriceIncrementAmount = obj.MinPriceIncrementAmount,
                Lun = obj.Lun,
                Lud = obj.Lud
            };
        }

        public static IEnumerable<Item> ItemsMap(IEnumerable<BO.Item> list)
        {
            if (list == null) return null;

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
        public static Item Item(BO.Item obj)
        {
            if (obj == null) return null;

            return new Item()
            {
                Id = obj.Id,
                Name = obj.Name,
                SoldDate = obj.SoldDate,
                SoldPrice = obj.SoldPrice,
                Image = obj.Image,
                Amount = obj.Amount,
                MeasurementUnits = obj.MeasurementUnits,
                Details = obj.Details,
                StartPrice = obj.StartPrice,
                Lud = obj.Lud,
                Lun = obj.Lun,
                InD = obj.InD
            };
        }

        public static IEnumerable<Users> UsersMap(IEnumerable<BO.Users> list)
        {
            if (list == null) return null;

            var mappedList = list.Select(obj => new Users()
            {
                Id = obj.Id,
                RoleId = obj.RoleId,
                Username = obj.Username,
                Password = obj.Password,
                Name = obj.Name,
                Dob = DateTime.Parse(obj.DoB.ToString()),
                InD = obj.InD,
            });

            return mappedList;
        }
        public static IEnumerable<WithdrawHistory> WithdrawsMap(IEnumerable<BO.WithdrawHistory> list)
        {
            if (list == null) return null;

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
        public static IEnumerable<ActiveAuction> ActiveAuctionsMap(IEnumerable<BO.ActiveAuction> list)
        {
            if (list == null) return null;

            var mappedList = list.Select(obj => new ActiveAuction()
            {
                AuctionId = obj.AuctionId,
                Open = obj.Open,
                OpenedBy = obj.OpenedBy
            });

            return mappedList;
        }

    }
}
