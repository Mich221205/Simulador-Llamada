using System;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Threading; // Para Timer

namespace SimuladorLlamadas
{
    public class FrmSimuladorLlamadas : Form
    {
        private TextBox txtNumero;
        private Button[] botonesNumeros;
        private Button btnAsterisco;
        private Button btnNumeral;
        private Button btnLlamar;
        private Button btnLimpiar;
        private Button btnColgar;
        private Label lblEstado;
        private System.Windows.Forms.Timer timerLlamada;
        private double ultimoCostoPorSegundo = 0.0;

        private DateTime inicioLlamada;
        private string tiempoMaximo = "000000";
        private string ipServidor = "127.0.0.1";
        private int puertoServidor = 5000;

        public FrmSimuladorLlamadas()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Simulador de Llamadas - Teléfono";
            this.ClientSize = new Size(350, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.Black;

            txtNumero = new TextBox();
           // txtNumero.ReadOnly = true;
            txtNumero.Font = new Font("Segoe UI", 16F);
            txtNumero.ForeColor = Color.White;
            txtNumero.BackColor = Color.FromArgb(30, 30, 30);
            txtNumero.BorderStyle = BorderStyle.FixedSingle;
            txtNumero.TextAlign = HorizontalAlignment.Right;
            txtNumero.Location = new Point(20, 20);
            txtNumero.Size = new Size(280, 40);
            this.Controls.Add(txtNumero);

           


            lblEstado = new Label();
            lblEstado.Text = "";
            lblEstado.Font = new Font("Segoe UI", 12F);
            lblEstado.ForeColor = Color.White;
            lblEstado.BackColor = Color.Transparent;
            lblEstado.TextAlign = ContentAlignment.MiddleCenter;
            lblEstado.Size = new Size(280, 30);
            lblEstado.Location = new Point(20, 65);
            this.Controls.Add(lblEstado);

            botonesNumeros = new Button[10];
            for (int i = 0; i <= 9; i++)
            {
                botonesNumeros[i] = new Button();
                botonesNumeros[i].Text = i.ToString();
                botonesNumeros[i].Font = new Font("Segoe UI", 14F, FontStyle.Bold);
                botonesNumeros[i].ForeColor = Color.White;
                botonesNumeros[i].BackColor = Color.FromArgb(45, 45, 45);
                botonesNumeros[i].FlatStyle = FlatStyle.Flat;
                botonesNumeros[i].FlatAppearance.BorderSize = 0;
                botonesNumeros[i].Size = new Size(60, 60);
                botonesNumeros[i].Tag = i.ToString();
                botonesNumeros[i].Click += BotonNumero_Click;
                this.Controls.Add(botonesNumeros[i]);
            }

            btnAsterisco = new Button();
            btnAsterisco.Text = "*";
            btnAsterisco.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnAsterisco.ForeColor = Color.White;
            btnAsterisco.BackColor = Color.FromArgb(45, 45, 45);
            btnAsterisco.FlatStyle = FlatStyle.Flat;
            btnAsterisco.FlatAppearance.BorderSize = 0;
            btnAsterisco.Size = new Size(60, 60);
            btnAsterisco.Tag = "*";
            btnAsterisco.Click += BotonNumero_Click;
            this.Controls.Add(btnAsterisco);

            btnNumeral = new Button();
            btnNumeral.Text = "#";
            btnNumeral.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnNumeral.ForeColor = Color.White;
            btnNumeral.BackColor = Color.FromArgb(45, 45, 45);
            btnNumeral.FlatStyle = FlatStyle.Flat;
            btnNumeral.FlatAppearance.BorderSize = 0;
            btnNumeral.Size = new Size(60, 60);
            btnNumeral.Tag = "#";
            btnNumeral.Click += BotonNumero_Click;
            this.Controls.Add(btnNumeral);

            btnLlamar = new Button();
            btnLlamar.Text = "Llamar";
            btnLlamar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLlamar.ForeColor = Color.White;
            btnLlamar.BackColor = Color.FromArgb(0, 150, 0);
            btnLlamar.FlatStyle = FlatStyle.Flat;
            btnLlamar.FlatAppearance.BorderSize = 0;
            btnLlamar.Size = new Size(100, 40);
            btnLlamar.Click += BtnLlamar_Click;
            this.Controls.Add(btnLlamar);

            btnLimpiar = new Button();
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.BackColor = Color.FromArgb(150, 0, 0);
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.FlatAppearance.BorderSize = 0;
            btnLimpiar.Size = new Size(100, 40);
            btnLimpiar.Click += BtnLimpiar_Click;
            this.Controls.Add(btnLimpiar);

            btnColgar = new Button();
            btnColgar.Text = "Colgar";
            btnColgar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnColgar.ForeColor = Color.White;
            btnColgar.BackColor = Color.FromArgb(150, 0, 0);
            btnColgar.FlatStyle = FlatStyle.Flat;
            btnColgar.FlatAppearance.BorderSize = 0;
            btnColgar.Size = new Size(100, 40);
            btnColgar.Click += BtnColgar_Click;
            this.Controls.Add(btnColgar);

            int anchoBoton = 60, altoBoton = 60, espacio = 10;
            int anchoTotalGrid = 3 * anchoBoton + 2 * espacio;
            int margenX = (this.ClientSize.Width - anchoTotalGrid) / 2;
            int margenY = 100;
            int contador = 1;

            for (int fila = 0; fila < 3; fila++)
            {
                for (int col = 0; col < 3; col++)
                {
                    botonesNumeros[contador].Location = new Point(margenX + col * (anchoBoton + espacio), margenY + fila * (altoBoton + espacio));
                    contador++;
                }
            }

            btnAsterisco.Location = new Point(margenX, margenY + 3 * (altoBoton + espacio));
            botonesNumeros[0].Location = new Point(margenX + (anchoBoton + espacio), margenY + 3 * (altoBoton + espacio));
            btnNumeral.Location = new Point(margenX + 2 * (anchoBoton + espacio), margenY + 3 * (altoBoton + espacio));

            int anchoTotalBotones = btnLlamar.Width + btnLimpiar.Width + btnColgar.Width + espacio;
            int margenXBotones = (this.ClientSize.Width - anchoTotalBotones) / 2;
            int posYBotones = margenY + 4 * (altoBoton + espacio) + 10;

            btnLlamar.Location = new Point(margenXBotones, posYBotones);
            btnLimpiar.Location = new Point(margenXBotones + btnLlamar.Width + espacio, posYBotones);
            btnColgar.Location = new Point(margenXBotones + btnLlamar.Width + espacio + btnLimpiar.Width + espacio, posYBotones);

            timerLlamada = new System.Windows.Forms.Timer();
            timerLlamada.Interval = 1000; // 1 segundo
            timerLlamada.Tick += TimerLlamada_Tick;
        }

