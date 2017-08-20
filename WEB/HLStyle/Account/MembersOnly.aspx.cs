using System;
using System.Collections.Generic;

namespace WEB
{
    public partial class MembersOnly : System.Web.UI.Page
    {
        private Double slid_val { get; set; }
        Double x { get; set; }
        Double End { get; set; }
        Double delta { get; set; }
        Double a { get; set; }
        Double b { get; set; }
        private List<Double> mass_y { get; set; } = new List<Double>();
        private List<Double> mass_x { get; set; } = new List<Double>();

        protected void Page_Load(object sender, EventArgs e)
        {
            update_values();
        }

        private void update_values()
        {
            Double val = 0.004;
            Double.TryParse(txtSlider.Text.Replace('.', ','), out val);
            this.slid_val = val;
            x = Double.Parse(start.Text);
            End = Double.Parse(end.Text);
            delta = this.slid_val;
            a = Double.Parse(A.Text);
            b = Double.Parse(B.Text);
        }

        public void loading_ends()
        {
            wait.CssClass = "";
        }

        public int get_number_of_steps() {
            int result = 0;

            result = (int)((Double.Parse(end.Text) - Double.Parse(start.Text)) / this.slid_val);

            return result;
        }

        protected void Calcing(object sender, EventArgs e)
        {
            update_values();
            if (slid_val > 0.0)
            {
                try
                {
                    Int32 i = 0;
                    while (x <= End)
                    {
                        if (x > 0.7 && x < 1.4)
                        {
                            this.mass_y.Add(a * Math.Pow(x, 2) * Math.Log(x));
                        }
                        else if (x <= 0.7)
                        {
                            this.mass_y.Add(1.0);
                        }
                        else if (x > 1.4)
                        {
                            this.mass_y.Add(Math.Pow(Math.E, a * x) * Math.Cos(b * x));
                        }
                        this.mass_x.Add(x);
                        //                    J_val.Text += j.ToString() + "<br />";
                        x += delta;// Central_block_central_col_txtSliderValue
                                   //                    J_val.Text = txtSliderValue;
                    }
                    Result.Text = String.Empty;
                    if (this.mass_y.Count < 0)
                    {
                        Result.Text = "Нет элементов для вывода!";
                    }
                    else
                    {
                        i = 0;
                        String result_str = String.Empty;
                        foreach (Double item in this.mass_y)
                        {
                            result_str += "<p>Элемент № " + (i+1) + "<br /><samp>" + item.ToString() + "</samp></p>";
                            if (((i + 1) % 4) == 0 && i != 0)
                            {
                                result_str += "<br /><hr />";
                            }
                            i++;
                        }
                        Result.Text = result_str;
                        loading_ends();
                    }
                }
                catch (Exception ex)
                {
                    Result.Text = ex.Message + ex.Data + "<br />" + ex.StackTrace;
                }
            }
        }

        protected void txtSlider_TextChanged(object sender, EventArgs e)
        {
            Double val = 0.0;
            Double.TryParse(((System.Web.UI.WebControls.TextBox)sender).Text.Replace('.', ','), out val);
            this.slid_val = val;

            Calcing(new object(), new EventArgs());
        }

        protected void value_in_point(object sender, EventArgs e)
        {
            try
            {
                int val = 0;
                String result = String.Empty;
                int.TryParse(((System.Web.UI.HtmlControls.HtmlButton)sender).InnerHtml.Split(' ')[0], out val);

                this.mass_y = new List<Double>();
                this.mass_x = new List<Double>();
                x = 0.0;
                End = 3.0;
                delta = 0.004;
                a = -0.5;
                b = 2.0;

                while (x <= End)
                {
                    if (x > 0.7 && x < 1.4)
                    {
                        this.mass_y.Add(a * Math.Pow(x, 2) * Math.Log(x));
                    }
                    else if (x <= 0.7)
                    {
                        this.mass_y.Add(1.0);
                    }
                    else if (x > 1.4)
                    {
                        this.mass_y.Add(Math.Pow(Math.E, a * x) * Math.Cos(b * x));
                    }
                    this.mass_x.Add(x);
                    x += delta;
                }
                
                result += "<p>Элемент № " + val.ToString() + "<br /><samp>" + this.mass_y[val - 1].ToString() + "</samp></p>";
                Result.Text = result;
                loading_ends();
            }
            catch (Exception ex) {
                Result.Text = ex.Message;
            }
        }
    }
}