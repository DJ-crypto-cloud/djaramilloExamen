namespace djaramilloExamen.Views;

public partial class vResumen : ContentPage
{
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
