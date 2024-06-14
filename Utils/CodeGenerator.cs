namespace BaobabBackEndService.Utils
{
    public class RandomCodeGenerator
    {
        public static string CodeGenerator(int lenght)
        {
            const string charactersAllowed = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var code = new string(Enumerable.Repeat(charactersAllowed, lenght)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return code;
        }
    }
}
