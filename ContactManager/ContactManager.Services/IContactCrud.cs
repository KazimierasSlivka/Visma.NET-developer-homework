using System.Threading.Tasks;
using ContactManager.Models;
using System.Collections.Generic;

namespace ContactManager.Services
{
    public interface IContactCrud
    {
        bool CreateNewContact(ContactModel contact);
        IEnumerable<ContactModel> GetAllContacts();
        bool UpdateContactByPhoneNumber(ContactModel newContact, int phoneNumber);
        ContactModel GetContactByPhoneNumber(int phoneNumber);
        bool DeleteContactByPhoneNumber(int phoneNumber);

    }
}
