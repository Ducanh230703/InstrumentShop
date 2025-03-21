﻿using InstrumentShop.API.Services;
using InstrumentShop.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InstrumentShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            return _customerService.GetAllCustomers();
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        [HttpPost]
        public ActionResult<Customer> PostCustomer(Customer customer)
        {
            // Loại bỏ validation cho Carts, Orders, Feedbacks
            ModelState.Remove("Orders");
            ModelState.Remove("Feedbacks");

            if (ModelState.IsValid)
            {
                _customerService.AddCustomer(customer);
                return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerId }, customer);
            }

            return BadRequest(ModelState); // Trả về lỗi validation
        }

        [HttpPut("{id}")]
        public IActionResult PutCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _customerService.UpdateCustomer(customer);
                return NoContent();
            }

            return BadRequest(ModelState); // Trả về lỗi validation
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            _customerService.DeleteCustomer(id);
            return NoContent();
        }
    }
}