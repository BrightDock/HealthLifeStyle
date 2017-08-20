using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.OleDb;


namespace Health_Project
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            load_day(); // подргружаем все точки графика из сети
            package_to_write = new Package(this);

        }

        // Переменная для записи данных в форму вспомогательного окна из главной
        private Package package_to_write;
        // Переменная для получения данных со вспомогательного окна
        private Package to_package_write;

        private DateTime last_saved_point;

        // Объявление переменном для передачи данных в главное окно, тип public чтобы можно было присвоить значение из другого класса (вспомогательного окна)
        public Package Package
        {
            get
            {
                return to_package_write;
            }

            set
            {
                to_package_write = value;
            }
        }

        // обработка нажатия кнопки добавления в таблицу вспомогательного окна
        private void Add_Click(object sender, EventArgs e)
        {
            // проверка на пустоту строк ввода
            if (!NameGood.Text.Equals(String.Empty) && !weight.Text.Equals(String.Empty))
            {
                NameGood.Focus();
                // открываем вспомогательное окно и переводим главное окно на вкладку графика
                package_to_write.Show();
                this.Select();
                grid_pos();

                Double grams;
                Double.TryParse(weight.Text, out grams);
                Package.Add_row_table(NameGood.Text, grams * Convert.ToDouble(NameGood.SelectedValue), grams); //вызов функции из вспомогательного окна для добавления строк в таблицу
                weight.Text = String.Empty;
            }
            else // действия в случае не ввода данных в нужные поля
            {
                weight.BackColor = Color.Red;
                DialogResult res = new DialogResult();
                res = MessageBox.Show("Неверно введены данные!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (res.Equals(DialogResult.OK))
                {
                    weight.BackColor = Color.DarkSeaGreen;
                    weight.Focus();
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSetHealsDB.Products_types". При необходимости она может быть перемещена или удалена.
            this.products_typesTableAdapter.Fill(this.dataSetHealsDB.Products_types);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSetHealsDB.Products". При необходимости она может быть перемещена или удалена.
            this.productsTableAdapter.Fill(this.dataSetHealsDB.Products);

            // установка временных диапазнов графика
            DateTime minDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime maxDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(+1);
            chart.ChartAreas[0].AxisX.Minimum = minDate.ToOADate();
            chart.ChartAreas[0].AxisX.Maximum = maxDate.ToOADate();

            // скрываем вспомогательное окно
            package_to_write.Hide();
            grid_pos();
        }
        
        // подгружаем все данные по текущему дню из базы в облаке
        void load_day() {
            String commandstr = "SELECT *\n FROM Chronologys";
            OleDbConnection Connection = new OleDbConnection(Properties.Settings.Default.ConnectionStringToHealsBD);
            DataTable DT = new DataTable();

            try
            {
                Connection.Open();
                OleDbCommand Command = new OleDbCommand(commandstr, Connection);
                OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
                Adapter.Fill(DT);

                foreach (DataRow DR in DT.Rows)
                {
                    if (Convert.ToDateTime(DR[2]).Date.Equals(DateTime.Now.Date))
                    {
                        chart.Series.ElementAt(0).Points.AddXY(Convert.ToDateTime(DR[2]).ToOADate(), Convert.ToDouble(DR[3]));

                        DateTime.TryParse(DR[2].ToString(), out last_saved_point);
//                        MessageBox.Show(Convert.ToDateTime(DR[1]).ToString() + '\n' +  Convert.ToDouble(DR[2]).ToString());
                    }
                }

                Notify.BackColor = Color.DarkSeaGreen;
                Notify.ForeColor = Color.White;
                Notify.Text = "Всё готово)";
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка БД!\n" + e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Notify.BackColor = Color.Red;
                Notify.ForeColor = Color.White;
                Notify.Text = "База не подключенна";
                MessageBox.Show("Проверьте ваше интернет соединение и повторите позже", "Выход", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            
            date.Text = "Сегодня: " + DateTime.Now.ToShortDateString();
            Notify.TextAlign = ContentAlignment.MiddleCenter;
            Loading.Visible = false;
            Notify.Width = TabControl.TabPages[Calc.Name].Width;
            Notify.Location = new Point(0, Notify.Location.Y + 20);
        }

        // двигаем вспомогательное окно, если главное было перемещено
        private void MainForm_Move(object sender, EventArgs e)
        {
            grid_pos();
        }

        void grid_pos() // функция вычисления положения главного окна и изменении положения вспомогательного
        {
            Point position_add_wind = new Point(this.Location.X + (this.Width + 10), this.Location.Y);
            if (package_to_write.Location.Y != this.Location.Y)
                package_to_write.Location = position_add_wind;

            if ((position_add_wind.X + package_to_write.Width) >= Screen.PrimaryScreen.WorkingArea.Width)
            {
                position_add_wind = new Point(this.Location.X - (package_to_write.Width + 10), this.Location.Y);
                package_to_write.Location = position_add_wind;
            }
            else if (package_to_write.Location.X == Screen.PrimaryScreen.Bounds.X)
            {
                position_add_wind = new Point(this.Location.X + (this.Width + 10), this.Location.Y);
                package_to_write.Location = position_add_wind;
            }

        }

        // функция отправки данных на сервер
        private void Confirm(List<Package_grid_row > data)
        {
            try
            {
                OleDbConnection conn = new OleDbConnection(Properties.Settings.Default.ConnectionStringToHealsBD);
                conn.Open();
                String com_str = "INSERT INTO Categories (Name, Date_Time, Kkal_1g) VALUES (?, ?, ?)";
                OleDbCommand Command = new OleDbCommand(com_str, conn);
                int i = 0;

                DataTable DT = new DataTable();
                DT.Columns.Add(new DataColumn());
                DT.Columns.Add(new DataColumn());
                DT.Columns.Add(new DataColumn());

                foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint point in chart.Series[0].Points)
                {
                    if (point.XValue > last_saved_point.ToOADate())
                    {
//                        MessageBox.Show(point.XValue.ToString() + '\n' + last_saved_point.ToOADate().ToString());
                        DT.Rows.Add(point.ToolTip, point.XValue, data.ElementAt(i).In_1g);
                        i++;
                    }
                }

//                MessageBox.Show(DT.Rows.Count.ToString());

                foreach (DataRow row in DT.Rows)
                {
                    MessageBox.Show(row[0].ToString() + '\n' + row[1].ToString() + '\n' + row[2].ToString());
                    Command.Parameters.Add("Name", OleDbType.LongVarChar).Value = row[0].ToString();
                    Command.Parameters.Add("Date_Time", OleDbType.DBDate).Value = Convert.ToDateTime(row[1]);
                    Command.Parameters.Add("Kkal_1g", OleDbType.Double).Value = Convert.ToDouble(row[2]);

                    Command.ExecuteNonQuery();
                }


                /*                    String cmdText = "INSERT INTO dbo.Chronologys(Date_Time, Number_Kkal_1g, Kkal_1g) VALUES (@Date_Time, @Number_Kkal_1g, @Kkal_1g)";
                                    SqlCommand cmd = new SqlCommand(cmdText, chronologysTableAdapter.Connection);

                                    chronologysTableAdapter.Adapter = (DateTime.Now, Convert.ToDouble(NameGood.SelectedValue) * Convert.ToDouble(weight.Text));
                                    chronologysTableAdapter.Connection*/
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка записи в БД!\n" + e.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            weight.Text = String.Empty;
            NameGood.Refresh();
            Package.Drop_table();
        }

        // функция перерисовки (добавления) данных на главном окне в графике ||||||||||| ИПОЛЬЗУЕТСЯ ТОЛЬКО ИЗ ВСПОМОГАТЕЛЬНОГО ОКНА
        public void Refresh_wind_add_point_graph(List<Package_grid_row> data)
        {
            Double Kkal = 0.0;
            String products = String.Empty;
            
            weight.Text = String.Empty;
            NameGood.Refresh();
            Package.Drop_table();

            foreach (Package_grid_row item in data)
            {
                Kkal += item.Kkal;
                products += item.Name.TrimEnd() + ": " + item.Kkal / item.In_1g + " гр (" + item.Kkal + " Ккал)\n";
            }

            add_point(Kkal, products);
            this.Select();
            TabControl.SelectTab(Graph.Name);
            Package.Visible = false;
            Confirm(data);
        }

        // функция добавления новых точек на графике
        private void add_point(Double kkal, String products)
        {
            System.Windows.Forms.DataVisualization.Charting.DataPoint point = new System.Windows.Forms.DataVisualization.Charting.DataPoint(DateTime.Now.ToOADate(), kkal);
            point.ToolTip = products;
            chart.Series.ElementAt(0).Points.Add(point);
        }

        private void MainForm_Enter(object sender, EventArgs e)
        {
            Package.Show();
        }

        private void MainForm_Leave(object sender, EventArgs e)
        {
            Package.Hide();
        }
    }
}
