using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using sample_api_csharp.DTOs;
using sample_api_csharp.Models;
using sample_api_csharp.Repositories;
using sample_api_csharp.Services;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;


namespace sample_api_csharp.Controllers
{
    [Route("api/customers")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        
        private readonly CustomerRepository _customerRepository;
        private readonly CustomerService _customerService;

        public CustomerController(CustomerRepository customerRepository, CustomerService customerService)
        {
            _customerRepository = customerRepository;
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult ReadAll()
        {
            try
            {
                List<Customer> customers = _customerRepository.ReadAll();
                List<CustomerDTO> customerDTOs = new List<CustomerDTO>();
                foreach (var customer in customers)
                {
                    CustomerDTO customerDTO = CustomerDTO.ToDTO(customer);
                    var addressData = _customerService.GetAddress(customer.ZipCode).Result;
                    customerDTO.State = addressData["uf"];
                    customerDTO.City = addressData["localidade"];
                    customerDTO.Street = addressData["logradouro"];
                    customerDTO.Neighborhood = addressData["bairro"];
                    customerDTOs.Add(customerDTO);
                }

                return Ok(customerDTOs);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult ReadOne(long id)
        {
            try
            {
                Customer customer = _customerRepository.ReadOne(id);
                CustomerDTO customerDTO = CustomerDTO.ToDTO(customer);

                var addressData = _customerService.GetAddress(customer.ZipCode).Result;

                customerDTO.State = addressData["uf"];
                customerDTO.City = addressData["localidade"];
                customerDTO.Street = addressData["logradouro"];
                customerDTO.Neighborhood = addressData["bairro"];

                return Ok(customerDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("filter/{name}")]
        public IActionResult FilterByName(string name)
        {
            try
            {
                List<Customer> customers = _customerRepository.FilterByName(name);
                List<CustomerDTO> customerDTOs = new List<CustomerDTO>();
                foreach (var customer in customers)
                {
                    CustomerDTO customerDTO = CustomerDTO.ToDTO(customer);
                    var addressData = _customerService.GetAddress(customer.ZipCode).Result;
                    customerDTO.State = addressData["uf"];
                    customerDTO.City = addressData["localidade"];
                    customerDTO.Street = addressData["logradouro"];
                    customerDTO.Neighborhood = addressData["bairro"];
                    customerDTOs.Add(customerDTO);
                }

                return Ok(customerDTOs);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create([FromBody] Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                customer.Password = BCrypt.Net.BCrypt.HashPassword(customer.Password);

                Customer createdCustomer = _customerRepository.Create(customer);
                CustomerDTO customerDTO = CustomerDTO.ToDTO(createdCustomer);

                var addressData = _customerService.GetAddress(customer.ZipCode).Result;

                customerDTO.State = addressData["uf"];
                customerDTO.City = addressData["localidade"];
                customerDTO.Street = addressData["logradouro"];
                customerDTO.Neighborhood = addressData["bairro"];

                return Ok(customerDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Customer updatedCustomer = _customerRepository.Update(id, customer);
                
                CustomerDTO customerDTO = CustomerDTO.ToDTO(updatedCustomer);

                var addressData = _customerService.GetAddress(customer.ZipCode).Result;

                customerDTO.State = addressData["uf"];
                customerDTO.City = addressData["localidade"];
                customerDTO.Street = addressData["logradouro"];
                customerDTO.Neighborhood = addressData["bairro"];

                return Ok(customerDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Customer customer = _customerRepository.Delete(id);
                CustomerDTO customerDTO = CustomerDTO.ToDTO(customer);

                return Ok(customerDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