        private void TimerLlamada_Tick(object sender, EventArgs e)
        {
            TimeSpan duracion = DateTime.Now - inicioLlamada;
            lblEstado.Text = "En llamada: " + duracion.ToString("hh\\:mm\\:ss");
        }

        private async void BtnLlamar_Click(object sender, EventArgs e)
        {
            string numero = txtNumero.Text.Trim();
            if (string.IsNullOrEmpty(numero))
            {
                MessageBox.Show("Ingrese un número válido antes de llamar.");
                return;
            }

            string idTelefono = "1234567890123456";
            string idTarjeta = "9876543210987654321";
            string coordenadas = "9.9333,-84.0833";
            string destino = numero;
            int tipoLlamada = 1;

            if (numero == "#9090*")
            {
                string tramaConsulta = JsonConvert.SerializeObject(new
                {
                    tipo_transaccion = 2,
                    telefono = "87654321"
                });

                string respuestaConsulta = await SimuladorUtils.EnviarTramaAsync(tramaConsulta, ipServidor, puertoServidor);

                if (!string.IsNullOrWhiteSpace(respuestaConsulta) && respuestaConsulta.Trim().StartsWith("{"))
                {
                    try
                    {
                        dynamic jsonResp = JsonConvert.DeserializeObject(respuestaConsulta);
                        if (jsonResp.status == "OK")
                        {
                            string saldo = (string)jsonResp.saldo;
                            MessageBox.Show($"Saldo actual: ₡{saldo}", "Consulta de saldo");
                        }
                        else
                        {
                            MessageBox.Show($"Error en la consulta de saldo.\n{respuestaConsulta}", "Respuesta inesperada");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al procesar la respuesta JSON:\n" + ex.Message, "Respuesta inesperada");
                    }
                }
                else
                {
                    MessageBox.Show("Respuesta vacía o inválida del servidor al consultar saldo.");
                }

                return;
            }

            string trama = SimuladorUtils.CrearTramaSolicitud(numero, idTelefono, idTarjeta, coordenadas, destino, tipoLlamada);
            string respuesta = await SimuladorUtils.EnviarTramaAsync(trama, ipServidor, puertoServidor);

            Console.WriteLine("Respuesta recibida del proveedor:");
            Console.WriteLine(respuesta);

            if (!string.IsNullOrWhiteSpace(respuesta) && respuesta.Trim().StartsWith("{"))
            {
                try
                {
                    dynamic jsonResp = JsonConvert.DeserializeObject(respuesta);
                    if (jsonResp.status == "OK")
                    {
                        string tiempoRecibido = (string)jsonResp.tiempo;
                        string costoRecibido = (string)jsonResp.costo;

                        Console.WriteLine($"Tiempo recibido: {tiempoRecibido}");
                        Console.WriteLine($"Costo recibido: {costoRecibido}");

                        int h = int.Parse(tiempoRecibido.Substring(0, 2));
                        int m = int.Parse(tiempoRecibido.Substring(2, 2));
                        int s = int.Parse(tiempoRecibido.Substring(4, 2));
                        double segundos = h * 3600 + m * 60 + s;

                        double costoTotal = double.Parse(costoRecibido) / 100.0;

                        ultimoCostoPorSegundo = segundos > 0 ? costoTotal / segundos : 0;

                        Console.WriteLine($"Segundos totales: {segundos}");
                        Console.WriteLine($"Costo total en colones: ₡{costoTotal}");
                        Console.WriteLine($"Costo por segundo calculado: ₡{ultimoCostoPorSegundo}");

                        tiempoMaximo = tiempoRecibido;
                        inicioLlamada = DateTime.Now;
                        timerLlamada.Start();
                        lblEstado.Text = "En llamada: 00:00:00";
                        MessageBox.Show("Llamada autorizada y comenzada.");
                    }
                    else
                    {
                        MessageBox.Show($"Error: {jsonResp.mensaje}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al procesar respuesta del servidor: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Respuesta inválida o vacía del servidor.");
            }
        }
        private async void BtnColgar_Click(object sender, EventArgs e)
        {
            timerLlamada.Stop();
            lblEstado.Text = "Llamada finalizada.";

            string destino = txtNumero.Text.Trim();
            string telefonoOrigen = "88765432";
            string idTelefono = "1234567890123456";
            string idTarjeta = "1234567890123456789";
            string coordenadas = "9.9333,-84.0833";

            TimeSpan duracion = DateTime.Now - inicioLlamada;
            string duracionFormateada = SimuladorUtils.FormatearDuracion(duracion);
            string fecha = DateTime.Now.ToString("yyyyMMdd");
            string hora = DateTime.Now.ToString("HHmmss");

            Console.WriteLine($"Duración real: {duracion.TotalSeconds} segundos");
            Console.WriteLine($"Costo por segundo usado: ₡{ultimoCostoPorSegundo}");

            double costoTotal = ultimoCostoPorSegundo * duracion.TotalSeconds;

            Console.WriteLine($"Costo total calculado: ₡{costoTotal}");

            var tramaFinal = new
            {
                tipo_transaccion = 5,
                telefono = telefonoOrigen,
                destino = destino,
                fecha = fecha,
                hora = hora,
                duracion = duracionFormateada,
                costo = costoTotal
            };

            string json = JsonConvert.SerializeObject(tramaFinal);
            Console.WriteLine("Trama enviada al servidor:");
            Console.WriteLine(json);

            string respuesta = await SimuladorUtils.EnviarTramaAsync(json, ipServidor, puertoServidor);

            SimuladorUtils.GuardarBitacoraAsync(new
            {
                telefonoOrigen,
                idTelefono,
                idTarjeta,
                coordenadas,
                Transaccion = "finalización",
                destino,
                duracion = duracionFormateada,
                fecha,
                hora,
                costo = costoTotal
            });

            MessageBox.Show(
                $"Llamada finalizada.\nDuración: {duracionFormateada}\nCosto: ₡{costoTotal:N2}",
                "Fin de llamada"
            );
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            txtNumero.Clear();
            lblEstado.Text = "";
        }

        private void BotonNumero_Click(object sender, EventArgs e)
        {
            if (sender is Button boton && boton.Tag != null)
            {
                txtNumero.Text += boton.Tag.ToString();
            }
        }
    }
}