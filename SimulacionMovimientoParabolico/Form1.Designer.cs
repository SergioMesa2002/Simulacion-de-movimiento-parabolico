namespace SimulacionMovimientoParabolico
{
    partial class Form1
    {
        /// <summary>
        /// Variable necesaria para la depuración.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">True si los recursos administrados deben eliminarse; en caso contrario, False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Requerido por el Diseñador de Windows Forms
        /// </summary>
        private void InitializeComponent()
        {
            btnCalcular = new Button();
            btnReiniciar = new Button();
            txtV0 = new TextBox();
            txtAngle = new TextBox();
            txtX0 = new TextBox();
            txtY0 = new TextBox();
            txtGravedad = new TextBox();
            dataGridView = new DataGridView();
            panel1 = new Panel();
            lblTiempoVuelo = new Label();
            lblAlturaMaxima = new Label();
            lblDesplazamiento = new Label();
            lblTitulo = new Label();
            lblV0 = new Label();
            lblAngle = new Label();
            lblX0 = new Label();
            lblY0 = new Label();
            lblGravedad = new Label();
            lblResultados = new Label();
            lblDatosSimulacion = new Label();
            lblVisualizacion = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // btnCalcular
            // 
            btnCalcular.BackColor = Color.LightGreen;
            btnCalcular.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCalcular.Location = new Point(40, 231);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(120, 30);
            btnCalcular.TabIndex = 0;
            btnCalcular.Text = "Calcular y Animar";
            btnCalcular.UseVisualStyleBackColor = false;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // btnReiniciar
            // 
            btnReiniciar.BackColor = Color.LightCoral;
            btnReiniciar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnReiniciar.Location = new Point(166, 231);
            btnReiniciar.Name = "btnReiniciar";
            btnReiniciar.Size = new Size(120, 30);
            btnReiniciar.TabIndex = 12;
            btnReiniciar.Text = "Reiniciar";
            btnReiniciar.UseVisualStyleBackColor = false;
            btnReiniciar.Click += btnReiniciar_Click;
            // 
            // txtV0
            // 
            txtV0.Location = new Point(166, 73);
            txtV0.Name = "txtV0";
            txtV0.Size = new Size(120, 23);
            txtV0.TabIndex = 1;
            // 
            // txtAngle
            // 
            txtAngle.Location = new Point(166, 102);
            txtAngle.Name = "txtAngle";
            txtAngle.Size = new Size(120, 23);
            txtAngle.TabIndex = 2;
            // 
            // txtX0
            // 
            txtX0.Location = new Point(166, 131);
            txtX0.Name = "txtX0";
            txtX0.Size = new Size(120, 23);
            txtX0.TabIndex = 3;
            // 
            // txtY0
            // 
            txtY0.Location = new Point(166, 160);
            txtY0.Name = "txtY0";
            txtY0.Size = new Size(120, 23);
            txtY0.TabIndex = 4;
            // 
            // txtGravedad
            // 
            txtGravedad.Location = new Point(166, 189);
            txtGravedad.Name = "txtGravedad";
            txtGravedad.Size = new Size(120, 23);
            txtGravedad.TabIndex = 5;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(320, 73);
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersVisible = false;
            dataGridView.Size = new Size(410, 140);
            dataGridView.TabIndex = 6;
            // 
            // panel1
            // 
            panel1.BackColor = Color.WhiteSmoke;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Location = new Point(320, 250);
            panel1.Name = "panel1";
            panel1.Size = new Size(410, 230);
            panel1.TabIndex = 7;
            panel1.Paint += panel1_Paint;
            // 
            // lblTiempoVuelo
            // 
            lblTiempoVuelo.AutoSize = true;
            lblTiempoVuelo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTiempoVuelo.Location = new Point(40, 290);
            lblTiempoVuelo.Name = "lblTiempoVuelo";
            lblTiempoVuelo.Size = new Size(121, 15);
            lblTiempoVuelo.TabIndex = 8;
            lblTiempoVuelo.Text = "Tiempo de vuelo: 0 s";
            // 
            // lblAlturaMaxima
            // 
            lblAlturaMaxima.AutoSize = true;
            lblAlturaMaxima.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAlturaMaxima.Location = new Point(40, 320);
            lblAlturaMaxima.Name = "lblAlturaMaxima";
            lblAlturaMaxima.Size = new Size(115, 15);
            lblAlturaMaxima.TabIndex = 9;
            lblAlturaMaxima.Text = "Altura máxima: 0 m";
            // 
            // lblDesplazamiento
            // 
            lblDesplazamiento.AutoSize = true;
            lblDesplazamiento.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDesplazamiento.Location = new Point(40, 350);
            lblDesplazamiento.Name = "lblDesplazamiento";
            lblDesplazamiento.Size = new Size(182, 15);
            lblDesplazamiento.TabIndex = 10;
            lblDesplazamiento.Text = "Desplazamiento horizontal: 0 m";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.Location = new Point(232, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(403, 30);
            lblTitulo.TabIndex = 11;
            lblTitulo.Text = "Simulación de Movimiento Parabólico";
            // 
            // lblV0
            // 
            lblV0.AutoSize = true;
            lblV0.Location = new Point(40, 76);
            lblV0.Name = "lblV0";
            lblV0.Size = new Size(127, 15);
            lblV0.TabIndex = 13;
            lblV0.Text = "Velocidad inicial (m/s):";
            // 
            // lblAngle
            // 
            lblAngle.AutoSize = true;
            lblAngle.Location = new Point(40, 105);
            lblAngle.Name = "lblAngle";
            lblAngle.Size = new Size(96, 15);
            lblAngle.TabIndex = 14;
            lblAngle.Text = "Ángulo (grados):";
            // 
            // lblX0
            // 
            lblX0.AutoSize = true;
            lblX0.Location = new Point(40, 134);
            lblX0.Name = "lblX0";
            lblX0.Size = new Size(121, 15);
            lblX0.TabIndex = 15;
            lblX0.Text = "Posición inicial X (m):";
            // 
            // lblY0
            // 
            lblY0.AutoSize = true;
            lblY0.Location = new Point(40, 163);
            lblY0.Name = "lblY0";
            lblY0.Size = new Size(121, 15);
            lblY0.TabIndex = 16;
            lblY0.Text = "Posición inicial Y (m):";
            // 
            // lblGravedad
            // 
            lblGravedad.AutoSize = true;
            lblGravedad.Location = new Point(40, 192);
            lblGravedad.Name = "lblGravedad";
            lblGravedad.Size = new Size(96, 15);
            lblGravedad.TabIndex = 17;
            lblGravedad.Text = "Gravedad (m/s²):";
            // 
            // lblResultados
            // 
            lblResultados.AutoSize = true;
            lblResultados.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblResultados.Location = new Point(40, 260);
            lblResultados.Name = "lblResultados";
            lblResultados.Size = new Size(151, 21);
            lblResultados.TabIndex = 18;
            lblResultados.Text = "Resultados Físicos:";
            // 
            // lblDatosSimulacion
            // 
            lblDatosSimulacion.AutoSize = true;
            lblDatosSimulacion.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblDatosSimulacion.Location = new Point(320, 49);
            lblDatosSimulacion.Name = "lblDatosSimulacion";
            lblDatosSimulacion.Size = new Size(189, 21);
            lblDatosSimulacion.TabIndex = 19;
            lblDatosSimulacion.Text = "Datos de la Simulación:";
            // 
            // lblVisualizacion
            // 
            lblVisualizacion.AutoSize = true;
            lblVisualizacion.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblVisualizacion.Location = new Point(320, 226);
            lblVisualizacion.Name = "lblVisualizacion";
            lblVisualizacion.Size = new Size(228, 21);
            lblVisualizacion.TabIndex = 20;
            lblVisualizacion.Text = "Visualización de Trayectoria:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(45, 498);
            label2.Name = "label2";
            label2.Size = new Size(122, 21);
            label2.TabIndex = 22;
            label2.Text = "INTEGRANTES:";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(173, 498);
            label3.Name = "label3";
            label3.Size = new Size(213, 21);
            label3.TabIndex = 23;
            label3.Text = "Sebastian Camargo Cuesto";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(173, 519);
            label4.Name = "label4";
            label4.Size = new Size(164, 21);
            label4.TabIndex = 24;
            label4.Text = "Sergio Mesa Benitez";
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(173, 540);
            label5.Name = "label5";
            label5.Size = new Size(119, 21);
            label5.TabIndex = 25;
            label5.Text = "Gabriel Correa";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightBlue;
            ClientSize = new Size(912, 583);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(lblVisualizacion);
            Controls.Add(lblDatosSimulacion);
            Controls.Add(lblResultados);
            Controls.Add(lblGravedad);
            Controls.Add(lblY0);
            Controls.Add(lblX0);
            Controls.Add(lblAngle);
            Controls.Add(lblV0);
            Controls.Add(btnReiniciar);
            Controls.Add(lblTitulo);
            Controls.Add(btnCalcular);
            Controls.Add(txtV0);
            Controls.Add(txtAngle);
            Controls.Add(txtX0);
            Controls.Add(txtY0);
            Controls.Add(txtGravedad);
            Controls.Add(dataGridView);
            Controls.Add(panel1);
            Controls.Add(lblTiempoVuelo);
            Controls.Add(lblAlturaMaxima);
            Controls.Add(lblDesplazamiento);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Simulador de Movimiento Parabólico";
            WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        // Declaración de controles
        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.Button btnReiniciar;
        private System.Windows.Forms.TextBox txtV0;
        private System.Windows.Forms.TextBox txtAngle;
        private System.Windows.Forms.TextBox txtX0;
        private System.Windows.Forms.TextBox txtY0;
        private System.Windows.Forms.TextBox txtGravedad;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Panel panel1; // Panel para dibujar la trayectoria
        private System.Windows.Forms.Label lblTiempoVuelo;
        private System.Windows.Forms.Label lblAlturaMaxima;
        private System.Windows.Forms.Label lblDesplazamiento;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblV0;
        private System.Windows.Forms.Label lblAngle;
        private System.Windows.Forms.Label lblX0;
        private System.Windows.Forms.Label lblY0;
        private System.Windows.Forms.Label lblGravedad;
        private System.Windows.Forms.Label lblResultados;
        private System.Windows.Forms.Label lblDatosSimulacion;
        private System.Windows.Forms.Label lblVisualizacion;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}