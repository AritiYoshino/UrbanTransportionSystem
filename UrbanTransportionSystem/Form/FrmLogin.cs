using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace UrbanTransportionSystem
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string username = txtAccount.Text;
            string password = txtPassword.Text;
            string connectionString = "Data Source=LAPTOP-18GE95OU;Initial Catalog=AccountDB;User ID=wangwenzuo;Password=123456";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT TOP (1) [UserID], [UserName], [Password], [UserType] " +
                               "FROM [AccountDB].[dbo].[Users] " +
                               "WHERE [UserName] = @Username AND [Password] = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        string userType = reader["UserType"].ToString();
                        if (userType == "Admin")
                        {
                            // 打开管理员窗口，假设AdminForm是管理员窗口的Form类
                            FrmAdminMap adminForm = new FrmAdminMap();
                            adminForm.Show();
                            this.Hide();
                        }
                        else if (userType == "Admin")
                        {
                            // 打开普通用户窗口，假设UserForm是普通用户窗口的Form类
                           // UserForm userForm = new UserForm();
                            //userForm.Show();
                           // this.Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("用户名或密码错误！");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("登录出错：" + ex.Message);
                }
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

    