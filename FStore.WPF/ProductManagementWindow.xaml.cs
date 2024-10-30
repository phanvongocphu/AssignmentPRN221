using FStore.BusinessObject;
using FStore.DataAccess.DAO;
using FStore.DataAccess.IRepository;
using FStore.DataAccess.Repository;
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

namespace FStore.SalesWPFApp
{
    /// <summary>
    /// Interaction logic for ProductManagementWindow.xaml
    /// </summary>
    public partial class ProductManagementWindow : Window
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductManagementWindow()
        {
            InitializeComponent();
            _productRepository = new ProductRepository();
            _categoryRepository = new CategoryRepository();
        }
        private void dgData_Loaded(object sender, RoutedEventArgs e)
        {
            this.dgDataGrid.ItemsSource = _productRepository.GetProducts();
        }
        private void LoadInitData()
        {
            this.dgDataGrid.ItemsSource = null;
            var products = _productRepository.GetProducts();
            this.dgDataGrid.ItemsSource = products;
            dgDataGrid.Items.Refresh();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadInitData();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void dgDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var categories = _categoryRepository.GetCategories();
            cbbCategory.ItemsSource = categories;
            cbbCategory.DisplayMemberPath = "CategoryName";
            cbbCategory.SelectedValuePath = "CategoryId";

            DataGrid dataGrid = sender as DataGrid;
            DataGridRow? row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex) as DataGridRow;

            if (row != null)
            {
                DataGridCell? dataGridCell = dataGrid.Columns[0].GetCellContent(row)?.Parent as DataGridCell;
                if (dataGridCell != null)
                {
                    string id = ((TextBlock)dataGridCell.Content).Text;
                    var product = _productRepository.GetProductById(int.Parse(id));
                    if (dgDataGrid.SelectedItem is Product selectedProduct)
                    {
                        txtProductId.Text = selectedProduct.ProductId.ToString();
                        txtProductName.Text = selectedProduct.ProductName;
                        txtWeight.Text = selectedProduct.Weight;
                        txtUnitPrice.Text = selectedProduct.UnitPrice.ToString();
                        txtUnitInStock.Text = selectedProduct.UnitInStock.ToString();
                        cbbCategory.SelectedValue = selectedProduct.CategoryId;
                    }
                }
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProductName.Text) &&
                !string.IsNullOrEmpty(txtWeight.Text) &&
                decimal.TryParse(txtUnitPrice.Text, out decimal unitPrice) &&
                int.TryParse(txtUnitInStock.Text, out int unitInStock) &&
                cbbCategory.SelectedValue != null)
            {
                Product product = new Product
                {
                    ProductName = txtProductName.Text,
                    Weight = txtWeight.Text,
                    UnitPrice = unitPrice,
                    UnitInStock = unitInStock,
                    CategoryId = (int)cbbCategory.SelectedValue
                };

                bool isSuccess = _productRepository.AddProduct(product);
                if (isSuccess)
                {
                    LoadInitData();
                    MessageBox.Show("Add Successfully!");
                }
                else
                {
                    MessageBox.Show("Add Failed!");
                }
            }
            else
            {
                MessageBox.Show("Add Failed! Please check the values entered.");
            }
        }

        private void cbbCategory_Loaded(object sender, RoutedEventArgs e)
        {
            var categories = _categoryRepository.GetCategories();
            cbbCategory.ItemsSource = categories;
            cbbCategory.DisplayMemberPath = "CategoryName";
            cbbCategory.SelectedValuePath = "CategoryId";
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtProductId.Text, out int productId))
            {
                Product product = _productRepository.GetProductById(productId);
                if (product != null)
                {
                    var result = MessageBox.Show("Are you sure you want to delete this product?", "Delete Confirmation", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        bool isDeleted = _productRepository.RemoveProduct(productId);

                        if (isDeleted)
                        {
                            LoadInitData();
                            ClearProductDetails();
                            MessageBox.Show("Product deleted successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete the product!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Product not found!");
                }
            }
            else
            {
                MessageBox.Show("You must select a valid Product!");
            }
        }

        private void ClearProductDetails()
        {
            txtProductId.Clear();
            txtProductName.Clear();
            txtWeight.Clear();
            txtUnitPrice.Clear();
            txtUnitInStock.Clear();
            cbbCategory.SelectedIndex = -1;
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgDataGrid.SelectedItem is Product selectedProduct)
            {
                var updateWindow = new UpdateProductWindow(selectedProduct);
                if (updateWindow.ShowDialog() == true)
                {
                    _productRepository.UpdateProduct(selectedProduct);
                    LoadInitData();
                    MessageBox.Show("Product updated successfully!");
                }
            }
            else
            {
                MessageBox.Show("Please select a product to update!");
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearchByName.Text.ToLower();
            var allProducts = ProductDAO.Instance.GetProducts();
            var filteredProducts = allProducts
                .Where(p => p.ProductName != null && p.ProductName.ToLower().Contains(searchText))
                .ToList();
            dgDataGrid.ItemsSource = filteredProducts;
            if (!filteredProducts.Any())
            {
                MessageBox.Show("No products found with the specified name.", "Search Result", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}
