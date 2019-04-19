using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginFormUsingWindowsForms
{
    public partial class LoginForm : Form
    {
        private string connectionString;
        private SqlConnection connection;
        public LoginForm()
        {
            InitializeComponent();
            connectionString = @"Data Source=.\sqlexpress;Initial Catalog=LoginDatabase;Integrated Security=True;Pooling=False";
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            string login = loginTextBox.Text, password = passwordTextBox.Text;
            using (connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("Login", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Login", login);
                        command.Parameters.AddWithValue("@Password", password);
                        connection.Open();

                        int count = (int)command.ExecuteScalar();
                        if(count == 1)
                        {
                            this.Hide();
                            WelcomeForm welcomeForm = new WelcomeForm();
                            welcomeForm.Show();
                        }
                        else
                        {
                            MessageBox.Show("Login and/or Password is wrong.");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
    }
}
