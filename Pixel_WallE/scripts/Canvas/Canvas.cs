using ICSharpCode.TextEditor;

class Canvas
{

    // Cells and Grid properties
    int scale = 900;
    static int cellSize;
    public static int gridSize { get; private set; }


    // Accessible properties [WALL-E STATE]
    public static int ActualX { get; set; }
    public static int ActualY { get; set; }
    public static int BrushSize { get; set; }
    public static Color[,] CellColors = null!;
    public static Color ActualColor { get; set; }


    // Left side controls
    public Panel LeftPanel { get; private set; }
    private TextEditorControl inputField;
    private Button runButton;





    // Right side controls

    public Button resizeButton { get; private set; }
    public Button cleanButton { get; private set; }
    public Button saveButton { get; private set; }
    public Button loadButton { get; private set; }





    public static PictureBox canvas = new();
    private static Bitmap? bitmap;









    public Canvas()
    {
        gridSize = 50;
        BrushSize = 1;

        // LEFT PANEL
        LeftPanel = new()
        {
            BorderStyle = BorderStyle.FixedSingle,
            Location = new Point(25, 25),
            Size = new System.Drawing.Size(500, 900)
        };

        // PLAY BUTTON
        runButton = new()
        {
            Text = "RUN",
            Location = new Point(LeftPanel.Left + 5, LeftPanel.Top + 5),
            Size = new System.Drawing.Size(80, 40),
            BackColor = Color.Green
        };
        runButton.Click += Click_Run;


        // TEXT EDITOR
        inputField = new TextEditorControl
        {
            Location = new Point(LeftPanel.Left + 5, runButton.Bottom + 5),
            Size = new System.Drawing.Size(450, 800),
            Font = new Font("Consolas", 10),
            BorderStyle = BorderStyle.Fixed3D,
            ShowLineNumbers = true
        };




        // RIGHT SIDE

        // SIZE BUTTON
        resizeButton = new()
        {
            Text = "Size",
            Location = new Point(1550, 30),
            Size = new System.Drawing.Size(120, 40)
        };

        ContextMenuStrip contextMenu = new();

        contextMenu.Items.Add("50", null, (s, e) => { gridSize = 50; PaintGrid(); });
        contextMenu.Items.Add("100", null, (s, e) => { gridSize = 100; PaintGrid(); });
        contextMenu.Items.Add("150", null, (s, e) => { gridSize = 150; PaintGrid(); });
        contextMenu.Items.Add("300", null, (s, e) => { gridSize = 300; PaintGrid(); });

        resizeButton.Click += (s, e) => contextMenu.Show(resizeButton, new Point(0, resizeButton.Height));

        // CLEAN BUTTON

        cleanButton = new()
        {
            Text = "Clean",
            Location = new Point(1750, 30),
            Size = new System.Drawing.Size(120, 40)
        };

        cleanButton.Click += (s, e) => { PaintGrid(); BrushSize = 1; };

        // SAVE BUTTON

        saveButton = new()
        {
            Text = "Save",
            Location = new Point(1550, 600),
            Size = new System.Drawing.Size(120, 40)
        };

        saveButton.Click += (s, e) => { Files.SaveFile(inputField.Text); };



        // LOAD BUTTON

        loadButton = new()
        {
            Text = "Load",
            Location = new Point(1550, 700),
            Size = new System.Drawing.Size(120, 40)
        };

        loadButton.Click += (s, e) => { inputField.Text = Files.LoadFile(); };


        // VISUAL CANVAS
        canvas = new PictureBox
        {
            Location = new Point(550, 25),
            Size = new System.Drawing.Size(scale, scale),
            BackColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle
        };
        PaintGrid();



        LeftPanel.Controls.Add(runButton);
        LeftPanel.Controls.Add(inputField);


    }



    // EXECUTE CODE
    private void Click_Run(object? sender, EventArgs e)
    {
        _ = new RunCode(inputField.Text);
    }


    // PAINT GRID LINES
    private void CreateGrid()
    {
        bitmap = new Bitmap(gridSize * cellSize, gridSize * cellSize);

        CellColors = new Color[gridSize, gridSize];
        ActualColor = Color.White;

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                CellColors[i, j] = Color.White;
            }
        }

        using (Graphics g = Graphics.FromImage(bitmap))
        {
            g.Clear(Color.White);
        }

        canvas.Image = bitmap;

    }


    private void Canvas_ShowGrid(object sender, PaintEventArgs e)
    {
        for (int x = 0; x <= gridSize; x++)
        {
            e.Graphics.DrawLine(new Pen(Color.LightGray),
                x * cellSize, 0,
                x * cellSize, gridSize * cellSize);
        }

        for (int y = 0; y <= gridSize; y++)
        {
            e.Graphics.DrawLine(new Pen(Color.LightGray),
                0, y * cellSize,
                gridSize * cellSize, y * cellSize);
        }

    }


    private void PaintGrid()
    {
        cellSize = scale / gridSize;
        CreateGrid();
        canvas.Paint += Canvas_ShowGrid!;
    }


    // PAINT CELLS
    public static void SetCellColor(int x, int y, Color color)
    {
        if (color == Color.Transparent) return;
        if (x >= 0 && x < gridSize && y >= 0 && y < gridSize)
        {
            CellColors[x, y] = color;
            RedrawCell(x, y);
        }
    }


    private static void RedrawCell(int x, int y)
    {
        if (bitmap == null) return;

        using (Graphics g = Graphics.FromImage(bitmap))
        {
            Brush brush = new SolidBrush(CellColors[x, y]);
            g.FillRectangle(brush, x * cellSize, y * cellSize, BrushSize * cellSize, BrushSize * cellSize);


        }

        canvas.Invalidate();
    }








}