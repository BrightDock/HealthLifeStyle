using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Health_Project
{
    public partial class Package : Form
    {
        private MainForm m_parent;
        
        public Package(MainForm MainF)
        {            
            InitializeComponent();
            m_parent = MainF;

            DataTable DT = new DataTable();
            BindingSource BS = new BindingSource();
            
            
//            Products.DataSource = DT;
            BS.DataSource = Products.DataSource;
            bindingNavigator.BindingSource = BS;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void Package_Load(object sender, EventArgs e)
        {
            m_parent.Package = this;
        }

        public void Add_row_table(String name, Double Kkal, Double grams)
        {
            Products.Rows.Add(name, Kkal, grams);
        }

        public void Drop_table()
        {
            Products.Rows.Clear();
        }

        private void Confirm_table_Click(object sender, EventArgs e)
        {
            List<Package_grid_row> Data = new List<Package_grid_row>();

            foreach (DataGridViewRow row in Products.Rows)
            {
                Data.Add(new Package_grid_row(row.Cells[Prod_name.Name].Value.ToString(), Convert.ToDouble(row.Cells[Prod_Kkal.Name].Value), Convert.ToDouble(row.Cells[Prod_Kkal.Name].Value)/Convert.ToDouble(row.Cells[Prod_grams.Name].Value)));
            }
            m_parent.Refresh_wind_add_point_graph(Data);
        }

        private void Products_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            calc_sumKkal_table();
        }

        private void Products_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            calc_sumKkal_table();
        }

        private Double calc_sumKkal_table()
        {
            Double sum_kkal = 0.0;
            foreach (DataGridViewRow row in Products.Rows)
            {
                sum_kkal += Convert.ToDouble(row.Cells[Prod_Kkal.Name].Value);
            }
            Sum_Kkal_Lable.Text = Sum_Kkal_Lable.Text.Split(':')[0] + ": " + sum_kkal.ToString() + " Ккал";

            return sum_kkal;
        }
    }

}
