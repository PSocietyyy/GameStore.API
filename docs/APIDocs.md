# GameStores API Documentation

Base URL: `http://localhost:5298`

> Semua endpoint menggunakan **JSON** untuk request dan response, kecuali disebutkan lain.

---

## Accounts Endpoints

### Get All Accounts

```
GET /accounts/
```

**Response 200 OK**

```json
[
  {
    "id": 1,
    "name": "John Doe",
    "email": "john@example.com",
    "role": 1
  },
  {
    "id": 2,
    "name": "Alice Smith",
    "email": "alice@example.com",
    "role": 2
  }
]
```

---

### Get Account by ID

```
GET /accounts/{id}
```

**Path Parameters:**

| Parameter | Type | Description                |
| --------- | ---- | -------------------------- |
| id        | int  | ID akun yang ingin diambil |

**Response 200 OK (Regular User)**

```json
{
  "id": 4,
  "name": "Ferdiansyah Pratama",
  "email": "ferdi@example.test",
  "role": 1
}
```

**Response 200 OK (Developer User)**

```json
{
  "id": 4,
  "name": "Ferdiansyah Pratama",
  "email": "ferdi@example.test",
  "role": 2,
  "studioName": "Ferdi INC",
  "description": null,
  "website": null
}
```

**Response 404 Not Found**

```json
{
  "error": "Account not found"
}
```

---

### Create Account

```
POST /accounts
Content-Type: application/json
```

**Request Body:**

```json
{
  "name": "Ferdiansyah Pratama",
  "email": "ferdi@example.test",
  "password": "ferdi*2122",
  "role": 2,
  "studioName": "Ferdi INC"
}
```

**Response 201 Created**

```json
{
  "id": 4,
  "name": "Ferdiansyah Pratama",
  "email": "ferdi@example.test",
  "role": 2
}
```

---

### Update Account

```
PUT /accounts/{id}
Content-Type: application/json
```

**Request Body (Update Account Info)**

```json
{
  "name": "Ferdiansyah Pratama",
  "email": "ferdiansyahpratama716@example.test",
  "password": "ferdiansyah*2122",
  "role": 1
}
```

**Response 204 No Content**

**Response 404 Not Found**

```json
{
  "error": "Account not found"
}
```

---

### Delete Account

```
DELETE /accounts/{id}
```

**Response 204 No Content**

**Response 404 Not Found**

```json
{
  "error": "Account not found"
}
```

---

## Developers Endpoints

### Get All Developers

```
GET /developers/
```

**Response 200 OK**

```json
[
  {
    "id": 1,
    "studioName": "Rockstar INC",
    "description": "Studio game populer",
    "website": "www.rockstar.com"
  }
]
```

---

### Get Developer by ID

```
GET /developers/{id}
```

**Response 200 OK**

```json
{
  "id": 1,
  "studioName": "Rockstar INC",
  "description": "Studio game populer",
  "website": "www.rockstar.com",
  "name": "John Doe",
  "email": "john@example.com"
}
```

**Response 404 Not Found**

```json
{
  "error": "Developer not found"
}
```

---

### Update Developer

```
PUT /developers/{id}
Content-Type: application/json
```

**Request Body:**

```json
{
  "studioName": "Rockstar Company",
  "description": "Rockstar adalah studio pengembang game yang populer dan salah satu yang terbesar",
  "website": "www.rockstar.com"
}
```

**Response 204 No Content**

**Response 404 Not Found**

```json
{
  "error": "Developer not found"
}
```

---

### Notes

- Role di Accounts:
  - `1` = User Biasa
  - `2` = Developer
  - `3` = Admin

- Password akan di-hash otomatis saat create atau update.
- Developer profile otomatis dibuat jika role = 2 saat create account.
