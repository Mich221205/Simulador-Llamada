using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SimuladorLlamadas
{

    public class SimuladorUtils
    {

        // Clave y vector de inicialización (debes cambiarlos por seguridad real)
        private static readonly byte[] key = Encoding.UTF8.GetBytes("1234567890123456");
        private static readonly byte[] iv = Encoding.UTF8.GetBytes("6543210987654321");

        public static string CifrarAES(string texto)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(texto);
                    sw.Close();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static async Task<string> EnviarTramaAsync(string json, string ip, int puerto, int timeoutMs = 10000)
        {
            using (TcpClient client = new TcpClient())
            {
                var connectTask = client.ConnectAsync(ip, puerto);
                if (await Task.WhenAny(connectTask, Task.Delay(timeoutMs)) != connectTask)
                    throw new TimeoutException("Tiempo de conexión excedido.");

                using (NetworkStream stream = client.GetStream())
                using (StreamWriter writer = new StreamWriter(stream))
                using (StreamReader reader = new StreamReader(stream))
                {
                    stream.ReadTimeout = timeoutMs;
                    stream.WriteTimeout = timeoutMs;

                    await writer.WriteAsync(json + "\n");
                    await writer.FlushAsync();

                    string respuestaCompleta = await reader.ReadToEndAsync();
                    int primerObjetoFin = respuestaCompleta.IndexOf('}') + 1;
                    if (primerObjetoFin > 0)
                    {
                        return respuestaCompleta.Substring(0, primerObjetoFin).Trim();
                    }
                    return null;
                }
            }
        }
        public static string CrearTramaSolicitud(string telefono, string idTel, string idChip, string coordenadas, string destino, int tipoLlamada)
        {
            var trama = new
            {
                telefono = telefono,
                identificadorTel = CifrarAES(idTel),
                identificadorChip = CifrarAES(idChip),
                coordenadas,
                tipo_transaccion = 1,
                destino = CifrarAES(destino),
                tipo_llamada = tipoLlamada
            };
            return JsonConvert.SerializeObject(trama);
        }

        public static string CrearTramaLlamada(string telefono, string idTel, string idChip, string coordenadas, string destino, string tiempo)
        {
            var trama = new
            {
                telefono = CifrarAES(telefono),
                identificadorTel = CifrarAES(idTel),
                identificadorChip = CifrarAES(idChip),
                coordenadas,
                tipo_transaccion = 3,
                destino = CifrarAES(destino),
                tiempo
            };
            return JsonConvert.SerializeObject(trama);
        }

        public static string CrearTramaFinalizacion(string telefono, string idTel, string idChip, string coordenadas, string destino)
        {
            var trama = new
            {
                telefono = CifrarAES(telefono),
                identificadorTel = CifrarAES(idTel),
                identificadorChip = CifrarAES(idChip),
                coordenadas,
                tipo_transaccion = 5,
                destino = CifrarAES(destino),
            };
            return JsonConvert.SerializeObject(trama);
        }

        public static string CrearTramaConsultaSaldo(string telefono, string idTel, string idChip, string coordenadas)
        {
            var trama = new
            {
                telefono = CifrarAES(telefono),
                identificadorTel = CifrarAES(idTel),
                identificadorChip = CifrarAES(idChip),
                coordenadas,
                tipo_transaccion = 2
            };
            return JsonConvert.SerializeObject(trama);
        }

        public static void GuardarBitacoraAsync(object jsonObject)
        {
            Task.Run(() =>
            {
                try
                {
                    string ruta = "bitacora.txt";
                    string fecha = DateTime.Now.ToString("dd/MM/yyyy");
                    string json = JsonConvert.SerializeObject(jsonObject, Newtonsoft.Json.Formatting.Indented);
                    string entrada = $"{fecha}: {json}{Environment.NewLine}";
                    lock (typeof(SimuladorUtils))
                    {
                        File.AppendAllText(ruta, entrada);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar bitácora: {ex.Message}");
                }
            });
        }

        public static string FormatearDuracion(TimeSpan duracion)
        {
            return $"{duracion.Hours:D2}{duracion.Minutes:D2}{duracion.Seconds:D2}";
        }


    }//class
}//name
