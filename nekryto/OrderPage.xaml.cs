using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace nekryto
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        private ObservableCollection<Products> orderItems;

        public OrderPage(ObservableCollection<Products> orderItems)
        {
            InitializeComponent();
            this.orderItems = orderItems;
            OrderListView.ItemsSource = orderItems;
            CalculateTotalCost();
        }
        private void CalculateTotalCost()
        {
            decimal totalCost = 0;
            decimal totalDiscount = 0;
            foreach (var item in orderItems)
            {
                totalCost += item.DiscountedPrice;
                totalDiscount += item.Price - item.DiscountedPrice;
            }
            TotalCostText.Text = $"Общая сумма: {totalCost} ₽\nСумма скидки: {totalDiscount} ₽";
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Button removeButton = sender as Button;
            if (removeButton != null)
            {
                Products productToRemove = removeButton.DataContext as Products;
                if (productToRemove != null)
                {
                    orderItems.Remove(productToRemove);
                    CalculateTotalCost();
                }
            }
        }
    }
}
