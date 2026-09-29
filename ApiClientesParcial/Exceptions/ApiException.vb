Imports System.Net

Namespace Exceptions

    Public Class ApiException
        Inherits Exception

        Public ReadOnly Property StatusCode As HttpStatusCode

        Public Sub New(statusCode As HttpStatusCode, message As String)
            MyBase.New(message)
            Me.StatusCode = statusCode
        End Sub

    End Class

End Namespace
