using DAL.Entities;
using StaticLogger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApi.Converter;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class UserRequestController : ApiController
    {
        private TwitterEntities db = new TwitterEntities();
        private UserConverter convert = new UserConverter();
        private DeleteFollows dfollows = new DeleteFollows();

        public UserRequestController()
        {

        }

        // GET api/UserRequest
        public HttpResponseMessage GetUsers()
        {
            var listofusers = db.Users.ToList();
            Logger.Log.Debug("List of users was requested.Method  GET was requested.Status code 200");
            //return convert.ApiUsers(listofusers);
            return Request.CreateResponse(HttpStatusCode.OK, convert.ApiUsers(listofusers), Configuration.Formatters.JsonFormatter);
        }

        // GET api/UserRequest/5
        public HttpResponseMessage GetUser(int id)
        {
            User user = db.Users.Find(id);
            var userapi = convert.ApiUser(user);
            if (user == null)
            {
                Logger.Log.Debug("Requested nullable user.");
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            Logger.Log.Debug("User with id:" + id + "was requested.Method:GET; Status code 200");
            return Request.CreateResponse(HttpStatusCode.OK, userapi, Configuration.Formatters.JsonFormatter);
        }

        // PUT api/UserRequest/5
        public HttpResponseMessage PutUser(int id, UserApiModel user)
        {
            if (ModelState.IsValid && id == user.Id)
            {
                db.Entry(convert.ApiModelToDBModel(user)).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    Logger.Log.Debug("NotFound exception occured. Status code: 404");
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                Logger.Log.Debug("User with id:" + id + "has been changed. Method: PUT requested. Status code: 201");
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                Logger.Log.Debug("Bad request occured.ModelState is not valid or user with id:" + id + "was not found.Status code: 404");
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/UserRequest
        public HttpResponseMessage PostUser(UserApiModel user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(convert.ApiModelToDBModel(user));
                db.SaveChanges();


                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, user);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = user.Id }));
                Logger.Log.Debug("");
                Logger.Log.Debug("Method POST was requested.Status code: 201. New user was successfuly added.");
                return response;
            }
            else
            {
                Logger.Log.Debug("Bad request occured.");
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/UserRequest/5
        public HttpResponseMessage DeleteUser(int id)
        {

            User user = db.Users.Find(id);


            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            dfollows.DeleteAllFollowWithPublisher(user.Id);
            dfollows.DeleteAllTweetsByUser(user.Id);

            db.Users.Remove(user);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                Logger.Log.Debug("Not found exception occured. Status code : 404");
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            Logger.Log.Debug("Method: DELETE was requested.Status code 200.User was successfuly deleted.");
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}