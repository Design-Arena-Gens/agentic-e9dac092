# RCPS – Professional Services Delivery Platform

RCPS (Revenue, Cost & Project Services) is an enterprise-grade project delivery and financial management system designed for professional services organizations. It consolidates engagement delivery, change control, revenue recognition, invoicing, and profitability analytics into a single platform.

## 📦 Solution Layout

```
src/
├── RCPS.Core/             # Domain entities, DTOs, validators, repository contracts
├── RCPS.Infrastructure/   # EF Core DbContext, repository implementations, DI helpers
├── RCPS.Services/         # Business services, AutoMapper profiles, FluentValidation
├── RCPS.Api/              # ASP.NET Core 8 Web API (controllers, configuration)
└── RCPS.Frontend/         # React + TypeScript + Tailwind + ShadCN UI client
```

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/sql-server) (Express or full edition)
- [Node.js 18+](https://nodejs.org/) and npm

### Backend

```bash
cd src
dotnet restore
dotnet build

# Update connection string in RCPS.Api/appsettings.json if required
dotnet ef database update --project RCPS.Infrastructure --startup-project RCPS.Api
dotnet run --project RCPS.Api
```

The API exposes REST endpoints under `/api/*` with Swagger available at `/swagger`.

### Frontend

```bash
cd src/RCPS.Frontend
npm install
VITE_API_URL=https://localhost:7230/api npm run dev
```

For production assets, run `npm run build`. Output will be placed in `dist/`.

## 🧠 Key Features

- Engagement lifecycle management with project health monitoring
- Change request intake, approvals, and financial impact tracking
- Effort logging with billable vs. non-billable classification
- Automated revenue recognition workflow and deferral handling
- Invoicing engine with outstanding balance and reminder automation
- Profitability dashboards combining revenue, cost, and margin analytics
- React-based control center with Tailwind + ShadCN UI components

## 🛡️ Technology Stack

- **Backend:** ASP.NET Core 8 Web API, Entity Framework Core, SQL Server
- **Architecture:** Clean layering (Core, Infrastructure, Services, Api)
- **Frontend:** React (TypeScript), Vite, Tailwind CSS, Recharts, React Query
- **Tooling:** AutoMapper, FluentValidation, Serilog, ShadCN-inspired UI kit

## 📈 Deployment

1. Build and publish the API (IIS, Azure App Service, containers, etc.)
2. Configure environment variable `VITE_API_URL` for the frontend build
3. Deploy frontend assets (e.g., Vercel, static hosting)

## 📄 License

This solution is 100 % open-source and free of commercial dependencies.

---

Customize the implementation, extend domain entities, and add data seeding as needed for your professional services organization.
