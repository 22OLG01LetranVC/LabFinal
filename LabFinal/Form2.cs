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
    public partial class Form2: Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-5V7J3DL\SQLEXPRESS;Initial Catalog=pinturas_recubrimientos_db;Integrated Security=true");

        public Form2()
        {
            InitializeComponent();
        }

        private void obtenerregistros()
        {
            // La consulta a la tabla 'productos' se mantiene igual
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
                    // Se cambió la consulta para que coincida con la nueva estructura de la tabla 'productos'
                    SqlDataAdapter sda = new SqlDataAdapter(
                        "INSERT INTO productos(nombre_producto, tipo_producto, color, acabado) VALUES ('"
                        + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')",
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
                    // Se cambió la consulta para que coincida con la nueva estructura de la tabla 'productos'
                    SqlDataAdapter sda = new SqlDataAdapter(
                        "UPDATE productos SET nombre_producto='" + textBox2.Text + "', tipo_producto='" + textBox3.Text + "', color='" + textBox4.Text + "', acabado='" + textBox5.Text + "' WHERE id_producto='" + textBox8.Text + "'",
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
                    textBox6.Text = reader[0].ToString();
                    textBox2.Text = reader[1].ToString();
                    textBox3.Text = reader[2].ToString();
                    textBox4.Text = reader[3].ToString();
                    textBox5.Text = reader[4].ToString();
                    // Las siguientes líneas se comentan porque la nueva tabla no tiene las mismas columnas
                    // textBox1.Text = reader[5].ToString();
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
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Se cambió la cadena de conexión
            SqlConnection conexion = new SqlConnection(@"Data Source=DESKTOP-5V7J3DL\SQLEXPRESS;Initial Catalog=pinturas_recubrimientos_db;Integrated Security=true");
            conexion.Open();
            MessageBox.Show("Se abrio la conexion con el servidor SQL Server y se selecciono la base de datos");
            conexion.Close();
            MessageBox.Show("Se cerro la conexion.");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Reporte1 rm1 = new Reporte1();
            // rm1.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
        }
    }

}
