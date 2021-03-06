
// This file has been generated by the GUI designer. Do not modify.
namespace CGTK
{
	public partial class LoginWindow
	{
		private global::Gtk.VBox loginForm;

		private global::Gtk.Entry loginUsername;

		private global::Gtk.Entry loginPassword;

		private global::Gtk.Button btnLogin;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget CGTK.LoginWindow
			this.Name = "CGTK.LoginWindow";
			this.Title = global::Mono.Unix.Catalog.GetString("Login");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			this.Resizable = false;
			// Container child CGTK.LoginWindow.Gtk.Container+ContainerChild
			this.loginForm = new global::Gtk.VBox();
			this.loginForm.Name = "loginForm";
			this.loginForm.Spacing = 6;
			this.loginForm.BorderWidth = ((uint)(100));
			// Container child loginForm.Gtk.Box+BoxChild
			this.loginUsername = new global::Gtk.Entry();
			global::Gtk.Tooltips w1 = new Gtk.Tooltips();
			w1.SetTip(this.loginUsername, "Usuario", "Usuario");
			this.loginUsername.CanFocus = true;
			this.loginUsername.Name = "loginUsername";
			this.loginUsername.IsEditable = true;
			this.loginUsername.InvisibleChar = '•';
			this.loginForm.Add(this.loginUsername);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.loginForm[this.loginUsername]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child loginForm.Gtk.Box+BoxChild
			this.loginPassword = new global::Gtk.Entry();
			w1.SetTip(this.loginPassword, "Contraseña", "Contraseña");
			this.loginPassword.CanFocus = true;
			this.loginPassword.Name = "loginPassword";
			this.loginPassword.IsEditable = true;
			this.loginPassword.Visibility = false;
			this.loginPassword.InvisibleChar = '•';
			this.loginForm.Add(this.loginPassword);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.loginForm[this.loginPassword]));
			w3.Position = 1;
			w3.Expand = false;
			w3.Fill = false;
			// Container child loginForm.Gtk.Box+BoxChild
			this.btnLogin = new global::Gtk.Button();
			this.btnLogin.CanFocus = true;
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.UseUnderline = true;
			this.btnLogin.Label = global::Mono.Unix.Catalog.GetString("Login");
			this.loginForm.Add(this.btnLogin);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.loginForm[this.btnLogin]));
			w4.Position = 2;
			w4.Expand = false;
			w4.Fill = false;
			this.Add(this.loginForm);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 349;
			this.Show();
			this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
			this.btnLogin.Clicked += new global::System.EventHandler(this.OnBtnLoginClicked);
		}
	}
}
