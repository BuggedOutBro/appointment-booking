## Steps to Set Up the Code

### 1. Clone the project

### 2. Set Up the Database
1. Navigate to the files folder:
   ```bash
   cd {RootPath}/files/database
   ```
2. Updates have been made to the `init.sql` file by adding a few indexes to optimize queries.
3. Build and run the database Docker container:
   ```bash
   docker build -t enpal-coding-challenge-db .
   docker run --name enpal-coding-challenge-db -p 5432:5432 -d enpal-coding-challenge-db
   ```
4. Open Docker Desktop and confirm that the container is running successfully.

### 3. Set Up the API
1. Ensure that the .NET 8 SDK is installed on your system.
   - If not, download and install it from the official [.NET website](https://dotnet.microsoft.com/).
2. Navigate to the `appointment-booking-api/appointment-booking` folder:
   ```bash
   cd {RootPath}/appointment-booking-api/appointment-booking
   ```
3. Build the application:
   ```bash
   dotnet build
   ```
4. Run the application:
   ```bash
   dotnet run
   ```

### 4. Verify the API
1. Once the application is running, it will be available at:
   - [http://localhost:3000](http://localhost:3000)
2. Open Swagger in your browser to explore and test the API endpoints. Swagger is enabled in the Development environment:
   - [http://localhost:3000/swagger/index.html](http://localhost:3000/swagger/index.html)

### Notes
- Replace `{RootPath}` with the actual path to your project root directory.
- I have added the connection string in the appsettings.json file. Although using secrets.json would be a better approach, appsettings.json has been used for simplicity in this challenge.

### Optional
- You can also use Visual Studio or Visual Studio Code to build and run the project instead of using the CLI.

