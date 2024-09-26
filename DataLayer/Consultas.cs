using System.Data;
using MySql.Data.MySqlClient;

namespace DataLayer
{
    public static class Consultas
    {
        public static DataTable PedidosCompras(int id)
        {
            DataTable Resultado = new DataTable();
            String Consulta = $@"SELECT *
            FROM 
                pedidocompras dpv
     
            WHERE 
                dpv.IDPedido = '{id}';";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Resultado;
        }

        public static DataTable ProductosPocoStock()
        {
            DataTable Resultado = new DataTable();
            String Consulta = $@"SELECT Nombre, Cantidad
            FROM productos
            WHERE Cantidad < 15 and esPlatillo = 0;
            ";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Resultado;
        }
        public static DataTable ProductosNoContables()
        {
            DataTable Resultado = new DataTable();
            String Consulta = @"SELECT IDProducto, Nombre, Precio, CostoUnitario, EsPlatillo, IDCategoria, Cantidad
            FROM productos
            WHERE EsPlatillo = 0;

            ";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);
            }
            catch (Exception)
            {

            }
            return Resultado;
        }
        public static DataTable CATEGORIAS()
        {
            DataTable Resultado = new DataTable();
            String Consulta = @"SELECT * FROM categorias ORDER BY Nombre ASC;";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);
            }
            catch (Exception)
            {

            }
            return Resultado;
        }
        public static DataTable CATEGORIAS(int pagina, int tamanoPagina)
        {
            DataTable Resultado = new DataTable();
            string Consulta = @"SELECT * FROM categorias ORDER BY Categoria Asc
    LIMIT @offset, @limit;"; // Para SQL Server

            // Crear un diccionario para los parámetros
            var parametros = new Dictionary<string, object>
    {
        { "@offset", (pagina - 1) * tamanoPagina },
        { "@limit", tamanoPagina }
    };

            DBOperacion operacion = new DBOperacion();
            try
            {
                // Ejecutar la consulta con los parámetros
                Resultado = operacion.Consultar(Consulta, parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener roles: " + ex.Message);
                // Manejo adicional de errores puede ser implementado aquí
            }

            return Resultado; // Devuelve el DataTable con los resultados
        }
        public static DataTable PedidosVentas(string inicio, string final)
        {
            DataTable Resultado = new DataTable();
            String Consulta = $@"SELECT
                DISTINCT pv.IDPedido,
                pv.Cliente,
                pv.FechaPedido,
                pv.Estado,
                pv.Comentarios,
                SUM(dpv.Cantidad * dpv.Precio) AS 'Total',
                CASE 
                    WHEN v.IDVentas IS NOT NULL THEN 'Pagado'
                    ELSE 'No Pagado'
                END AS 'EstadoPago'
            FROM
                pedidoventas pv
                LEFT JOIN detallepedidoventas dpv ON pv.IDPedido = dpv.IDPedido
                LEFT JOIN ventas v ON pv.IDPedido = v.IDPedido
            WHERE
                CAST(pv.FechaPedido AS DATE) BETWEEN '{inicio}' AND '{final}'
            GROUP BY
                pv.IDPedido,
                pv.Cliente,
                pv.FechaPedido,
                pv.Estado,
                pv.Comentarios,
                v.IDVentas
            ORDER BY
                pv.FechaPedido DESC; ";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);
            }
            catch (Exception)
            {

            }
            return Resultado;
        }
        public static DataTable PROVEEDORES()
        {
            DataTable Resultado = new DataTable();
            String Consulta = @"SELECT * FROM proveedores ORDER BY Nombre ASC;";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);
            }
            catch (Exception)
            {

            }
            return Resultado;
        }
        public static DataTable Empleados()
        {
            DataTable Resultado = new DataTable();
            String Consulta = @"SELECT * FROM empleados ORDER BY Nombre ASC;";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);
            }
            catch (Exception)
            {

            }
            return Resultado;
        }
        public static DataTable Empleados(int pagina, int tamanoPagina)
        {
            DataTable Resultado = new DataTable();
            string Consulta = @"
    SELECT * FROM empleados
    ORDER BY nombre ASC
    LIMIT @offset, @limit;"; // Para SQL Server

            // Crear un diccionario para los parámetros
            var parametros = new Dictionary<string, object>
    {
        { "@offset", (pagina - 1) * tamanoPagina },
        { "@limit", tamanoPagina }
    };

            DBOperacion operacion = new DBOperacion();
            try
            {
                // Ejecutar la consulta con los parámetros
                Resultado = operacion.Consultar(Consulta, parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener roles: " + ex.Message);
                // Manejo adicional de errores puede ser implementado aquí
            }

            return Resultado; // Devuelve el DataTable con los resultados
        }
        public static DataTable ROLES()
        {
            DataTable Resultado = new DataTable();
            String Consulta = @"SELECT * FROM roles ORDER BY Rol ASC;";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);
            }
            catch (Exception)
            {

            }
            return Resultado;
        }
        public static DataTable Usuarios()
        {
            DataTable Resultado = new DataTable();
            String Consulta = @"SELECT * FROM usuarios ORDER BY usuarios ASC;";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);
            }
            catch (Exception)
            {

            }
            return Resultado;
        }
        public static DataTable ROLES(int pagina, int tamanoPagina)
        {
            DataTable Resultado = new DataTable();
            string Consulta = @"
    SELECT * FROM roles
    ORDER BY Rol ASC
    LIMIT @offset, @limit;"; // Para SQL Server

            // Crear un diccionario para los parámetros
            var parametros = new Dictionary<string, object>
    {
        { "@offset", (pagina - 1) * tamanoPagina },
        { "@limit", tamanoPagina }
    };

            DBOperacion operacion = new DBOperacion();
            try
            {
                // Ejecutar la consulta con los parámetros
                Resultado = operacion.Consultar(Consulta, parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener roles: " + ex.Message);
                // Manejo adicional de errores puede ser implementado aquí
            }

            return Resultado; // Devuelve el DataTable con los resultados
        }
        public static DataTable PRODUCTOS()
        {
            DataTable Resultado = new DataTable();
            String Consulta = @"
    SELECT
        p.idProducto, 
        p.nombreProducto AS Nombre,
        p.precio, 
        p.costoUnitario,
        p.cantidadProducto AS Cantidad,
        c.nombre AS NombreC,
        pr.nombreProveedor AS NombreProveedor
    FROM
        Productos p
        INNER JOIN Categorias c ON p.idCategoria = c.idCategoria
        LEFT JOIN Proveedores pr ON p.idProveedor = pr.idProveedor
    ORDER BY
        p.nombreProducto ASC;
    ";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);
            }
            catch (Exception)
            {

            }
            return Resultado;
        }
        public static DataTable PRODUCTOS(int pagina, int tamanoPagina)
        {
            DataTable Resultado = new DataTable();
            String Consulta = @"
        SELECT
    p.idProducto, 
    p.nombreProducto,
    p.precio, 
    p.costoUnitario,
    p.cantidadProducto,
    c.idCategoria,
    c.categoria,
     pr.proveedor,
pr.idProveedor
     FROM
    Productos p
    INNER JOIN Categorias c ON p.idCategoria = c.idCategoria
    LEFT JOIN Proveedores pr ON p.idProveedor = pr.idProveedor
ORDER BY
    p.nombreProducto ASC

LIMIT @offset, @limit;
    "; // Agregar LIMIT para la paginación

            // Crear un diccionario para los parámetros
            var parametros = new Dictionary<string, object>
    {
        { "@offset", (pagina - 1) * tamanoPagina },
        { "@limit", tamanoPagina }
    };

            DBOperacion operacion = new DBOperacion();
            try
            {
                // Usar el método Consultar con parámetros
                Resultado = operacion.Consultar(Consulta, parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la consulta: " + ex.Message);
            }

            return Resultado;
        }
        public static DataTable USUARIOS(int pagina, int tamanoPagina)
        {
            DataTable resultado = new DataTable();
            string consulta = @"
        SELECT 
    u.IDUsuario, 
    u.Usuario, 
    u.Contrasenia, 
    u.idEmpleado, 
    u.IDRol, 
    r.Rol AS Rol,
    e.nombresEmpleado AS Empleado
FROM 
    usuario u
    INNER JOIN roles r ON u.IDRol = r.IDRol
    INNER JOIN empleados e ON u.idEmpleado = e.idEmpleados
ORDER BY 
    u.Usuario ASC
        LIMIT @limit OFFSET @offset;"; // Cambiar el orden de OFFSET y LIMIT

            // Crear un diccionario para los parámetros
            var parametros = new Dictionary<string, object>
    {
        { "@offset", (pagina - 1) * tamanoPagina }, // Cálculo de desplazamiento
        { "@limit", tamanoPagina } // Tamaño de la página
    };

            DBOperacion operacion = new DBOperacion();
            try
            {
                // Usar el método Consultar con parámetros
                resultado = operacion.Consultar(consulta, parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la consulta: " + ex.Message);
                // Podrías lanzar la excepción o manejarla de otra forma, según lo que necesites
            }

            return resultado;
        }

        public static DataTable Permisos(int pagina, int tamanoPagina)
        {
            DataTable Resultado = new DataTable();
            String Consulta = @"
        SELECT 
            *
        FROM 
            Permisos
        ORDER BY 
            Rol
        LIMIT @offset, @limit;"; // Agregar LIMIT para la paginación

            // Crear un diccionario para los parámetros
            var parametros = new Dictionary<string, object>
    {
        { "@offset", (pagina - 1) * tamanoPagina },
        { "@limit", tamanoPagina }
    };

            DBOperacion operacion = new DBOperacion();
            try
            {
                // Usar el método Consultar con parámetros
                Resultado = operacion.Consultar(Consulta, parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la consulta: " + ex.Message);
            }

            return Resultado;
        }

        public static DataTable ProductosNoIngredientes()
        {
            DataTable Resultado = new DataTable();
            String Consulta = @"SELECT
                p.IDProducto,
                p.Nombre,
                p.Precio,
                p.EsPlatillo,
                p.Cantidad
            FROM
                productos p
                INNER JOIN categorias c ON p.IDCategoria = c.IDCategoria
            WHERE
                c.EsIngrendiente = 0
                AND (p.Cantidad > 0 OR (p.Cantidad = 0 AND p.EsPlatillo = 1));
            ";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);
            }
            catch (Exception)
            {

            }
            return Resultado;
        }
        public static DataTable DetallePedidoVentas(int id)
        {
            DataTable Resultado = new DataTable();
            String Consulta = $@"SELECT 
             
       
                p.IDProducto,
                p.Nombre AS Producto,
                dpv.Cantidad,
                dpv.Precio,
                (dpv.Cantidad * dpv.Precio) AS Importe
            FROM 
                detallepedidoventas dpv
            INNER JOIN 
                productos p ON dpv.IDProducto = p.IDProducto
            WHERE 
                dpv.IDPedido = '{id}';";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);
                Console.WriteLine("el id es" + id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Resultado;
        }

        public static DataTable PedidosCompras(string inicio, string final)
        {


            DataTable Resultado = new DataTable();
            string Consulta = $@"
        SELECT
           DISTINCT pc.IDPedido,
            pc.IDProveedor,
            ps.Nombre AS Proveedor,
            pc.FechaPedido,

            pc.Estado,
            pc.Comentarios,
            SUM(dpv.Cantidad * dpv.Precio) AS Total,
            CASE 
                WHEN c.IDCompras IS NOT NULL THEN 'Pagado'
                ELSE 'No Pagado'
            END AS EstadoPago
        FROM
            pedidocompras pc
            LEFT JOIN detallepedidocompras dpv ON pc.IDPedido = dpv.IDPedido
            LEFT JOIN compras c ON pc.IDPedido = c.IDPedido
            LEFT JOIN proveedores ps ON pc.IDProveedor = ps.IDProveedor
        WHERE
           CAST(pc.FechaPedido AS DATE) BETWEEN '{inicio}' AND '{final}'
        GROUP BY
            pc.IDPedido,
            pc.IDProveedor,
            ps.Nombre,
            pc.FechaPedido,
            pc.Estado,
            pc.Comentarios,
            c.IDCompras
        ORDER BY
            pc.FechaPedido DESC;";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);
            }
            catch (Exception)
            {

            }
            return Resultado;
        }

        public static DataTable Permisos()
        {
            DataTable Resultado = new DataTable();
            string Consulta = @"SELECT 
                p.IDPermiso, 
                p.IDRol, 
                p.IDOpcion,  
                r.Rol AS Rol,
                o.Opcion AS Opcion
            FROM 
                permisos p
                INNER JOIN roles r ON p.IDRol = r.IDRol
                INNER JOIN opciones o ON p.IDOpcion = o.IDOpcion
            ORDER BY 
                p.IDPermiso ASC;";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);
            }
            catch (Exception)
            {

            }
            return Resultado;


        }

        public static DataTable OPCIONES()
        {
            DataTable Resultado = new DataTable();
            String Consulta = @"SELECT * FROM opciones ORDER BY Opcion ASC;";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);
            }
            catch (Exception)
            {

            }
            return Resultado;
        }

        public static DataTable INVENTARIOPRODUCTOS()
        {
            DataTable Resultado = new DataTable();
            String Consulta = @"SELECT DISTINCT
                p.IDProducto, 
                p.Nombre,
                p.Precio, 
                p.CostoUnitario,
                p.IDCategoria, 
                COALESCE(MAX(cs.FechaCompra), 'N/A') AS FechaCompra,
                c.Nombre AS NombreC,
	            (p.Cantidad* p.Precio) as TotalPrecio,
                p.Cantidad as Existencia
            FROM
                productos p
                left join detallepedidocompras dpc on dpc.IDProducto = p.IDProducto
                left join compras cs on cs.IDPedido = dpc.IDPedido

                INNER JOIN categorias c ON p.IDCategoria = c.IDCategoria
            Where
                c.EsIngrendiente = 0
            GROUP BY
                p.IDProducto, 
                p.Nombre,
                p.CostoUnitario, 
                p.IDCategoria, 
                c.Nombre
            ORDER BY
                p.Nombre ASC;";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);
            }
            catch (Exception)
            {

            }
            return Resultado;
        }
        public static DataTable INVENTARIOINGREDIENTES()
        {
            DataTable Resultado = new DataTable();
            String Consulta = @"SELECT DISTINCT
                p.IDProducto, 
                p.Nombre,
                p.CostoUnitario, 
                p.IDCategoria, 
                COALESCE(MAX(cs.FechaCompra), 'N/A') AS FechaCompra,
                c.Nombre AS NombreC,
                (p.Cantidad * p.CostoUnitario) AS 'Total costo',
                p.Cantidad AS Existencia
            FROM
                productos p
                LEFT JOIN detallepedidocompras dpc ON dpc.IDProducto = p.IDProducto
                LEFT JOIN compras cs ON cs.IDPedido = dpc.IDPedido
                INNER JOIN categorias c ON p.IDCategoria = c.IDCategoria
            WHERE 
                c.EsIngrendiente = 1
            GROUP BY
                p.IDProducto, 
                p.Nombre,
                p.CostoUnitario, 
                p.IDCategoria, 
                c.Nombre
            ORDER BY
                p.Nombre ASC;";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);
            }
            catch (Exception)
            {

            }
            return Resultado;
        }

        public static DataTable DetallePedidoCompras(int id)
        {
            DataTable Resultado = new DataTable();
            String Consulta = $@"SELECT 
             
       
                p.IDProducto,
                p.Nombre AS Producto,
                dpv.Cantidad,
                dpv.Precio as CostoUnitario,
                (dpv.Cantidad * dpv.Precio) AS Importe
            FROM 
                detallepedidocompras dpv
            INNER JOIN 
                productos p ON dpv.IDProducto = p.IDProducto
            WHERE 
                dpv.IDPedido = '{id}';";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);
                Console.WriteLine("el id es" + id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Resultado;
        }
        public static DataTable SEGUN_PERIODO_INGREDIENTES(string pFechaInicio, string pFechaFinal)
        {
            DataTable Resultado = new DataTable();
            String Consulta = @"SELECT DISTINCT
                p.IDProducto, 
                p.Nombre,
                p.CostoUnitario, 
                p.IDCategoria, 
                COALESCE(MAX(cs.FechaCompra), 'N/A') AS FechaCompra,
                c.Nombre AS NombreC,
                (p.Cantidad * p.CostoUnitario) AS 'Total costo',
                p.Cantidad AS Existencia
            FROM
                productos p
                LEFT JOIN detallepedidocompras dpc ON dpc.IDProducto = p.IDProducto
                LEFT JOIN compras cs ON cs.IDPedido = dpc.IDPedido
                INNER JOIN categorias c ON p.IDCategoria = c.IDCategoria
            WHERE 
                c.EsIngrendiente = 1
            AND 
	            CAST(cs.FechaCompra AS DATE) between '" + pFechaInicio + "' AND '" + pFechaFinal + @"'
            GROUP BY
                p.IDProducto, 
                p.Nombre,
                p.CostoUnitario, 
                p.IDCategoria, 
                c.Nombre
            ORDER BY
                p.Nombre ASC;";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);
            }
            catch (Exception)
            {

            }
            return Resultado;
        }

        public static DataTable SEGUN_PERIODO_PRODUCTOS(string pFechaInicio, string pFechaFinal)
        {
            DataTable Resultado = new DataTable();
            String Consulta = @"SELECT
                p.IDProducto, 
                p.Nombre,
                p.Precio, 
                p.CostoUnitario, 
                cs.FechaCompra,
			
	            (p.Cantidad* p.Precio) as TotalPrecio,
                p.Cantidad as Existencia
            FROM
                productos p
                left join detallepedidocompras dpc on dpc.IDProducto = p.IDProducto
                left join compras cs on cs.IDPedido = dpc.IDPedido

                INNER JOIN categorias c ON p.IDCategoria = c.IDCategoria
            Where
                c.EsIngrendiente = 0
            AND 
	            CAST(cs.FechaCompra AS DATE) between '" + pFechaInicio + "' AND '" + pFechaFinal + @"'
            GROUP BY
                p.IDProducto, 
                p.Nombre,
                p.CostoUnitario, 
                p.IDCategoria, 
                c.Nombre,
                cs.FechaCompra
            ORDER BY
                p.Nombre ASC;";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);
            }
            catch (Exception)
            {

            }
            return Resultado;
        }

        public static DataTable VENTAS_SEGUN_PERIODO_PRODUCTOS(string pFechaInicio, string pFechaFinal)
        {
            DataTable Resultado = new DataTable();
            String Consulta = $@"SELECT
                v.IDVentas AS 'ID Venta',
                pv.Cliente AS 'Cliente',
                v.FechaVenta AS 'Fecha de Venta',
                v.Precio AS 'Precio Total',
                e.Nombre AS 'Empleado'
            FROM
                ventas v
            INNER JOIN
                empleados e ON v.IDEmpleado = e.IDEmpleado
            INNER JOIN
                pedidoventas pv ON v.IDPedido = pv.IDPedido
            WHERE
                CAST(v.FechaVenta AS DATE) BETWEEN '{pFechaInicio}' AND '{pFechaFinal}'
            ORDER BY
                v.FechaVenta DESC;";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);
            }
            catch (Exception)
            {

            }
            return Resultado;
        }

        public static DataTable COMPRAS_SEGUN_PEDIDO(string pFechaInicio, string pFechaFinal)
        {
            DataTable Resultado = new DataTable();
            String Consulta = @"SELECT DISTINCT
               c.IDCompras,
               c.FechaCompra,
               c.IDPedido,
               p.Nombre As Producto,
               e.Nombre AS Empleado,
               (p.Cantidad* p.Precio) as TotalCosto
               
               FROM 
               GestionRestauranteDB.compras c
               left join GestionRestauranteDB.detallepedidocompras dc on c.IDPedido = dc.IDPedido
               left join GestionRestauranteDB.productos p on dc.IDProducto = p.IDProducto
               INNER JOIN GestionRestauranteDB.empleados e ON c.IDEmpleado = e.IDEmpleado
               
               WHERE
                CAST(c.FechaCompra AS DATE) BETWEEN '" + pFechaInicio + "' AND '" + pFechaFinal + @"'
             GROUP BY
                c.IDCompras,
               c.FechaCompra,

               c.IDPedido,
               p.Nombre,
               e.Nombre,
               dc.Precio
             ORDER BY
               c.FechaCompra ASC; ";
            DBOperacion operacion = new DBOperacion();
            try
            {
                Resultado = operacion.Consultar(Consulta);
            }
            catch (Exception)
            {

            }
            return Resultado;
        }
        public static decimal VENTAS_TOTAL()
        {
            decimal total = 0;
            String consulta = @"SELECT SUM(Precio) AS 'Precio' FROM ventas;";
            DBOperacion operacion = new DBOperacion();
            try
            {
                DataTable resultado = operacion.Consultar(consulta);
                if (resultado.Rows.Count > 0)
                {
                    total = Convert.ToDecimal(resultado.Rows[0]["Precio"]);
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción si es necesario, por ejemplo, registrando el error
                Console.WriteLine(ex.Message);
            }
            return total;
        }

        public static DataRow PRODUCTOS_MAS_VENDIDO()
        {
            DataRow resultado = null;
            String consulta = @"SELECT
                p.Nombre AS 'Producto',
                v.Precio
            FROM
                ventas v
            INNER JOIN
                empleados e ON v.IDEmpleado = e.IDEmpleado
            INNER JOIN
                pedidoventas pv ON v.IDPedido = pv.IDPedido
            INNER JOIN
                detallepedidoventas dpv ON pv.IDPedido = dpv.IDPedido
            INNER JOIN
                productos p ON dpv.IDProducto = p.IDProducto
            ORDER BY
                v.Precio DESC
            LIMIT 1;";
            DBOperacion operacion = new DBOperacion();
            try
            {
                DataTable tablaResultado = operacion.Consultar(consulta);
                if (tablaResultado.Rows.Count > 0)
                {
                    resultado = tablaResultado.Rows[0];
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción si es necesario
                Console.WriteLine(ex.Message);
            }
            return resultado;
        }
        public static int ContarProductos()
        {
            int totalUsuarios = 0;
            string consulta = "SELECT COUNT(*) FROM Productos"; // Cambia 'usuarios' al nombre real de tu tabla

            DBOperacion operacion = new DBOperacion();
            try
            {
                // Ejecutar la consulta y obtener el resultado
                DataTable resultado = operacion.Consultar(consulta);
                if (resultado.Rows.Count > 0)
                {
                    totalUsuarios = Convert.ToInt32(resultado.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción si es necesario
                Console.WriteLine("Error al contar usuarios: " + ex.Message);
            }
            return totalUsuarios;
        }
        public static int ContarPermisos()
        {
            int totalUsuarios = 0;
            string consulta = "SELECT COUNT(*) FROM Permisos"; // Cambia 'usuarios' al nombre real de tu tabla

            DBOperacion operacion = new DBOperacion();
            try
            {
                // Ejecutar la consulta y obtener el resultado
                DataTable resultado = operacion.Consultar(consulta);
                if (resultado.Rows.Count > 0)
                {
                    totalUsuarios = Convert.ToInt32(resultado.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción si es necesario
                Console.WriteLine("Error al contar usuarios: " + ex.Message);
            }
            return totalUsuarios;
        }

        public static int ContarUsuarios()
        {
            int totalUsuarios = 0;
            string consulta = "SELECT COUNT(*) FROM usuarios"; // Cambia 'usuarios' al nombre real de tu tabla

            DBOperacion operacion = new DBOperacion();
            try
            {
                // Ejecutar la consulta y obtener el resultado
                DataTable resultado = operacion.Consultar(consulta);
                if (resultado.Rows.Count > 0)
                {
                    totalUsuarios = Convert.ToInt32(resultado.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción si es necesario
                Console.WriteLine("Error al contar usuarios: " + ex.Message);
            }
            return totalUsuarios;
        }
        public static int ContarRoles()
        {
            int totalUsuarios = 0;
            string consulta = "SELECT COUNT(*) FROM Roles"; // Cambia 'usuarios' al nombre real de tu tabla

            DBOperacion operacion = new DBOperacion();
            try
            {
                // Ejecutar la consulta y obtener el resultado
                DataTable resultado = operacion.Consultar(consulta);
                if (resultado.Rows.Count > 0)
                {
                    totalUsuarios = Convert.ToInt32(resultado.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción si es necesario
                Console.WriteLine("Error al contar usuarios: " + ex.Message);
            }
            return totalUsuarios;
        }
        public static int ContarEmpleados()
        {
            int totalUsuarios = 0;
            string consulta = "SELECT COUNT(*) FROM Empleados"; // Cambia 'usuarios' al nombre real de tu tabla

            DBOperacion operacion = new DBOperacion();
            try
            {
                // Ejecutar la consulta y obtener el resultado
                DataTable resultado = operacion.Consultar(consulta);
                if (resultado.Rows.Count > 0)
                {
                    totalUsuarios = Convert.ToInt32(resultado.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción si es necesario
                Console.WriteLine("Error al contar usuarios: " + ex.Message);
            }
            return totalUsuarios;
        }
        public static int ContarCategorias()
        {
            int totalUsuarios = 0;
            string consulta = "SELECT COUNT(*) FROM Categorias"; // Cambia 'usuarios' al nombre real de tu tabla

            DBOperacion operacion = new DBOperacion();
            try
            {
                // Ejecutar la consulta y obtener el resultado
                DataTable resultado = operacion.Consultar(consulta);
                if (resultado.Rows.Count > 0)
                {
                    totalUsuarios = Convert.ToInt32(resultado.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                // Maneja la excepción si es necesario
                Console.WriteLine("Error al contar usuarios: " + ex.Message);
            }
            return totalUsuarios;
        }
    }
}

