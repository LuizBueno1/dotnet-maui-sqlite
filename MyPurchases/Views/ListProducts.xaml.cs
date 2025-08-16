namespace MyPurchases.Views;

public partial class ListProducts : ContentPage
{
	public ListProducts()
	{
		InitializeComponent();
	}

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Navigation.PushAsync(new Views.NewProduct());
		} catch(Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "OK");
		}

    }
}