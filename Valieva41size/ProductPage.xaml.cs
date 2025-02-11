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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Valieva41size
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        int CountRecords;
        int CurrrentRecords;

        List <Product> CurrentRecordsList = new List <Product> ();
        List<Product> TableList;
        public ProductPage()
        {
            InitializeComponent();
            var currentProducts = Valieva41Entities.GetContext().Product.ToList();
            ProductListView.ItemsSource = currentProducts;

            ComboType.SelectedIndex = 0;    
        }

        private void UpdateProducts()
        {


            var currentProducts = Valieva41Entities.GetContext().Product.ToList();
            var start_count = currentProducts.Count;

            if (ComboType.SelectedIndex == 0)
            {
                currentProducts = currentProducts.Where(p => (Convert.ToInt32(p.ProductDiscountAmount) >= 0 && Convert.ToInt32(p.ProductDiscountAmount) <= 100)).ToList();
            }
            if (ComboType.SelectedIndex == 1)
            {
                currentProducts = currentProducts.Where(p => (Convert.ToInt32(p.ProductDiscountAmount) >= 0 && Convert.ToInt32(p.ProductDiscountAmount) < 10)).ToList();
            }
            if (ComboType.SelectedIndex == 2)
            {
                currentProducts = currentProducts.Where(p => (Convert.ToInt32(p.ProductDiscountAmount) >= 10 && Convert.ToInt32(p.ProductDiscountAmount) < 15)).ToList();
            }
            if (ComboType.SelectedIndex == 3)
            {
                currentProducts = currentProducts.Where(p => (Convert.ToInt32(p.ProductDiscountAmount) >= 15 && Convert.ToInt32(p.ProductDiscountAmount) <= 100)).ToList();
            }


            currentProducts = currentProducts.Where(p => p.ProductName.ToLower().Contains(TBSearch.Text.ToLower())).ToList();
           
            ProductListView.ItemsSource = currentProducts.ToList();

            if (RButtonDown.IsChecked.Value)
            {
                ProductListView.ItemsSource = currentProducts.OrderByDescending(p => p.ProductCost).ToList();
            }

            if (RButtonUp.IsChecked.Value)
            {
                ProductListView.ItemsSource = currentProducts.OrderBy(p => p.ProductCost).ToList();
            }

            //CountRecords = TableList.Count;
            //счетчик
            TBcount.Text = string.Format("{0} из {1}", currentProducts.Count, start_count);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }

        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProducts();
        }

        private void RadioButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProducts();

        }

        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProducts();

        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProducts();

        }
    }
}
