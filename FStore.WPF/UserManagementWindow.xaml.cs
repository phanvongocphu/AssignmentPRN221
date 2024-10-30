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
    /// Interaction logic for UserManagementWindow.xaml
    /// </summary>
    public partial class UserManagementWindow : Window
    {
        private Member _currentUser;
        private IMemberRepository _memberRepository;
        private IOrderRepository _orderRepository;
        private IOrderDetailRepository _orderDetailRepository;

        public UserManagementWindow(Member currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _memberRepository = new MemberRepository();
            _orderRepository = new OrderRepository();
            _orderDetailRepository = new OrderDetailRepository();
            LoadOrderHistory();
        }

        private void LoadOrderHistory()
        {
            var orders = _orderRepository.GetOrderByMemberId(_currentUser.MemberId);

            if (orders == null || !orders.Any())
            {
                MessageBox.Show("No orders found for this member.");
                return;
            }

            var orderViewModel = orders.Select(o => new
            {
                MemberName = _memberRepository.GetMemberById(o.MemberId)?.CompanyName ?? "Unknown",
                OrderDate = o.OrderDate,
                RequiredDate = o.OrderDate,
                ShippedDate = o.OrderDate.AddDays(7),
                Freight = o.Freight,
                Order = o,
            }).ToList();

            dataGridOrders.ItemsSource = orderViewModel;
        }

        private void btnUpdateProfile_Click(object sender, RoutedEventArgs e)
        {
            UpdateProfileWindow updateProfileWindow = new UpdateProfileWindow(_currentUser);
            updateProfileWindow.ShowDialog();
            _currentUser = _memberRepository.GetMember(_currentUser.Email);
        }

        private void btnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            CreateOrderWindow createOrderWindow = new CreateOrderWindow(_currentUser.MemberId);
            createOrderWindow.ShowDialog();
            LoadOrderHistory();
        }

        private void dataGridOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dataGridOrders.SelectedItem is not null)
            {
                var selectedOrder = (dynamic)dataGridOrders.SelectedItem;
                var order = selectedOrder.Order;
                if (order == null)
                {
                    MessageBox.Show("Selected order is not valid.");
                    return;
                }

                var orderId = order.OrderId;
                var orderDetails = _orderDetailRepository.GetOrderDetails(orderId);

                if (orderDetails == null)
                {
                    MessageBox.Show("No order details available for this order.");
                    return;
                }

                OrderDetailWindow orderDetailWindow = new OrderDetailWindow(orderDetails);
                orderDetailWindow.ShowDialog();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }

}
