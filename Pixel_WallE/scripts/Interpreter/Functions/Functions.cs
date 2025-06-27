
public interface ICallable
{
    int Arity { get; }

    AstType[] Types { get; }

    AstType ReturnType{ get; }

    bool CheckArguments(List<Expresion> arguments)
    {
        if (Arity != Types.Length) throw new PoorlyImplementedFunctionError(-1, this);

        for (int i = 0; i < Types.Length; i++)
        {
            if (arguments[i] is null) return false;
            if (Types[i] != arguments[i].Type) return false;
        }
        return true;
    }

    object? Call(object[] arguments);
}


// INSTRUCTIONS
public class Spawn : ICallable
{
    public int Arity => 2;

    public AstType ReturnType => AstType.NULL; 

    public AstType[] Types => [AstType.INT, AstType.INT];

    public object? Call(object[] arguments)
    {
        if ((int)arguments[0] >= Canvas.gridSize || (int)arguments[1] >= Canvas.gridSize) throw new IndexOutOfRangeError();
        Canvas.ActualX = (int)arguments[0];
        Canvas.ActualY = (int)arguments[1];
        return null;
    }
}

public class ChangeColor : ICallable
{
    public int Arity => 1;

    public AstType ReturnType => AstType.NULL; 

    public AstType[] Types => [AstType.COLOR];

    public object? Call(object[] arguments)
    {
        string color = (string)arguments[0];
        color = color[1..^1];

        switch (color)
        {
            case "Red" : Canvas.ActualColor = Color.Red; break;
            case "White": Canvas.ActualColor = Color.White; break;
            case "Blue": Canvas.ActualColor = Color.Blue; break;
            case "Green": Canvas.ActualColor = Color.Green; break;
            case "Yellow": Canvas.ActualColor = Color.Yellow; break;
            case "Orange": Canvas.ActualColor = Color.Orange; break;
            case "Purple": Canvas.ActualColor = Color.Purple; break;
            case "Black": Canvas.ActualColor = Color.Black; break;
            case "Transparent": Canvas.ActualColor = Color.Transparent; break;
        }

        return null;
    }
}

public class Size : ICallable
{
    public int Arity => 1;

    public AstType ReturnType => AstType.NULL; 

    public AstType[] Types => [AstType.INT];

    public object? Call(object[] arguments)
    {
        if ((int)arguments[0] < 1) throw new Error(-1, "Brush size cannot be less than 1");
        if ((int)arguments[0] % 2 == 0) Canvas.BrushSize = (int)arguments[0] - 1;
        else Canvas.BrushSize = (int)arguments[0];
        return null;
    }
}

public class DrawLine : ICallable
{
    public int Arity => 3;

    public AstType ReturnType => AstType.NULL;

    public AstType[] Types => [AstType.INT, AstType.INT, AstType.INT];

    public object? Call(object[] arguments)
    {
        int x = (int)arguments[0];
        int y = (int)arguments[1];
        int length = (int)arguments[2];

        if (x > 1 || x < -1) throw new Error(-1, "Invalid direction");
        if (y > 1 || y < -1) throw new Error(-1, "Invalid direction");

        for (int i = 0; i < length; i++)
        {
            Canvas.SetCellColor(Canvas.ActualX, Canvas.ActualY, Canvas.ActualColor);
            if (Canvas.ActualX + x >= Canvas.gridSize || Canvas.ActualY + y >= Canvas.gridSize || Canvas.ActualX + x < 0 || Canvas.ActualY + y < 0) throw new IndexOutOfRangeError();
            Canvas.ActualX += x;
            Canvas.ActualY += y;
        }

        return null;
    }
}

public class DrawCircle : ICallable
{
    public int Arity => 3;

    public AstType ReturnType => AstType.NULL;

    public AstType[] Types => [AstType.INT, AstType.INT, AstType.INT];

    public object? Call(object[] arguments)
    {
        int dir_x = (int)arguments[0];
        int dir_y = (int)arguments[1];
        int radius = (int)arguments[2];

        if (dir_x > 1 || dir_x < -1) throw new Error(-1, "Invalid direction");
        if (dir_y > 1 || dir_y < -1) throw new Error(-1, "Invalid direction");
        if (radius < 1) throw new Error(-1, "Radius cannot be less than 1");


