﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfferMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OfferMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OfferController : ControllerBase
    {
        private static List<Offer> offers = new List<Offer>
            {

                new Offer { EmployeeId=2159447,OfferId = 1, Status = "Available", Likes = 10, Category = "Electronics", OpenedDate =new DateTime(2022,07,28), Details="Apple iPhone 13 Pro Max",ClosedDate=new DateTime(2022,08,02),EngagedDate=new DateTime(2022,08,01)},

                 new Offer { EmployeeId=2142912,OfferId = 2, Status = "Engaged", Likes = 55, Category = "Electronics", OpenedDate = new DateTime(2022,07,20),ClosedDate=new DateTime(2022,07,29) , Details="IFB Washing Machine",EngagedDate=new DateTime(2022,07,30)},

                 new Offer { EmployeeId=2144009,OfferId = 3, Status = "Engaged", Likes = 20, Category = "Pets", OpenedDate = new DateTime(2022,07,21),ClosedDate=new DateTime(2022,07,23) , Details="Golden Retriever for Adoption",EngagedDate=new DateTime(2022,07,23)},

                 new Offer { EmployeeId=2159071,OfferId = 4, Status = "Available", Likes = 25, Category = "Electronics", OpenedDate = new DateTime(2022,07,30),ClosedDate=new DateTime(2022,08,02) , Details="Samsung Galaxy S20",EngagedDate=new DateTime(2022,08,01)},

                 new Offer { EmployeeId=2141156,OfferId = 5, Status = "Available", Likes = 10, Category = "Electronics", OpenedDate = new DateTime(2022,07,09),ClosedDate=new DateTime(2022,07,15) , Details="HP Laptop",EngagedDate=new DateTime(2022,07,13)},

                 new Offer { EmployeeId=2142922,OfferId = 6 ,Status = "Engaged", Likes = 24, Category = "Books", OpenedDate = new DateTime(2022,07,15),EngagedDate=new DateTime(2022,07,25), ClosedDate=new DateTime(2022,07,23),Details="Wings Of Fire"},

                 new Offer {EmployeeId=2141156,OfferId = 7, Status = "Available", Likes = 25, Category = "Pets", OpenedDate =new DateTime(2022,07,18), Details="German Shepherd for Adoption",ClosedDate=new DateTime(2022,07,21),EngagedDate=new DateTime(2022,07,19)},

                 new Offer { EmployeeId=2159071,OfferId = 8, Status = "Engaged", Likes = 30, Category = "Electronics", OpenedDate = new DateTime(2022,07,04),ClosedDate=new DateTime(2022,07,06) , Details="Epson Printer",EngagedDate=new DateTime(2022,07,05)},

                 new Offer { EmployeeId=2159447,OfferId = 9, Status = "Engaged", Likes = 47, Category = "Furniture", OpenedDate = new DateTime(2022,07,11),ClosedDate=new DateTime(2022,07,21) , Details="Sofa Set",EngagedDate=new DateTime(2022,08,20)},

                 new Offer { EmployeeId=2142912,OfferId = 10, Status = "Closed", Likes = 18, Category = "Books", OpenedDate = new DateTime(2022,06,21),EngagedDate=new  DateTime(2022,06,23), ClosedDate=new DateTime(2022,07,05),Details="Harry Potter Books"},





            };




        // GET: api/<OfferController>
        [HttpGet]
        [Route("GetOffersList")]
        public IEnumerable<Offer> GetOffersList()
        {
            return offers;
        }

        // GET api/<OfferController>/5
        [HttpGet]
        [Route("GetOfferById/{id}")]
        public ActionResult<Offer> GetOfferById(int id)
        {

            var offer = offers.FirstOrDefault(c => c.OfferId == id);
            if (offer == null)
            {
                return NotFound();
            }

            return offer;

        }

        // POST api/<OfferController>
        [HttpPost]
        [Route("PostOffer")]
        public ActionResult<IEnumerable<Offer>> PostOffer(Offer newOffer)
        {
            if (newOffer.OfferId == 0 || newOffer.EmployeeId == 0 || newOffer.Category == null || newOffer.Details == null)

            {
                return NotFound();
            }
            else
            {
                offers.Add(newOffer);
            }

            return offers;
        }

        // PUT api/<OfferController>/5
        [HttpPut]
        [Route("EditOffer")]

        public ActionResult<Offer> EditOffer(Offer updatedOffer)

        {

            Offer offer = offers.FirstOrDefault(c => c.OfferId == updatedOffer.OfferId && c.EmployeeId == updatedOffer.EmployeeId);
            if (offer == null)
            {
                return NotFound("Offer not found");
            }



            offer.ClosedDate = updatedOffer.ClosedDate;

            offer.Status = updatedOffer.Status;

            offer.Details = updatedOffer.Details;

            offer.Category = updatedOffer.Category;

            if (offer.ClosedDate > offer.EngagedDate && offer.Status != "Closed")
            {
                return BadRequest("Please update status to Closed");
            }

            return Ok("Edited Successfully");

            //return offers;

        }




        [HttpGet]
        [Route("GetOfferByCategory/{category}")]
        public ActionResult<Offer> GetOfferByCategory(string category)
        {

            var offer = from c in offers where c.Category == category select c;
            if (offer.Count() == 0)
            {
                return NotFound();
            }
            return Ok(offer); // results in 200 ok status 

        }

        [HttpGet]
        [Route("GetOfferByOpenedDate/{openedDate}")]
        public ActionResult<Offer> GetOfferByOpenedDate(string openedDate)
        {

            var offer = from c in offers where c.OpenedDate.ToString("dd-MM-yyyy") == openedDate select c;
            if (offer.Count() == 0)
            {
                return NotFound();
            }
            return Ok(offer); // results in 200 ok status 

        }



        [HttpGet]
        [Route("GetOfferByTopThreeLikes")]
        public ActionResult<Offer> GetOfferByTopThreeLikes()
        {

            var offer = (from c in offers where 1 == 1 orderby c.Likes descending select c).Take(3);
            if (offer.Count() == 0)
            {
                return NotFound();
            }
            return Ok(offer); // results in 200 ok status 

        }








        [HttpGet]
        [Route("GetOfferByTopThreeLikes/{category}")]
        public ActionResult<Offer> GetOfferByTopThreeLikes(string category)
        {

            var offer = (from c in offers where c.Category == category orderby c.Likes descending select c).Take(3);
            if (offer.Count() == 0)
            {
                return NotFound();
            }
            return Ok(offer); // results in 200 ok status 

        }

        [HttpPost]
        [Route("EngageOffer")]
        public ActionResult<IEnumerable<Offer>> EngageOffer(Offer offerDetails)
        {
            //Offer o = new Offer();
            //o.OfferId = offerDetails.OfferId;
            //o.EmployeeId = offerDetails.EmployeeId;

            Offer offer = offers.FirstOrDefault(c => c.OfferId == offerDetails.OfferId && c.EmployeeId == offerDetails.EmployeeId);
            if (offer == null)
            {
                return NotFound("Offer not found");
            }
            else if (offer.Status == "Engaged" || offer.Status == "Closed")
            {

                return BadRequest("Offer is either Engaged or Closed");
            }

            else
            {

                //Note : Display Status as Engaged in ViewBag in Views

                offer.Status = "Engaged";
                offer.EngagedDate = DateTime.Now;
                return Ok("Offer status updated to Engaged");
                //return offers;
            }

        }
        [HttpGet]
        [Route("EditOfferList")]
        public ActionResult<Offer> EditOfferList(Offer offer)
        {
            //Offer o = new Offer();
            //o.OfferId = offerDetails.OfferId;
            //o.EmployeeId = offerDetails.EmployeeId;

            offer.EmployeeId = Convert.ToInt32(HttpContext.Session.GetInt32("EmployeeId"));


            Offer offerObj = offers.FirstOrDefault(c => c.OfferId == offer.OfferId && c.EmployeeId == offer.EmployeeId);
            /*
             if (offerObj == null)
             {
                 return NotFound("Offer not found");
             }
            */

            return offer;


        }
        [HttpGet]
        [Route("LikeOffer/{offerid}")]
        [EnableCors("AllowOrigin")]
        public ActionResult LikeOffer(int offerid)
        {


            // Like like = new Like();

            Offer offer = offers.FirstOrDefault(c => c.OfferId == offerid);
            if (offer == null)
            {
                return NotFound("Offer not found");
            }

            else
            {

                //Note : Display Status as Engaged in ViewBag in Views

                offer.Likes = offer.Likes + 1;
                //like.OfferId = o.OfferId;
                //like.LikeDate = DateTime.Now;
                //Likes.Add(like);
                Console.WriteLine(offer.Likes);
                return Ok(offer.Likes);
                //return offers;
            }

        }
        //[HttpPost]
        //[Route("LikeOffer")]
        //public ActionResult<Offer> LikeOffer(Offer o)
        //{


        //    // Like like = new Like();

        //    Offer offer = offers.FirstOrDefault(c => c.OfferId == o.OfferId);
        //    if (offer == null)
        //    {
        //        return NotFound("Offer not found");
        //    }

        //    else
        //    {

        //        //Note : Display Status as Engaged in ViewBag in Views

        //        offer.Likes = offer.Likes + 1;
        //        //like.OfferId = o.OfferId;
        //        //like.LikeDate = DateTime.Now;
        //        //Likes.Add(like);

        //        return Ok("Liked");
        //        //return offers;
        //    }

        //}
        // DELETE api/<OfferController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var entity = offers.FirstOrDefault(e => e.OfferId == id);
            offers.Remove(entity);
            return StatusCode(StatusCodes.Status200OK, "Deleted");
        }



    }
}
