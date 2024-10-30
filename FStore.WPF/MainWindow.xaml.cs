using FStore.BusinessObject;
using FStore.DataAccess.IRepository;
using FStore.DataAccess.Repository;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FStore.SalesWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IMemberRepository _memberRepository;
        public MainWindow()
        {
            InitializeComponent();
            _memberRepository = new MemberRepository();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Member member = _memberRepository.GetMember(txtEmail.Text);
            if (member != null && txtPassword.Password.Equals(member.Password) && member.Role == "Admin")
            {
                ProductManagementWindow productManagementWindow = new ProductManagementWindow();
                productManagementWindow.Show();
                this.Hide();
            }
            else if (member != null && txtPassword.Password.Equals(member.Password) && member.Role == "User")
            {
                UserManagementWindow userManagementWindow = new UserManagementWindow(member);
                userManagementWindow.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login Fail! Incorrect Email or Password!");
            }
        }
    }
}