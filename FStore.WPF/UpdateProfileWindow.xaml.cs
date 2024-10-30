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
    /// Interaction logic for UpdateProfileWindow.xaml
    /// </summary>
    public partial class UpdateProfileWindow : Window
    {
        private Member _currentUser;
        private IMemberRepository _memberRepository;

        public UpdateProfileWindow(Member currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _memberRepository = new MemberRepository();

            txtName.Text = _currentUser.CompanyName;
            txtEmail.Text = _currentUser.Email;
            txtPassword.Password = _currentUser.Password;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _currentUser.CompanyName = txtName.Text;
            _currentUser.Email = txtEmail.Text;
            _currentUser.Password = txtPassword.Password;

            _memberRepository.UpdateMember(_currentUser);
            MessageBox.Show("Profile updated successfully!");
            this.Close();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
