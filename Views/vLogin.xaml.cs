namespace djaramilloExamen.Views;

public partial class vLogin : ContentPage
{
	// Matriz 4 x 2: usuario, contraseña
	string[,] usuarios =
	{
		{ "recepcion", "gym2026" },
		{ "entrenador", "fit_01" },
		{ "uisrael", "2026" },
		{ "gerente", "admin123" }
	};

	public vLogin()
	{
		InitializeComponent();
	}

	private async void btnIniciarSesion_Clicked(object? sender, EventArgs e)
	{
		try
		{
			string usuario = (txtUsuario.Text ?? "").Trim();
			string contrasena = txtContrasena.Text ?? "";

			// Buscar en la matriz
			bool valido = false;
			for (int i = 0; i < usuarios.GetLength(0); i++)
			{
				if (usuarios[i, 0] == usuario && usuarios[i, 1] == contrasena)
					valido = true;
			}

			if (valido)
			{
				txtUsuario.Text = "";
				txtContrasena.Text = "";
				await Navigation.PushAsync(new vRegistro(usuario));
			}
			else
			{
				await DisplayAlertAsync("Error", "Dato incorrecto: usuario o contraseña no válidos", "OK");
				txtContrasena.Text = "";
			}
		}
		catch (Exception ex)
		{
			await DisplayAlertAsync("Error", ex.Message, "OK");
		}
	}

	private async void btnAcercaDe_Clicked(object? sender, EventArgs e)
	{
		await Navigation.PushAsync(new vAcercaDe());
	}
}
