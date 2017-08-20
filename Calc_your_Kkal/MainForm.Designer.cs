namespace Health_Project
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.TabControl = new System.Windows.Forms.TabControl();
            this.Calc = new System.Windows.Forms.TabPage();
            this.Add = new System.Windows.Forms.Button();
            this.Notify = new System.Windows.Forms.Label();
            this.Loading = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.NameGood = new System.Windows.Forms.ComboBox();
            this.productsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetHealsDB = new Health_Project.DataSetHealsDB();
            this.weight = new System.Windows.Forms.TextBox();
            this.Graph = new System.Windows.Forms.TabPage();
            this.date = new System.Windows.Forms.Label();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.productstypesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.productsTableAdapter = new Health_Project.DataSetHealsDBTableAdapters.ProductsTableAdapter();
            this.tableAdapterManager = new Health_Project.DataSetHealsDBTableAdapters.TableAdapterManager();
            this.products_typesTableAdapter = new Health_Project.DataSetHealsDBTableAdapters.Products_typesTableAdapter();
            this.Add_p = new System.Windows.Forms.TabPage();
            this.productsDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TabControl.SuspendLayout();
            this.Calc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetHealsDB)).BeginInit();
            this.Graph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productstypesBindingSource)).BeginInit();
            this.Add_p.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.Calc);
            this.TabControl.Controls.Add(this.Graph);
            this.TabControl.Controls.Add(this.Add_p);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Margin = new System.Windows.Forms.Padding(0);
            this.TabControl.Name = "TabControl";
            this.TabControl.Padding = new System.Drawing.Point(0, 0);
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(684, 361);
            this.TabControl.TabIndex = 0;
            // 
            // Calc
            // 
            this.Calc.AutoScroll = true;
            this.Calc.BackgroundImage = global::Health_Project.Properties.Resources.back;
            this.Calc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Calc.Controls.Add(this.Add);
            this.Calc.Controls.Add(this.Notify);
            this.Calc.Controls.Add(this.Loading);
            this.Calc.Controls.Add(this.label2);
            this.Calc.Controls.Add(this.label1);
            this.Calc.Controls.Add(this.NameGood);
            this.Calc.Controls.Add(this.weight);
            this.Calc.Location = new System.Drawing.Point(4, 26);
            this.Calc.Name = "Calc";
            this.Calc.Padding = new System.Windows.Forms.Padding(3);
            this.Calc.Size = new System.Drawing.Size(676, 331);
            this.Calc.TabIndex = 0;
            this.Calc.Text = "Расчёт";
            this.Calc.UseVisualStyleBackColor = true;
            // 
            // Add
            // 
            this.Add.BackColor = System.Drawing.Color.White;
            this.Add.Cursor = System.Windows.Forms.Cursors.Default;
            this.Add.FlatAppearance.BorderColor = System.Drawing.Color.DarkSeaGreen;
            this.Add.FlatAppearance.MouseDownBackColor = System.Drawing.Color.BlanchedAlmond;
            this.Add.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Add.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Add.Location = new System.Drawing.Point(207, 108);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(117, 43);
            this.Add.TabIndex = 4;
            this.Add.Text = "Добавить";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Notify
            // 
            this.Notify.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Notify.Location = new System.Drawing.Point(506, 285);
            this.Notify.Name = "Notify";
            this.Notify.Size = new System.Drawing.Size(162, 20);
            this.Notify.TabIndex = 0;
            this.Notify.Text = "Загрузка";
            this.Notify.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // Loading
            // 
            this.Loading.BackColor = System.Drawing.SystemColors.Window;
            this.Loading.Location = new System.Drawing.Point(506, 311);
            this.Loading.MarqueeAnimationSpeed = 10;
            this.Loading.Name = "Loading";
            this.Loading.Size = new System.Drawing.Size(162, 10);
            this.Loading.Step = 1;
            this.Loading.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.Loading.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(271, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "Количество (в граммах)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(32, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название прод- та";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NameGood
            // 
            this.NameGood.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.NameGood.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.NameGood.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.NameGood.DataSource = this.productsBindingSource;
            this.NameGood.DisplayMember = "Name";
            this.NameGood.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NameGood.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NameGood.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NameGood.ForeColor = System.Drawing.SystemColors.Window;
            this.NameGood.FormattingEnabled = true;
            this.NameGood.Location = new System.Drawing.Point(35, 54);
            this.NameGood.Name = "NameGood";
            this.NameGood.Size = new System.Drawing.Size(215, 29);
            this.NameGood.TabIndex = 1;
            this.NameGood.ValueMember = "Kkal_1g";
            // 
            // productsBindingSource
            // 
            this.productsBindingSource.DataMember = "Products";
            this.productsBindingSource.DataSource = this.dataSetHealsDB;
            // 
            // dataSetHealsDB
            // 
            this.dataSetHealsDB.DataSetName = "DataSetHealsDB";
            this.dataSetHealsDB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // weight
            // 
            this.weight.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.weight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.weight.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.weight.ForeColor = System.Drawing.SystemColors.Window;
            this.weight.Location = new System.Drawing.Point(275, 54);
            this.weight.MaxLength = 4;
            this.weight.Name = "weight";
            this.weight.Size = new System.Drawing.Size(197, 29);
            this.weight.TabIndex = 2;
            this.weight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Graph
            // 
            this.Graph.Controls.Add(this.date);
            this.Graph.Controls.Add(this.chart);
            this.Graph.Location = new System.Drawing.Point(4, 26);
            this.Graph.Margin = new System.Windows.Forms.Padding(0);
            this.Graph.Name = "Graph";
            this.Graph.Size = new System.Drawing.Size(676, 331);
            this.Graph.TabIndex = 1;
            this.Graph.Text = "График";
            // 
            // date
            // 
            this.date.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.date.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.date.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.date.ForeColor = System.Drawing.SystemColors.Window;
            this.date.Location = new System.Drawing.Point(495, 302);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(178, 24);
            this.date.TabIndex = 0;
            this.date.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chart
            // 
            this.chart.BackColor = System.Drawing.Color.Transparent;
            this.chart.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.TileFlipXY;
            this.chart.BorderlineColor = System.Drawing.Color.Transparent;
            this.chart.BorderlineWidth = 0;
            this.chart.BorderSkin.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.Scaled;
            this.chart.BorderSkin.BorderWidth = 0;
            this.chart.BorderSkin.PageColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Lines;
            chartArea1.AxisX.InterlacedColor = System.Drawing.Color.White;
            chartArea1.AxisX.Interval = 60D;
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Hours;
            chartArea1.AxisX.LabelAutoFitMaxFontSize = 11;
            chartArea1.AxisX.LabelAutoFitMinFontSize = 8;
            chartArea1.AxisX.LabelStyle.Interval = 0D;
            chartArea1.AxisX.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Hours;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Hours;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.DodgerBlue;
            chartArea1.AxisX.Title = "Время";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.AxisY.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Lines;
            chartArea1.AxisY.LabelAutoFitMaxFontSize = 11;
            chartArea1.AxisY.LabelAutoFitMinFontSize = 8;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY.Title = "Ккал";
            chartArea1.AxisY.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.BackHatchStyle = System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.ZigZag;
            chartArea1.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            chartArea1.BackImageTransparentColor = System.Drawing.Color.Transparent;
            chartArea1.BorderWidth = 0;
            chartArea1.Name = "ChartArea";
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.BackColor = System.Drawing.Color.AntiqueWhite;
            legend1.BorderColor = System.Drawing.Color.Black;
            legend1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            legend1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            legend1.IsTextAutoFit = false;
            legend1.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Column;
            legend1.Name = "Legend";
            legend1.Title = "Графики";
            legend1.TitleFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            legend1.TitleSeparator = System.Windows.Forms.DataVisualization.Charting.LegendSeparatorStyle.DotLine;
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(0, 0);
            this.chart.Margin = new System.Windows.Forms.Padding(0);
            this.chart.Name = "chart";
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Color = System.Drawing.Color.DeepSkyBlue;
            series1.CustomProperties = "IsXAxisQuantitative=False, DrawingStyle=Cylinder, LabelStyle=Top";
            series1.EmptyPointStyle.Color = System.Drawing.Color.Transparent;
            series1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            series1.IsValueShownAsLabel = true;
            series1.LabelBackColor = System.Drawing.Color.BlanchedAlmond;
            series1.LabelBorderColor = System.Drawing.Color.Black;
            series1.Legend = "Legend";
            series1.MarkerBorderColor = System.Drawing.Color.Black;
            series1.MarkerColor = System.Drawing.Color.LimeGreen;
            series1.MarkerSize = 10;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Kkal";
            series1.SmartLabelStyle.CalloutBackColor = System.Drawing.Color.Bisque;
            series1.SmartLabelStyle.CalloutLineColor = System.Drawing.Color.Transparent;
            series1.SmartLabelStyle.IsMarkerOverlappingAllowed = true;
            series1.SmartLabelStyle.IsOverlappedHidden = false;
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series1.YValuesPerPoint = 6;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int64;
            this.chart.Series.Add(series1);
            this.chart.Size = new System.Drawing.Size(676, 331);
            this.chart.TabIndex = 0;
            // 
            // productstypesBindingSource
            // 
            this.productstypesBindingSource.DataMember = "Products_types";
            this.productstypesBindingSource.DataSource = this.dataSetHealsDB;
            // 
            // productsTableAdapter
            // 
            this.productsTableAdapter.ClearBeforeFill = true;
            //
            // products_typesTableAdapter
            // 
            this.products_typesTableAdapter.ClearBeforeFill = true;
            // 
            // Add_p
            // 
            this.Add_p.AutoScroll = true;
            this.Add_p.Controls.Add(this.productsDataGridView);
            this.Add_p.Location = new System.Drawing.Point(4, 26);
            this.Add_p.Name = "Add_p";
            this.Add_p.Size = new System.Drawing.Size(676, 331);
            this.Add_p.TabIndex = 2;
            this.Add_p.Text = "Добавление";
            this.Add_p.UseVisualStyleBackColor = true;
            // 
            // productsDataGridView
            // 
            this.productsDataGridView.AutoGenerateColumns = false;
            this.productsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.productsDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.productsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.productsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewComboBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.productsDataGridView.DataSource = this.productsBindingSource;
            this.productsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productsDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.productsDataGridView.Location = new System.Drawing.Point(0, 0);
            this.productsDataGridView.Name = "productsDataGridView";
            this.productsDataGridView.ShowCellErrors = false;
            this.productsDataGridView.Size = new System.Drawing.Size(676, 331);
            this.productsDataGridView.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn2.HeaderText = "Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewComboBoxColumn3
            // 
            this.dataGridViewComboBoxColumn3.DataPropertyName = "Prod_type";
            this.dataGridViewComboBoxColumn3.DataSource = this.productstypesBindingSource;
            this.dataGridViewComboBoxColumn3.DisplayMember = "name_type";
            this.dataGridViewComboBoxColumn3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dataGridViewComboBoxColumn3.HeaderText = "Prod_type";
            this.dataGridViewComboBoxColumn3.Name = "dataGridViewComboBoxColumn3";
            this.dataGridViewComboBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewComboBoxColumn3.ValueMember = "Id";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Kkal_1g";
            this.dataGridViewTextBoxColumn4.HeaderText = "Kkal_1g";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // MainForm
            // 
            this.AcceptButton = this.Add;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(684, 361);
            this.Controls.Add(this.TabControl);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UHealth";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Enter += new System.EventHandler(this.MainForm_Enter);
            this.Leave += new System.EventHandler(this.MainForm_Leave);
            this.Move += new System.EventHandler(this.MainForm_Move);
            this.TabControl.ResumeLayout(false);
            this.Calc.ResumeLayout(false);
            this.Calc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetHealsDB)).EndInit();
            this.Graph.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productstypesBindingSource)).EndInit();
            this.Add_p.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.productsDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage Calc;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.TextBox weight;
        private System.Windows.Forms.TabPage Graph;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.ComboBox NameGood;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar Loading;
        private System.Windows.Forms.Label date;
        private System.Windows.Forms.Label Notify;
        private DataSetHealsDB dataSetHealsDB;
        private System.Windows.Forms.BindingSource productsBindingSource;
        private DataSetHealsDBTableAdapters.ProductsTableAdapter productsTableAdapter;
        private DataSetHealsDBTableAdapters.TableAdapterManager tableAdapterManager;
        private DataSetHealsDBTableAdapters.Products_typesTableAdapter products_typesTableAdapter;
        private System.Windows.Forms.BindingSource productstypesBindingSource;
        private System.Windows.Forms.TabPage Add_p;
        private System.Windows.Forms.DataGridView productsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}

