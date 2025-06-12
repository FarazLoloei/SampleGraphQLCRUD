# Sample GraphQL CRUD API with .NET 8

This project demonstrates a clean and modern implementation of a GraphQL CRUD API using .NET 8, HotChocolate, and Entity Framework Core. It includes two entities: **Customer** and **Purchase**, with full support for creating, reading, updating, and deleting data.

---

## 📦 Features

- .NET 8 Web API with GraphQL endpoint
- GraphQL server powered by **HotChocolate**
- Entity Framework Core with SQLite persistence
- Clean architecture with DTOs and validation
- Field descriptions using HotChocolate `ObjectType<T>` for self-documenting GraphQL schema
- Filtering, sorting, and projection support
- Private setters on models for immutability

---

## 📚 Technologies Used

- [.NET 8](https://dotnet.microsoft.com/)
- [HotChocolate](https://chillicream.com/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [SQLite](https://www.sqlite.org/)
- [DataAnnotations](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations)

---

## 🧱 Project Structure

```
SampleGraphQLCRUD/
├── GraphQL/
│   ├── Types/
│   │   ├── CustomerType.cs
│   │   └── PurchaseType.cs
│   ├── Queries/
│   │   └── Query.cs
│   └── Mutations/
│       └── Mutation.cs
├── Models/
│   ├── Customer.cs
│   └── Purchase.cs
├── Data/
│   └── AppDbContext.cs
├── DTOs/
│   ├── CustomerInput.cs
│   └── PurchaseInput.cs
├── Program.cs
└── README.md
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) or VS Code

### Run the Application

```bash
dotnet restore
dotnet ef database update
dotnet run
```

Visit the GraphQL Playground:

```
http://localhost:5000/graphql
```

---

## 🧪 Example Queries

### Get all customers

```graphql
query {
  customers {
    id
    firstName
    lastName
    email
    purchases {
      productName
      price
    }
  }
}
```

### Create a new customer

```graphql
mutation {
  createCustomer(
    input: {
      firstName: "Faraz"
      lastName: "Loloei"
      email: "faraz@example.com"
    }
  ) {
    id
    email
  }
}
```

---

## ✍️ Author

**Faraz Loloei**  
Software engineer, Software analyzer and Software developer also Web & Data Science Master's Student | .NET Developer | GraphQL Enthusiast  
🇩🇪 Currently based in Germany

---

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
