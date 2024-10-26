Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Data
Imports System.Data.SqlClient

Namespace isnp115024_isnp201823_isnp205923_bloque2
    Friend Class db_conexion

        Private miConexion As SqlConnection
        Private miComando As SqlCommand
        Private miLector As SqlDataReader
        Private ds As New DataSet()

        Public Sub New()
            miConexion = New SqlConnection("Data Source=.\SQLEXPRESS;Initial Catalog=bd_isnp115024_isnp201823_isnp205923;Integrated Security=True")
            miConexion.Open()
        End Sub

        ' Add methods for this class if needed

    End Class
End Namespace

Namespace academica
    Friend Class db_conexion

        Private miConexion As New SqlConnection() ' Para conectarnos a la base de datos
        Private miComando As New SqlCommand() ' Para ejecutar comandos SQL en la base de datos
        Private miAdaptador As New SqlDataAdapter() ' Intermediario entre la base de datos y la aplicación
        Private ds As New DataSet() ' Arquitectura de la base de datos en memoria RAM

        ' Constructor: Inicializa la conexión con la base de datos
        Public Sub New()
            miConexion.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\db_academica.mdf;Integrated Security=True"
            miConexion.Open() ' Abrimos la conexión
        End Sub

        ' Método para obtener los datos de la tabla clientes
        Public Function obtenerDatos() As DataSet
            ds.Clear() ' Limpiamos el dataset
            miComando.Connection = miConexion ' Asignamos la conexión al comando
            miComando.CommandText = "SELECT * FROM clientes" ' Consulta SQL para obtener todos los registros de clientes

            miAdaptador.SelectCommand = miComando ' Asignamos el comando al adaptador
            miAdaptador.Fill(ds, "clientes") ' Llenamos el dataset con la tabla clientes

            Return ds
        End Function

        ' Método para administrar (insertar, modificar o eliminar) clientes
        Public Function administrarClientes(cliente As String()) As String
            Dim sql As String = ""
            If cliente(0) = "nuevo" Then ' Acción para un nuevo cliente
                sql = "INSERT INTO clientes(codigo, nombre, direccion) VALUES(" +
                  "'" + cliente(2) + "'," +
                  "'" + cliente(3) + "'," +
                  "'" + cliente(4) + "')"
            ElseIf cliente(0) = "modificar" Then ' Acción para modificar un cliente existente
                sql = "UPDATE clientes SET codigo='" + cliente(2) + "', nombre='" + cliente(3) + "', " +
                  "direccion='" + cliente(4) + "' WHERE cliente=" + cliente(1)
            ElseIf cliente(0) = "eliminar" Then ' Acción para eliminar un cliente
                sql = "DELETE FROM clientes WHERE cliente='" + cliente(1) + "'"
            End If
            Return ejecutarSQL(sql)
        End Function

        ' Método privado para ejecutar SQL
        Private Function ejecutarSQL(sql As String) As String
            Try
                miComando.Connection = miConexion
                miComando.CommandText = sql
                Return miComando.ExecuteNonQuery().ToString()
            Catch ex As Exception
                Return ex.Message
            End Try
        End Function

    End Class
End Namespace

Module Program
    Sub Main()
        Dim conexion As New academica.db_conexion()

        ' Obtener datos
        Dim dataSet As DataSet = conexion.obtenerDatos()
        Console.WriteLine("Datos obtenidos: " & dataSet.Tables("clientes").Rows.Count.ToString() & " registros.")

        ' Agregar un nuevo cliente
        Dim nuevoCliente() As String = {"nuevo", "", "456", "Carlos Ruiz", "Av. Principal 123"}
        Console.WriteLine("Resultado de agregar cliente: " & conexion.administrarClientes(nuevoCliente))

        ' Modificar un cliente existente
        Dim modificarCliente() As String = {"modificar", "1", "456", "Carlos Ruiz Modificado", "Calle Nueva 456"}
        Console.WriteLine("Resultado de modificar cliente: " & conexion.administrarClientes(modificarCliente))

        ' Eliminar un cliente
        Dim eliminarCliente() As String = {"eliminar", "1"}
        Console.WriteLine("Resultado de eliminar cliente: " & conexion.administrarClientes(eliminarCliente))

        ' Cerrar la conexión manualmente
        conexion.miConexion.Close()
    End Sub
End Module




