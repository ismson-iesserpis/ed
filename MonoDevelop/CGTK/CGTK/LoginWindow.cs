using System;
using System.Data;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using Gtk;

namespace CGTK
{
    public partial class LoginWindow : Gtk.Window
    {
        public LoginWindow() : base(Gtk.WindowType.Toplevel)
        {
            Build();
        }

        protected void OnDeleteEvent(object o, DeleteEventArgs args)
        {
            Application.Quit();
            args.RetVal = true;
        }

        protected void OnBtnLoginClicked(object sender, EventArgs e)
        {
            String username, password;
            username = loginUsername.Text;
            password = loginPassword.Text;

            if (username == "")
            {
                MessageDialog dialog = new MessageDialog(this, DialogFlags.DestroyWithParent,
                                                        MessageType.Error, ButtonsType.Ok,
                                                        "Debes introducir un nombre de usuario");
                dialog.Run();
                dialog.Destroy();
                return;
            }

            IDbConnection conn;
            try
            {
                conn = new MySqlConnection("server=localhost;database=dbtest;user=" + username + ";password=" + password + ";");
                conn.Open();
            }
            catch (MySqlException ex)
            {
                MessageDialog dialog = new MessageDialog(this, DialogFlags.DestroyWithParent,
                                                        MessageType.Error, ButtonsType.Ok,
                                                        ex.Message);
                dialog.Run();
                dialog.Destroy();

                loginPassword.Text = "";
                return;
            }

            MainWindow mainWindow = new MainWindow(conn);
            mainWindow.Show();
            this.Destroy();
        }
    }
}
