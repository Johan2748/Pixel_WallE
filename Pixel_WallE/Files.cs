public static class Files
{

    public static string path = Directory.GetCurrentDirectory();
    private static bool beenSaved = false;


    public static string[]? Archives
    {
        get
        { try
            {
                return Directory.GetFiles(path, "*.pw");
            }
            catch (Exception)
            {
                MessageBox.Show("There are not any file saved yet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            
        }
    }


    public static void SaveFile(string name, string text)
    {
        if (!beenSaved)
        {
            beenSaved = true;
            path += @"\Saved Files";
            Directory.CreateDirectory(path);
        }

        TextWriter archive = new StreamWriter(path + $@"\{name}.pw");
        archive.WriteLine(text);
        archive.Close();
    }



    public static string LoadFile(string name)
    {
        if (Archives is null)  return ""; 
        else
        {
            if (Archives.Contains(@$"{name}.pw"))
            {
                TextReader archive = new StreamReader(path + @$"{name}.pw");
                string text = archive.ReadToEnd();
                archive.Close();
                return text;
            }
            else {MessageBox.Show("There are not any file whit that name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return ""; }
        }
    }




}