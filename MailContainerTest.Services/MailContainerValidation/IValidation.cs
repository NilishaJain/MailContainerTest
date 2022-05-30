namespace MailContainerTest.Services.MailContainerValidation
{
    public interface IValidation<T>
    {
        bool Validate(T container);
    }
}
