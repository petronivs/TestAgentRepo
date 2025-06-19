namespace DynamicShapeLogic;

public class ShapeLine
{
    public double X1 { get; set; }
    public double Y1 { get; set; }
    public double X2 { get; set; }
    public double Y2 { get; set; }
    public string Color { get; set; } = "";
    public int Width { get; set; }
}

public class DynamicShapeGenerator
{
    private const double CENTER_X = 250;
    private const double CENTER_Y = 200;
    private const double SVG_WIDTH = 500;
    private const double SVG_HEIGHT = 400;
    private const double MARGIN = 10;

    public List<ShapeLine> GenerateShape(string inputText)
    {
        var shapeLines = new List<ShapeLine>();
        
        if (string.IsNullOrEmpty(inputText))
            return shapeLines;

        double currentX = CENTER_X;
        double currentY = CENTER_Y;
        
        for (int i = 0; i < inputText.Length; i++)
        {
            char c = inputText[i];
            int unicode = (int)c;
            
            // Use unicode value directly as angle (in degrees)
            double angle = unicode;
            double length = (unicode % 80) + 20; // Line length between 20-99
            
            // Calculate line end position from current position
            double lineAngle = angle * Math.PI / 180;
            double proposedEndX = currentX + Math.Cos(lineAngle) * length;
            double proposedEndY = currentY + Math.Sin(lineAngle) * length;
            
            // Check if line points away from both center lines
            bool pointsAwayFromVerticalCenter = Math.Abs(proposedEndX - CENTER_X) > Math.Abs(currentX - CENTER_X);
            bool pointsAwayFromHorizontalCenter = Math.Abs(proposedEndY - CENTER_Y) > Math.Abs(currentY - CENTER_Y);
            
            // Flip the line if it points away from both center lines
            if (pointsAwayFromVerticalCenter && pointsAwayFromHorizontalCenter)
            {
                angle += 180;
                lineAngle = angle * Math.PI / 180;
                proposedEndX = currentX + Math.Cos(lineAngle) * length;
                proposedEndY = currentY + Math.Sin(lineAngle) * length;
            }
            
            // Ensure the line fits within the SVG bounds
            double endX = Math.Max(MARGIN, Math.Min(SVG_WIDTH - MARGIN, proposedEndX));
            double endY = Math.Max(MARGIN, Math.Min(SVG_HEIGHT - MARGIN, proposedEndY));
            
            // Generate color based on unicode value
            string color = GenerateColor(unicode);
            int width = (unicode % 3) + 1; // Line width between 1-3
            
            shapeLines.Add(new ShapeLine
            {
                X1 = currentX,
                Y1 = currentY,
                X2 = endX,
                Y2 = endY,
                Color = color,
                Width = width
            });
            
            // Update current position to the end of this line for the next line to connect
            currentX = endX;
            currentY = endY;
        }

        return shapeLines;
    }

    private string GenerateColor(int unicode)
    {
        // Generate color based on unicode value
        int r = (unicode * 7) % 256;
        int g = (unicode * 13) % 256;
        int b = (unicode * 19) % 256;
        
        return $"rgb({r},{g},{b})";
    }
}
