using sample_api_csharp.Models;

namespace sample_api_csharp.DTOs
{
    public class CustomerDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }


        public static CustomerDTO ToDTO(Customer customer)
        {
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.Id = customer.Id;
            customerDTO.Name = customer.Name;
            customerDTO.Email = customer.Email;
            customerDTO.ZipCode = customer.ZipCode;
            return customerDTO;
        }

        public static Customer ToEntity(CustomerDTO customerDTO)
        {
            Customer customer = new Customer();
            customer.Id = customerDTO.Id;
            customer.Name = customerDTO.Name;
            customer.Email = customerDTO.Email;
            customer.ZipCode = customerDTO.ZipCode;
            return customer;
        }
    }
}
