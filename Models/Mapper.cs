using System;
using System.Collections.Generic;
using System.Linq;
using Auction.BO;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Auction.Models
{
    public static class Mapper
    {
        public static IEnumerable<Auctions> AuctionDataMap(IEnumerable<BO.Auctions> list)
        {
            if (list == null) return null;

            var mappedList = list.Select(obj => new Models.Auctions()
            {
                Id = obj.Id,
                StartDate = DateTime.Parse(obj.StartDate.ToString()),
                EndDate = DateTime.Parse(obj.EndDate.ToString())
            });

            return mappedList;
        }
        public static IEnumerable<Bids> BidsMap(IEnumerable<BO.Bids> list)
        {
            if (list == null) return null;

            var mappedList = list.Select(obj => new Bids()
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

        public static IEnumerable<Events> EventsMap(IEnumerable<BO.Events> list)
        {
            if (list == null) return null;

            //var mappedList = list.Select(obj => new Events()
            //{
            //    Id = obj.Id,
            //    AuctionId = obj.AuctionId,
            //    ItemId = obj.ItemId,
            //    StartDate = obj.StartDate,
            //    EndDate = obj.EndDate,
            //    Lun = obj.Lun,
            //    Lud = obj.Lud,
            //    CurrentPrice = obj.CurrentPrice,
            //    MinPriceIncrementAmount = obj.MinPriceIncrementAmount
            //});
            var mappedList = new List<Models.Events>();
            foreach (var obj in list)
            {
                mappedList.Add(new Models.Events
                {
                    AuctionId = obj.AuctionId,
                    CurrentPrice = obj.CurrentPrice,
                    EndDate = obj.EndDate,
                    Id = obj.Id,
                    ItemId = obj.ItemId,
                    Lud = obj.Lud,
                    Lun = obj.Lun,
                    MinPriceIncrementAmount = obj.MinPriceIncrementAmount,
                    StartDate = obj.StartDate
                });
            }
            return mappedList;
        }

        public static Events Event(BO.Events obj)
        {
            if (obj == null) return null;

            return new Events()
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

        public static IEnumerable<Items> ItemsMap(IEnumerable<BO.Items> list)
        {
            if (list == null) return null;

            var mappedList = list.Select(obj => new Items()
            {
                Id = obj.Id,
                Name = obj.Name,
                Details = obj.Details,
                MeasurementUnit = obj.MeasurementUnit,
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
        public static Models.Items Item(BO.Items obj)
        {
            if (obj == null) return null;

            return new Models.Items()
            {
                Id = obj.Id,
                Name = obj.Name,
                SoldDate = obj.SoldDate,
                SoldPrice = obj.SoldPrice,
                Image = obj.Image,
                Amount = obj.Amount,
                MeasurementUnit = obj.MeasurementUnit,
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
        public static IEnumerable<Withdrawals> WithdrawsMap(IEnumerable<BO.Withdrawals> list)
        {
            if (list == null) return null;

            var mappedList = list.Select(obj => new Withdrawals()
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
        public static IEnumerable<Models.ActiveAuctions> ActiveAuctionsMap(IEnumerable<BO.ActiveAuctions> list)
        {
            if (list == null) return null;

            //var mappedList = list.Select(obj => new Models.ActiveAuctions()
            //{
            //    AuctionId = obj.AuctionId,
            //    Open = obj.Open,
            //    OpenedBy = obj.OpenedBy,
            //});

            var mappedList = new List<ActiveAuctions>();
            foreach (var obj in list)
            {
                mappedList.Add(new Models.ActiveAuctions
                {
                    AuctionId = obj.AuctionId,
                    Open = obj.Open,
                    OpenedBy = obj.OpenedBy
                });
            }

            return mappedList;
        }

    }
}
