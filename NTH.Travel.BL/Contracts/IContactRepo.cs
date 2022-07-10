using NTH.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTH.Travel.BL.Contracts
{
    public interface IContactRepo
    {
        public Task<IEnumerable<Contact>> GetContacts();
        public Task<IEnumerable<Contact>> GetContactsList();
    }
}
