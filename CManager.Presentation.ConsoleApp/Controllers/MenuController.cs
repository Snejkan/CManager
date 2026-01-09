using CManager.Application.Services;
using CManager.Presentation.ConsoleApp.Helpers;

namespace CManager.Presentation.ConsoleApp.Controllers;

public class MenuController(ICustomerService customerService)
{
    private readonly ICustomerService _customerService = customerService;

    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("--- Welcome to CManager ---");
            Console.WriteLine();
            Console.WriteLine("1. Create customer");
            Console.WriteLine("2. View all customers");
            Console.WriteLine("3. Delete customer");
            Console.WriteLine("4. Exit");
            Console.WriteLine();
            Console.Write("Choose option: ");


            var option = Console.ReadLine();

            switch(option)
            {
                case "1":
                    CreateCustomer();
                    break;

                case "2":
                    ViewAllCustomers();
                    break;

                case "3":
                    DeleteCustomer();
                    break;

                case "4":
                    return;

                default:
                    OutputDialog("Invalid option! Press any key to continue...");
                    break;
            }
        }
    }

    private void CreateCustomer()
    {
        Console.Clear();
        Console.WriteLine("Create customer");

        var firstName = InputHelper.ValidateInput("First name", ValidationType.Required);
        var lastName = InputHelper.ValidateInput("Last name", ValidationType.Required);
        var email = InputHelper.ValidateInput("Email", ValidationType.Email);
        var phoneNumber = InputHelper.ValidateInput("Phone number", ValidationType.Required);
        var streetAddress = InputHelper.ValidateInput("Address", ValidationType.Required);
        var postalCode = InputHelper.ValidateInput("Postal code", ValidationType.Required);
        var city = InputHelper.ValidateInput("City", ValidationType.Required);

        var result = _customerService.CreateCustomer(firstName, lastName, email, phoneNumber, streetAddress, postalCode, city);

        if (result)
        {
            Console.WriteLine("Customer created");
            Console.WriteLine($"Name: {firstName} {lastName}");
        }
        else
        {
            Console.WriteLine("Something went wrong. Please try again");
        }
        OutputDialog("Press any key to continue...");
    }

    private void ViewAllCustomers()
    {
        Console.Clear();
        Console.WriteLine("All Customers");

        var customers = _customerService.GetAllCustomers(out bool hasError);

        if (hasError)
        {
            Console.WriteLine("Something went wrong. Please try again later");
        }

        if (!customers.Any())
        {
            Console.WriteLine("No customers found");
        }
        else
        {
            foreach(var customer in customers)
            {
                Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine($"Phone: {customer.PhoneNumber}");
                Console.WriteLine($"Address: {customer.Address.StreetAddress} {customer.Address.PostalCode} {customer.Address.City}");
                Console.WriteLine($"ID: {customer.Id}");
                Console.WriteLine();
            }
        }

        OutputDialog("Press any key to continue...");
    }

    private void DeleteCustomer()
    {
        Console.Clear();
        Console.WriteLine("Delete Customer");

        var customers = _customerService.GetAllCustomers(out bool hasError).ToList();

        if (hasError)
        {
            OutputDialog("Something went wrong. Please try again later");
            return;
        }

        if (!customers.Any())
        {
            OutputDialog("No customers found");
            return;
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("--- Customer List ---");
            foreach (var c in customers)
            {
                Console.WriteLine($"Email: {c.Email} | Name: {c.FirstName} {c.LastName}");
            }
           
            Console.WriteLine("Enter '0' to go back to menu");
            Console.Write("Enter email address of the customer to delete: ");

            var input = Console.ReadLine()?.Trim().ToLower();

           
            if (input == "0")
            {
                return;
            }

           
            var selectedCustomer = customers.FirstOrDefault(x => x.Email.ToLower() == input);

            if (selectedCustomer == null)
            {
                OutputDialog("No customer found with that email address. Press any key to try again...");
                continue;
            }

           
            Console.WriteLine($"\nYou have selected: {selectedCustomer.FirstName} {selectedCustomer.LastName} ({selectedCustomer.Email})");

            while (true)
            {
                Console.Write("Are you sure you want to delete this customer? (y/n): ");
                var confirmation = Console.ReadLine()?.ToLower();

                if (confirmation == "y")
                {
                    var result = _customerService.DeleteCustomer(selectedCustomer.Id);
                    if (result)
                    {
                        OutputDialog("Customer was removed successfully!");
                        return;
                    }
                    else
                    {
                        OutputDialog("Error: Could not delete customer from database.");
                        return;
                    }
                }
                else if (confirmation == "n")
                {
                    break; 
                }
                else
                {
                    Console.WriteLine("Please enter 'y' or 'n'.");
                }
            }
        }
    }
    

    private void OutputDialog(string message)
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }
}