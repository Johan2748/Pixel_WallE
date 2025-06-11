

class Canvas
{

    public string Code = "";


    public TextBox inputField;
    public Button runButton;
    public PictureBox canvas;


    public Canvas()
    {
        inputField = new TextBox
        {
            Location = new Point(20, 30),
            Size = new System.Drawing.Size(400, 900),
            Multiline = true

        };
        inputField.Leave += Desselected_Text;



        runButton = new()
        {
            Size = new System.Drawing.Size(40, 40),
            Location = new Point(1000, 20),
            BackColor = Color.Green
        };
        runButton.Click += Click_Run;

        canvas = new PictureBox
        {
            Location = new Point(500, 30),
            Size = new System.Drawing.Size(200, 200),
            BackColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle
        };
    }


    public void Desselected_Text(object? sender, EventArgs e)
    {

        MessageBox.Show("Escribiste : " + inputField.Text);
        Code = inputField.Text;
    }

    public void Click_Run(object? sender, EventArgs e)
    {
        MessageBox.Show("Botón RUN clickeado!");
        
    }











}