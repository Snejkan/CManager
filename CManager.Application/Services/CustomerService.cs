using CManager.Domain.Models;
using CManager.Infrastructure.Repos;

namespace CManager.Application.Services;

public class CustomerService(ICustomerRepo customerRepo) : ICustomerService
{

    private readonly ICustomerRepo _customerRepo = customerRepo;

    public bool CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postalCode, string city)
    {
        CustomerModel customerModel = new()
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            Address = new AddressModel
            {
                StreetAddress = streetAddress,
                PostalCode = postalCode,
                City = city
            }
        };

        try
        {
            var customers = _customerRepo.GetAllCustomers();
            customers.Add(customerModel);
            var result = _customerRepo.SaveCustomers(customers);
            return result;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public IEnumerable<CustomerModel> GetAllCustomers(out bool hasError)
    {
        hasError = false;
        try
        {
           var customers =  _customerRepo.GetAllCustomers();
           return customers;
        }
        catch (Exception)
        {
            hasError = true;
            return [];
        }
    }


    public bool DeleteCustomer(Guid id)
    {
        try
        {
            var customers = _customerRepo.GetAllCustomers();
            var customer = customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
                return false;

            customers.Remove(customer);
            var result = _customerRepo.SaveCustomers(customers);
            return result;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting customer: {ex.Message}");
            return false;
        }
    }

}
