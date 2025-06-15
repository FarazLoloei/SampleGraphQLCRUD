using Microsoft.EntityFrameworkCore;
using SampleGraphQLCRUD.API.Abstraction;
using SampleGraphQLCRUD.API.Data;
using SampleGraphQLCRUD.API.Extensions.Middleware;
using SampleGraphQLCRUD.API.GraphQL.Mutations;
using SampleGraphQLCRUD.API.GraphQL.Queries;
using SampleGraphQLCRUD.API.GraphQL.Types;
using SampleGraphQLCRUD.API.Services;

namespace SampleGraphQLCRUD.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Use SQLite file
            builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(opt =>
                opt.UseInMemoryDatabase("GraphQLCrud"));

            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IPurchaseService, PurchaseService>();

            builder.Services.AddGraphQLServer()
                .AddQueryType<CustomerQuery>()
                .AddQueryType<PurchaseQuery>()
                .AddMutationType<CustomerMutation>()
                .AddMutationType<PurchaseMutation>()
                .AddType<CustomerType>()
                .AddType<PurchaseType>()
                .AddErrorFilter<ErrorFilter>()
                .UseField<CustomExceptionMiddleware>()
                .AddProjections()
                .AddFiltering()
                .AddSorting();

            var app = builder.Build();

            app.MapGraphQL(); // Endpoint: /graphql
            app.Run();
        }
    }
}