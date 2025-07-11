﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.AggregateNodes;

namespace Domain.Domain_Services
{
    public class CustomerDeletionService
    {
        /// <summary>
        /// Checks if a customer can be deleted.
        /// </summary>
        /// <param name="customerId">The id of the customer that has to be deleted</param>
        /// <returns>True if the customer can be deleted, false otherwise</returns>
        /// <exception cref="Exception">If the customer isn't found</exception>
        public async Task<bool> CanDeleteCustomer(Customer customer)
        {
            if (customer == null)
                throw new Exception("Customer not found");

            if (customer.Invoices.Any(inv => inv.isPaid))
                return false;
            return true;
        }
    }
}
