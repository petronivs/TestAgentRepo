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

### Project Structure (Legacy)

- `TestAgentRepo.sln` - Visual Studio solution file
- `WebAssemblyApp/` - Blazor WebAssembly project
  - `WebAssemblyApp.csproj` - Project file
  - `wwwroot/` - Static web assets including HTML page
  - `Pages/` - Razor pages and components
  - `Program.cs` - Application entry point
- `DynamicShapeLogic/` - Business logic library
  - `Class1.cs` - Shape generation algorithms
  - `DynamicShapeLogic.csproj` - Project file

## Running in GitHub Codespaces

This project is optimized for development in GitHub Codespaces, providing a complete cloud-based development environment.

### Opening in Codespace

1. Navigate to the repository on GitHub
2. Click the green "Code" button
3. Select the "Codespaces" tab
4. Click "Create codespace on main" (or your branch)

### Running in Codespace

The codespace environment comes pre-configured with .NET 8.0 SDK. To run the application:

1. **Build the solution:**
   ```bash
   dotnet build
   ```

2. **Run the WebAssembly application:**
   ```bash
   cd WebAssemblyApp
   dotnet run
   ```

3. **Access the application:**
   - The application will be available on port 5110
   - Codespaces will automatically forward the port and provide a link
   - Look for the "Ports" tab in the terminal panel or notification popup
   - Click the generated URL to open the application in your browser

### Development Tips for Codespaces

- The terminal is accessible via Terminal → New Terminal
- Port forwarding is automatic for common development ports
- Extensions and settings are automatically configured
- Files are synchronized in real-time

## Architecture

This solution follows a layered architecture with clear separation of concerns:

### Project Structure

```
TestAgentRepo/
├── TestAgentRepo.sln              # Visual Studio solution file
├── WebAssemblyApp/                # Blazor WebAssembly UI project
│   ├── Pages/                     # Razor pages and components
│   │   ├── Home.razor            # Landing page
│   │   ├── DynamicShape.razor    # Original dynamic shape generator
│   │   ├── DynamicShape2.razor   # Enhanced dynamic shape with line flipping
│   │   ├── DynamicStar.razor     # Dynamic star patterns
│   │   └── FivePointStar.razor   # Static five-point star
│   ├── Layout/                    # Layout components and navigation
│   ├── wwwroot/                   # Static web assets
│   └── Program.cs                 # Application entry point
└── DynamicShapeLogic/             # Business logic library
    ├── Class1.cs                  # Shape generation algorithms
    └── DynamicShapeLogic.csproj   # Project reference file
```

### Key Components

#### WebAssemblyApp (Presentation Layer)
- **Technology**: Blazor WebAssembly with .NET 8.0
- **Purpose**: Provides the web-based user interface
- **Features**: 
  - Interactive SVG shape generation
  - Real-time text input processing
  - Responsive navigation menu
  - Multiple shape generation modes

#### DynamicShapeLogic (Business Logic Layer)
- **Technology**: .NET 8.0 Class Library
- **Purpose**: Contains the core shape generation algorithms
- **Key Classes**:
  - `ShapeLine`: Data model representing individual line segments
  - `DynamicShapeGenerator`: Core algorithm for generating connected line patterns

### Shape Generation Algorithm

The Dynamic Shape 2 implementation uses a sophisticated algorithm:

1. **Unicode-based Angles**: Each character's unicode value directly determines line angle
2. **Line Flipping Logic**: Lines that point away from both center axes are automatically flipped
3. **Connected Patterns**: Each line connects to the end point of the previous line
4. **Bounded Drawing**: All shapes are constrained within the SVG canvas boundaries
5. **Dynamic Styling**: Colors and line widths are generated based on unicode values

### Data Flow

1. User enters text in the web interface
2. Blazor component passes text to `DynamicShapeGenerator`
3. Generator processes each character's unicode value
4. Algorithm calculates line positions, angles, and styling
5. Generated `ShapeLine` objects are returned to the UI
6. Blazor renders the lines as SVG elements in real-time

### How it Works

1. The C# code in the WebAssembly project gets compiled to WebAssembly (.wasm files)
2. The HTML page (`wwwroot/index.html`) loads the Blazor WebAssembly runtime
3. The runtime executes the WebAssembly code in the browser
4. Blazor provides the bridge between WebAssembly and the DOM for interactive web UI
5. The business logic library is referenced and executed within the WebAssembly context
