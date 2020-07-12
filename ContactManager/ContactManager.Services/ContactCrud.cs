using System.Collections.Generic;
using System.Linq;
using ContactManager.Data;
using ContactManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Services
{
    public class ContactCrud : IContactCrud
    {
        public bool CreateNewContact(ContactModel contact)
        {
            using (var dbContext = new ContactManagerDbContext())
            {
                try
                {
                    var contacts = dbContext.Contacts
                        .Where(p => p.PhoneNumber == contact.PhoneNumber)
                        .Select(p => p);

                    if (contacts.Count() == 0)
                    {
                        dbContext.Contacts.Add(contact);
                        dbContext.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        public IEnumerable<ContactModel> GetAllContacts()
        {
            using (var dbContext = new ContactManagerDbContext())
            {
                try
                {
                    return dbContext.Contacts.ToList();
                }
                catch
                {
                    return null;
                }
            }
        }

        public bool UpdateContactByPhoneNumber(ContactModel newContact, int phoneNumber)
        {
            using (var dbContext = new ContactManagerDbContext())
            {
                try
                {
                    var contactFromDb = GetContactByPhoneNumber(phoneNumber);
                    if(contactFromDb != null)
                    {
                        var contacts = dbContext.Contacts
                            .Where(p => p.PhoneNumber == newContact.PhoneNumber)
                            .Select(p => p);

                        if (contacts.Count() == 0)
                        {
                            contactFromDb.Name = newContact.Name;
                            contactFromDb.LastName = newContact.LastName;
                            contactFromDb.PhoneNumber = newContact.PhoneNumber;
                            contactFromDb.Address = newContact.Address;

                            dbContext.Entry(contactFromDb).State = EntityState.Modified;
                            dbContext.SaveChanges();

                            return true;
                        }
                        return false;
                    }
                    return false;                    
                }
                catch
                {
                    return false;
                }
            }
        }

        public ContactModel GetContactByPhoneNumber(int phoneNumber)
        {
            using (var dbContext = new ContactManagerDbContext())
            {
                try
                {
                    List<ContactModel> contacts = GetAllContacts().ToList();
                    var contact = contacts.Find(contact => contact.PhoneNumber == phoneNumber);

                    return contact;
                }
                catch
                {
                    return null;
                }
            }
        }

        public bool DeleteContactByPhoneNumber(int phoneNumber)
        {
            using (var dbContext = new ContactManagerDbContext())
            {
                try
                {
                    List<ContactModel> contacts = GetAllContacts().ToList();
                    var contact = contacts.Find(contact => contact.PhoneNumber == phoneNumber);
                    if (contact != null)
                    {
                        dbContext.Contacts.Remove(contact);
                        dbContext.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
