# TestAgentRepo
A repo for testing the agent

## WebAssembly Application

This repository contains a Visual Studio solution with a Blazor WebAssembly application that compiles C# code into WebAssembly and displays it in a web browser.

### Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022 or Visual Studio Code (optional, can use command line)

### Building the Solution

To build the entire solution:

```bash
dotnet build
```

### Running the Application

To run the WebAssembly application:

```bash
cd WebAssemblyApp
dotnet run
```

The application will be available at `http://localhost:5110` (or another port if 5110 is in use).

### Project Structure

- `TestAgentRepo.sln` - Visual Studio solution file
- `WebAssemblyApp/` - Blazor WebAssembly project
  - `WebAssemblyApp.csproj` - Project file
  - `wwwroot/` - Static web assets including HTML page
  - `Pages/` - Razor pages and components
  - `Program.cs` - Application entry point

### How it Works

1. The C# code in the WebAssembly project gets compiled to WebAssembly (.wasm files)
2. The HTML page (`wwwroot/index.html`) loads the Blazor WebAssembly runtime
3. The runtime executes the WebAssembly code in the browser
4. Blazor provides the bridge between WebAssembly and the DOM for interactive web UI
