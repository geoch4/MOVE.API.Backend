# MOVE Dancewear API

Backend API för MOVE Dancewear — en e-handelsplattform för danskläder och danskor för Women, Men och Kids.

Byggt med Clean Architecture, CQRS, MediatR, Entity Framework Core och JWT-autentisering.

---

## Tekniker

- **ASP.NET Core Web API** (.NET 10)
- **Clean Architecture** (4 lager)
- **CQRS** med MediatR
- **Entity Framework Core** med SQL Server
- **AutoMapper** för DTOs
- **FluentValidation** med Pipeline Behaviour
- **JWT Bearer** autentisering
- **ASP.NET Core Identity** med rollbaserad access (RBAC)
- **Swagger UI** för API-dokumentation

---

## Projektstruktur

```
MOVE.sln
src/
├── MOVE.API/              # Controllers, Program.cs, appsettings
├── MOVE.Application/      # Commands, Queries, Handlers, DTOs, Validators
├── MOVE.Domain/           # Entities, Interfaces
└── MOVE.Infrastructure/   # DbContext, Repositories, Migrations
```

### Lagerstruktur (Clean Architecture)

```
MOVE.Domain
└── Entities: Product, Category
└── Interfaces: IProductRepository, ICategoryRepository

MOVE.Application
└── Products/Commands: CreateProductCommand, UpdateProductCommand, DeleteProductCommand
└── Products/Queries: GetAllProductsQuery, GetProductByIdQuery
└── Categories/Commands: CreateCategoryCommand, UpdateCategoryCommand, DeleteCategoryCommand
└── Categories/Queries: GetAllCategoriesQuery, GetCategoryByIdQuery
└── DTOs: ProductDto, CategoryDto
└── Mappings: MappingProfile
└── Behaviours: ValidationBehaviour

MOVE.Infrastructure
└── Data: MoveDbContext
└── Repositories: ProductRepository, CategoryRepository
└── Migrations

MOVE.API
└── Controllers: ProductsController, CategoriesController, AuthController
└── Program.cs
```

---

## Kom igång

### Krav

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server eller SQL Server Express

### Installation

1. Klona repot:
```bash
git clone https://github.com/geoch4/MOVE.API.Backend.git
cd MOVE.API.Backend
```

2. Uppdatera connection string i `src/MOVE.API/appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=MoveDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```

3. Kör migrations för att skapa databasen:
```bash
dotnet ef database update --project src/MOVE.Infrastructure --startup-project src/MOVE.API
```

4. Starta API:et:
```bash
dotnet run --project src/MOVE.API
```

5. Öppna Swagger UI: `http://localhost:5267/swagger`

---

## API Endpoints

### Auth

| Method | Endpoint | Beskrivning |
|--------|----------|-------------|
| POST | `/api/auth/register` | Registrera ny användare |
| POST | `/api/auth/login` | Logga in och få JWT-token |

**Registrera Admin:**
```json
{
  "email": "admin@move.com",
  "password": "Admin123!",
  "role": "Admin"
}
```

**Registrera User:**
```json
{
  "email": "user@move.com",
  "password": "User123!",
  "role": "User"
}
```

---

### Products

| Method | Endpoint | Kräver roll | Beskrivning |
|--------|----------|-------------|-------------|
| GET | `/api/products` | — | Hämta alla produkter |
| GET | `/api/products/{id}` | — | Hämta produkt med ID |
| POST | `/api/products` | Admin | Skapa ny produkt |
| PUT | `/api/products/{id}` | Admin | Uppdatera produkt |
| DELETE | `/api/products/{id}` | Admin | Ta bort produkt |

---

### Categories

| Method | Endpoint | Beskrivning |
|--------|----------|-------------|
| GET | `/api/categories` | Hämta alla kategorier |
| GET | `/api/categories/{id}` | Hämta kategori med ID |
| POST | `/api/categories` | Skapa ny kategori |
| PUT | `/api/categories/{id}` | Uppdatera kategori |
| DELETE | `/api/categories/{id}` | Ta bort kategori |

---

## Autentisering

API:et använder JWT Bearer-tokens.

1. Logga in via `POST /api/auth/login`
2. Kopiera token från svaret
3. I Swagger — klicka på **Authorize** och skriv: `Bearer <din-token>`
4. Nu har du tillgång till skyddade endpoints

### Roller

| Roll | Beskrivning |
|------|-------------|
| **Admin** | Full tillgång — kan skapa, uppdatera och ta bort produkter |
| **User** | Läsrättigheter |

---

## Datamodell

### Product
```
Id, Name, Description, Price, StockQuantity, ImageUrl, CreatedAt, CategoryId
```

### Category
```
Id, Name, Description
```

**Relation:** En Category har många Products (1-till-många)

---

## Validering

CreateProductCommand valideras med FluentValidation:
- Name: obligatoriskt, max 200 tecken
- Price: måste vara större än 0
- StockQuantity: kan inte vara negativt
- CategoryId: måste vara ett giltigt ID
- Description: obligatorisk
