using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace App_Health
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Main_WindowView_Model Main_grid_rows = new Main_WindowView_Model();
        //        private List<Main_Grid> main_Grid_rows = new List<Main_Grid>();
        private List<DevExpress.Xpf.Charts.SeriesPoint> chart_points = new List<DevExpress.Xpf.Charts.SeriesPoint>();
        private Exception notify_status_e = null;
        private DateTime last_saved_point;
        private int last_Dose = 1;
        /*
                public List<Main_Grid> Main_Grid_rows
                {
                    get
                    {
                        return main_Grid_rows;
                    }

                    set
                    {
                        main_Grid_rows = value;
                    }
                }
        */
        public MainWindow()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                App_Health.BD_healthDataSet bD_healthDataSet = ((App_Health.BD_healthDataSet)(this.FindResource("bD_healthDataSet")));
                // Загрузить данные в таблицу Products. Можно изменить этот код как требуется.
                App_Health.BD_healthDataSetTableAdapters.ProductsTableAdapter bD_healthDataSetProductsTableAdapter = new App_Health.BD_healthDataSetTableAdapters.ProductsTableAdapter();
                bD_healthDataSetProductsTableAdapter.Fill(bD_healthDataSet.Products);
                System.Windows.Data.CollectionViewSource productsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("productsViewSource")));
                productsViewSource.View.MoveCurrentToFirst();

                Main_grid_rows = (Main_WindowView_Model)this.DataContext;

                load_day();
                notify_status();
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message);
            }
        }

        void load_day()
        {
            String commandstr = "SELECT Chronologys.ID, Chronologys.User_id, Chronologys.Date, Products.Name, Products.Colarific_value, Chronologys.Grams, Chronologys.Dose FROM Chronologys INNER JOIN Products ON Chronologys.Product_id = Products.ID";
            OleDbConnection Connection = new OleDbConnection(Properties.Settings.Default.ConnectionString);
            DataTable DT = new DataTable();

            try
            {
                Connection.Open();
                OleDbCommand Command = new OleDbCommand(commandstr, Connection);
                OleDbDataAdapter Adapter = new OleDbDataAdapter(Command);
                Adapter.Fill(DT);
                DevExpress.Xpf.Charts.SeriesPoint point = new DevExpress.Xpf.Charts.SeriesPoint();
                String s = String.Empty;
                Double val = 0.0;

                foreach (DataRow DR in DT.Rows)
                {
/*                    foreach (DataColumn DC in DT.Columns)
                        s += ' ' + DR[DC].ToString();
                    MessageBox.Show(s);
                    s = String.Empty;*/
                    if (Convert.ToDateTime(DR[2]).Date.Equals(DateTime.Now.Date))
                    {
                        if (!last_Dose.Equals(Convert.ToInt32(DR["Dose"])))
                        {
                            last_Dose++;
                            point.Value = val;
                            chart_points.Add(point);
                            point = new DevExpress.Xpf.Charts.SeriesPoint();
                            val = 0.0;
                        }
                        val += Convert.ToDouble(DR["Colarific_value"]) * Convert.ToDouble(DR["Grams"]);
                        point.Argument = Convert.ToDateTime(DR[2]).ToLongTimeString();
                        point.ToolTipHint += String.Format("{0, 25} {1:0.##}", DR[3].ToString().TrimEnd(), (Convert.ToDouble(DR[4]) * Convert.ToDouble(DR[5]))).TrimStart() + " Ккал\n";

                        DateTime.TryParse(DR[2].ToString(), out last_saved_point);
                        //                        MessageBox.Show(Convert.ToDateTime(DR[1]).ToString() + '\n' +  Convert.ToDouble(DR[2]).ToString());
                    }
                }
                if (!point.Equals(new DevExpress.Xpf.Charts.SeriesPoint()))
                {
                    point.Value = val;
                    chart_points.Add(point);
                }
            }
            catch (Exception e)
            {
                notify_status_e = e;
            }
        }

        void notify_status()
        {
            if (notify_status_e == null)
            {
                //                MessageBox.Show(points.Count.ToString());
                foreach (DevExpress.Xpf.Charts.SeriesPoint point in chart_points)
                {
                    Chart_Area.Series.ElementAt(0).Points.Add(point);
                }

                Notify.Background = Brushes.DarkSeaGreen;
                Notify.Foreground = Brushes.Black;
                Notify.Content = "Всё готово)";

                //            date.Text = "Сегодня: " + DateTime.Now.ToShortDateString();
                Notify.HorizontalAlignment = HorizontalAlignment.Right;
                Loading.Visibility = Visibility.Hidden;
                Notify.Background = new ImageBrush(new BitmapImage(new Uri("E:/Programs/NUOS/Programming/test/HealthLifeStyle/App_Health/img/smile.png", UriKind.RelativeOrAbsolute)));
                Notify.RenderSize = new Size(24, 24);
                Notify.Height = 24;
                Notify.Width = 24;
                //            Notify.Width = this.Width;
            }
            else
            {
                MessageBox.Show("Ошибка БД!\n" + notify_status_e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Notify.Background = Brushes.Red;
                Notify.Foreground = Brushes.White;
                Notify.Content = "База не подключенна";
                MessageBox.Show("Проверьте ваше интернет соединение и повторите позже", "Выход", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }


        private void main_wind_Dock_Panel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter) && (productsComboBox.IsFocused || Grams.IsFocused))
                Add_prod_Click(sender, e);
        }

        private void Add_prod_Click(object sender, RoutedEventArgs e)
        {
            // проверка на пустоту строк ввода
            if (!productsComboBox.Text.Equals(String.Empty) && !Grams.Text.Equals(String.Empty))
            {
                int grams;
                int.TryParse(Grams.Text, out grams);

                if (grams > 0.0)
                {
                    Main_grid_rows.Main_Grid.Add(new Main_Grid(productsComboBox.Text, grams * Convert.ToDouble(productsComboBox.SelectedValue), grams));
                    Grams.Text = String.Empty;

                    is_show_grid();
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
            }
        }

        private void Accept_basket_Click(object sender, RoutedEventArgs e)
        {
            Double val = 0.0;
            DevExpress.Xpf.Charts.SeriesPoint point = new DevExpress.Xpf.Charts.SeriesPoint();

            //            MessageBox.Show(Main_grid_rows.Main_Grid.Count.ToString());
            if (Main_grid_rows.Main_Grid.Count > 0)
            {
                foreach (Main_Grid row in Main_grid_rows.Main_Grid)
                {
                    point.Argument = DateTime.Now.ToLongTimeString();
                    point.ToolTipHint += string.Format("{0} {1}\n", row.Name, row.Kkal);
                    val += row.Kkal;
                    point.Value = val;
                }
                //                MessageBox.Show(point.Argument.ToString() + ' ' + point.Value.ToString() + '\n' + point.ToolTipHint + '\n');
                chart_points.Add(point);
                Chart_Area.Series.ElementAt(0).Points.Add(chart_points.ElementAt(chart_points.Count - 1));
            }

            Main_grid_rows.Main_Grid.Clear();
            is_show_grid();
            productsComboBox.SelectedItem = productsComboBox.Items.GetItemAt(0);
            productsComboBox.Focus();
        }

        private void is_show_grid()
        {
            if (second_wind.Width == 0)
            {
                size_change_animate(main_wind_add, -300, 0);
                size_change_animate(second_wind, 300, 0);
                position_change_animate(Add_prod, 100, -40);
                position_change_animate(label, 0, -10);
                position_change_animate(productsComboBox, 0, -10);
                position_change_animate(label1, -(Convert.ToInt32(label1.Width) + 24), 50);
                position_change_animate(Grams, -(Convert.ToInt32(Grams.Width) + 24), 50);

                size_change_animate(label, 30, 0);
                size_change_animate(productsComboBox, 30, 0);
                size_change_animate(label1, 30, 0);
                size_change_animate(Grams, 30, 0);
            }
            else if (Main_grid_rows.Main_Grid.Count == 0 && second_wind.Width == 300)
            {
                size_change_animate(main_wind_add, 300, 0);
                size_change_animate(second_wind, -300, 0);
                position_change_animate(Add_prod, -100, 40);
                position_change_animate(label, 0, 10);
                position_change_animate(productsComboBox, 0, 10);
                position_change_animate(label1, Convert.ToInt32(label1.Width) + 24 - 30, -50);
                position_change_animate(Grams, Convert.ToInt32(Grams.Width) + 24 - 30, -50);

                size_change_animate(label, -30, 0);
                size_change_animate(productsComboBox, -30, 0);
                size_change_animate(label1, -30, 0);
                size_change_animate(Grams, -30, 0);
            }
        }

        private void size_change_animate(FrameworkElement el, int dx, int dy)
        {
            Duration t = TimeSpan.FromSeconds(0.25);//Время, за которое должна происходить анимация

            DoubleAnimation w;//ширина элемента
            DoubleAnimation h;//высота элемента

            //Настройка анимации изменения ширины
            w = new DoubleAnimation();
            ConfigureDoubleAnimation(el.ActualWidth, dx, t, w);
            //Настройка анимации изменения высоты
            h = new DoubleAnimation();
            ConfigureDoubleAnimation(el.ActualHeight, dy, t, h);

            //Запускаю анимацию
            el.BeginAnimation(Button.WidthProperty, w);
            el.BeginAnimation(Button.HeightProperty, h);
        }

        /// <summary>
        /// Настройка анимированного свойства
        /// </summary>
        /// <param name="val">стартовое значение</param>
        /// <param name="scale">Значение коэффициента увеличения</param>
        /// <param name="t">Время, за которое должна выполняться анимация</param>
        /// <param name="anim">Объект анимации, который следует настроить</param>
        private void ConfigureDoubleAnimation(double val, double scale, Duration t, DoubleAnimation anim)
        {
            anim.From = val;
            anim.To = anim.From + scale;
            anim.Duration = t;
        }

        private void position_change_animate(FrameworkElement el, int dx, int dy)
        {
            Duration t = TimeSpan.FromSeconds(0.25);//Время, за которое должна происходить анимация
            ThicknessAnimation pos;

            //Настройка анимации изменения положения
            pos = new ThicknessAnimation();
            ConfigureThicknessAnimation(el.Margin.Left, el.Margin.Top, dx, dy, t, pos);

            //Запускаю анимацию
            el.BeginAnimation(Button.MarginProperty, pos);
        }

        private void ConfigureThicknessAnimation(Double x, Double y, double dx, double dy, Duration t, ThicknessAnimation anim)
        {
            anim.From = new Thickness(x, y, 0, 0);
            anim.To = new Thickness(x + dx, y + dy, 0, 0);
            anim.Duration = t;
        }
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
