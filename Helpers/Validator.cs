namespace Commento
{
    public static class Validator
    {
        public static bool QueryValidator(string hmac, string token)
        {
            if (hmac == null || token == null)
                return false;
            return true;
        }
    }
}