        int centerX = Canvas.ActualX + dir_x * radius;
        int centerY = Canvas.ActualY + dir_y * radius;

        if (centerX >= Canvas.gridSize || centerY >= Canvas.gridSize || centerX < 0 || centerY < 0) throw new IndexOutOfRangeError();
        Canvas.ActualX += dir_x * radius;
        Canvas.ActualY += dir_y * radius;


        int x = radius;
        int y = 0;
        int err = 0;

        while (x >= y)
        {
            PlotCirclePoints(centerX, centerY, x, y);

            if (err <= 0)
            {
                y += 1;
                err += 2 * y + 1;
            }
            if (err > 0)
            {
                x -= 1;
                err -= 2 * x + 1;
            }
        }

        return null;

    }
    
    private void PlotCirclePoints(int cx, int cy, int x, int y)
    {
        // Dibujar en los 8 octantes (simetría circular)
        SetPixelSafely(cx + x, cy + y);
        SetPixelSafely(cx - x, cy + y);
        SetPixelSafely(cx + x, cy - y);
        SetPixelSafely(cx - x, cy - y);
        SetPixelSafely(cx + y, cy + x);
        SetPixelSafely(cx - y, cy + x);
        SetPixelSafely(cx + y, cy - x);
        SetPixelSafely(cx - y, cy - x);
    }

    private void SetPixelSafely(int x, int y)
    {
        if (x >= 0 && x < Canvas.gridSize && y >= 0 && y < Canvas.gridSize)
        {
            Canvas.SetCellColor(x, y, Canvas.ActualColor);
        }
    }





}

public class DrawRectangle : ICallable
{
    public int Arity => 5;

    public AstType ReturnType => AstType.NULL; 

    public AstType[] Types => [AstType.INT, AstType.INT, AstType.INT, AstType.INT, AstType.INT];

    public object? Call(object[] arguments)
    {
        int x = (int)arguments[0];
        int y = (int)arguments[1];
        int lenght = (int)arguments[2];
        int width = (int)arguments[3];
        int height = (int)arguments[4];

        if (x > 1 || x < -1) throw new Error(-1, "Invalid direction");
        if (y > 1 || y < -1) throw new Error(-1, "Invalid direction");
        if (lenght < 0) throw new Error(-1, "Distance cannot be neggative");
        if (width < 1) throw new Error(-1, "Width cannot be less than 1");
        if (height < 1) throw new Error(-1, "Height cannot be less than 1");

        if (Canvas.ActualX + x * lenght >= Canvas.gridSize || Canvas.ActualY + y * lenght >= Canvas.gridSize || Canvas.ActualX + x * lenght < 0 || Canvas.ActualY + y * lenght < 0) throw new IndexOutOfRangeError();
        Canvas.ActualX += x * lenght;
        Canvas.ActualY += y * lenght;

        int startX = Canvas.ActualX - (width / 2);
        int startY = Canvas.ActualY - (height / 2);

        for (int i = 0; i < width ; i++)
        {
            Canvas.SetCellColor(startX + i, startY, Canvas.ActualColor);
            Canvas.SetCellColor(startX + i, startY + height - 1, Canvas.ActualColor);
        }
        for (int i = 0; i < height ; i++)
        {
            Canvas.SetCellColor(startX, startY + i, Canvas.ActualColor);
            Canvas.SetCellColor(startX + width - 1, startY + i, Canvas.ActualColor);
        }

        return null;
    }
}

public class Fill : ICallable
{
    public int Arity => 0;

    public AstType ReturnType => AstType.NULL;

    public AstType[] Types => [];

    public object? Call(object[] arguments)
    {
        int startX = Canvas.ActualX;
        int startY = Canvas.ActualY;
        Color newColor = Canvas.ActualColor;
        Color targetColor = Canvas.CellColors[startX, startY];

        if (targetColor == newColor) return null;

        Queue<(int x, int y)> queue = new();
        queue.Enqueue((startX, startY));

