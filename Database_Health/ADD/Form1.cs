using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADD
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void wEB_PagesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.wEB_PagesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.bD_healthDataSet);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bD_healthDataSet.Chronologys". При необходимости она может быть перемещена или удалена.
            this.chronologysTableAdapter.Fill(this.bD_healthDataSet.Chronologys);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bD_healthDataSet.Products_types". При необходимости она может быть перемещена или удалена.
            this.products_typesTableAdapter.Fill(this.bD_healthDataSet.Products_types);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bD_healthDataSet.Products". При необходимости она может быть перемещена или удалена.
            this.productsTableAdapter.Fill(this.bD_healthDataSet.Products);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bD_healthDataSet.Account_types". При необходимости она может быть перемещена или удалена.
            this.account_typesTableAdapter.Fill(this.bD_healthDataSet.Account_types);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bD_healthDataSet.User_weight_chronology". При необходимости она может быть перемещена или удалена.
            this.user_weight_chronologyTableAdapter.Fill(this.bD_healthDataSet.User_weight_chronology);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bD_healthDataSet.WEB_Pages_topics". При необходимости она может быть перемещена или удалена.
            this.wEB_Pages_topicsTableAdapter.Fill(this.bD_healthDataSet.WEB_Pages_topics);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bD_healthDataSet.Users". При необходимости она может быть перемещена или удалена.
            this.usersTableAdapter.Fill(this.bD_healthDataSet.Users);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "bD_healthDataSet.WEB_Pages". При необходимости она может быть перемещена или удалена.
            this.wEB_PagesTableAdapter.Fill(this.bD_healthDataSet.WEB_Pages);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainForm.ActiveForm.Text = "Add " + tabControl1.SelectedTab.Text;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            wEB_PagesBindingNavigatorSaveItem_Click(sender, e);
            toolStripButton7_Click(sender, e);
            toolStripButton14_Click(sender, e);
            toolStripButton21_Click(sender, e);
            toolStripButton28_Click(sender, e);
            toolStripButton35_Click(sender, e);
            toolStripButton42_Click(sender, e);
            toolStripButton49_Click(sender, e);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.wEB_Pages_topicsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.bD_healthDataSet);
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.usersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.bD_healthDataSet);
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.user_weight_chronologyBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.bD_healthDataSet);
        }

        private void toolStripButton28_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.account_typesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.bD_healthDataSet);
        }

        private void toolStripButton35_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.productsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.bD_healthDataSet);
        }

        private void toolStripButton42_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.products_typesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.bD_healthDataSet);
        }

        private void toolStripButton49_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.chronologysBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.bD_healthDataSet);
        }
    }
}
