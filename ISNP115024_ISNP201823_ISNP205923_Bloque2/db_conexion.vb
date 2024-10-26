Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing.Text

Namespace isnp115024_isnp201823_isnp205923_bloque2
    Friend Class db_conexion
        Private miConexion As New SqlConnection("Data Source=DESKTOP-1VJ1VJ2;Initial Catalog=bd_isnp115024_isnp201823_isnp205923;Integrated Security=True")
        Private miComando As New SqlCommand()
        Private miAdaptador As New SqlDataAdapter()
        Private ds As New DataSet()

        Public Sub New()
            miConexion.ConnectionString = "Data Source=DESKTOP-1VJ1VJ2;Initial Catalog=bd_isnp115024_isnp201823_isnp205923;Integrated Security=True"
            miConexion.Open()
        End Sub

        Public Function administrarClientes(Cliente As String()) As String
            Dim sql As String = ""
            If Cliente(0) = "nuevo" Then
                sql = "INSERT into clientes (codigo, nombre, direccion) Values (" +
                    "'" + Cliente(2) + "'," +
                    "'" + Cliente(3) + "'," +
                    "'" + Cliente(4) + "')"
            ElseIf Cliente(0) == "modificar" Then
                sql = "UPDATE clientes set codigo='" + Cliente(2) + "', nombre='" + Cliente(3) + "', " + "direccion='" + Cliente(4) + "' where cliente='" + Cliente(1)
            ElseIf Cliente(0) = "eliminar" Then
                sql = "DELETE FROM clientes WHERE cliente='" + Cliente(1) + "'"
            End If
            Return Ejecutar(sql)

            Private Funtion ejecutar(sql As String) As String
                miComando = New SqlCommand(sql, miConexion)
            Try
                miComando.ExecuteNonQuery()
                Return "OK"
            Catch ex As Exception
                Return ex.Message
            End Try
        End Function


        End Function
    End Class
