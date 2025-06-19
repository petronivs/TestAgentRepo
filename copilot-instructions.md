# Copilot Instructions for TestAgentRepo

This document provides comprehensive guidance for AI assistants working on the TestAgentRepo project, a Blazor WebAssembly application with dynamic shape generation capabilities.

## Project Overview

TestAgentRepo is a Visual Studio solution containing a Blazor WebAssembly frontend with separated business logic for generating dynamic SVG shapes based on text input. The project demonstrates various shape generation algorithms and serves as a testing ground for AI-assisted development.

### Key Technologies
- **Frontend**: Blazor WebAssembly (.NET 8.0)
- **Business Logic**: .NET 8.0 Class Library
- **Testing**: xUnit with comprehensive unit tests
- **Deployment**: GitHub Codespaces optimized

## Architecture Guidelines

### Project Structure
```
TestAgentRepo/
├── WebAssemblyApp/           # Blazor WebAssembly UI project
│   ├── Pages/               # Razor pages for different shape generators
│   ├── Layout/              # Navigation and layout components
│   └── wwwroot/             # Static web assets
├── DynamicShapeLogic/       # Business logic library
│   └── DynamicShapeGenerator.cs  # Core shape generation algorithms
├── DynamicShapeLogic.Tests/ # Unit tests for business logic
└── README.md                # Project documentation
```

### Separation of Concerns
- **UI Layer (WebAssemblyApp)**: Handle user interaction, rendering, and Blazor-specific logic
- **Business Logic (DynamicShapeLogic)**: Contain all shape generation algorithms and mathematical calculations
- **Testing Layer**: Comprehensive unit tests for all business logic

## Development Guidelines

### Code Style and Patterns

#### Naming Conventions
- **Classes**: PascalCase (e.g., `DynamicShapeGenerator`)
- **Methods**: PascalCase (e.g., `GenerateShape`)
- **Properties**: PascalCase (e.g., `X1`, `Y1`, `Color`)
- **Fields**: PascalCase for public, camelCase for private
- **Constants**: UPPER_SNAKE_CASE (e.g., `CENTER_X`, `SVG_WIDTH`)

#### File Organization
- **Razor Pages**: Name matches functionality (e.g., `DynamicShape2.razor`)
- **Business Logic**: Descriptive class names (e.g., `DynamicShapeGenerator.cs`)
- **Tests**: Match the class being tested with `.Tests` suffix

#### Shape Generation Patterns
All shape generators should follow this pattern:
1. Accept string input text
2. Return `List<ShapeLine>` objects
3. Handle null/empty input gracefully
4. Apply bounds checking (SVG_WIDTH: 500, SVG_HEIGHT: 400)
5. Use consistent center point (CENTER_X: 250, CENTER_Y: 200)

### Data Models

#### ShapeLine Class
```csharp
public class ShapeLine
{
    public double X1 { get; set; }    // Start X coordinate
    public double Y1 { get; set; }    // Start Y coordinate  
    public double X2 { get; set; }    // End X coordinate
    public double Y2 { get; set; }    // End Y coordinate
    public string Color { get; set; } // Hex color code
    public int Width { get; set; }    // Line width in pixels
}
```

### Algorithm Implementations

#### DynamicShape vs DynamicShape2
- **DynamicShape**: Uses incremental 180-degree rotations
- **DynamicShape2**: Uses unicode values directly as angles with line flipping logic

#### Line Flipping Logic (DynamicShape2)
```csharp
bool pointsAwayFromVerticalCenter = Math.Abs(proposedEndX - centerX) > Math.Abs(currentX - centerX);
bool pointsAwayFromHorizontalCenter = Math.Abs(proposedEndY - centerY) > Math.Abs(currentY - centerY);

if (pointsAwayFromVerticalCenter && pointsAwayFromHorizontalCenter) {
    angle += 180; // Flip the line
}
```

## Testing Guidelines

### Unit Test Requirements
- All business logic methods must have unit tests
- Test edge cases: null input, empty input, single character
- Verify bounds checking and mathematical calculations
- Test color and width generation algorithms
- Ensure line connectivity (end point of one line = start point of next)

