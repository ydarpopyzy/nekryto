using System;
using System.Collections.Generic;
using System.Linq;
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
        public Products(string username)
        {
            InitializeComponent();
            ProductsListView.ItemsSource = db.Products.ToList();

            products = db.Products.ToList();

            //var orders = db.Orders.ToList();
           //decimal maxSales = orders.Sum(o => o.TotalCost);

            //foreach (var product in products)
            //{
            //   // product.Discount = CalculateDiscount(maxSales, totalSales);
            //    product.DiscountedPrice = product.Price * (1 - product.Discount / 100);
            //}

        }
        private decimal CalculateDiscount(decimal maxSales, decimal totalSales)
        {
            if (totalSales < maxSales / 4) return 0;
            if (totalSales < maxSales / 2) return 5;
            if (totalSales < (3 * maxSales) / 4) return 10;
            return 15;
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
