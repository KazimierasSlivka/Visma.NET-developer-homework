using System;
using ContactManager.Services;
using ContactManager.Models;
using System.Collections.Generic;
using System.Linq;

namespace ContactManager.ConsoleApplication
{
    public class ContactsManager
    {
        private readonly IContactCrud _contactCrud ; 

        public ContactsManager(IContactCrud contactCrud)
        {
            _contactCrud = contactCrud;
        }

        public void Run()
        {
            int option;
            do
            {
                Select();
                option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 0:
                        break;
                    case 1:
                        GetAllContacts();
                        break;
                    case 2:
                        CreateNewContact();
                        break;
                    case 3:
                        UpdateContactByPhoneNumber();
                        break;
                    case 4:
                        DeleteContactByPhoneNumber();
                        break;
                    default:
                        Console.WriteLine("Not possible option");
                        Console.WriteLine();
                        break;
                }
            }
            while (option != 0);
        }

        void Select()
        {
            Console.WriteLine("Select option and press ENTER:");
            Console.WriteLine("1. Contacts List");
            Console.WriteLine("2. Create new contact");
            Console.WriteLine("3. Update contact");
            Console.WriteLine("4. Remove contact");
            Console.WriteLine();
            Console.WriteLine("0. EXIT");
            Console.WriteLine();
        }

        void CreateNewContact()
        {
            ContactModel contact = new ContactModel();

            Console.Write("Enter NEW contact NAME: ");
            contact.Name = Console.ReadLine();
            Console.Write("Enter NEW contact LAST NAME: ");
            contact.LastName = Console.ReadLine();
            Console.Write("Enter NEW contact PHONE NUMBER: ");
            contact.PhoneNumber = int.Parse(Console.ReadLine());
            Console.Write("Enter NEW contact ADDRESS: ");
            contact.Address = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Wait...");
            Console.WriteLine();

            if (_contactCrud.CreateNewContact(contact) == true)
            {
                Console.WriteLine("Contact is created");
                Console.WriteLine();
            }                
            else
            {
                Console.WriteLine("Contact is not created. Make sure that phone number is unique in database or other errors cause problem");
                Console.WriteLine();
            }                
        }

        void GetAllContacts()
        {
             IEnumerable<ContactModel> contacts =  _contactCrud.GetAllContacts();

            if (contacts != null || contacts.Count() != 0)
                foreach (var contact in contacts)
                {
                    Console.WriteLine();
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine("Name: {0}", contact.Name);
                    Console.WriteLine("Last Name: {0}", contact.LastName);
                    Console.WriteLine("Phone number: {0}", contact.PhoneNumber);
                    Console.WriteLine("Address: {0}", contact.Address);
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine();
                }
            else
                Console.WriteLine("List is empty or database error");
        }

        void UpdateContactByPhoneNumber()
        {
            ContactModel contact = new ContactModel();
            int phoneNumber;

            Console.Write("Enter contact PHONE NUMBER to update:");
            phoneNumber = int.Parse(Console.ReadLine());

            GetContactByPhoneNumber(phoneNumber);

            Console.Write("Enter UPDATED contact NAME: ");
            contact.Name = Console.ReadLine();
            Console.Write("Enter UPDATED contact LAST NAME: ");
            contact.LastName = Console.ReadLine();
            Console.Write("Enter UPDATED contact PHONE NUMBER: ");
            contact.PhoneNumber = int.Parse(Console.ReadLine());
            Console.Write("Enter UPDATED contact ADDRESS: ");
            contact.Address = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Wait...");
            Console.WriteLine();

            if (_contactCrud.UpdateContactByPhoneNumber(contact, phoneNumber) == true)
            {
                Console.WriteLine("Contact is updated");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Contact is not updated. Make sure that phone number is unique in database or other errors cause problem");
                Console.WriteLine();
            }
        }

        void GetContactByPhoneNumber(int phoneNumber)
        {
            var contact = _contactCrud.GetContactByPhoneNumber(phoneNumber);

            if (contact != null)
            {
                Console.WriteLine();
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("Name: {0}", contact.Name);
                Console.WriteLine("Last Name: {0}", contact.LastName);
                Console.WriteLine("Phone number: {0}", contact.PhoneNumber);
                Console.WriteLine("Address: {0}", contact.Address);
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No contact or database error");
                Console.WriteLine();
            }
        }

        void DeleteContactByPhoneNumber()
        {
            int phoneNumber;

            Console.Write("Enter contact PHONE NUMBER to delete: ");
            phoneNumber = int.Parse(Console.ReadLine());

            bool isContactDeleted = _contactCrud.DeleteContactByPhoneNumber(phoneNumber);

            Console.WriteLine();
            Console.WriteLine("Wait...");
            Console.WriteLine();

            if (isContactDeleted == true)
                Console.WriteLine("Contact deleted");
            else
                Console.WriteLine("No contact or database error");
        }
    }
}
