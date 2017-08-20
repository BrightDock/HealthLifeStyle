using System;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB
{
    public class Slide
    {
        Panel slide = new Panel() { CssClass= "slide" };
        Panel slide__bg = new Panel() { CssClass= "slide__bg" };
        Panel slide__content = new Panel() { CssClass= "slide__content" };
        Literal slide__overlay = new Literal() { Text= "<svg class=\"slide__overlay\" viewBox=\"0 0 720 405\" preserveAspectRatio=\"xMaxYMax slice\">" };
        Literal slide__overlay_path = new Literal() { Text= "<path class=\"slide__overlay-path\"  d=\"M0,0 300,0 600,405 0,405\" /></svg>" };
        Panel slide__text = new Panel() { CssClass= "slide__text" };
        Literal slide__text_heading = new Literal() { Text= "<h2 class=\"slide__text-heading\"></h2>" };
        Literal slide__text_desc = new Literal() { Text = "<p class=\"slide__text-desc\"></p>" };
        HyperLink slide__text_link = new HyperLink() { CssClass= "slide__text-link", Text = "Ссылка" };

        public Slide() { }

        public Slide(String Name, String Text, String img_link, int position, VirtualPathData Post_id, String text_background) {
            this.slide.CssClass = "slide slide-" + position.ToString();
            if (position.Equals(0)) {
                this.slide.CssClass += " active";
            }
            this.slide.Style.Add("left", (position * 100).ToString() + '%');
            this.slide__text_heading.Text = add_text_to_tag(this.slide__text_heading.Text, Name);
            this.slide__text_desc.Text = add_text_to_tag(this.slide__text_desc.Text, Text);
            this.slide__text_link.NavigateUrl = Post_id.VirtualPath;
            this.slide__bg.Style.Add("background-image", img_link.TrimEnd());
            this.slide__text.Style.Add("background-color", '#' + text_background);
            this.slide__bg.Style.Add("left", (position * (-50)).ToString() + '%');
            this.slide__overlay_path.Text = add_fill_style_to_literal(this.slide__overlay_path.Text, text_background);
        }

        public void Initialize(Panel slider) {;
            slider.Controls.Add(slide);
            this.slide.Controls.Add(slide__bg);
            this.slide.Controls.Add(slide__content);
            this.slide__content.Controls.Add(slide__overlay);
            this.slide__content.Controls.Add(slide__overlay_path);
            this.slide__content.Controls.Add(slide__text);
            this.slide__text.Controls.Add(slide__text_heading);
            this.slide__text.Controls.Add(slide__text_desc);
            this.slide__text.Controls.Add(slide__text_link);

        }

        private String add_text_to_tag(String past_into, String to_past) {
            String result = String.Empty;
            result = past_into.Split('>')[0] + '>' + to_past + past_into.Split('>')[1] + '>';

            return result;
        }

        private String add_fill_style_to_literal(String past_into, String color) {
            String result = String.Empty;
            if (color.Equals("0"))
                color = "000";
            result = String.Format("{0}style=\"fill:#{1};\"/></{2}", past_into.Split('/')[0], color, past_into.Split('/')[2]);

            return result;
        }

    }
}