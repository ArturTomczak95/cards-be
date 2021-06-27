using System;

namespace DataAccess.Models.Dtos
{
    public class UserCardBaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long CardNumber { get; set; }
        public DateTime ValidThru { get; set; }
        public string OrganizationName { get; set; }
        public string Type { get; set; }
    }
}
