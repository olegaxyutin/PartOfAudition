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

namespace ProgramFORaudition
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;

        public Form1()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\olega\source\repos\ProgramFORaudition\ProgramFORaudition\Database_Critical.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [TableCritical],[PartialTable] WHERE PartialTable.Id_k = TableCritical.Id ORDER BY Id_k", sqlConnection);
            try {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["Id"]));
                    listBox2.Items.Add(Convert.ToString(sqlReader["Level"]));
                    listBox3.Items.Add(Convert.ToString(sqlReader["Name"]));
                    listBox4.Items.Add(Convert.ToString(sqlReader["URL"]));
                    listBox5.Items.Add(Convert.ToString(sqlReader["Id_ch"]));
                    listBox6.Items.Add(Convert.ToString(sqlReader["Name_ch"]));
                    listBox7.Items.Add(Convert.ToString(sqlReader["URL_ch"]));
                    listBox8.Items.Add(Convert.ToString(sqlReader["Id_k"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }


        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (label25.Visible)
                label25.Visible = false;

            if (checkedListBox1.CheckedItems.Count == 1)
            {
                SqlCommand command = new SqlCommand("INSERT INTO [TableCritical] (Level, Name, URL)VALUES(@Level, @Name, @URL)", sqlConnection);
                command.Parameters.AddWithValue("Level", checkedListBox1.SelectedItem);
                command.Parameters.AddWithValue("Name", textBox2.Text);
                command.Parameters.AddWithValue("URL", textBox1.Text);
                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label25.Visible = true;
                label25.Text = "Укажите ТОЛЬКО один уровень оценки!";
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private async void обновитьТребованияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [TableCritical]", sqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["Id"]));
                    listBox2.Items.Add(Convert.ToString(sqlReader["Level"]));
                    listBox3.Items.Add(Convert.ToString(sqlReader["Name"]));
                    listBox4.Items.Add(Convert.ToString(sqlReader["URL"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (label26.Visible)
                label26.Visible = false;

            if (checkedListBox2.CheckedItems.Count == 1)
                //&& !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text)
            {
                SqlCommand command = new SqlCommand("UPDATE [TableCritical] SET [Level]=@Level,[Name]=@Name,[URL]=@URL WHERE [Id]=@Id", sqlConnection);
                command.Parameters.AddWithValue("Id", textBox5.Text);
                command.Parameters.AddWithValue("Level", checkedListBox2.SelectedItem);
                command.Parameters.AddWithValue("Name", textBox3.Text);
                command.Parameters.AddWithValue("URL", textBox4.Text);

                await command.ExecuteNonQueryAsync();

            }
            else if (!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                label26.Visible = true;
                label26.Text = "Укажите Id!";
            }
            else
            {
                label26.Visible = true;
                label26.Text = "Укажите ТОЛЬКО один уровень оценки!";
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (label27.Visible)
                label27.Visible = false;

            if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [TableCritical] WHERE [Id]=@id", sqlConnection);
                command.Parameters.AddWithValue("Id", textBox6.Text);
                await command.ExecuteNonQueryAsync();
            }
            else
            { 
                label27.Visible = true;
                label27.Text = "Укажите Id!";
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (label33.Visible)
                label33.Visible = false;

            if (!string.IsNullOrEmpty(textBox14.Text) && !string.IsNullOrWhiteSpace(textBox14.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [PartialTable] (Name_ch, URL_ch, Id_k)VALUES(@Name_ch, @URL_ch, @Id_k)", sqlConnection);
                command.Parameters.AddWithValue("Name_ch", textBox7.Text);
                command.Parameters.AddWithValue("URL_ch", textBox8.Text);
                command.Parameters.AddWithValue("Id_k", textBox14.Text);
                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label33.Visible = true;
                label33.Text = "Укажите ссылку на требование (ID Требования)";
            }
        }

        private async void обновитьЧастныеПоказателиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox5.Items.Clear();
            listBox6.Items.Clear();
            listBox7.Items.Clear();
            listBox8.Items.Clear();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [PartialTable]", sqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox5.Items.Add(Convert.ToString(sqlReader["Id_ch"]));
                    listBox6.Items.Add(Convert.ToString(sqlReader["Name_ch"]));
                    listBox7.Items.Add(Convert.ToString(sqlReader["URL_ch"]));
                    listBox8.Items.Add(Convert.ToString(sqlReader["Id_k"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            if (label34.Visible)
                label34.Visible = false;

            if (!string.IsNullOrEmpty(textBox11.Text) && !string.IsNullOrWhiteSpace(textBox11.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [PartialTable] SET [Name_ch]=@Name_ch,[URL_ch]=@URL_ch,[Id_k]=@Id_k WHERE [Id_ch]=@Id_ch", sqlConnection);
                command.Parameters.AddWithValue("Id_ch", textBox11.Text);
                command.Parameters.AddWithValue("Name_ch", textBox9.Text);
                command.Parameters.AddWithValue("URL_ch", textBox10.Text);
                command.Parameters.AddWithValue("Id_k", textBox15.Text);

                await command.ExecuteNonQueryAsync();

            }
            else if (!string.IsNullOrEmpty(textBox15.Text) && !string.IsNullOrWhiteSpace(textBox15.Text))
            {
                label34.Visible = true;
                label34.Text = "Укажите ссылку на критерий (ID требования)!";
            }
            else 
            {
                label34.Visible = true;
                label34.Text = "Укажите ID частного показателя!";
            }
            
        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private async void button6_Click(object sender, EventArgs e)
        {
            if (label36.Visible)
                label36.Visible = false;

            if (!string.IsNullOrEmpty(textBox12.Text) && !string.IsNullOrWhiteSpace(textBox12.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [PartialTable] WHERE [Id_ch]=@id_ch", sqlConnection);
                command.Parameters.AddWithValue("Id_ch", textBox12.Text);
                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label36.Visible = true;
                label36.Text = "Укажите Id!";
            }
        }

        private void начатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }
    }
    }

