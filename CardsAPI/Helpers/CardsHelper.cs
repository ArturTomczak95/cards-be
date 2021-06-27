using System.Text;
using DataAccess.Models.Dtos;

namespace CardsAPI.Helpers
{
    public static class CardsHelper
    {
        public static string IsUserCardBaseDtoInvalid(UserCardBaseDto userCardBase)
        {
            StringBuilder message = new StringBuilder();

            if (string.IsNullOrEmpty(userCardBase.FirstName))
            {
                message.Append("First name cannot be empty,");
            }

            if (string.IsNullOrEmpty(userCardBase.LastName))
            {
                message.Append("Last name cannot be empty,");
            }

            if (string.IsNullOrEmpty(userCardBase.OrganizationName))
            {
                message.Append("Organization name cannot be empty,");
            }

            if (string.IsNullOrEmpty(userCardBase.Type))
            {
                message.Append("Organization name cannot be empty,");
            }

            return message.ToString();
        }
    }
}
