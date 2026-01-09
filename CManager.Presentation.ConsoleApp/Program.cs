using CManager.Application.Services;
using CManager.Infrastructure.Repos;
using CManager.Presentation.ConsoleApp.Controllers;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
    .AddScoped<ICustomerService, CustomerService>()
    .AddScoped<ICustomerRepo, CustomerRepo>()
    .AddScoped<MenuController>()
    .BuildServiceProvider();

var controller = services.GetRequiredService<MenuController>();
controller.ShowMenu();