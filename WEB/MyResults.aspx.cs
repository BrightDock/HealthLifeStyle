using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Globalization;
using System.Collections.ObjectModel;

namespace WEB
{
    public partial class Logon : System.Web.UI.Page
    {
        private DateTime last_saved_point;
        private int last_Dose = 1;
        private Main_WindowView_Model Main_grid_rows { get; set; } = new Main_WindowView_Model();
        private Exception notify_status_e = null;
        //        private List<Main_Grid> main_Grid_rows = new List<Main_Grid>();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = String.Format("Мои результаты");
            if (this.Context.User.Identity.IsAuthenticated)
            {
                load_day();
            }
            else
            {
                not_autorized();
            }
        }

        private String get_id()
        {
            String result = String.Empty;

            if (this.Context.User.Identity.IsAuthenticated/* && (this.Page.RouteData.Values["id"].ToString().Equals(null))*/)
            {
                result = this.Context.User.Identity.Name;
                //                this.Label.Text += this.Page.RouteData.Values["id"].ToString() + "<br />" + this.Context.User.Identity.Name;
            }
            else if (this.Context.User.Identity.IsAuthenticated)
            {
                //                result = this.Page.RouteData.Values["id"].ToString();
                //                this.Label.Text += this.Page.RouteData.Values["id"].ToString() + "<br />" + this.Context.User.Identity.Name;
            }

            return result;
        }

        void loadMyWeight()
        {
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            String commandstr = "SELECT ID, Date, Weight, User_ID FROM User_weight_chronology WHERE(User_weight_chronology.User_ID = ?)";

            try
            {
                OleDbCommand Command = new OleDbCommand(commandstr, Connection);
                Command.Parameters.Add("User_ID", OleDbType.BigInt).Value = get_id();
                OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
                DataTable DT = new DataTable();
                Connection.Open();
                Adapter.Fill(DT);
                Connection.Close();
                List<float> DaysWeight = new List<float>();
                List<DateTime> Days = new List<DateTime>();

                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow row in DT.Rows)
                    {
                        DaysWeight.Add(float.Parse(row["Weight"].ToString()));
                        Days.Add(DateTime.Parse(row["Date"].ToString()));
                    }
                }

