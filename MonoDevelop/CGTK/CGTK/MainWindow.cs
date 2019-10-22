using System;
using System.Data;
using Gtk;
using MySql.Data.MySqlClient;

public partial class MainWindow : Gtk.Window
{
    private IDbConnection sqlConn;

    private TreeStore data;
    private string curTable;
    private bool selectingTable;

    public MainWindow(IDbConnection conn) : base(Gtk.WindowType.Toplevel)
    {
        Build();

        sqlConn = conn;

        data = new TreeStore(typeof(string));
        dataViewer.Model = data;

        selectingTable = true;
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

        SetupDataViewer(dataReader);
        AppendData(dataReader);

        dataReader.Close();

        selectingTable = true;
    }

    private void LoadTableData()
    {
        IDbCommand command = sqlConn.CreateCommand();
        command.CommandText = "SELECT * FROM " + curTable;

        IDataReader dataReader = command.ExecuteReader();

        SetupDataViewer(dataReader);
        AppendData(dataReader);

        dataReader.Close();
    }

    private void ClearDataViewer()
    {
        int cols = dataViewer.Columns.Length;
        for (int i = 0; i < cols; ++i)
        {
            dataViewer.RemoveColumn(dataViewer.GetColumn(0));
        }

        data.Clear();
    }

    private void SetupDataViewer(IDataReader dReader)
    {
        // Clear everything
        ClearDataViewer();

        // Setup the types for TreeStore
        Type[] types = new Type[dReader.FieldCount];

        for (int i = 0; i < dReader.FieldCount; ++i)
        {
            // Grab each column from fields and append it
            dataViewer.AppendColumn(dReader.GetName(i), new CellRendererText(), "text", i);

            // Grab each type from fields and save inside 'types'
            types[i] = dReader.GetFieldType(i);
        }

        // Setup the TreeStore
        data = new TreeStore(types);
        dataViewer.Model = data;
    }

    private void AppendData(IDataReader dReader)
    {
        while (dReader.Read())
        {
            data.AppendValues(dReader[0].ToString());
        }
    }

    protected void OnDataViewerRowActivated(object o, RowActivatedArgs args)
    {
        data.GetIter(out TreeIter iter, args.Path);

        if (selectingTable)
        {
            string s = (string)data.GetValue(iter, 0);
            curTable = s;
            selectingTable = false;

            LoadTableData();
            return;
        }
    }
}
