using System.Collections.Generic;
using System.Linq;
using ContactManager.Data;
using ContactManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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
                    dbContext.Contacts.Add(contact);
                    dbContext.SaveChanges();
                    return true;
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
                    var contactFromDb = GetContactByPhoneNumber(contact.PhoneNumber);
                    if(contactFromDb != null)
                    {
                        dbContext.Entry(contact).State = EntityState.Modified;
                        dbContext.SaveChangesAsync();
                    }
                    return true;
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
