# **OnWay - Logistics & Delivery Management API**

🚚 **Streamline your logistics operations** with OnWay, a robust **C# ASP.NET Core API** for managing shipments, deliveries, drivers, and vehicles. Built with **Domain-Driven Design (DDD)** and **Clean Architecture**, OnWay provides a scalable solution for logistics businesses.

---

## ✨ **Features**

✅ **Authentication & Authorization** – Secure login, registration, and account verification
✅ **Shipment Management** – Create, track, and manage shipments with real-time updates
✅ **Driver & Vehicle Assignment** – Assign drivers and vehicles to shipments efficiently
✅ **Geolocation & Routing** – Generate Waze routes using **Geoapify API**
✅ **Package Tracking** – Add, track, and manage packages within shipments
✅ **Real-Time Status Updates** – Monitor shipment status (Created, In Transit, Delivered, etc.)
✅ **Scalable & Maintainable** – Built with **DDD, CQRS, and Repository Pattern**

---

## 🛠️ **Tech Stack**

| Category          | Technologies Used                          |
|-------------------|------------------------------------------|
| **Language**      | C# (.NET 8)                              |
| **Framework**     | ASP.NET Core Web API                      |
| **Architecture**  | Domain-Driven Design (DDD)               |
| **Database**      | SQL (PostgreSQL, SQL Server)             |
| **Geocoding**     | Geoapify API (for address resolution)    |
| **Routing**       | Waze API (for real-time navigation)       |
| **Authentication**| JWT (JSON Web Tokens)                    |
| **Testing**       | xUnit, Moq (Mocking)                     |

---

## 📦 **Installation**

### **Prerequisites**
- **.NET 8 SDK** ([Download here](https://dotnet.microsoft.com/download))
- **PostgreSQL / SQL Server** (for database storage)
- **Geoapify API Key** (for geolocation services)
- **Waze API Key** (for route generation)

### **Quick Start**

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-repo/OnWay.git
   cd OnWay
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Set up environment variables**
   Create a `.env` file in `src/ONW_API/` with:
   ```env
   ASPNETCORE_ENVIRONMENT=Development
   GEOAPIFY_API_KEY=your_geoapify_key
   WAZE_API_KEY=your_waze_key
   ```

4. **Run migrations (if using Entity Framework Core)**
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

5. **Start the API**
   ```bash
   dotnet run --project src/ONW_API/ONW_API.csproj
   ```

6. **Access the API**
   The API will be available at `http://localhost:5000`

---

## 🎯 **Usage Examples**

### **1. Register a Transporter**
```csharp
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

var client = new HttpClient();
var url = "https://localhost:5000/api/auth/register";

var command = new
{
    Email = "transporter@example.com",
    Password = "SecurePassword123!",
    Name = "John Doe",
    Phone = "+5511999999999"
};

var json = JsonSerializer.Serialize(command);
var content = new StringContent(json, Encoding.UTF8, "application/json");

var response = await client.PostAsync(url, content);
var result = await response.Content.ReadAsStringAsync();
Console.WriteLine(result);
```

### **2. Login & Get JWT Token**
```csharp
var loginUrl = "https://localhost:5000/api/auth/login";
var loginCommand = new { Email = "transporter@example.com", Password = "SecurePassword123!" };
var loginJson = JsonSerializer.Serialize(loginCommand);
var loginContent = new StringContent(loginJson, Encoding.UTF8, "application/json");

var loginResponse = await client.PostAsync(loginUrl, loginContent);
var loginResult = await loginResponse.Content.ReadAsStringAsync();
var loginData = JsonSerializer.Deserialize<dynamic>(loginResult);
var token = loginData.AccessToken;
```

### **3. Create a Shipment**
```csharp
var shipmentUrl = "https://localhost:5000/api/shipments";
var shipmentRequest = new
{
    Origin = new { Address = "Rua A", City = "São Paulo", State = "SP" },
    Destination = new { Address = "Rua B", City = "Rio de Janeiro", State = "RJ" }
};

var shipmentJson = JsonSerializer.Serialize(shipmentRequest);
var shipmentContent = new StringContent(shipmentJson, Encoding.UTF8, "application/json");
shipmentContent.Headers.Add("Authorization", $"Bearer {token}");

var shipmentResponse = await client.PostAsync(shipmentUrl, shipmentContent);
var shipmentResult = await shipmentResponse.Content.ReadAsStringAsync();
Console.WriteLine(shipmentResult);
```

### **4. Generate a Waze Route**
```csharp
var wazeUrl = "https://localhost:5000/geolocation/waze-route";
var wazeRequest = new
{
    Street = "Rua A",
    Number = "123",
    District = "Centro",
    City = "São Paulo",
    State = "SP",
    ZipCode = "01000000"
};

var wazeJson = JsonSerializer.Serialize(wazeRequest);
var wazeContent = new StringContent(wazeJson, Encoding.UTF8, "application/json");
wazeContent.Headers.Add("Authorization", $"Bearer {token}");

var wazeResponse = await client.PostAsync(wazeUrl, wazeContent);
var wazeResult = await wazeResponse.Content.ReadAsStringAsync();
Console.WriteLine(wazeResult);
```

---

## 📁 **Project Structure**

```
OnWay/
├── src/
│   ├── ONW_API/                  # Main API project
│   │   ├── API/                  # Controllers & DTOs
│   │   ├── Application/          # Use Cases & Commands
│   │   ├── Domain/               # Core business logic
│   │   ├── Infrastructure/       # External services (Geoapify, Waze)
│   │   └── ONW_API.csproj        # Project file
│   └── Tests/                    # Unit & Integration Tests
├── .gitignore
├── README.md                     # This file
└── LICENSE
```

---

## 🔧 **Configuration**

### **Environment Variables**
| Variable          | Description                          | Example Value                     |
|-------------------|--------------------------------------|-----------------------------------|
| `GEOAPIFY_API_KEY` | Geoapify API key for geocoding      | `your_geoapify_api_key_here`      |
| `WAZE_API_KEY`    | Waze API key for route generation    | `your_waze_api_key_here`          |
| `JWT_SECRET`      | Secret key for JWT authentication    | `your_jwt_secret_here`            |

### **Database Configuration**
- Modify `appsettings.json` for connection strings:
  ```json
  {
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Database=OnWayDB;User Id=postgres;Password=yourpassword;"
    }
  }
  ```

---

```bash
git clone https://github.com/your-repo/OnWay.git
cd OnWay
dotnet run
```

🔗 **API Documentation:** [Swagger UI](http://localhost:5000/swagger) (after running the API)

---

