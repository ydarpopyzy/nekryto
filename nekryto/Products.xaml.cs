using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace nekryto
{
    /// <summary>
    /// Логика взаимодействия для Products.xaml
    /// </summary>
    public partial class Products : Window
    {
        user36015Entities db = new user36015Entities();
        private List<Products> products;
        private string currentSort = "";
        public string username;

        public Products(string username)
        {
            InitializeComponent();
            ProductsListView.ItemsSource = db.Products.ToList();

            products = db.Products.ToList();

            var orders = db.Orders.ToList();
           decimal totalSales = orders.Sum(o => o.Quantity * o.Products.Price);
           decimal maxSales = orders.Sum(o => o.TotalCost);

            foreach (var product in products)
            {
                product.Discount = CalculateDiscount(maxSales, totalSales);
                product.DiscountedPrice = Math.Round(product.Price * (1 - product.Discount / 100),2);
            }

            ProductsListView.ItemsSource = products;

            BrandCombo.Items.Add("Все бренды");
            foreach (var brand in db.Products.ToList())
                BrandCombo.Items.Add(brand.Brand);
            BrandCombo.SelectedIndex = 0;

            CategoryCombo.Items.Add("Все категории");
            foreach (var category in db.Category.ToList())
                CategoryCombo.Items.Add(category.CategoryName);
            CategoryCombo.SelectedIndex = 0;

        }
        private decimal CalculateDiscount(decimal maxSales, decimal totalSales)
        {
            if (totalSales < maxSales / 4) return 0;
            if (totalSales < maxSales / 2) return 5;
            if (totalSales < (3 * maxSales) / 4) return 10;
            return 15;
        }
        private void UpdateProducts()
        {
            if (products == null) return;

            var filtered = products.AsQueryable();

            if (BrandCombo.SelectedIndex > 0)
                filtered = filtered.Where(p => p.Brand == BrandCombo.SelectedItem.ToString());

            if (CategoryCombo.SelectedIndex > 0)
                filtered = filtered.Where(p => p.Category.CategoryName == CategoryCombo.SelectedItem.ToString());

            if (!string.IsNullOrEmpty(SearchBox.Text))
                filtered = filtered.Where(p => p.ProductName.Contains(SearchBox.Text));

            if (currentSort == "asc")
                filtered = filtered.OrderBy(p => p.Price);
            else if (currentSort == "desc")
                filtered = filtered.OrderByDescending(p => p.Price);

            ProductsListView.ItemsSource = filtered.ToList();
        }
        private void BrandCombo_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            UpdateProducts();
        }
        private void CategoryCombo_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryCombo.SelectedIndex == 0)
                ProductsListView.ItemsSource = products;
            else
                ProductsListView.ItemsSource = products.Where(p => p.Category.CategoryName == CategoryCombo.SelectedItem.ToString()).ToList();
            UpdateProducts();
        }
        private void SearchBox_TextChanged(object sender, RoutedEventArgs e)
        {
            UpdateProducts();
        }
        private void SortAsc_Click(object sender, RoutedEventArgs e)
        {
            currentSort = "asc";
            UpdateProducts();
        }
        private void SortDesc_Click(object sender, RoutedEventArgs e)
        {
            currentSort = "desc";
            UpdateProducts();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MainFrame.Navigate(new OrderWindow(orderItems));
        }
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Products products = (Products)e.AddedItems[0];

                string ProductName = products.ProductName;

            }
        }
    }
}
