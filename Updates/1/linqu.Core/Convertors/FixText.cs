
namespace linqu.Core.Convertors
{
    public static class FixText
    {     
        public static string FixEmail(string email)
        {
            return email.Trim().ToLower();
        }

        public static string FixString(string text)
        {
            return text.Replace(" ", string.Empty).ToLower();
        }
    }
}
