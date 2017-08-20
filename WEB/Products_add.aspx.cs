using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB
{
    public partial class Products_add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = String.Format("Добавить продукты | {0}", Master.CompanyName);
            if (this.Context.User.Identity.IsAuthenticated && is_moderator_or_admin(this.Context.User.Identity.Name))
            {
                this.Form.DefaultButton = Apply.UniqueID;
            }
            else
            {
                not_autorized();
            }
        }

        private bool is_moderator_or_admin(String id) {
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            String command = "SELECT ID, Account_type_id FROM Users WHERE (ID = ?) AND ((Account_type_id = 1) OR (Account_type_id = 3))";
            OleDbCommand Command = new OleDbCommand(command, Connection);
            Command.Parameters.Add("ID", OleDbType.BigInt).Value = Int64.Parse(id);
            OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
            DataTable DT = new DataTable();

            Connection.Open();
            Adapter.Fill(DT);
            Connection.Close();

            if (DT.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void not_autorized()
        {
            Panel non_autorized = new Panel() { CssClass = "non_autrized_panel" };

            this.Content.Visible = false;

            this.Login.Controls.Add(non_autorized);
            non_autorized.Controls.Add(new Label() { Text = "Доступ закрыт, пожалуйста,", CssClass = "non_autorized_msg_parts" });
            non_autorized.Controls.Add(new HyperLink() { NavigateUrl = "~/Account/Login", Text = "Войдите", CssClass = "non_autorized_msg_parts" });
            non_autorized.Controls.Add(new Label() { Text = "или", CssClass = "non_autorized_msg_parts" });
            non_autorized.Controls.Add(new HyperLink() { NavigateUrl = "~/Account/Registration", Text = "Зарегистрируйтесь,", CssClass = "non_autorized_msg_parts" });
            non_autorized.Controls.Add(new Label() { Text = "чтобы просмотреть эту страницу", CssClass = "non_autorized_msg_parts" });

        }

        protected void Apply_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                updating.Visible = true;
                Apply.Visible = false;
                OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                String command = "INSERT INTO Products (Name, Colarific_value, Product_type_id, Fats, Carbohydrates, Proteins) VALUES (''+?+'', ?, ?, ?, ?, ?)";
                OleDbCommand Command = new OleDbCommand(command, Connection);

                Command.Parameters.Add("Name", OleDbType.WChar).Value = Name_product.Text;
                Command.Parameters.Add("Colarific_value", OleDbType.Double).Value = parse_to_double(KKal_product.Text) / 100.0;
                Command.Parameters.Add("Product_type_id", OleDbType.BigInt).Value = Int64.Parse(prod_type.SelectedValue);
                Command.Parameters.Add("Fats", OleDbType.Double).Value = parse_to_double(Fats_product.Text);
                Command.Parameters.Add("Carbohydrates", OleDbType.Double).Value = parse_to_double(Carbohydrats_product.Text);
                Command.Parameters.Add("Proteins", OleDbType.Double).Value = parse_to_double(Proteins_product.Text);

                try
                {
                    Connection.Open();
                    Command.ExecuteNonQuery();
                    Connection.Close();
                    status_label.CssClass = "good";
                    status_label.Text = "Добавлено";

                    Name_product.Text = "";
                    KKal_product.Text = "";
                    prod_type.SelectedValue = "1";
                    Fats_product.Text = "";
                    Carbohydrats_product.Text = "";
                    Proteins_product.Text = "";
                }
                catch (Exception ex)
                {
                    status_label.CssClass = "bad";
                    status_label.Text = "Ошибка<br />"/* + ex.Message*/;
                }
                finally {
                    updating.Visible = false;
                    Apply.Visible = true;
                }
            }
            else {
                status_label.CssClass = "bad";
                status_label.Text = "Некоторые поля введены не верно!";
            }
        }

        private double parse_to_double(String input)
        {
            Double result = 0.0;

//            status_label.Text += input.Replace(".", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator) + "<br />";
            Double.TryParse(input.Replace(".", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator),  out result);

            return result;
        }

        protected void isNameExist(object sender, EventArgs e) {
            OleDbConnection Connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            String command = "SELECT Name FROM Products WHERE (Name = ''+?+'')";
            OleDbCommand Command = new OleDbCommand(command, Connection);
            OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
            DataTable DT = new DataTable();

            Command.Parameters.Add("Name", OleDbType.WChar).Value = First_letter_to_upper(Name_product.Text);

            Name_product.BorderColor = new System.Drawing.Color();
            try
            {
                Connection.Open();
                Adapter.Fill(DT);
                Connection.Close();

                status_label.Text = DT.Rows.Count.ToString();

                if (DT.Rows.Count > 0)
                {
                    Name_product.BorderColor = System.Drawing.Color.Red;
                    status_label.CssClass = "bad";
                    status_label.Text = "Уже имеется";
                }
            }
            catch (Exception ex)
            {
                status_label.CssClass = "bad";
                status_label.Text = ex.Message;
            }
        }

        public static string First_letter_to_upper(string str)
        {
            if (str.Length > 1) { return Char.ToUpper(str[0]) + str.Substring(1); }
            return "";
        }
    }
}