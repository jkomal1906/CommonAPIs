# CommonAPIs

CommonAPIs is a backend API project built with ASP.NET Core that provides essential user authentication and management features like registration, login, password reset, and OTP (One-Time Password) verification. Designed as a foundational service, it helps any application implement secure user authentication with ease.

---

## 🚀 Features

- **User Registration:** Sign up new users with their email and password.
- **User Login:** Authenticate users and provide a JWT token for secure access.
- **Forgot Password:** Send secure password reset emails.
- **Reset Password:** Allow users to safely update their password.
- **Send OTP:** Generate and email a One-Time Password (OTP) for verification.

---

## 🛠 Prerequisites

- .NET 7 SDK or higher (https://dotnet.microsoft.com/download)
- SQL Server (or compatible) database instance
- SMTP email account for sending emails (e.g., Gmail SMTP)

**Key NuGet Packages:**
- Microsoft.EntityFrameworkCore
- Microsoft.AspNetCore.Identity
- Microsoft.IdentityModel.Tokens
- BCrypt.Net-Next
- System.IdentityModel.Tokens.Jwt

---

## ⚡️ Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/jkomal1906/CommonAPIs.git
cd CommonAPIs
```

### 2. Configure the Environment

Edit `appsettings.json` and update the following settings:

- **ConnectionStrings:** Add your database connection string.
- **JwtSettings:** Set values for `SecretKey`, `Issuer`, `Audience`, and `ExpiryMinutes`.
- **SMTP Settings:** Add your email credentials (for example, Gmail SMTP).

### 3. Apply Migrations & Update Database

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Run the API

```bash
dotnet run
```

Your API will be available at:  
`https://localhost:<port>/api/CommonAPI/`

---

## 📦 Example API Usage

### Register a New User

```
POST /api/CommonAPI/register
Content-Type: application/json

{
  "firstName": "John",
  "lastName": "Doe",
  "userName": "johndoe",
  "email": "john@example.com",
  "password": "YourPassword123!",
  "confirmPassword": "YourPassword123!"
}
```

### User Login

```
POST /api/CommonAPI/login
Content-Type: application/json

{
  "email": "john@example.com",
  "password": "YourPassword123!"
}
```

### Forgot Password

```
POST /api/CommonAPI/forgot-password
Content-Type: application/json

{
  "email": "john@example.com"
}
```

### Reset Password

```
POST /api/CommonAPI/reset-password
Content-Type: application/json

{
  "email": "john@example.com",
  "newPassword": "NewSecurePassword123!"
}
```

### Send OTP

POST /api/CommonAPI/send-otp
Content-Type: application/json

{
  "email": "john@example.com"
}

## 🤝 Contributing

Contributions are very welcome! Please fork the repository and submit a pull request with a clear description of your changes.

- For issues or feature requests, [open an issue](https://github.com/jkomal1906/CommonAPIs/issues).
- For direct contact, email the maintainer: [jkomal1906@gmail.com](mailto:jkomal1906@gmail.com)


**Happy Coding! 🚀**