                Page.ClientScript.RegisterClientScriptBlock(GetType(), "dates", string.Format("var dates = new Array('{0}');",  String.Join("','", Days)), true);
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "weightData", string.Format("var weightData = new Array({0});", String.Join("|", DaysWeight).Replace(',', '.').Replace('|', ',')), true);
                //                Data_panel.ContentTemplateContainer.Controls.Add(new Literal() { Text = string.Format("var weightData = series :[{{name: myDayWeight, data[{0}]}}]", String.Join("|", DaysWeight).Replace(',', '.').Replace('|', ',')) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void load_day()
        {
            DateTimeFormatInfo dtfi = new CultureInfo("ru-RU").DateTimeFormat;
            date_results.InnerText = date_results.InnerText.Split(' ')[0] + ' ' + date_results.InnerText.Split(' ')[1] + " на " + DateTime.Now.Day + ' ' + dtfi.GetMonthName(DateTime.Now.Month).Substring(0, dtfi.GetMonthName(DateTime.Now.Month).Length - 1).ToLower() + 'я';
            try
            {
                loadMyWeight();

                String commandstr = String.Format("SELECT Chronologys.ID, Chronologys.User_id, Chronologys.Date, Products.Name, Products.Colarific_value, " +
                    "Chronologys.Grams, Chronologys.Dose FROM Chronologys INNER JOIN Products ON Chronologys.Product_id = Products.ID WHERE (User_id = {0})", get_id());
                OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                DataTable DT = new DataTable();
                System.Web.UI.DataVisualization.Charting.DataPoint point = new System.Web.UI.DataVisualization.Charting.DataPoint();

                Connection.Open();
                OleDbCommand Command = new OleDbCommand(commandstr, Connection);
                OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
                Adapter.Fill(DT);
                Connection.Close();


                // установка временных диапазнов графика
                DateTime minDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                DateTime maxDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(+1);
                
                foreach (DataRow DR in DT.Rows)
                {
                    //                    Label.Text += Convert.ToDateTime(DR[2]).ToString() + ' ' + DateTime.Now.ToString() + "<br />";
                    if (Convert.ToDateTime(DR[2]).AddHours(2).Date.Equals(DateTime.Now.Date))
                    {
                        if (!last_Dose.Equals(Convert.ToInt32(DR["Dose"])))
                        {
                            
                        }
                        
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void not_autorized()
        {
            Panel non_autorized = new Panel() { CssClass = "non_autrized_panel" };

            date_results.Visible = false;

            this.Content_Panel.Controls.Add(non_autorized);
            non_autorized.Controls.Add(new Label() { Text = "Пожалуйста,", CssClass = "non_autorized_msg_parts" });
            non_autorized.Controls.Add(new HyperLink() { NavigateUrl = "~/Account/Login", Text = "Войдите", CssClass = "non_autorized_msg_parts" });
            non_autorized.Controls.Add(new Label() { Text = "или", CssClass = "non_autorized_msg_parts" });
            non_autorized.Controls.Add(new HyperLink() { NavigateUrl = "~/Account/Registration", Text = "Зарегистрируйтесь,", CssClass = "non_autorized_msg_parts" });
            non_autorized.Controls.Add(new Label() { Text = "чтобы просмотреть эту страницу", CssClass = "non_autorized_msg_parts" });

        }


        public DataTable GetTableWithInitialData()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Название", typeof(string));
            table.Columns.Add("Кол- во Ккал", typeof(string));
            table.Columns.Add("Граммы", typeof(string));


            return table;
        }
/*
        private void Add(object sender, EventArgs e)
        {
            if (!productsComboBox.Text.Equals(String.Empty) && !Grams.Text.Equals(String.Empty))
            {
                int grams;
                int.TryParse(Grams.Text, out grams);

                if (grams > 0.0)
                {
                    Main_grid_rows.Main_Grid.Add(new Main_Grid(productsComboBox.Text, grams * Convert.ToDouble(productsComboBox.SelectedValue), grams));
                    Grams.Text = String.Empty;


                    DataTable dt = GetTableWithInitialData();
                    DataRow dr;

                    foreach (GridViewRow gvr in this.Grid.Rows)
                    {
                        dr = dt.NewRow();
                        
                        dr[0] = productsComboBox.Text;
                        dr[1] = grams * Convert.ToDouble(productsComboBox.SelectedValue);
                        dr[2] = grams;
                        dt.Rows.Add(dr);

                        GridView.DataBind();


                    }

                    dr = dt.NewRow();
                    dt.Rows.Add(dr);

                    GridView.DataSource = dt;
                    GridView.DataBind();

                    GridView.UpdateEdit();


                }
                else // действия в случае не ввода данных в нужные поля
                {
                                        Grams.Background = new SolidColorBrush(Colors.Red);
                                        MessageBoxResult res = new MessageBoxResult();
                                        if (Grams.Text.Equals(String.Empty))
                                            res = MessageBox.Show(string.Format("Строка \"{0}\" не может быть пустой!", label1.Content), "Информация", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                                        else
                                            res = MessageBox.Show("Неверно введены данные!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                                        if (res.Equals(MessageBoxResult.OK))
                                        {
                                            Grams.Background = new SolidColorBrush(Colors.DarkSeaGreen);
                                            Grams.Text = String.Empty;
                                            Grams.Focus();
                                        }
                }
            }
            else // действия в случае не ввода данных в нужные поля
            {
                                Grams.Background = new SolidColorBrush(Colors.Red);
                                MessageBoxResult res = new MessageBoxResult();
                                if (Grams.Text.Equals(String.Empty))
                                    res = MessageBox.Show(string.Format("Строка \"{0}\" не может быть пустой!", label1.Content), "Информация", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                                else
                                    res = MessageBox.Show("Неверно введены данные!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                                if (res.Equals(MessageBoxResult.OK))
                                {
                                    Grams.Background = new SolidColorBrush(Colors.DarkSeaGreen);
                                    Grams.Text = String.Empty;
                                    Grams.Focus();
            }
        }*/
    }

    public class Main_Grid
    {
        public Main_Grid(string name, double kkal, int grams)
        {
            this.Name = name;
            this.Kkal = kkal;
            this.Grams = grams;
        }

        public Main_Grid()
        {

        }

        public string Name { get; set; }
        public double Kkal { get; set; }
        public int Grams { get; set; }
    }

    class Main_WindowView_Model
    {
        private ObservableCollection<Main_Grid> Main_Grid_elem = new ObservableCollection<Main_Grid>();

        public ObservableCollection<Main_Grid> Main_Grid
        {
            get { return Main_Grid_elem; }
        }

        public Main_WindowView_Model()
        {

        }
    }
}
