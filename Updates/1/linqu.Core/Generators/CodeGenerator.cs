

using System;


namespace linqu.Core.Generators
{
    public static class CodeGenerator
    {
        public static string GenerateUniqCode(int length = 5)
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, length);
        }

        public static Guid GetId()
        {
            return Guid.NewGuid();   // Create Uniqe IdentiFier Code
        }

        public static string GetUniqeFileName()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        public static string GetOrderCode()
        {
            var random = new Random();
            return random.Next(10000000, 99999999).ToString();
        }
    }
}
