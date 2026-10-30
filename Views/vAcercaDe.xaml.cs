namespace djaramilloExamen.Views;

public partial class vAcercaDe : ContentPage
{
	public vAcercaDe()
	{
		InitializeComponent();
	}

	private async void btnVolver_Clicked(object? sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}
