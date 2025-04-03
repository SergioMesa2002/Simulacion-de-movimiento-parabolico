using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimulacionMovimientoParabolico
{
    public partial class Form1 : Form
    {
        // Usar una variable de tipo diferente para evitar el conflicto
        private System.Windows.Forms.Timer animTimer;
        private double currentTime = 0;
        private double timeInterval = 0.1;
        private double maxTime = 0;
        
        // Variables para almacenar los parámetros de movimiento
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
            // Crear explícitamente un Timer de Windows.Forms
            animTimer = new System.Windows.Forms.Timer();
            animTimer.Interval = 50; // 50ms para una animación fluida
            animTimer.Tick += AnimationTimer_Tick;
        }

        private void SetDefaultValues()
        {
            // Establecer valores predeterminados
            txtV0.Text = "20";
            txtAngle.Text = "45";
            txtX0.Text = "0";
            txtY0.Text = "0";
            txtGravedad.Text = "9.8";
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
                // Detener cualquier animación en curso
                if (animTimer.Enabled)
                {
                    animTimer.Stop();
                }

                // Obtener los valores ingresados por el usuario
                v0 = Convert.ToDouble(txtV0.Text);
                angle = Convert.ToDouble(txtAngle.Text) * Math.PI / 180; // Convertir a radianes
                x0 = Convert.ToDouble(txtX0.Text);
                y0 = Convert.ToDouble(txtY0.Text);
                g = Convert.ToDouble(txtGravedad.Text);

                // Validar que los valores sean adecuados
                if (v0 <= 0 || g <= 0)
                {
                    MessageBox.Show("La velocidad inicial y la gravedad deben ser mayores que cero.", 
                        "Valores inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Inicializar las variables
                timeInterval = 0.1; // Intervalo de tiempo en segundos
                maxTime = CalculateFlightTime(v0, angle, y0, g); // Tiempo total de vuelo

                // Limpiar el DataGridView
                dataGridView.Rows.Clear();

                // Cálculo del movimiento en el tiempo
                for (double t = 0; t <= maxTime; t += timeInterval)
                {
                    AddDataPoint(t);
                }

                // Calcular los resultados finales
                double maxHeight = CalculateMaxHeight(v0, angle, y0, g);
                double horizontalDisplacement = CalculateRange(v0, angle, x0, y0, g);

                // Mostrar los resultados en las etiquetas
                lblTiempoVuelo.Text = "Tiempo de vuelo: " + maxTime.ToString("F2") + " s";
                lblAlturaMaxima.Text = "Altura máxima: " + maxHeight.ToString("F2") + " m";
                lblDesplazamiento.Text = "Desplazamiento horizontal: " + horizontalDisplacement.ToString("F2") + " m";

                // Iniciar la animación
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
            // Ecuaciones de movimiento
            double x = x0 + v0 * Math.Cos(angle) * t;
            double y = y0 + v0 * Math.Sin(angle) * t - 0.5 * g * t * t;
            double vx = v0 * Math.Cos(angle);
            double vy = v0 * Math.Sin(angle) - g * t;

            // Registrar los valores en el DataGridView
            dataGridView.Rows.Add(t.ToString("F2"), x.ToString("F2"), y.ToString("F2"), vx.ToString("F2"), vy.ToString("F2"));
        }

        private double CalculateFlightTime(double v0, double angle, double y0, double g)
        {
            // Si empezamos desde una altura positiva, hay que considerar el tiempo adicional
            double timeUp = v0 * Math.Sin(angle) / g;
            double maxHeight = y0 + v0 * Math.Sin(angle) * timeUp - 0.5 * g * timeUp * timeUp;
            
            // Tiempo de caída desde la altura máxima
            double timeDown = Math.Sqrt(2 * maxHeight / g);
            
            return timeUp + timeDown;
        }

        private double CalculateMaxHeight(double v0, double angle, double y0, double g)
        {
            return y0 + Math.Pow(v0 * Math.Sin(angle), 2) / (2 * g);
        }

        private double CalculateRange(double v0, double angle, double x0, double y0, double g)
        {
            // Si la altura inicial es cero, usar la fórmula estándar
            if (y0 == 0)
            {
                return x0 + (v0 * v0 * Math.Sin(2 * angle)) / g;
            }
            
            // Si no, calcular el tiempo total y multiplicar por la velocidad horizontal
            return x0 + v0 * Math.Cos(angle) * CalculateFlightTime(v0, angle, y0, g);
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // Incrementar el tiempo actual
            currentTime += timeInterval;
            
            if (currentTime > maxTime)
            {
                // Detener la animación cuando se completa
                animTimer.Stop();
                currentTime = 0;
                return;
            }
            
            // Forzar actualización del Panel para dibujar la trayectoria
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gPanel = e.Graphics;
            
            // Dibujar una línea horizontal representando el suelo
            gPanel.DrawLine(Pens.Black, 0, panel1.Height - 10, panel1.Width, panel1.Height - 10);
            
            try
            {
                // Si no hay datos de animación, no dibujamos nada
                if (v0 <= 0)
                    return;

                // Calcular el factor de escala para que la trayectoria se ajuste al panel
                double horizontalRange = CalculateRange(v0, angle, x0, y0, g) - x0;
                double maxHeight = CalculateMaxHeight(v0, angle, y0, g) - y0;
                
                // Dejar un margen del 10% en los bordes
                double scaleX = (panel1.Width * 0.8) / Math.Max(1, horizontalRange);
                double scaleY = (panel1.Height * 0.8) / Math.Max(1, maxHeight);
                double scale = Math.Min(scaleX, scaleY);
                
                // Offset para centrar la trayectoria
                int offsetX = 20;
                int offsetY = 10;
                
                // Dibujar la trayectoria completa
                Point[] points = new Point[(int)(maxTime / timeInterval) + 1];
                int i = 0;
                
                for (double t = 0; t <= maxTime; t += timeInterval)
                {
                    // Ecuaciones de movimiento
                    double x = x0 + v0 * Math.Cos(angle) * t;
                    double y = y0 + v0 * Math.Sin(angle) * t - 0.5 * g * t * t;

                    // Transformar las coordenadas para ajustarlas al tamaño del panel
                    int panelX = (int)(offsetX + (x - x0) * scale);
                    int panelY = (int)(panel1.Height - offsetY - (y - y0) * scale);

                    // Limitar las coordenadas al tamaño del panel
                    panelX = Math.Max(0, Math.Min(panel1.Width, panelX));
                    panelY = Math.Max(0, Math.Min(panel1.Height, panelY));

                    if (i < points.Length)
                    {
                        points[i] = new Point(panelX, panelY);
                        i++;
                    }
                }

                // Dibujar la línea de trayectoria
                if (points.Length > 1)
                {
                    gPanel.DrawCurve(Pens.Gray, points, 0, points.Length - 1, 0.5f);
                }

                // Dibujar la posición actual del proyectil si la animación está en curso
                if (animTimer.Enabled)
                {
                    double x = x0 + v0 * Math.Cos(angle) * currentTime;
                    double y = y0 + v0 * Math.Sin(angle) * currentTime - 0.5 * g * currentTime * currentTime;

                    // Transformar las coordenadas
                    int panelX = (int)(offsetX + (x - x0) * scale);
                    int panelY = (int)(panel1.Height - offsetY - (y - y0) * scale);

                    // Dibujar el proyectil como un círculo rojo más grande
                    gPanel.FillEllipse(Brushes.Red, panelX - 7, panelY - 7, 14, 14);
                    gPanel.DrawEllipse(Pens.Black, panelX - 7, panelY - 7, 14, 14);
                }
            }
            catch (Exception ex)
            {
                // Manejar errores silenciosamente durante la animación
                Console.WriteLine("Error en paint: " + ex.Message);
            }
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            // Detener cualquier animación en curso
            animTimer.Stop();
            currentTime = 0;
            
            // Limpiar el DataGridView
            dataGridView.Rows.Clear();
            
            // Restablecer etiquetas de resultados
            lblTiempoVuelo.Text = "Tiempo de vuelo: 0 s";
            lblAlturaMaxima.Text = "Altura máxima: 0 m";
            lblDesplazamiento.Text = "Desplazamiento horizontal: 0 m";
            
            // Volver a dibujar el panel vacío
            panel1.Invalidate();
        }
    }
}