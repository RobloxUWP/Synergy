using System.Windows.Forms;

public class FileDialog
{
    public static string OpenFileDialog()
    {
        using (var dialog = new OpenFileDialog())
        {
            //dialog.InitialDirectory = @"C:\LabWare\Scripts";
            dialog.Filter = "Lua files (*.lua)|*.lua|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.Title = "Open a Lua script";

            return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
        }
    }

    public static string SaveFileDialog()
    {
        using (var dialog = new SaveFileDialog())
        {
            //dialog.InitialDirectory = @"C:\LabWare\Scripts";
            dialog.Filter = "Lua files (*.lua)|*.lua|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.Title = "Save a Lua script";
            dialog.FileName = "NewScript.lua";

            return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
        }
    }
}