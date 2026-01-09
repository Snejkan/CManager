using CManager.Domain.Models;

namespace CManager.Application.Services;

public interface ICustomerService
{
    bool CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string streetAddress, string postalCode, string city);

    IEnumerable<CustomerModel> GetAllCustomers(out bool hasError);

    bool DeleteCustomer(Guid id);
}
