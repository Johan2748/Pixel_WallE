namespace Pixel_WallE;

public partial class MainForm : Form
{



    public MainForm()
    {
        InitializeComponent();

        Canvas canvas = new();


        Controls.Add(canvas.LeftPanel);
        Controls.Add(Canvas.canvas);
        Controls.Add(canvas.resizeButton);
        Controls.Add(canvas.saveButton);
        Controls.Add(canvas.saveText);
        Controls.Add(canvas.loadButton);
        Controls.Add(canvas.loadText);
    }





}
