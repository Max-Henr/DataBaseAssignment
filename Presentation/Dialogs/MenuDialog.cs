using Business.Interfaces;
using Data.Entities;

namespace Presentation.Dialogs;

public class MenuDialog(IContactService contactService, ICustomerService customerService) : IMenuDialog
{
    private readonly IContactService _contactService = contactService;
    private readonly ICustomerService _customerService = customerService;

    public void Show()
    {
        do
        {
            Console.Clear();
            Console.WriteLine("--- Customer Management App");
            Console.WriteLine("1. Create New Contact");
            Console.WriteLine("2. View All Contacts");
            Console.WriteLine("3. Create New Customer");
            Console.WriteLine("4. View All Customers");
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
                default:
                    Console.WriteLine("Invalid Choice, Please Try Again!");
                    Console.ReadKey();
                    break;
            }
        } while (true);
    }


    public void NewCustomerDialog()
    {
        var customerEntity = new CustomerEntity();

        Console.Clear();
        Console.WriteLine("--- Creating New Customer ---");
        Console.Write("Name: ");
        customerEntity.Name = Console.ReadLine();
        Console.Write("Contact Id: ");
        customerEntity.ContactId = int.Parse(Console.ReadLine());

        var result = _customerService.CreateCustomer(customerEntity);
        if (result != null)
        {
            Console.WriteLine($"Customer Was Created With Id '{result.Id}'");
            Console.WriteLine($"Name: {result.Name} Contact Id: {result.ContactId}");
        }
        else { Console.Write("Something Went Wrong When Trying To Create Customer"); }

        Console.ReadKey();
    }
    public void ViewAllCustomerDialog()
    {
        Console.Clear();
        Console.WriteLine("--- View All Customers ---");
        var result = _customerService.GetCustomers();
        if (result.Any())
        {
            foreach (var customer in result)
            {
                Console.WriteLine($"Id: {customer.Id}, Name: {customer.Name}, Contact Person: {customer.Contact.FirstName} {customer.Contact.LastName} {customer.Contact.Email} {customer.Contact.PhoneNumber}");
            }
        }
        else
        {
            Console.Write("No Customer Was Found!");
        }
        Console.ReadKey();
    }
    public void NewContactDialog()
    {
        var contactEntity = new ContactEntity();

        Console.Clear();
        Console.WriteLine("--- Creating New Contact ---");
        Console.Write("First Name: ");
        contactEntity.FirstName = Console.ReadLine();
        Console.Write("Last Name: ");
        contactEntity.LastName = Console.ReadLine();
        Console.Write("Email: ");
        contactEntity.Email = Console.ReadLine();
        Console.Write("Phone Number: ");
        contactEntity.PhoneNumber = Console.ReadLine();

        var result = _contactService.CreateContact(contactEntity);
        if (result != null)
        {
            Console.WriteLine($"Following Contact Was Created With Id '{result.Id}'");
            Console.WriteLine($"{result.FirstName} {result.LastName} <{result.Email}>");
        }
        else
        {
            Console.Write("Something Went Wrong When Trying To Create Contact");
        }

        Console.ReadKey();
    }

    public void ViewAllContactDialog()
    {
        Console.Clear();
        Console.WriteLine("--- View All Contacts ---");

        var result = _contactService.GetContacts();
        if (result.Any())
        {
            foreach (var contact in result)
            {
                Console.WriteLine($"Id: {contact.Id}, Info: {contact.FirstName} {contact.LastName} <{contact.Email}>");
            }
        }
        else
        {
            Console.Write("No Contact Was Found!");
        }
        Console.ReadKey();
    }
}
