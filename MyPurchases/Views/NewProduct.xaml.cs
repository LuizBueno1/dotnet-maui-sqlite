using System.Threading.Tasks;
using MyPurchases.Models;

namespace MyPurchases.Views;

public partial class NewProduct : ContentPage
{
	public NewProduct()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Product product = new Product
			{
				Description = txt_description.Text,
				Amount = Convert.ToDouble(txt_amount.Text),
				Price = Convert.ToDouble(txt_price.Text)
			};

			await App.Db.Insert(product);
			await DisplayAlert("Success!", "Product saved", "Ok");

		}
		catch(Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "Ok");
		}
    }
}