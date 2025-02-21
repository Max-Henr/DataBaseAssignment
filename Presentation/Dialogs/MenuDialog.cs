using Business.Dtos;
using Business.Interfaces;
using Data.Entities;

namespace Presentation.Dialogs;

public class MenuDialog(IContactService contactService, ICustomerService customerService, IRoleService roleService) : IMenuDialog
{
    private readonly IContactService _contactService = contactService;
    private readonly ICustomerService _customerService = customerService;
    private readonly IRoleService _roleService = roleService;

    public async Task Show()
    {
        do
        {
            Console.Clear();
            Console.WriteLine("--- Customer Management App");
            Console.WriteLine("1. Create New Contact");
            Console.WriteLine("2. View All Contacts");
            Console.WriteLine("3. Create New Customer");
            Console.WriteLine("4. View All Customers");
            Console.WriteLine("6. View All Roles");
            Console.WriteLine("7. Create New Role");
            Console.WriteLine("5. Exit");
            Console.Write("Enter Your Choice: ");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    NewContactDialog();
                    break;
                case "2":
                    ViewAllContactDialog();
                    break;
                case "3":
                    NewCustomerDialog();
                    break;
                case "4":
                    ViewAllCustomerDialog();
                    break;
                case "5":
                    return;
                case "6":
                    await ViewAllRoles();
                    break;
                case "7":
                    await NewRoleDialog();
                    break;
                default:
                    Console.WriteLine("Invalid Choice, Please Try Again!");
                    Console.ReadKey();
                    break;
            }
        } while (true);
    }


    public async Task NewCustomerDialog()
    {

        Console.Clear();
        Console.WriteLine("--- Creating New Customer ---");
        Console.Write("Name: ");
        var Name = Console.ReadLine();
        Console.Write("Contact Id: ");
        var ContactId = int.Parse(Console.ReadLine());

        var registrationForm = new CustomerRegistrationForm
        {
            Name = Name,
            ContactId = ContactId
        };
        var result = await _customerService.CreateCustomer(registrationForm);
        if (result)
        {
            Console.WriteLine("Customer Was Created Successfully");
        }
        else { Console.Write("Something Went Wrong When Trying To Create Customer"); }

        Console.ReadKey();
    }
    public async Task ViewAllCustomerDialog()
    {
        var customers = await _customerService.GetAllCustomers();

        Console.Clear();
        Console.WriteLine("--- View All Customers ---");

        foreach (var customer in customers)
        {
            var contact = await _contactService.GetContactById(customer.ContactId);
            var contactName = contact != null ? $"{contact.FirstName} {contact.LastName}" : "Unknown";
            Console.WriteLine($"Id: {customer.Id}, Name: {customer.Name}, Contact Id: {customer.ContactId}, Contact Person: {contactName}");
        }
        Console.ReadKey();
    }
    public async Task NewContactDialog()
    {

        Console.Clear();
        Console.WriteLine("--- Creating New Contact ---");
        Console.Write("First Name: ");
        var FirstName = Console.ReadLine();
        Console.Write("Last Name: ");
        var LastName = Console.ReadLine();
        Console.Write("Email: ");
        var Email = Console.ReadLine();
        Console.Write("Phone Number: ");
        var PhoneNumber = Console.ReadLine();

        var registrationForm = new ContactRegistrationForm
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            PhoneNumber = PhoneNumber
        };
        var result = await _contactService.CreateContact(registrationForm);
        if (result)
        {
            Console.WriteLine("Contact Was Created Successfully");
        }
        else
        {
            Console.Write("Something Went Wrong When Trying To Create Contact");
        }

        Console.ReadKey();
    }

    public async Task ViewAllContactDialog()
    {
        var contacts = await _contactService.GetAllContacts();
        Console.Clear();
        Console.WriteLine("--- View All Contacts ---");

        foreach (var contact in contacts)
        {
            Console.WriteLine($"Id: {contact.Id}, First Name: {contact.FirstName}, Last Name: {contact.LastName}, Email: {contact.Email}, Phone Number: {contact.PhoneNumber}");
        }

        Console.ReadKey();
    }

    public async Task ViewAllRoles()
    {
        var roles = await _roleService.GetAllRoles();
        Console.Clear();
        Console.WriteLine("--- View All Roles ---");

        foreach (var role in roles)
        {
            Console.WriteLine($"Id: {role.Id}, Role Name: {role.RoleName}");
        }
        Console.ReadKey();
    }
    public async Task NewRoleDialog()
    { 

        Console.Clear();
        Console.WriteLine("--- Creating New Role ---");
        Console.Write("Role Name: ");
        var RoleName = Console.ReadLine();
        var registrationForm = new RoleRegistrationForm
        {
            RoleName = RoleName
        };
        var result = await _roleService.CreateRole(registrationForm);
        if (result)
        {
            Console.WriteLine("Role Was Created Successfully");
        }
        else
        {
            Console.Write("Something Went Wrong When Trying To Create Role");
        }
    }
}