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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LabFinal
{
    public partial class Form2 : Form
    {
        // Change the connection string to use the correct server instance and database.
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-5V7J3DL\SQLEXPRESS;Initial Catalog=pinturas_recubrimientos_db;Integrated Security=true");

        public Form2()
        {
            InitializeComponent();
        }

        private void obtenerregistros()
        {
            // Change the connection string to use the correct server instance and database.
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM productos", "server=DESKTOP-5V7J3DL\\SQLEXPRESS; database = pinturas_recubrimientos_db; Integrated Security=true");
            DataSet ds = new DataSet();
            da.Fill(ds, "nombre");
            dataGridView1.DataSource = ds.Tables["nombre"].DefaultView;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            obtenerregistros();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                {
                    MessageBox.Show("Llena las celdas primero");
                }
                else
                {
                    conn.Open();
                    // Corrected INSERT statement to match the database schema.
                    SqlDataAdapter sda = new SqlDataAdapter(
                        "INSERT INTO productos(nombre_producto, tipo_producto, precio, stock) VALUES ('"
                        + textBox2.Text + "','" + textBox3.Text + "'," + textBox4.Text + "," + textBox5.Text + ")",
                        conn);
                    sda.SelectCommand.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Datos ingresados correctamente.");
                    obtenerregistros();
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Error de SQL verificar:" + ex.ToString());
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox1.Text == "")
                {
                    MessageBox.Show("Llena las celdas primero");
                }
                else
                {
                    conn.Open();
                    // Corrected UPDATE statement to match the database schema.
                    SqlDataAdapter sda = new SqlDataAdapter(
                        "UPDATE productos SET nombre_producto='" + textBox2.Text + "', tipo_producto='" + textBox3.Text + "', precio=" + textBox4.Text + ", stock=" + textBox5.Text + " WHERE id_producto='" + textBox8.Text + "'",
                        conn);
                    sda.SelectCommand.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Registro modificado correctamente.");
                    obtenerregistros();
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Error de SQL verificar: " + ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM productos WHERE id_producto=" + textBox6.Text;
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();

            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Assign values to the correct text boxes for display and update.
                    textBox8.Text = reader[0].ToString(); // Assuming textBox8 is used for the ID to update.
                    textBox2.Text = reader[1].ToString();
                    textBox3.Text = reader[2].ToString();
                    textBox4.Text = reader[3].ToString();
                    textBox5.Text = reader[4].ToString();
                    textBox1.Text = reader[5].ToString();
                }
                else
                    MessageBox.Show("Ningun registro encontrado con el id ingresado !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
            finally
            {
                //Cierro la conexion
                conn.Close();
            }
            textBox6.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sql = "DELETE FROM productos WHERE id_producto=" + textBox7.Text;
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    MessageBox.Show("Registro eliminado correctamente !");
                obtenerregistros();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
            finally
            {
                // cierro la conexion
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            SqlConnection conexion = new SqlConnection(@"Data Source=DESKTOP-5V7J3DL\SQLEXPRESS;Initial Catalog=pinturas_recubrimientos_db;Integrated Security=true");
            conexion.Open();
            MessageBox.Show("Se abrio la conexion con el servidor SQL Server y se selecciono la base de datos");
            conexion.Close();
            MessageBox.Show("Se cerro la conexion.");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Reporte1 rm1 = new Reporte1();
            rm1.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
        }
    }
}
