using Microsoft.EntityFrameworkCore;
using SampleGraphQLCRUD.API.Data;
using SampleGraphQLCRUD.API.GraphQL;
using SampleGraphQLCRUD.API.GraphQL.Queries;
using SampleGraphQLCRUD.API.GraphQL.Types;

namespace SampleGraphQLCRUD.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Use SQLite file
            builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(opt =>
                opt.UseSqlite("Data Source=GraphQLCrud.db"));

            builder.Services
                .AddGraphQLServer()
                .AddQueryType<CustomerQuery>()
                .AddQueryType<PurchaseQuery>()
                .AddMutationType<Mutation>()
                .AddType<CustomerType>()
                .AddType<PurchaseType>()
                .AddProjections()
                .AddFiltering()
                .AddSorting();

            var app = builder.Build();

            app.Run();
        }
    }
}