
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.VBox vbox2;

	private global::Gtk.HButtonBox topMenuBox;

	private global::Gtk.Button btnLoadTable;

	private global::Gtk.Button btnReload;

	private global::Gtk.Button btnSave;

	private global::Gtk.Button btnDelete;

	private global::Gtk.ScrolledWindow GtkScrolledWindow;

	private global::Gtk.TreeView dataViewer;

    protected virtual void Build()
    {
        global::Stetic.Gui.Initialize(this);
        // Widget MainWindow
        this.Name = "MainWindow";
        this.Title = global::Mono.Unix.Catalog.GetString("MainWindow");
        this.WindowPosition = ((global::Gtk.WindowPosition)(4));
        // Container child MainWindow.Gtk.Container+ContainerChild
        this.vbox2 = new global::Gtk.VBox();
        this.vbox2.Name = "vbox2";
        this.vbox2.Spacing = 6;
        this.vbox2.BorderWidth = ((uint)(20));
        // Container child vbox2.Gtk.Box+BoxChild
        this.topMenuBox = new global::Gtk.HButtonBox();
        this.topMenuBox.Name = "topMenuBox";
        this.topMenuBox.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(1));
        // Container child topMenuBox.Gtk.ButtonBox+ButtonBoxChild
        this.btnLoadTable = new global::Gtk.Button();
        this.btnLoadTable.CanFocus = true;
        this.btnLoadTable.Name = "btnLoadTable";
        this.btnLoadTable.UseUnderline = true;
        this.btnLoadTable.Label = global::Mono.Unix.Catalog.GetString("Cargar Tabla");
        this.topMenuBox.Add(this.btnLoadTable);
        global::Gtk.ButtonBox.ButtonBoxChild w1 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.topMenuBox[this.btnLoadTable]));
        w1.Expand = false;
        w1.Fill = false;
        // Container child topMenuBox.Gtk.ButtonBox+ButtonBoxChild
        this.btnReload = new global::Gtk.Button();
        this.btnReload.CanFocus = true;
        this.btnReload.Name = "btnReload";
        this.btnReload.UseUnderline = true;
        this.btnReload.Label = global::Mono.Unix.Catalog.GetString("Recargar");
        this.topMenuBox.Add(this.btnReload);
        global::Gtk.ButtonBox.ButtonBoxChild w2 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.topMenuBox[this.btnReload]));
        w2.Position = 1;
        w2.Expand = false;
        w2.Fill = false;
        // Container child topMenuBox.Gtk.ButtonBox+ButtonBoxChild
        this.btnSave = new global::Gtk.Button();
        this.btnSave.CanFocus = true;
        this.btnSave.Name = "btnSave";
        this.btnSave.UseUnderline = true;
        this.btnSave.Label = global::Mono.Unix.Catalog.GetString("Guardar");
        this.topMenuBox.Add(this.btnSave);
        global::Gtk.ButtonBox.ButtonBoxChild w3 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.topMenuBox[this.btnSave]));
        w3.Position = 2;
        w3.Expand = false;
        w3.Fill = false;
        // Container child topMenuBox.Gtk.ButtonBox+ButtonBoxChild
        this.btnDelete = new global::Gtk.Button();
        this.btnDelete.CanFocus = true;
        this.btnDelete.Name = "btnDelete";
        this.btnDelete.UseUnderline = true;
        this.btnDelete.Label = global::Mono.Unix.Catalog.GetString("Eliminar");
        this.topMenuBox.Add(this.btnDelete);
        global::Gtk.ButtonBox.ButtonBoxChild w4 = ((global::Gtk.ButtonBox.ButtonBoxChild)(this.topMenuBox[this.btnDelete]));
        w4.Position = 3;
        w4.Expand = false;
        w4.Fill = false;
        this.vbox2.Add(this.topMenuBox);
        global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.topMenuBox]));
        w5.Position = 0;
        w5.Expand = false;
        w5.Fill = false;
        // Container child vbox2.Gtk.Box+BoxChild
        this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
        this.GtkScrolledWindow.Name = "GtkScrolledWindow";
        this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
        // Container child GtkScrolledWindow.Gtk.Container+ContainerChild
        this.dataViewer = new global::Gtk.TreeView();
        this.dataViewer.CanFocus = true;
        this.dataViewer.Name = "dataViewer";
        this.GtkScrolledWindow.Add(this.dataViewer);
        this.vbox2.Add(this.GtkScrolledWindow);
        global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.GtkScrolledWindow]));
        w7.Position = 1;
        this.Add(this.vbox2);
        if ((this.Child != null))
        {
            this.Child.ShowAll();
        }
        this.DefaultWidth = 872;
        this.DefaultHeight = 532;
        this.Show();
        this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
        this.btnLoadTable.Clicked += new global::System.EventHandler(this.OnBtnLoadTableClicked);
        this.btnReload.Clicked += new global::System.EventHandler(this.OnBtnReloadClicked);
        this.btnSave.Clicked += new global::System.EventHandler(this.OnBtnSaveClicked);
        this.btnDelete.Clicked += new global::System.EventHandler(this.OnBtnDeleteClicked);
        this.dataViewer.RowActivated += new global::Gtk.RowActivatedHandler(this.OnDataViewerRowActivated);
    }
}
