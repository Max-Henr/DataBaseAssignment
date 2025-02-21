using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Dialogs;

var serviceCollection = new ServiceCollection();

serviceCollection.AddDbContext<DataContext>(options => options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Projects\LocalDB\LocalDB\Data\Database.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True"));
serviceCollection.AddScoped<IContactService, ContactService>();
serviceCollection.AddScoped<ICustomerService, CustomerService>();
serviceCollection.AddScoped<IRoleService, RoleService>();
serviceCollection.AddScoped<IMenuDialog, MenuDialog>();
serviceCollection.AddScoped<IRoleRepository, RoleRepository>();
serviceCollection.AddScoped<IContactRepository, ContactRepository>();
serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();

var serviceProvider = serviceCollection.BuildServiceProvider();
var menuDialogs = serviceProvider.GetRequiredService<IMenuDialog>();

while (true)
{
    await menuDialogs.Show();
}

