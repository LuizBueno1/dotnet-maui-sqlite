using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyPurchases.Models;

namespace MyPurchases.Views;

public partial class CategoryProduct : ContentPage
{
    ObservableCollection<Product> list = new ObservableCollection<Product>();
    public CategoryProduct()
	{
		InitializeComponent();
        product_list.ItemsSource = list;
    }

    protected async override void OnAppearing()
    {
        try
        {
            list.Clear();

            List<Product> l = await App.Db.GetAll();

            l.ForEach(i => list.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string query = e.NewTextValue;

            product_list.IsRefreshing = true;

            list.Clear();

            List<Product> l = await App.Db.SearchByCategory(query);

            l.ForEach(i => list.Add(i));

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
        finally
        {
            product_list.IsRefreshing = false;
        }
    }

    private async void product_list_Refreshing(object sender, EventArgs e)
    {
        try
        {
            list.Clear();

            List<Product> l = await App.Db.GetAll();

            l.ForEach(i => list.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
        finally
        {
            product_list.IsRefreshing = false;
        }
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            var sumsByCategory = list
                .GroupBy(i => i.Category)            // agrupa por categoria
                .Select(g => new
                {
                    Category = g.Key,                 // nome da categoria
                    Total = g.Sum(i => i.Total)       // soma da categoria
                })
                .ToList();

            // monta a mensagem
            string msg = string.Join(Environment.NewLine,
                sumsByCategory.Select(x => $"{x.Category}: {x.Total:C}"));

            DisplayAlert("Total by Category", msg, "Ok");
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}