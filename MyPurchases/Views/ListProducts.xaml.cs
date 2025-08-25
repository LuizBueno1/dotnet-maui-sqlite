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
		List<Product> l = await App.Db.GetAll();

		l.ForEach(i => list.Add(i));
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
        string query = e.NewTextValue;

        list.Clear();

        List<Product> l = await App.Db.Search(query);

        l.ForEach(i => list.Add(i));
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
		double sum = list.Sum(i => i.Total);

		string msg = $"The total is {sum:C}";

		DisplayAlert("Total products", msg, "Ok");
    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {

    }
}