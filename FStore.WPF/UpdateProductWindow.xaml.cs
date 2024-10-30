using FStore.BusinessObject;
using FStore.DataAccess.IRepository;
using FStore.DataAccess.Repository;
using System.Windows;

namespace FStore.SalesWPFApp
{
    public partial class UpdateProductWindow : Window
    {
        private Product _product;
        private readonly ICategoryRepository _categoryRepository = new CategoryRepository();

        public UpdateProductWindow(Product product)
        {
            InitializeComponent();
            _product = product;
            LoadProductData();
        }

        private void LoadProductData()
        {
            txtProductName.Text = _product.ProductName;
            txtWeight.Text = _product.Weight;
            txtUnitPrice.Text = _product.UnitPrice.ToString();
            txtUnitInStock.Text = _product.UnitInStock.ToString();

            var categories = _categoryRepository.GetCategories();
            cbbCategory.ItemsSource = categories;
            cbbCategory.DisplayMemberPath = "CategoryName";
            cbbCategory.SelectedValuePath = "CategoryId";
            cbbCategory.SelectedValue = _product.CategoryId;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _product.ProductName = txtProductName.Text;
            _product.Weight = txtWeight.Text;
            _product.UnitPrice = decimal.Parse(txtUnitPrice.Text);
            _product.UnitInStock = int.Parse(txtUnitInStock.Text);
            _product.CategoryId = (int)cbbCategory.SelectedValue;

            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
