using System;
using System.Data;
using Gtk;
using MySql.Data.MySqlClient;

public partial class MainWindow : Gtk.Window
{
    private IDbConnection sqlConn;

    private TreeStore data;
    private string curTable;

    private Tuple<TreeIter, int>[] saveRequired;
    private int needsSave;

    private bool selectingTable;
    private TreeSelection curSelected;

    public MainWindow(IDbConnection conn) : base(Gtk.WindowType.Toplevel)
    {
        Build();

        sqlConn = conn;

        data = new TreeStore(typeof(string));
        dataViewer.Model = data;

        selectingTable = true;

        saveRequired = new Tuple<TreeIter, int>[100];
        needsSave = 0;
        RemoveAllSaveList();

        dataViewer.Selection.Changed += OnSelectionChanged;
        btnDelete.Sensitive = false;
    }

    private void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        if (!WantsDiscardChanges())
        {
            a.RetVal = true;   
            return;
        }

        sqlConn.Close();

        Application.Quit();
        a.RetVal = false;
    }

    private void OnBtnLoadTableClicked(object sender, EventArgs e)
    {
        if (!WantsDiscardChanges())
        {
            return;
        }

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

            // Set the attributes of the cell renderer
            col.AddAttribute(cellRendererText, "text", i);
            col.Resizable = true;

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

        // Buttons
        btnSave.Sensitive = false;
        btnDelete.Sensitive = false;
    }

    private void AppendData(IDataReader dReader)
    {
        while (dReader.Read())
        {
            // Holds all values of the returning query
            object[] arr = new object[dReader.FieldCount];

            for (int i = 0; i < dReader.FieldCount; ++i)
            {
                arr[i] = dReader[i];
            }

            // Append values to out TreeView model
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
        if (selectingTable)
        {
            return;
        }

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
        int col = (int)cellRenderer.Data["column"];

        try
        {
            object helper = data.GetValue(iter, col);

            if (helper.ToString() == args.NewText)
            {
                return;
            }

            object newValue = Convert.ChangeType(args.NewText, helper.GetType());
            data.SetValue(iter, col, newValue);

            AddToSave(new Tuple<TreeIter, int>(iter, col));
        }
        catch (FormatException /* ex */)
        {
            MessageDialog dialog = new MessageDialog(this, DialogFlags.DestroyWithParent,
                                                        MessageType.Error, ButtonsType.Ok,
                                                        "El tipo de datos no es correcto");
            dialog.Run();
            dialog.Destroy();
        }
    }

    private bool RequiresSave()
    {
        return needsSave > 0;
    }

    private void AddToSave(Tuple<TreeIter, int> tuple)
    {
        saveRequired[needsSave] = tuple;

        if (needsSave == 0)
        {
            btnSave.Sensitive = true;
        }

        needsSave++;
    }

    private void RemoveAllSaveList()
    {
        for (int i = 0; i < needsSave; ++i)
        {
            saveRequired[i] = null;
        }

        needsSave = 0;
        btnSave.Sensitive = false;
    }

    private bool WantsDiscardChanges()
    {
        if (!RequiresSave())
        {
            return true;
        }

        MessageDialog dialog = new MessageDialog(this, DialogFlags.DestroyWithParent,
                                                MessageType.Question, ButtonsType.YesNo,
                                                "Hay cambios no guardados. ¿Salir?");
        ResponseType response = (ResponseType) dialog.Run();
        dialog.Destroy();

        if (response == ResponseType.Yes)
        {
            RemoveAllSaveList();
            return true;
        }

        return false;
    }

    private void OnBtnSaveClicked(object sender, EventArgs e)
    {
        if (!RequiresSave())
        {
            btnSave.Sensitive = false;
            return;
        }

        IDbTransaction transaction = sqlConn.BeginTransaction();

        // Create command and set connection and transaction objects
        IDbCommand command = sqlConn.CreateCommand();
        command.Connection = sqlConn;
        command.Transaction = transaction;

        try
        {
            TreeIter iter;
            int col;

            // Add the updates for each changed value
            for (int i = 0; i < needsSave; ++i)
            {
                iter = saveRequired[i].Item1;
                col = saveRequired[i].Item2;

                command.CommandText = "UPDATE " + curTable + " SET ";
                command.CommandText += dataViewer.GetColumn(col).Title;
                command.CommandText += " = '";
                command.CommandText += data.GetValue(iter, col).ToString();
                command.CommandText += "' WHERE ";
                command.CommandText += dataViewer.GetColumn(0).Title;
                command.CommandText += " = ";
                command.CommandText += data.GetValue(iter, 0).ToString();
                command.CommandText += ";";

                Console.WriteLine(command.CommandText);
                command.ExecuteNonQuery();
            }

            // Start the transaction
            transaction.Commit();

            // Remove everything from the save list
            RemoveAllSaveList();

            // Successful
            MessageDialog dialog = new MessageDialog(this, DialogFlags.DestroyWithParent,
                                                        MessageType.Info, ButtonsType.Ok,
                                                        "Datos actualizados.");
            dialog.Run();
            dialog.Destroy();
        }
        catch (Exception /* ex */)
        {
            MessageDialog dialog = new MessageDialog(this, DialogFlags.DestroyWithParent,
                                                        MessageType.Error, ButtonsType.Ok,
                                                        "No se ha podido completar la transacción, volviendo atrás.");
            dialog.Run();
            dialog.Destroy();

            try
            {
                transaction.Rollback();
            }
            catch (Exception /* nestedEx */)
            {
                MessageDialog dialog1 = new MessageDialog(this, DialogFlags.DestroyWithParent,
                                                        MessageType.Error, ButtonsType.Ok,
                                                        "No se ha podido completar la transacción, ni volver atrás. Quizás haya datos dañados.");
                dialog1.Run();
                dialog1.Destroy();
            }
        }
    }

    private void OnSelectionChanged(object o, System.EventArgs args)
    {
        if (selectingTable)
        {
            return;
        }

        curSelected = (TreeSelection)o;

        if (curSelected == null)
        {
            btnDelete.Sensitive = false;
            return;
        }

        btnDelete.Sensitive = true;
    }

    private void OnBtnDeleteClicked(object sender, EventArgs e)
    {
        if (!curSelected.GetSelected(out TreeIter iter))
        {
            btnDelete.Sensitive = false;
            return;
        }

        string idStr = data.GetValue(iter, 0).ToString();

        IDbCommand command = sqlConn.CreateCommand();
        command.CommandText = "DELETE FROM " + curTable + " WHERE " + dataViewer.GetColumn(0).Title + " = " + idStr;

        command.ExecuteNonQuery();

        btnReload.Click();
    }
}
