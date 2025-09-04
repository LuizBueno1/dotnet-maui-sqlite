using MyPurchases.Models;

namespace MyPurchases.Views;

public partial class EditProduct : ContentPage
{
	public EditProduct()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Product product_attached = BindingContext as Product;

            Product product = new Product
            {   
                Id = product_attached.Id,
                Description = txt_description.Text,
                Amount = Convert.ToDouble(txt_amount.Text),
                Price = Convert.ToDouble(txt_price.Text)
            };

            await App.Db.Update(product);
            await DisplayAlert("Success!", "Product Updated", "Ok");
            await Navigation.PopAsync();

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "Ok");
        }
    }
}