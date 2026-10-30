using System.Globalization;

namespace djaramilloExamen.Views;

public partial class vRegistro : ContentPage
{
	string usuario;

	public vRegistro(string usuario)
	{
		InitializeComponent();
		this.usuario = usuario;
		lblUsuario.Text = "Usuario conectado: " + usuario;
		dpFecha.MinimumDate = DateTime.Today;
		dpFecha.Date = DateTime.Today;
	}

	// Solo permite numeros
	private void SoloNumeros_TextChanged(object? sender, TextChangedEventArgs e)
	{
		Entry entry = (Entry)sender!;
		string texto = new string((e.NewTextValue ?? "").Where(char.IsDigit).ToArray());
		if (entry.Text != texto)
			entry.Text = texto;
	}

	// Solo permite letras y espacios
	private void SoloLetras_TextChanged(object? sender, TextChangedEventArgs e)
	{
		Entry entry = (Entry)sender!;
		string texto = new string((e.NewTextValue ?? "").Where(c => char.IsLetter(c) || c == ' ').ToArray());
		if (entry.Text != texto)
			entry.Text = texto;
	}

	// Solo permite numeros y un separador decimal
	private void SoloDecimales_TextChanged(object? sender, TextChangedEventArgs e)
	{
		Entry entry = (Entry)sender!;
		string texto = "";
		bool tieneSeparador = false;
		foreach (char c in e.NewTextValue ?? "")
		{
			if (char.IsDigit(c))
				texto += c;
			else if ((c == '.' || c == ',') && !tieneSeparador)
			{
				texto += '.';
				tieneSeparador = true;
			}
		}
		if (entry.Text != texto)
			entry.Text = texto;
	}

	// Devuelve el primer error o null si todo es valido
	private string? Validar()
	{
		if (string.IsNullOrWhiteSpace(txtNombre.Text))
			return "Ingrese el nombre";
		if (string.IsNullOrWhiteSpace(txtApellido.Text))
			return "Ingrese el apellido";
		if (!int.TryParse(txtEdad.Text, out int edad) || edad < 16)
			return "La edad debe ser mayor o igual a 16";
		if (dpFecha.Date == null || dpFecha.Date < DateTime.Today)
			return "La fecha de inicio no puede ser pasada";
		if (pkPlan.SelectedIndex == -1)
			return "Seleccione el plan";
		if (!double.TryParse(txtMonto.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double monto) || monto < 120 || monto > 600)
			return "El monto inicial debe estar entre 120 y 600";
		return null;
	}

	// Cuota = (1200 - monto) / 5 + 2% de 1200 + 10 si tiene casillero
	private double CalcularPago()
	{
		double monto = double.Parse(txtMonto.Text!, CultureInfo.InvariantCulture);
		double cuota = (1200 - monto) / 5 + 1200 * 0.02;
		if (swCasillero.IsToggled)
			cuota += 10;
		return Math.Round(cuota, 2);
	}

	private async void btnCalcular_Clicked(object? sender, EventArgs e)
	{
		try
		{
			string? error = Validar();
			if (error != null)
			{
				await DisplayAlertAsync("Error", error, "OK");
				return;
			}

			txtPago.Text = CalcularPago().ToString("F2");
		}
		catch (Exception ex)
		{
			await DisplayAlertAsync("Error", ex.Message, "OK");
		}
	}

	private async void btnVerResumen_Clicked(object? sender, EventArgs e)
	{
		try
		{
			string? error = Validar();
			if (error != null)
			{
				await DisplayAlertAsync("Error", error, "OK");
				return;
			}

			double monto = double.Parse(txtMonto.Text!, CultureInfo.InvariantCulture);
			double pago = CalcularPago();
			double total = Math.Round(monto + pago * 5, 2);
			txtPago.Text = pago.ToString("F2");

			await Navigation.PushAsync(new vResumen(usuario,
				txtNombre.Text!.Trim(),
				txtApellido.Text!.Trim(),
				int.Parse(txtEdad.Text!),
				dpFecha.Date!.Value,
				pkPlan.SelectedItem.ToString()!,
				swCasillero.IsToggled,
				monto, pago, total));

			Limpiar();
		}
		catch (Exception ex)
		{
			await DisplayAlertAsync("Error", ex.Message, "OK");
		}
	}

	// Dejar formulario vacio
	private void Limpiar()
	{
		txtNombre.Text = "";
		txtApellido.Text = "";
		txtEdad.Text = "";
		dpFecha.Date = DateTime.Today;
		pkPlan.SelectedIndex = -1;
		swCasillero.IsToggled = false;
		txtMonto.Text = "";
		txtPago.Text = "";
	}
}
