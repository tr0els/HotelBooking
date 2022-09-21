using System;
using System.Collections.Generic;
using System.Linq;
using HotelBooking.Core;

namespace HotelBooking.Infrastructure.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly HotelBookingContext db;

        public CustomerRepository(HotelBookingContext context)
        {
            db = context;
        }

        public void Add(Customer entity)
        {
            db.Customer.Add(entity);
            db.SaveChanges();
        }

        public void Edit(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Customer Get(int id)
        {
            // The FirstOrDefault method below returns null
            // if there is no customer with the specified Id.
            return db.Customer.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return db.Customer.ToList();
        }

        public void Remove(int id)
        {
            // The Single method below throws an InvalidOperationException
            // if there is not exactly one customer with the specified Id.
            var customer = db.Customer.Single(r => r.Id == id);
            db.Customer.Remove(customer);
            db.SaveChanges();
        }
    }
}
