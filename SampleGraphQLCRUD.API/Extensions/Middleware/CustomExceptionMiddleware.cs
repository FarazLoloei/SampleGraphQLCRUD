using HotChocolate.Resolvers;
using System.ComponentModel.DataAnnotations;

namespace SampleGraphQLCRUD.API.Extensions.Middleware;

public class CustomExceptionMiddleware(FieldDelegate next)
{
    public async Task InvokeAsync(IMiddlewareContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            // Handle data validation errors
            context.ReportError(ErrorBuilder.New()
                .SetMessage(ex.Message)
                .SetCode("VALIDATION_ERROR")
                .SetPath(context.Path)
                .AddLocation(context.Selection.SyntaxNode)
                .Build());
        }
        catch (InvalidOperationException ex)
        {
            // Handle business rule violations
            context.ReportError(ErrorBuilder.New()
                .SetMessage(ex.Message)
                .SetCode("BUSINESS_RULE_VIOLATION")
                .SetPath(context.Path)
                .AddLocation(context.Selection.SyntaxNode)
                .Build());
        }
        catch (Exception ex)
        {
            // Handle unexpected errors
            context.ReportError(ErrorBuilder.New()
                .SetMessage("An unexpected error occurred")
                .SetCode("INTERNAL_SERVER_ERROR")
                .SetException(ex)
                .SetPath(context.Path)
                .AddLocation(context.Selection.SyntaxNode)
                .Build());
        }
    }
}