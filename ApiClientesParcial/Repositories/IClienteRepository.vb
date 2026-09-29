Imports ApiClientesParcial.Models

Namespace Repositories

    Public Interface IClienteRepository
        Function GetClient() As Task(Of List(Of Cliente))
        Function GetClientById(id As Integer) As Task(Of Cliente)
        Function EmailExists(email As String, Optional excluirId As Integer? = Nothing) As Task(Of Boolean)
        Function AddClient(cliente As Cliente) As Task(Of Cliente)
        Function UpdateClient(cliente As Cliente) As Task
        Function DeleteClient(cliente As Cliente) As Task
    End Interface

End Namespace
