# GraphQLCrudSample
A sample C# GraphQL CRUD API using HotChocolate, EF Core (SQLite), DTOs, and validation.

## Setup
1. `dotnet tool install --global dotnet-ef`
2. `dotnet ef migrations add Init`
3. `dotnet ef database update`
4. `dotnet run`
5. Open `http://localhost:5000/graphql`

## Queries / Mutations
- `addCustomer`, `updateCustomer`, `deleteCustomer`
- `addPurchase`, `updatePurchase`, `deletePurchase`
- Query `customers`, `purchases` with filtering, sorting, projection
