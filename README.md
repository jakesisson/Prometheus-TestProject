# Prometheus-TestProject

## Prometheus API and UI Test Suite

This project contains two distinct test suites:

- One for validating the behavior of the [JSONPlaceholder API](https://jsonplaceholder.typicode.com/)
- One for automating a user journey in a browser using [Playwright](https://playwright.dev/dotnet/)

---

## Test Suites

### 1. Rest API Tests (`RestApiTests`)

This test suite covers Create, Read, Update, and Delete (CRUD) operations, along with validation and deserialization tests for multiple JSONPlaceholder endpoints.

**Libraries and tools used:**

- RestSharp for HTTP operations  
- xUnit for the test framework  
- FluentAssertions for readable assertions  

### 2. UI Automation Tests (`PlaywrightTests`)

This test suite uses Playwright to automate a user scenario in the browser:

- Opens Google and searches for the term **"Prometheus Group"**  
- Navigates to the Prometheus Group website  
- Enters sample data in the Contact form  
- Submits the form and validates required field errors  

---

## Setup Instructions

### Step 1: Clone the Repository

git clone https://github.com/jakesisson/Prometheus-TestProject.git
cd Prometheus-TestProject

### Step 2: Install Dependencies

Ensure that .NET 6.0 SDK (or later) is installed on your system.

Restore required packages:

dotnet restore

### Install Playwright browser dependencies:

cd PlaywrightTests
playwright install

### Step 3: Verify Configuration
Ensure the appsettings.json file is located at RestApiTests/Config/appsettings.json with the correct base URL:

{
  "BaseUrl": "https://jsonplaceholder.typicode.com/"
}

## Confirm Build

dotnet build

## Running the Tests
### To run all tests in both projects:

dotnet test

### To run an individual suite:

dotnet test RestApiTests
dotnet test PlaywrightTests

## Test Coverage Overview

### API Tests (RestApiTests)
Includes endpoint tests for:
- posts
- comments
- albums
- photos
- todos
- users

## Test types:

GET by ID
POST with valid payloads
PUT and PATCH on /posts
DELETE on /posts
Nested GETs like /posts/1/comments
Query-based filtering like /comments?postId=1
DTO deserialization verification
Negative tests for 404 responses

## UI Tests (PlaywrightTests)
### Covers:

Navigating to Google
Performing a search
Navigating to the Prometheus Group site
Filling and submitting a contact form
Verifying error messages for missing required fields

## Technologies Used
.NET 8.0
RestSharp
xUnit
FluentAssertions
Playwright for .NET

## Additional Notes
Google may trigger CAPTCHA or bot protection depending on traffic and IP location.