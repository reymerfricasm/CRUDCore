using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CRUDCore.Models
{
    public partial class Citizen
    {
        private readonly PRGContext _context;

        public int CitizenId { get; set; }
        public long? CitizenIdentification { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        //
        // Summary:
        //     Return if an any data is duplicate in database.
        public bool FindDuplicate(long? id)
        {
            bool value = IsDuplicate(id);

            return value;
        }

        private bool IsDuplicate(long? id)
        {
            return _context.Citizen.Any(c => c.CitizenIdentification == id);
        }
    }
}
