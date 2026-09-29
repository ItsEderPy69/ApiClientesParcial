Imports ApiClientesParcial.Models

Namespace Services

    Public Interface IClienteService

        Function ObtenerTodosAsync() As Task(Of List(Of Cliente))

        Function GetById(id As Integer) As Task(Of Cliente)

        Function AddClient(cliente As Cliente) As Task(Of Cliente)

        Function UpdateClient(id As Integer, datos As Cliente) As Task(Of Cliente)

        Function DeleteClient(id As Integer) As Task

    End Interface

End Namespace
