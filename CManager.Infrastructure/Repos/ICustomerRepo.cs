using CManager.Domain.Models;

namespace CManager.Infrastructure.Repos;

public interface ICustomerRepo
{
    List<CustomerModel> GetAllCustomers();
    bool SaveCustomers(List<CustomerModel> customers);
}

