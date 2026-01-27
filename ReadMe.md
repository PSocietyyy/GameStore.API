# GameStores API

**GameStores API** adalah proyek backend berbasis **.NET 10** menggunakan **ASP.NET Core Web API** dan **Entity Framework Core (SQLite)** untuk menyimpan data.

---

## Prasyarat

Sebelum menjalankan proyek ini, pastikan Anda memiliki:

- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) terinstal
- [Visual Studio 2022](https://visualstudio.microsoft.com/) atau editor pilihan lain
- SQLite (opsional, karena database akan dibuat otomatis)

---

## Setup Project

1. **Clone repository**

```bash
git clone https://github.com/PSocietyyy/GameStore.API.git
cd GameStores
cd GameStores.Api
```

2. **Restore paket NuGet**

```bash
dotnet restore
```

3. **Cek konfigurasi database**

Database menggunakan SQLite. Pastikan `appsettings.json` sudah benar:

```json
"ConnectionStrings": {
  "GameStore": "Data Source=GameStore.db"
}
```

Database akan dibuat otomatis di folder project jika belum ada.

4. **Build project**

```bash
dotnet build
```

5. **Menjalankan project**

Ada dua profil environment: `http` dan `https`.

- Menjalankan dengan HTTP:

```bash
dotnet run --launch-profile http
```

- Menjalankan dengan HTTPS:

```bash
dotnet run --launch-profile https
```

Aplikasi akan berjalan di:

- HTTP: `http://localhost:5298`
- HTTPS: `https://localhost:7097`

---

## Struktur Folder

```
GameStores.Api/
│
├─ Data/             # Folder untuk context dan migration EF
├─ Dtos/             # Folder DTO untuk transfer data
│  ├─ Accounts/
│  └─ Developer/
├─ Endpoints/        # Folder untuk endpoint minimal API
└─ Models/           # Folder untuk entity/model database
```

---

## Logging

Level logging default:

```json
"Logging": {
  "LogLevel": {
    "Default": "Information",
    "Microsoft.AspNetCore": "Warning"
  }
}
```

---

## Tools & Dependencies

- **Microsoft.AspNetCore.OpenApi** (10.0.2)
- **Microsoft.EntityFrameworkCore.Design** (10.0.2)
- **Microsoft.EntityFrameworkCore.Sqlite** (10.0.2)

---

## Migrasi Database (Opsional)

Jika ingin menggunakan migration EF Core:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```
