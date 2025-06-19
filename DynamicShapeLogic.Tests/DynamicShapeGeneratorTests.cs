namespace DynamicShapeLogic.Tests;

public class DynamicShapeGeneratorTests
{
    private readonly DynamicShapeGenerator _generator;

    public DynamicShapeGeneratorTests()
    {
        _generator = new DynamicShapeGenerator();
    }

    [Fact]
    public void GenerateShape_WithEmptyInput_ReturnsEmptyList()
    {
        // Arrange
        string emptyInput = "";

        // Act
        var result = _generator.GenerateShape(emptyInput);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GenerateShape_WithNullInput_ReturnsEmptyList()
    {
        // Arrange
        string? nullInput = null;

        // Act
        var result = _generator.GenerateShape(nullInput);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GenerateShape_WithSingleCharacter_ReturnsOneShapeLine()
    {
        // Arrange
        string input = "A";

        // Act
        var result = _generator.GenerateShape(input);

        // Assert
        Assert.Single(result);
        
        var line = result[0];
        Assert.Equal(250, line.X1); // Should start at center X
        Assert.Equal(200, line.Y1); // Should start at center Y
        Assert.NotEmpty(line.Color);
        Assert.InRange(line.Width, 1, 3);
    }

    [Fact]
    public void GenerateShape_WithMultipleCharacters_ReturnsConnectedLines()
    {
        // Arrange
        string input = "AB";

        // Act
        var result = _generator.GenerateShape(input);

        // Assert
        Assert.Equal(2, result.Count);
        
        // Second line should start where first line ends
        Assert.Equal(result[0].X2, result[1].X1);
        Assert.Equal(result[0].Y2, result[1].Y1);
    }

    [Fact]
    public void GenerateShape_LineStaysWithinBounds()
    {
        // Arrange
        string input = "XYZ"; // Some characters that might generate extreme angles

        // Act
        var result = _generator.GenerateShape(input);

        // Assert
        foreach (var line in result)
        {
            Assert.InRange(line.X1, 0, 500);
            Assert.InRange(line.Y1, 0, 400);
            Assert.InRange(line.X2, 10, 490); // Should respect MARGIN
            Assert.InRange(line.Y2, 10, 390); // Should respect MARGIN
        }
    }

    [Fact]
    public void GenerateShape_ColorIsGeneratedCorrectly()
    {
        // Arrange
        string input = "A";

        // Act
        var result = _generator.GenerateShape(input);

        // Assert
        var line = result[0];
        Assert.StartsWith("rgb(", line.Color);
        Assert.EndsWith(")", line.Color);
        Assert.Contains(",", line.Color);
    }

    [Fact]
    public void GenerateShape_LineWidthIsWithinExpectedRange()
    {
        // Arrange
        string input = "ABCDEF";

        // Act
        var result = _generator.GenerateShape(input);

        // Assert
        foreach (var line in result)
        {
            Assert.InRange(line.Width, 1, 3);
        }
    }

    [Fact]
    public void GenerateShape_DifferentCharactersGenerateDifferentAngles()
    {
        // Arrange
        string input1 = "A";
        string input2 = "B";

        // Act
        var result1 = _generator.GenerateShape(input1);
        var result2 = _generator.GenerateShape(input2);

        // Assert
        // The lines should be different (different unicode values should generate different angles)
        Assert.NotEqual(result1[0].X2, result2[0].X2);
        Assert.NotEqual(result1[0].Y2, result2[0].Y2);
    }

    [Fact]
    public void ShapeLine_PropertiesCanBeSetAndRetrieved()
    {
        // Arrange
        var shapeLine = new ShapeLine();

        // Act
        shapeLine.X1 = 10.5;
        shapeLine.Y1 = 20.3;
        shapeLine.X2 = 30.7;
        shapeLine.Y2 = 40.1;
        shapeLine.Color = "rgb(255,0,0)";
        shapeLine.Width = 2;

        // Assert
        Assert.Equal(10.5, shapeLine.X1);
        Assert.Equal(20.3, shapeLine.Y1);
        Assert.Equal(30.7, shapeLine.X2);
        Assert.Equal(40.1, shapeLine.Y2);
        Assert.Equal("rgb(255,0,0)", shapeLine.Color);
        Assert.Equal(2, shapeLine.Width);
    }

    [Fact]
    public void ShapeLine_DefaultColorIsEmpty()
    {
        // Arrange & Act
        var shapeLine = new ShapeLine();

        // Assert
        Assert.Equal("", shapeLine.Color);
    }
}