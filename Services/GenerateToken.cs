namespace API_Users.Services
{
    public class GenerateToken
    {
        public string GenerateTokens()
        {
            Random random = new Random();
            string numbers = "0123456789";
            string letters = "abcdefghijklmnopqrstuvwxyz";
            string lettersUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string specials = ".-,@/*+{}[]?=#&%()|°";
            string token = "";
            for (int i = 0; i < 5; i++)
            {
                int indexNumbersRandom = random.Next(0, 10);
                token += numbers[indexNumbersRandom];
                int indexLetterRandom = random.Next(0, 26);
                token += letters[indexLetterRandom];
                token += lettersUpper[indexLetterRandom];
                int indexSpecialRandom = random.Next(0, 20);
                token += specials[indexSpecialRandom];
            }
            return token;
        }
    }
}
