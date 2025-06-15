namespace SampleGraphQLCRUD.API;

public class ErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        return error.WithMessage(error.Exception?.Message ?? error.Message);
    }
}