        while (queue.Count > 0)
        {
            var (x, y) = queue.Dequeue();

            if (Canvas.CellColors[x, y] != targetColor) continue;

            Canvas.SetCellColor(x, y, newColor);

            ExploreNeighbor(queue, x - 1, y, targetColor);
            ExploreNeighbor(queue, x + 1, y, targetColor);
            ExploreNeighbor(queue, x, y - 1, targetColor);
            ExploreNeighbor(queue, x, y + 1, targetColor);

        }


        return null;
    }


    private void ExploreNeighbor(Queue<(int x, int y)> queue, int x, int y, Color targetColor)
    {
        // Verificar límites y color
        if (x >= 0 && x < Canvas.gridSize && 
            y >= 0 && y < Canvas.gridSize && 
            Canvas.CellColors[x, y] == targetColor)
        {
            queue.Enqueue((x, y));
        }
    }
}

// FUNCTIONS


public class GetActualX : ICallable
{
    public int Arity => 0;

    public AstType ReturnType => AstType.INT; 

    public AstType[] Types => [];

    public object? Call(object[] arguments)
    {
        return Canvas.ActualX;
    }
}

public class GetActualY : ICallable
{
    public int Arity => 0;

    public AstType ReturnType => AstType.INT; 

    public AstType[] Types => [];

    public object? Call(object[] arguments)
    {
        return Canvas.ActualY;
    }
}

public class GetCanvasSize : ICallable
{
    public int Arity => 0;

    public AstType ReturnType => AstType.INT; 

    public AstType[] Types => [];

    public object? Call(object[] arguments)
    {
        return Canvas.gridSize;
    }
}

public class GetColorCount : ICallable
{
    public int Arity => 5;

    public AstType ReturnType => AstType.INT; 

    public AstType[] Types => [AstType.COLOR, AstType.INT, AstType.INT, AstType.INT, AstType.INT];

    public object? Call(object[] arguments)
    {
        string color = (string)arguments[0];
        color = color[1..^1];

        int x1 = (int)arguments[1];
        int y1 = (int)arguments[2];
        int x2 = (int)arguments[3];
        int y2 = (int)arguments[4];

        if (x1 < 0 || x1 >= Canvas.gridSize || y1 < 0 || y1 >= Canvas.gridSize || x2 < 0 || x2 >= Canvas.gridSize || y2 < 0 || y2 >= Canvas.gridSize) throw new IndexOutOfRangeError();

        int startX = Math.Min(x1, x2);
        int startY = Math.Min(y1, y2);

        int count = 0;

        for (int i = startX; i < Math.Abs(x2 - x1) + 1; i++)
        {
            for (int j = 0; j < Math.Abs(y2 - y1) + 1; j++)
            {
                if (Canvas.CellColors[i, j].ToString() == $"Color [{color}]") count++;
            }
        }


        return count;

    }
}

public class IsBrushColor : ICallable
{
    public int Arity => 1;

    public AstType ReturnType => AstType.INT; 

    public AstType[] Types => [AstType.COLOR];

    public object? Call(object[] arguments)
    {
        string color = (string)arguments[0];
        color = color[1..^1];
        if (Canvas.ActualColor.ToString() == $"Color [{color}]") return 1;
        return 0;
    }
}

public class IsBrushSize : ICallable
{
    public int Arity => 1;

    public AstType ReturnType => AstType.INT; 

    public AstType[] Types => [AstType.INT];

    public object? Call(object[] arguments)
    {
        if ((int)arguments[0] == Canvas.BrushSize) return 1;
        return 0;
    }
}

public class IsCanvasColor : ICallable
{
    public int Arity => 3;

    public AstType ReturnType => AstType.INT;

    public AstType[] Types => [AstType.COLOR, AstType.INT, AstType.INT];

    public object? Call(object[] arguments)
    {
        string color = (string)arguments[0];
        color = color[1..^1];

        int x = (int)arguments[1];
        int y = (int)arguments[2];

        if (Canvas.ActualX + x >= Canvas.gridSize || Canvas.ActualY + y >= Canvas.gridSize || Canvas.ActualX + x < 0 || Canvas.ActualY + y < 0) return 0;
        string cellColor = Canvas.CellColors[Canvas.ActualX + x, Canvas.ActualY + y].ToString();
        if (cellColor == $"Color [{color}]") return 1;
        return 0;

        
    }
}
