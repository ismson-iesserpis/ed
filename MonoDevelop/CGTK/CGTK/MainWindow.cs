using System;
using System.Data;
using Gtk;

public partial class MainWindow : Gtk.Window
{
    private IDbConnection sqlConn;

    public MainWindow(IDbConnection conn) : base(Gtk.WindowType.Toplevel)
    {
        Build();

        sqlConn = conn;
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        sqlConn.Close();

        Application.Quit();
        a.RetVal = true;
    }

    protected void OnBtnLoadTableClicked(object sender, EventArgs e)
    {
        IDbCommand command = sqlConn.CreateCommand();
        command.CommandText = "SHOW TABLES;";

        IDataReader dataReader = command.ExecuteReader();

        dataViewer.AppendColumn(dataReader.GetName(0), new CellRendererText(), "text");

        while (dataReader.Read())
        {

        }
        dataReader.Close();
    }

    private void ClearDataViewer()
    {
        for (int i = 0; i < dataViewer.Columns.Length; ++i)
        {
            dataViewer.RemoveColumn(dataViewer.GetColumn(0));
        }
    }
}
