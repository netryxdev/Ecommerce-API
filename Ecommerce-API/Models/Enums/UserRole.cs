namespace Ecommerce_API.Models.Enums
{
    public enum UserRole
    {
        admin = 1,
        employee = 2,
        customer = 3
    }

    public class ClaimsConverter
    {
        public static string GetRoleName(int roleId)
        {
            return Enum.GetName(typeof(UserRole), roleId);
        }
    }
}
