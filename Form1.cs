using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.IO;


namespace DemoSaveDataInLocalDBUsingQuery
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            BindGrid();
        }

        /// <summary>
        /// Save record directly in local database table using query.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCeConnection con = new SqlCeConnection("Data Source="
                    + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "MyDB.sdf"));
                con.Open();
                SqlCeCommand cmd = con.CreateCommand();
                cmd.CommandText = "insert into MyDemoTable(name,Age,Qualification) values(@name,@Age,@Qualification)";
                cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@Age", txtAge.Text.Trim());
                cmd.Parameters.AddWithValue("@Qualification", txtQualification.Text.Trim());
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
                BindGrid();
                Reset();
                MessageBox.Show("Record Save Successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Update exsting record using query.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void brnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCeConnection con = new SqlCeConnection("Data Source="
                   + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "MyDB.sdf"));
                con.Open();
                SqlCeCommand cmd = con.CreateCommand();
                cmd.CommandText = "update MyDemoTable set name=@name,Age=@Age,Qualification=@Qualification where id=@id";
                cmd.Parameters.AddWithValue("@id", lblId.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@Age", txtAge.Text.Trim());
                cmd.Parameters.AddWithValue("@Qualification", txtQualification.Text.Trim());
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
                BindGrid();
                Reset();
                MessageBox.Show("Record Updated Successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Delete record from table using query.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCeConnection con = new SqlCeConnection("Data Source="
                    + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "MyDB.sdf"));
                con.Open();
                SqlCeCommand cmd = con.CreateCommand();
                cmd.CommandText = "delete  MyDemoTable where id=@id";
                cmd.Parameters.AddWithValue("@id", lblId.Text);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
                BindGrid();
                Reset();
                MessageBox.Show("Record Removed Successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void dgvOldData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvOldData.Rows.Count > 0 && e.RowIndex != -1)
            {
                if (dgvOldData.Rows[e.RowIndex].Cells[0].Selected)
                {
                    txtName.Text = dgvOldData.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtAge.Text = dgvOldData.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtQualification.Text = dgvOldData.Rows[e.RowIndex].Cells[3].Value.ToString();
                    lblId.Text = dgvOldData.Rows[e.RowIndex].Cells[4].Value.ToString();

                    btnDelete.Enabled = true;
                    brnUpdate.Enabled = true;
                    btnAdd.Enabled = false;

                }
            }
        }

        /// <summary>
        /// bind grid with all data.
        /// </summary>
        private void BindGrid()
        {
            DataSet _ds = new DataSet();
            SqlCeConnection con = new SqlCeConnection("Data Source="
               + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "MyDB.sdf"));
            SqlCeDataAdapter sda = new SqlCeDataAdapter("select * from MyDemoTable", con);
            sda.Fill(_ds);

            if (_ds.Tables.Count > 0)
            {
                dgvOldData.DataSource = _ds.Tables[0];
            }
        }
        /// <summary>
        /// Reset All control of Form.
        /// </summary>
        private void Reset()
        {
            txtName.Text = string.Empty;
            txtAge.Text = string.Empty;
            txtQualification.Text = string.Empty;
            btnDelete.Enabled = false;
            brnUpdate.Enabled = false;
            btnAdd.Enabled = true;
        }

      
    }
}
