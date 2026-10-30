namespace djaramilloExamen.Views;

public partial class vResumen : ContentPage
{
	string contenido;

	public vResumen(string usuario, string nombre, string apellido, int edad, DateTime fecha,
		string plan, bool casillero, double monto, double pago, double total)
	{
		InitializeComponent();
		lblUsuario.Text = "Usuario conectado: " + usuario;
		lblNombre.Text = nombre;
		lblApellido.Text = apellido;
		lblEdad.Text = edad.ToString();
		lblFecha.Text = fecha.ToString("dd/MM/yyyy");
		lblPlan.Text = plan;
		lblCasillero.Text = casillero ? "Sí" : "No";
		lblMonto.Text = "$ " + monto.ToString("F2");
		lblPago.Text = "$ " + pago.ToString("F2");
		lblTotal.Text = "$ " + total.ToString("F2");

		contenido =
			"Usuario conectado: " + usuario + "\n" +
			"Nombre: " + nombre + "\n" +
			"Apellido: " + apellido + "\n" +
			"Edad: " + lblEdad.Text + "\n" +
			"Fecha de inicio: " + lblFecha.Text + "\n" +
			"Plan: " + plan + "\n" +
			"Casillero: " + lblCasillero.Text + "\n" +
			"Monto Inicial: " + lblMonto.Text + "\n" +
			"Pago mensual: " + lblPago.Text + "\n" +
			"Pago Total: " + lblTotal.Text + "\n" +
			"------------------------------\n";
	}

	// Guardar el resumen en TXT
	private async void btnExportar_Clicked(object? sender, EventArgs e)
	{
		try
		{
			string archivo = "socios.txt";
			string ruta = Path.Combine(FileSystem.AppDataDirectory, archivo);
			File.AppendAllText(ruta, contenido);

			await DisplayAlertAsync("Archivo exportado", "Archivo: " + archivo + "\nRuta: " + ruta, "OK");
		}
		catch (Exception ex)
		{
			await DisplayAlertAsync("Error", ex.Message, "OK");
		}
	}

	// Regresa a Registro (ya limpio)
	private async void btnNuevo_Clicked(object? sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}

	// Regresa al Login
	private async void btnCerrarSesion_Clicked(object? sender, EventArgs e)
	{
		await Navigation.PopToRootAsync();
	}
}