### Test Structure
```csharp
[Fact]
public void GenerateShape_MethodName_ExpectedBehavior()
{
    // Arrange
    var generator = new DynamicShapeGenerator();
    
    // Act
    var result = generator.GenerateShape("test input");
    
    // Assert
    Assert.NotNull(result);
    // Additional assertions
}
```

## Building and Testing

### Build Commands
```bash
# Build entire solution
dotnet build

# Run WebAssembly app
cd WebAssemblyApp
dotnet run

# Run tests
dotnet test
```

### GitHub Codespaces
- Project is optimized for Codespaces development
- Port 5110 is automatically forwarded
- All dependencies are pre-configured

## AI Assistant Guidelines

### When Adding New Features

1. **Business Logic First**: Implement core algorithms in `DynamicShapeLogic` project
2. **Add Unit Tests**: Create comprehensive tests before UI implementation  
3. **UI Integration**: Create Razor page that uses the business logic
4. **Update Navigation**: Add menu entries in layout components
5. **Documentation**: Update README.md with new functionality

### When Fixing Bugs

1. **Reproduce with Tests**: Create failing unit test that demonstrates the bug
2. **Fix Business Logic**: Address the issue in the appropriate class
3. **Verify Fix**: Ensure all tests pass, including the new one
4. **UI Validation**: Test the fix in the web interface

### Code Review Checklist

- [ ] Business logic separated from UI concerns
- [ ] Unit tests cover all new functionality
- [ ] Null/empty input handling implemented
- [ ] Bounds checking applied to coordinates
- [ ] Consistent naming conventions followed
- [ ] No hardcoded magic numbers (use constants)
- [ ] Color generation follows existing patterns
- [ ] Line connectivity maintained for connected shapes

### Common Tasks

#### Adding a New Shape Generator

1. Create algorithm in `DynamicShapeLogic/DynamicShapeGenerator.cs`
2. Add comprehensive unit tests in `DynamicShapeLogic.Tests`
3. Create new Razor page in `WebAssemblyApp/Pages/`
4. Update navigation menu
5. Update README.md architecture section

#### Modifying Existing Algorithms

1. Update business logic method
2. Modify or add unit tests to cover changes
3. Verify existing tests still pass
4. Test changes in web interface

#### Performance Optimization

1. Profile shape generation algorithms
2. Optimize mathematical calculations
3. Consider caching for repeated operations
4. Maintain readability while improving performance

## Mathematical Concepts

### Coordinate System
- SVG uses top-left origin (0,0)
- Center point is (250, 200) in 500x400 canvas
- Y increases downward

### Angle Calculations
- Angles in degrees (0-360)
- Conversion to radians: `angle * Math.PI / 180`
- Line endpoints: `(x + length * cos(θ), y + length * sin(θ))`

### Color Generation
- Uses unicode values for color calculation
- Ensures good contrast and visibility
- Maintains consistency across related lines

## Project Evolution Notes

### Recent Changes
- Business logic separated into dedicated project (commit db5f3d4)
- Class1.cs renamed to DynamicShapeGenerator.cs (commit 093389a)
- Comprehensive unit tests added (commit 093389a)
- README updated with Codespace instructions (commit 16da9ce)

### Future Considerations
- Consider adding more shape generation algorithms
- Explore 3D shape projections
- Add animation capabilities
- Implement shape export functionality

## Troubleshooting

### Common Issues
- **Build Failures**: Ensure all project references are correct
- **Test Failures**: Verify mathematical calculations and edge cases
- **UI Not Updating**: Check Blazor component state management
- **Port Issues**: Use different ports if 5110 is unavailable

### Debug Strategies
1. Use unit tests to isolate business logic issues
2. Add logging to shape generation methods
3. Verify SVG coordinates are within bounds
4. Check browser developer tools for client-side errors

This document should be updated as the project evolves to maintain accuracy and usefulness for future AI assistants working on this codebase.