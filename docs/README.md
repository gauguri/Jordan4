# Capco Jordan Almonds Store

Capco Confectionery's e-commerce experience built with ASP.NET Core MVC, Entity Framework Core, and SQL Server. The site mirrors a MilkCratesDirect-style flow with responsive HTML/CSS, Stripe-ready checkout, and an admin dashboard.

## Prerequisites

1. [.NET 8 SDK](https://dotnet.microsoft.com/)
2. SQL Server (LocalDB or full edition)
3. PowerShell 7+
4. Stripe test API key

## Configuration

1. Copy `src/Capco.Web/appsettings.Development.json.sample` to `src/Capco.Web/appsettings.Development.json`.
2. Update the following keys:
   - `ConnectionStrings:DefaultConnection` – SQL Server connection string.
   - `Stripe:SecretKey` and `Stripe:PublishableKey` – Stripe test keys.
   - `Smtp:*` – Optional SMTP settings for order confirmation emails.

## Database Migrations

```powershell
dotnet ef database update --project src/Capco.Data --startup-project src/Capco.Web
```

### Manual SQL Initialization

An equivalent schema script is available at [`docs/init.sql`](init.sql).

## Running the Solution

```powershell
dotnet build
dotnet run --project src/Capco.Web
```

Navigate to `https://localhost:5001` (or the configured port).

### Default Accounts

- **Admin:** `admin@capco.local` / `Pass!123`

## Stripe Webhooks (Development)

1. Install the Stripe CLI.
2. Run `stripe listen --forward-to https://localhost:5001/webhooks/stripe`.
3. Configure the signing secret in `appsettings.Development.json` if using webhook validation.

## Deployment Notes

- **IIS**: Publish via `dotnet publish`, configure the app pool for `No Managed Code`, and set environment variables for connection strings and API keys.
- **Azure App Service**: Deploy the published output, set configuration values (connection string, Stripe keys, SMTP) in the Azure portal.

## Testing

Run the automated tests:

```powershell
dotnet test
```

The `Capco.Tests` project includes service-level unit tests using xUnit and EF Core's in-memory provider.

## Project Structure

```
/CapcoAlmonds.sln
/src
  /Capco.Web        # MVC site, Identity UI, Razor views, static assets
  /Capco.Domain     # Entities and identity models
  /Capco.Data       # DbContext, EF Core configurations, migrations, seeding
  /Capco.Services   # Business logic services (catalog, cart, orders, payments)
/tests
  /Capco.Tests      # Automated tests
/docs
  README.md         # This file
  init.sql          # Database DDL script
/assets
  prompts.md        # Image generation prompts
```

## Seeding

On first run, the application seeds:

- Five Jordan Almond products (White, Pink, Blue, Yellow, Green) with three size variants each.
- Placeholder product imagery.
- Content blocks for About, Wholesale, and Contact pages.
- Default admin account and roles.

Order confirmation emails are logged via a stub email sender; plug in SMTP to enable delivery.
