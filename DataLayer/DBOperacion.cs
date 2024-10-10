using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace DataLayer
{
    public class DBOperacion : DBConexion
    {
        public DataTable Consultar(string pConsulta, Dictionary<string, object> parametros)
        {
            DataTable Resultado = new DataTable();
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();
            MySqlCommand Comando = new MySqlCommand();

            try
            {
                if (base.Conectar()) // Asegúrate de que esta función conecta a la base de datos
                {
                    Comando.Connection = base._CONEXION;
                    Comando.CommandType = CommandType.Text;
                    Comando.CommandText = pConsulta;

                    // Añadir los parámetros al comando
                    foreach (var parametro in parametros)
                    {
                        Comando.Parameters.AddWithValue(parametro.Key, parametro.Value);
                    }

                    Adaptador.SelectCommand = Comando;
                    Adaptador.Fill(Resultado);
                }
            }
            catch (Exception ex)
            {
                Resultado = new DataTable();
                Console.WriteLine(ex.Message);
            }
            finally
            {
                base.Desconectar(); // Asegurarse de que la conexión se cierre incluso si hay una excepción
            }

            return Resultado;
        }
        public DataTable Consultar(string pConsulta)
        {
            DataTable Resultado = new DataTable();
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();
            MySqlCommand Comando = new MySqlCommand();
            try
            {
                if (base.Conectar()) // Base para acceder a los miembros de la clase base
                {
                    Comando.Connection = base._CONEXION;
                    Comando.CommandType = System.Data.CommandType.Text;
                    Comando.CommandText = pConsulta;
                    Adaptador.SelectCommand = Comando;
                    Adaptador.Fill(Resultado);
                }
            }
            catch (Exception ex)
            {
                Resultado = new DataTable();
                Console.WriteLine(ex.Message);
            }
            finally
            {
                base.Desconectar(); // Asegurarse de que la conexión se cierre incluso si hay una excepción
            }
            return Resultado;
        }

        public Int32 EjecutarSentenciaYObtenerID(String pSentencia, Dictionary<string, object> parametros)
        {
            Int32 idGenerado = -1; // Valor por defecto si no se puede obtener el ID

            using (MySqlCommand comando = new MySqlCommand())
            {
                try
                {
                    if (base.Conectar())
                    {
                        comando.Connection = base._CONEXION;
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.CommandText = pSentencia;

                        // Agregar parámetros
                        if (parametros != null)
                        {
                            foreach (var parametro in parametros)
                            {
                                comando.Parameters.AddWithValue(parametro.Key, parametro.Value);
                            }
                        }

                        comando.ExecuteNonQuery();

                        // Obtener el ID generado por la sentencia SQL
                        comando.CommandText = "SELECT LAST_INSERT_ID();";
                        idGenerado = Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al ejecutar sentencia: " + ex.Message);
                }
                finally
                {
                    Desconectar();
                }
            }

            return idGenerado;
        }


        public Int32 EjecutarSentencia(String pSentencia)
        {
            Int32 FilasAfectadas = 0;
            MySqlCommand Comando = new MySqlCommand();
            try
            {
                if (base.Conectar())
                {
                    Comando.Connection = base._CONEXION;
                    Comando.CommandType = System.Data.CommandType.Text;
                    Comando.CommandText = pSentencia;
                    FilasAfectadas = Comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al ejecutar sentencia: " + ex.Message);
                FilasAfectadas = -1;
            }
            finally
            {
                Desconectar();
            }
            return FilasAfectadas;
        }
        public int EjecutarSentencia(String pSentencia, Dictionary<string, object> parametros)
        {
            int FilasAfectadas = 0;
            MySqlCommand Comando = new MySqlCommand();
            try
            {
                if (base.Conectar())
                {
                    Comando.Connection = base._CONEXION;
                    Comando.CommandType = System.Data.CommandType.Text;
                    Comando.CommandText = pSentencia;

                    // Agregar parámetros
                    if (parametros != null)
                    {
                        foreach (var parametro in parametros)
                        {
                            Comando.Parameters.AddWithValue(parametro.Key, parametro.Value);
                        }
                    }

                    // Construir la consulta final
                    string consultaFinal = pSentencia;
                    foreach (var parametro in parametros)
                    {
                        consultaFinal = consultaFinal.Replace(parametro.Key, $"'{parametro.Value}'");
                    }

                    // Mostrar la consulta final en un MessageBox
                    MessageBox.Show(consultaFinal, "Consulta SQL", MessageBoxButton.OK, MessageBoxImage.Information);
                    Console.WriteLine(consultaFinal);
                   FilasAfectadas = Comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar sentencia: " + ex.ToString());
                FilasAfectadas = -1;
            }
            finally
            {
                Desconectar();
            }
            return FilasAfectadas;
        }



    }
}
