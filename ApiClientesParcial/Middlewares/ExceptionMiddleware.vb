Imports System.Net
Imports ApiClientesParcial.Exceptions
Imports Microsoft.AspNetCore.Http

Namespace Middlewares

    Public Class ExceptionMiddleware

        Private ReadOnly _next As RequestDelegate

        Public Sub New([next] As RequestDelegate)
            _next = [next]
        End Sub

        Public Async Function InvokeAsync(context As HttpContext) As Task
            Dim statusCode As HttpStatusCode
            Dim message As String

            Try
                Await _next(context)
                Return
            Catch ex As ApiException
                statusCode = ex.StatusCode
                message = ex.Message
            Catch ex As Exception
                statusCode = HttpStatusCode.InternalServerError
                message = "Error interno del servidor"
            End Try

            context.Response.StatusCode = CInt(statusCode)
            Await context.Response.WriteAsJsonAsync(New With {.statusCode = CInt(statusCode), .message = message})
        End Function

    End Class

End Namespace
