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
   - `ConnectionStrings:DefaultConnection` – SQL Server connection string. The provided sample uses the `capco_app` SQL login created by the install script.
   - `Stripe:SecretKey` and `Stripe:PublishableKey` – Stripe test keys.
   - `Smtp:*` – Optional SMTP settings for order confirmation emails.

## Database Provisioning

The application expects the SQL Server database to be pre-provisioned. Run the install script to rebuild the schema, seed data, and create a dedicated SQL login on `WINDESKTOP\\SQLEXPRESS`:

```powershell
sqlcmd -S WINDESKTOP\\SQLEXPRESS -i docs/sql/Install_WINDESKTOP_SQLEXPRESS.sql
```

Update `appsettings.Development.json` if you need a different server name or credentials.

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
  /Capco.Data       # DbContext and EF Core configurations
  /Capco.Services   # Business logic services (catalog, cart, orders, payments)
/tests
  /Capco.Tests      # Automated tests
/docs
  README.md         # This file
  /sql              # SQL Server installation scripts
/assets
  prompts.md        # Image generation prompts
```

Order confirmation emails are logged via a stub email sender; plug in SMTP to enable delivery.
