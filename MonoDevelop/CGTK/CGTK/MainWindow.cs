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

    private void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        sqlConn.Close();

        Application.Quit();
        a.RetVal = true;
    }

    private void OnBtnLoadTableClicked(object sender, EventArgs e)
    {
        selectingTable = true;

        IDbCommand command = sqlConn.CreateCommand();
        command.CommandText = "SHOW TABLES;";

        IDataReader dataReader = command.ExecuteReader();

        SetupDataViewer(dataReader);
        AppendData(dataReader);

        dataReader.Close();
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
            // Create new column
            TreeViewColumn col = new TreeViewColumn();
            col.Title = dReader.GetName(i);

            // Create new CellRenderertext
            CellRendererText cellRendererText = new CellRendererText();
            col.PackStart(cellRendererText, true);

            // Add the column to the dataViewer
            dataViewer.AppendColumn(col);

            // Set the attribute of the cell renderer
            col.AddAttribute(cellRendererText, "text", i);

            // If selecting table, false; else true
            if (selectingTable)
            {
                cellRendererText.Editable = false;
            }
            else
            {
                cellRendererText.Editable = true;
                cellRendererText.Data["column"] = i; // Helper for edited function
            }

            cellRendererText.Edited += OnCellEdited;

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
            object[] arr = new object[dReader.FieldCount];

            for (int i = 0; i < dReader.FieldCount; ++i)
            {
                arr[i] = dReader[i];
            }

            data.AppendValues(arr);
        }
    }

    private void OnDataViewerRowActivated(object o, RowActivatedArgs args)
    {
        data.GetIter(out TreeIter iter, args.Path);

        // User is selecting a new table
        if (selectingTable)
        {
            string s = (string)data.GetValue(iter, 0);
            curTable = s;
            selectingTable = false;

            LoadTableData();
            return;
        }
    }

    private void OnBtnReloadClicked(object sender, EventArgs e)
    {
        IDbCommand command = sqlConn.CreateCommand();
        command.CommandText = "SELECT * FROM " + curTable;

        IDataReader dReader = command.ExecuteReader();

        SetupDataViewer(dReader);
        AppendData(dReader);

        dReader.Close();
    }

    protected void OnCellEdited(object o, EditedArgs args)
    {
        data.GetIter(out TreeIter iter, new TreePath(args.Path));

        CellRendererText cellRenderer = (CellRendererText)o;
        int col = (int) cellRenderer.Data["column"];

        data.SetValue(iter, col, args.NewText);
    }
}
