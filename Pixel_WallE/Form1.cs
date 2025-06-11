namespace Pixel_WallE;

public partial class MainForm : Form
{



    public MainForm()
    {
        InitializeComponent();

        Canvas canvas = new();


        this.MouseDown += (sender, e) =>
        {
            if (!canvas.inputField.Bounds.Contains(e.Location))
            {
                MessageBox.Show("Escribiste :\n" + canvas.inputField.Text);
                new RunCode(canvas.inputField.Text);
            }
        };




        Controls.Add(canvas.inputField);
        Controls.Add(canvas.runButton);
        Controls.Add(canvas.canvas);
    }


    














}
