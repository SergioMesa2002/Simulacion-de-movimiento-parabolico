using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimulacionMovimientoParabolico
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer animTimer;
        private double currentTime = 0;
        private double timeInterval = 0.1;
        private double maxTime = 0;

        private double v0, angle, x0, y0, g;

        public Form1()
        {
            InitializeComponent();
            InitializeDataGridView();
            InitializeAnimationTimer();
            SetDefaultValues();
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

        private void Form1_Load(object sender, EventArgs e)
        {
            // Código adicional para inicializar después de cargar
        }

        private void InitializeDataGridView()
        {
            if (dataGridView.Columns.Count == 0)
            {
                dataGridView.Columns.Add("Time", "Tiempo (s)");
                dataGridView.Columns.Add("X", "Posición X (m)");
                dataGridView.Columns.Add("Y", "Posición Y (m)");
                dataGridView.Columns.Add("Vx", "Velocidad X (m/s)");
                dataGridView.Columns.Add("Vy", "Velocidad Y (m/s)");
            }
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                if (animTimer.Enabled)
                {
                    animTimer.Stop();
                }

                v0 = Convert.ToDouble(txtV0.Text);
                angle = Convert.ToDouble(txtAngle.Text) * Math.PI / 180;
                x0 = Convert.ToDouble(txtX0.Text);
                y0 = Convert.ToDouble(txtY0.Text);
                g = Convert.ToDouble(txtGravedad.Text);

                if (v0 <= 0 || g <= 0)
                {
                    MessageBox.Show("La velocidad inicial y la gravedad deben ser mayores que cero.",
                        "Valores inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validación adicional para el ángulo
                if (Convert.ToDouble(txtAngle.Text) < -90 || Convert.ToDouble(txtAngle.Text) > 90)
                {
                    MessageBox.Show("El ángulo debe estar entre -90° y 90°",
                        "Ángulo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                timeInterval = 0.05; // Reducido para mayor precisión
                maxTime = CalculateFlightTime(v0, angle, y0, g);

                dataGridView.Rows.Clear();

                for (double t = 0; t <= maxTime; t += timeInterval)
                {
                    AddDataPoint(t);
                }

                double maxHeight = CalculateMaxHeight(v0, angle, y0, g);
                double horizontalDisplacement = CalculateRange(v0, angle, x0, y0, g);

                lblTiempoVuelo.Text = "Tiempo de vuelo: " + maxTime.ToString("F2") + " s";
                lblAlturaMaxima.Text = "Altura máxima: " + maxHeight.ToString("F2") + " m";
                lblDesplazamiento.Text = "Desplazamiento horizontal: " + horizontalDisplacement.ToString("F2") + " m";

                currentTime = 0;
                animTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en el cálculo: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddDataPoint(double t)
        {
            double x = x0 + v0 * Math.Cos(angle) * t;
            double y = y0 + v0 * Math.Sin(angle) * t - 0.5 * 9.8 * t * t; // Usa 9.8 directamente
            double vx = v0 * Math.Cos(angle);
            double vy = v0 * Math.Sin(angle) - 9.8 * t; // Usa 9.8 directamente

            dataGridView.Rows.Add(t.ToString("F2"), x.ToString("F2"),
                                y.ToString("F2"), vx.ToString("F2"), vy.ToString("F2"));
        }

        private double CalculateFlightTime(double v0, double angle, double y0, double g)
        {
            double vy = v0 * Math.Sin(angle);
            double a = -0.5 * g;
            double b = vy;
            double c = y0;

            double discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                return 0; // No llega al suelo
            }

            double t1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            double t2 = (-b - Math.Sqrt(discriminant)) / (2 * a);

            return Math.Max(t1, t2);
        }

        private double CalculateMaxHeight(double v0, double angle, double y0, double g)
        {
            double vy = v0 * Math.Sin(angle);
            double tMax = vy / g; // Tiempo para alcanzar altura máxima
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
            gPanel.DrawLine(Pens.LightGray, 20, 10, 20, panel1.Height - 10); // Eje Y
            gPanel.DrawLine(Pens.LightGray, 20, panel1.Height - 10, panel1.Width, panel1.Height - 10); // Eje X

            try
            {
                if (v0 <= 0)
                    return;

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

                    if (i < points.Length)
                    {
                        points[i] = new Point(panelX, panelY);
                        i++;
                    }
                }

                if (points.Length > 1)
                {
                    gPanel.DrawCurve(Pens.Blue, points, 0, points.Length - 1, 0.5f);
                }

                if (animTimer.Enabled)
                {
                    double x = x0 + v0 * Math.Cos(angle) * currentTime;
                    double y = y0 + v0 * Math.Sin(angle) * currentTime - 0.5 * g * currentTime * currentTime;

                    int panelX = (int)(offsetX + (x - x0) * scale);
                    int panelY = (int)(panel1.Height - offsetY - (y - y0) * scale);

                    gPanel.FillEllipse(Brushes.Red, panelX - 7, panelY - 7, 14, 14);
                    gPanel.DrawEllipse(Pens.Black, panelX - 7, panelY - 7, 14, 14);

                    // Dibujar línea de posición actual
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

            lblTiempoVuelo.Text = "Tiempo de vuelo: 0 s";
            lblAlturaMaxima.Text = "Altura máxima: 0 m";
            lblDesplazamiento.Text = "Desplazamiento horizontal: 0 m";

            panel1.Invalidate();
        }
    }
}