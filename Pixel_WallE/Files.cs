public static class Files
{

    private static string path = Directory.GetCurrentDirectory() + @"\Saved Files";



    public static void SaveFile(string text)
    {
        Directory.CreateDirectory(path);

        using var dialog = new SaveFileDialog()
        {
            Filter = "PW Files (*.pw)|*.pw",
            InitialDirectory = path
        };

        if (dialog.ShowDialog() == DialogResult.OK) File.WriteAllText(dialog.FileName, text);
        
    }



    public static string LoadFile()
    {

        string text = "";

        using var dialog = new OpenFileDialog
        {
            Filter = "PW Files (*.pw)|*.pw",
            InitialDirectory = path
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            text = File.ReadAllText(dialog.FileName);

        }
        return text; 
    }
    
}