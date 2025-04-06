using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SimulacionMovimientoParabolico
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer animTimer;
        private double currentTime = 0;
        private double timeInterval = 0.1;
        private double maxTime = 0;
        private double v0, angle, x0, y0, g;
        private Chart trajectoryChart; // Control Chart modificado

        public Form1()
        {
            InitializeComponent();
            CreateChart(); // Primero creamos el gráfico
            InitializeComponents();
            SetDefaultValues();
        }

        private void CreateChart()
        {
            // Crear y configurar el control Chart
            trajectoryChart = new Chart();
            var chartArea = new ChartArea("MainChartArea");

            // Configurar ejes
            chartArea.AxisX.Title = "Posición X (m)";
            chartArea.AxisY.Title = "Posición Y (m)";
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisY.Minimum = 0;

            trajectoryChart.ChartAreas.Add(chartArea);

            // Configurar serie
            var series = new Series("Trayectoria")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Blue,
                BorderWidth = 2
            };
            trajectoryChart.Series.Add(series);

            // Posición y tamaño
            trajectoryChart.Location = new Point(300, 50);
            trajectoryChart.Size = new Size(400, 300);
            trajectoryChart.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;

            // Agregar al formulario
            this.Controls.Add(trajectoryChart);
        }

        private void InitializeComponents()
        {
            InitializeDataGridView();
            InitializeAnimationTimer();
        }

        private void InitializeAnimationTimer()
        {
            animTimer = new System.Windows.Forms.Timer();
            animTimer.Interval = 50;
            animTimer.Tick += AnimationTimer_Tick;
        }

        private void SetDefaultValues()
        {
            txtV0.Text = "20";
            txtAngle.Text = "45";
            txtX0.Text = "0";
            txtY0.Text = "0";
            txtGravedad.Text = "9,8";
        }

        private void InitializeDataGridView()
        {
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add("Time", "Tiempo (s)");
            dataGridView.Columns.Add("X", "Posición X (m)");
            dataGridView.Columns.Add("Y", "Posición Y (m)");
            dataGridView.Columns.Add("Vx", "Velocidad X (m/s)");
            dataGridView.Columns.Add("Vy", "Velocidad Y (m/s)");
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                if (animTimer.Enabled) animTimer.Stop();

                // Obtener valores de entrada
                v0 = Convert.ToDouble(txtV0.Text);
                angle = Convert.ToDouble(txtAngle.Text) * Math.PI / 180;
                x0 = Convert.ToDouble(txtX0.Text);
                y0 = Convert.ToDouble(txtY0.Text);
                g = Convert.ToDouble(txtGravedad.Text);

                // Validaciones
                if (v0 <= 0 || g <= 0)
                {
                    MessageBox.Show("La velocidad inicial y la gravedad deben ser mayores que cero.",
                        "Valores inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (Convert.ToDouble(txtAngle.Text) < -90 || Convert.ToDouble(txtAngle.Text) > 90)
                {
                    MessageBox.Show("El ángulo debe estar entre -90° y 90°",
                        "Ángulo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Calcular parámetros
                timeInterval = 0.05;
                maxTime = CalculateFlightTime(v0, angle, y0, g);

                // Limpiar datos anteriores
                dataGridView.Rows.Clear();
                trajectoryChart.Series["Trayectoria"].Points.Clear();

                // Generar y registrar datos
                for (double t = 0; t <= maxTime; t += timeInterval)
                {
                    AddDataPoint(t);
                }

                // Mostrar resultados
                UpdateResults();

                // Iniciar animación
                currentTime = 0;
                animTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en el cálculo: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddDataPoint(double t)
        {
            // Calcular valores
            double x = x0 + v0 * Math.Cos(angle) * t;
            double y = y0 + v0 * Math.Sin(angle) * t - 0.5 * g * t * t;
            double vx = v0 * Math.Cos(angle);
            double vy = v0 * Math.Sin(angle) - g * t;

            // Agregar a DataGridView
            dataGridView.Rows.Add(t.ToString("F2"), x.ToString("F2"),
                                y.ToString("F2"), vx.ToString("F2"), vy.ToString("F2"));

            // Agregar al gráfico
            trajectoryChart.Series["Trayectoria"].Points.AddXY(x, y);
        }

        private void UpdateResults()
        {
            double maxHeight = CalculateMaxHeight(v0, angle, y0, g);
            double horizontalDisplacement = CalculateRange(v0, angle, x0, y0, g);

            lblTiempoVuelo.Text = $"Tiempo de vuelo: {maxTime:F2} s";
            lblAlturaMaxima.Text = $"Altura máxima: {maxHeight:F2} m";
            lblDesplazamiento.Text = $"Desplazamiento horizontal: {horizontalDisplacement:F2} m";

            // Ajustar escala del gráfico
            AdjustChartScale();
        }

        private void AdjustChartScale()
        {
            if (dataGridView.Rows.Count > 1)
            {
                double maxX = Convert.ToDouble(dataGridView.Rows[dataGridView.Rows.Count - 1].Cells["X"].Value);
                double maxY = 0;

                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells["Y"].Value != null)
                    {
                        double y = Convert.ToDouble(row.Cells["Y"].Value);
                        if (y > maxY) maxY = y;
                    }
                }

                trajectoryChart.ChartAreas["MainChartArea"].AxisX.Maximum = maxX * 1.1;
                trajectoryChart.ChartAreas["MainChartArea"].AxisY.Maximum = maxY * 1.1;
            }
        }

        private double CalculateFlightTime(double v0, double angle, double y0, double g)
        {
            double vy = v0 * Math.Sin(angle);
            double a = -0.5 * g;
            double b = vy;
            double c = y0;

            double discriminant = b * b - 4 * a * c;

            if (discriminant < 0) return 0;

            double t1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            double t2 = (-b - Math.Sqrt(discriminant)) / (2 * a);

            return Math.Max(t1, t2);
        }

        private double CalculateMaxHeight(double v0, double angle, double y0, double g)
        {
            double vy = v0 * Math.Sin(angle);
            double tMax = vy / g;
            return y0 + vy * tMax - 0.5 * g * tMax * tMax;
        }

        private double CalculateRange(double v0, double angle, double x0, double y0, double g)
        {
            double totalTime = CalculateFlightTime(v0, angle, y0, g);
            return x0 + v0 * Math.Cos(angle) * totalTime;
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            currentTime += timeInterval;

            if (currentTime > maxTime)
            {
                animTimer.Stop();
                currentTime = 0;
                return;
            }

            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gPanel = e.Graphics;

            // Dibujar suelo y ejes
            gPanel.DrawLine(Pens.Black, 0, panel1.Height - 10, panel1.Width, panel1.Height - 10);
            gPanel.DrawLine(Pens.LightGray, 20, 10, 20, panel1.Height - 10);
            gPanel.DrawLine(Pens.LightGray, 20, panel1.Height - 10, panel1.Width, panel1.Height - 10);

            try
            {
                if (v0 <= 0) return;

                double horizontalRange = CalculateRange(v0, angle, x0, y0, g) - x0;
                double maxHeight = CalculateMaxHeight(v0, angle, y0, g) - y0;

                double scaleX = (panel1.Width * 0.8) / Math.Max(1, horizontalRange);
                double scaleY = (panel1.Height * 0.8) / Math.Max(1, maxHeight);
                double scale = Math.Min(scaleX, scaleY);

                int offsetX = 20;
                int offsetY = 10;

                // Dibujar trayectoria completa
                Point[] points = new Point[(int)(maxTime / timeInterval) + 1];
                int i = 0;

                for (double t = 0; t <= maxTime; t += timeInterval)
                {
                    double x = x0 + v0 * Math.Cos(angle) * t;
                    double y = y0 + v0 * Math.Sin(angle) * t - 0.5 * g * t * t;

                    int panelX = (int)(offsetX + (x - x0) * scale);
                    int panelY = (int)(panel1.Height - offsetY - (y - y0) * scale);

                    panelX = Math.Max(0, Math.Min(panel1.Width, panelX));
                    panelY = Math.Max(0, Math.Min(panel1.Height, panelY));

                    if (i < points.Length) points[i++] = new Point(panelX, panelY);
                }

                if (points.Length > 1) gPanel.DrawCurve(Pens.Blue, points);

                if (animTimer.Enabled)
                {
                    double x = x0 + v0 * Math.Cos(angle) * currentTime;
                    double y = y0 + v0 * Math.Sin(angle) * currentTime - 0.5 * g * currentTime * currentTime;

                    int panelX = (int)(offsetX + (x - x0) * scale);
                    int panelY = (int)(panel1.Height - offsetY - (y - y0) * scale);

                    gPanel.FillEllipse(Brushes.Red, panelX - 7, panelY - 7, 14, 14);
                    gPanel.DrawEllipse(Pens.Black, panelX - 7, panelY - 7, 14, 14);
                    gPanel.DrawLine(Pens.Red, panelX, panel1.Height - 10, panelX, panelY);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en paint: " + ex.Message);
            }
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            animTimer.Stop();
            currentTime = 0;

            dataGridView.Rows.Clear();
            trajectoryChart.Series["Trayectoria"].Points.Clear();

            lblTiempoVuelo.Text = "Tiempo de vuelo: 0 s";
            lblAlturaMaxima.Text = "Altura máxima: 0 m";
            lblDesplazamiento.Text = "Desplazamiento horizontal: 0 m";

            panel1.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}