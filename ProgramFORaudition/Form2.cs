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
using System.IO;

namespace ProgramFORaudition
{
    public partial class Form2 : Form
    {
        SqlConnection sqlConnection;
        public Form2()
        {
            InitializeComponent();
        }
        //public List<TextBox> arrayTB = new List<TextBox>();
        //public float sum;
        public async void Form2_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\olega\source\repos\ProgramFORaudition\ProgramFORaudition\Database_Critical.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [TableCritical],[PartialTable] WHERE PartialTable.Id_k = TableCritical.Id ORDER BY Id_k", sqlConnection);
            try
            {
                int i = 0, x = 15, height = 20;
                //List<TextBox> arrayTB = new List<TextBox>();
                float sum = 0;
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    Label newLabel = new Label();
                    Label newLabel1 = new Label();
                    TextBox newTextBox = new TextBox();
                    newLabel.Name = "label" + i.ToString();
                    newLabel.Name = "label_number" + i.ToString();
                    newTextBox.Name = "textBox" + i.ToString();
                    newLabel.Text = (i + 1).ToString() + "." + Convert.ToString(sqlReader["Name_ch"]);
                    newLabel1.Text = (i + 1).ToString() + ".";
                    newLabel.Parent = panel1;
                    newTextBox.Parent = panel2;
                    newTextBox.Text = Convert.ToString(0);
                    newLabel1.Parent = panel2;
                    newLabel.Visible = true;
                    newLabel.AutoSize = true;
                    newLabel1.Visible = true;
                    newLabel.Location = new Point(x, height);
                    newLabel1.Location = new Point(x - 15, height);
                    newTextBox.Visible = true;
                    newTextBox.Location = new Point(x + 10, height);
                    newTextBox.Size = new Size(30, 17);
                    //arrayTB.Add(newTextBox);
                    //float Z = float.Parse(newTextBox.Text);
                    //float Z = Convert.ToSingle(newTextBox.Text);
                    //sum += Z;
                    //label200.Text = Convert.ToString(sum);
                    height += 20;
                    height += newLabel.Height;
                    i++;
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

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }
        private void сохранитьСеансToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            float sum = 0;
            int i = 0;
            foreach (Control value in panel2.Controls)
            {
                TextBox valuer = value as TextBox;
                if (valuer != null)
                {
                    valuer.Name = "textBox" + i.ToString();
                    valuer.Text = Convert.ToString(valuer.Text);
                    float Z = Convert.ToSingle(value.Text);
                    sum += Z;
                    label300.Text = Convert.ToString(sum/i);
                    i++;
                }
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}

