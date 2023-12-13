namespace Capstone.DAO
{
    public interface IRecoveryQuestionDao
    {
        public string GetQuestionText(string email);

        public string GetAnswer(string answer);
    }
}

