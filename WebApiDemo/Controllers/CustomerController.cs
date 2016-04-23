using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    public class CustomerController : ApiController
    {

        DAL _context = null;


        public CustomerController()
        {
            _context = new DAL();
        }



        //used for help page to get the request and response type
        [ResponseType(typeof(Customer))]
        public HttpResponseMessage POST(Customer cust)
        {
            //if model validation gets fail return the Bad Request and return the model state
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            cust= _context.customers.Add(cust);
            _context.SaveChanges();
            // data inserted successfully return the 201 created status and return the resource uri
            var response = Request.CreateResponse<Customer>(HttpStatusCode.Created, cust);
            response.Headers.Location = new Uri(Request.RequestUri, string.Format("customer/{0}", cust.Id));
            return response;

        }

        [ResponseType(typeof(List<Customer>))]
        public HttpResponseMessage Get()
        {
            //get the list of customers from table
            var customers = _context.customers.ToList();
            //if customers is not null
            if (customers != null)
            {
               //return Customers  with status 200 and response
                if (customers.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, customers);
            }

            // return not found response 
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Customers not found");
        }


        [ResponseType(typeof(Customer))]
        public HttpResponseMessage Get(int Id)
        {
            //get the list of customer from table
            var customer = _context.customers.Where(cust => cust.Id == Id).Single();
            //if customer is not null
            if (customer!=null)
            {//return Customer  with status 200 and response
                return Request.CreateResponse(HttpStatusCode.OK, customer);

            }
            // return not found response 
            return Request.CreateResponse(HttpStatusCode.NotFound, "Customer not found");

        }


        public HttpResponseMessage Put(int id, [FromBody]Customer Cust)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != Cust.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            _context.Entry(Cust).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
               
                    throw;   
            }

            return  Request.CreateResponse(HttpStatusCode.NoContent);

            
        }

        [HttpDelete]
        [ResponseType(typeof(Customer))]
        public HttpResponseMessage DeleteCustomer([FromBody] int id)
        {
            //retrieve the customer first
            Customer customer = _context.customers.Find(id);

            //if customer object is null
            if (customer== null)
            {
                //return HttpStatusCode.NotFound response
                return Request.CreateResponse(HttpStatusCode.NotFound, "No records found");

            }
            //remove from the table
            _context.customers.Remove(customer);
            //save changes in database
            _context.SaveChanges();

            //return response 2
            return Request.CreateResponse(HttpStatusCode.OK);
        }



    }
}
