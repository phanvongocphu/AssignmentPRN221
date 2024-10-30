using FStore.BusinessObject;
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
    /// Interaction logic for CreateOrderWindow.xaml
    /// </summary>
    public partial class CreateOrderWindow : Window
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly int _memberId;
        private List<OrderDetail> _orderDetails;

        public CreateOrderWindow(int memberId)
        {
            InitializeComponent();
            _productRepository = new ProductRepository();
            _orderRepository = new OrderRepository();
            _memberId = memberId;
            _orderDetails = new List<OrderDetail>();

            LoadProducts();
            dataGridOrderDetails.ItemsSource = _orderDetails;
        }

        private void LoadProducts()
        {
            cbProducts.ItemsSource = _productRepository.GetProducts();
        }

        private void btnAddOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            if (cbProducts.SelectedValue == null || !int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please select a product and enter a valid quantity.");
                return;
            }

            var productId = (int)cbProducts.SelectedValue;
            var product = _productRepository.GetProductById(productId);

            var discount = float.TryParse(txtDiscount.Text, out float disc) ? disc : 0;

            var existingOrderDetail = _orderDetails.FirstOrDefault(od => od.ProductId == productId);

            if (existingOrderDetail != null)
            {
                existingOrderDetail.Quantity += quantity;
                existingOrderDetail.Discount = discount;
            }
            else
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = productId,
                    Quantity = quantity,
                    UnitPrice = product.UnitPrice,
                    Discount = discount,
                    Product = product
                };
                _orderDetails.Add(orderDetail);
            }
            dataGridOrderDetails.ItemsSource = _orderDetails.Select(od => new
            {
                od.ProductId,
                ProductName = od.Product?.ProductName ?? "Unknown",
                od.Quantity,
                od.UnitPrice,
                od.Discount
            }).ToList();
            dataGridOrderDetails.Items.Refresh();
        }

        private void btnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            var order = new Order
            {
                MemberId = _memberId,
                OrderDate = DateTime.Now,
                Freight = CalculateFreight(),
                RequiredDate = DateTime.Now,
                ShippedDate = DateTime.Now.AddDays(7),
                OrderDetails = _orderDetails.Select(od => new OrderDetail
                {
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice,
                    Discount = od.Discount
                }).ToList()
            };

            bool isOrderCreated = _orderRepository.AddOrder(order);
            if (isOrderCreated)
            {
                MessageBox.Show("Order created successfully!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to create order.");
            }
        }


        private decimal CalculateFreight()
        {
            return 10.0m;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
