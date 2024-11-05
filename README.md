# Contact Management API

A .NET Core 6.0 Web API for managing contacts, built using the repository pattern and xUnit for unit testing. This API uses a JSON file to mock a database, providing an easy-to-use solution for storing and retrieving contact information.

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Configuration](#configuration)
- [Usage](#usage)
- [Running Tests](#running-tests)
- [API Endpoints](#api-endpoints)
- [Contributing](#contributing)

---

## Features

- **CRUD operations**: Add, retrieve, update, and delete contacts.
- **Repository Pattern**: Separates data access logic for easy maintenance and testability.
- **Mock Database**: Uses a JSON file as a mock database for demonstration and testing.
- **Unit Testing**: Comprehensive unit tests using xUnit.

## Technologies Used

- **.NET Core 6.0**
- **xUnit** for unit testing
- **Repository Pattern**
- **JSON** for mock data storage

---

## Project Structure

```plaintext
ContactManagementAPI/
├── Controllers/          # API Controllers for handling HTTP requests
├── Interfaces/           # Interfaces defining repository contracts
├── Models/               # Contact models and JSON structure
├── Repositories/         # Repository implementations for data access
├── Data/                 # Mock JSON data file and data context
├── ContactManagementAPI.csproj
└── README.md
