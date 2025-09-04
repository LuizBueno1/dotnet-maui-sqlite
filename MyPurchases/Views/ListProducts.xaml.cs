using System.Collections.ObjectModel;
using MyPurchases.Models;

namespace MyPurchases.Views;

public partial class ListProducts : ContentPage
{
	ObservableCollection<Product> list = new ObservableCollection<Product>();

	public ListProducts()
	{
		InitializeComponent();

		product_list.ItemsSource = list;
	}

	protected async override void OnAppearing()
	{
		try
		{
			List<Product> l = await App.Db.GetAll();

			l.ForEach(i => list.Add(i));
		} catch(Exception ex)
		{
            await DisplayAlert("Ops", ex.Message, "OK");
        }
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

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
	{
		try
		{
			string query = e.NewTextValue;

			list.Clear();

			List<Product> l = await App.Db.Search(query);

			l.ForEach(i => list.Add(i));
        } catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
	{
		try
		{
			double sum = list.Sum(i => i.Total);

			string msg = $"The total is {sum:C}";

			DisplayAlert("Total products", msg, "Ok");
        } catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
		try
		{

			MenuItem selected = sender as MenuItem;
			Product p = selected.BindingContext as Product;

			bool confirm = await DisplayAlert("Are you sure?", $"Remove {p.Description}?", "Yes", "No");

			if (confirm)
			{
				await App.Db.Delete(p.Id);
				list.Remove(p);
			}
        } catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void product_list_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		try
		{
			Product p = e.SelectedItem as Product;

			Navigation.PushAsync(new Views.EditProduct
			{
				BindingContext = p,
			});
		} catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}