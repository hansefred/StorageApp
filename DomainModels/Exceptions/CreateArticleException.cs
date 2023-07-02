namespace Infrastructure.Exceptions
{
    internal class CreateArticleException : Exception
    {
        public CreateArticleException()
        {
                
        }

        public CreateArticleException(string message) : base (message) { }

    }
}
