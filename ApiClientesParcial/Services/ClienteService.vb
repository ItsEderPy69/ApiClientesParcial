Imports System.Net
Imports ApiClientesParcial.Exceptions
Imports ApiClientesParcial.Models
Imports ApiClientesParcial.Repositories
Imports FluentValidation

Namespace Services

    Public Class ClienteService
        Implements IClienteService

        Private ReadOnly _repository As IClienteRepository
        Private ReadOnly _validator As IValidator(Of Cliente)

        Public Sub New(repository As IClienteRepository, validator As IValidator(Of Cliente))
            _repository = repository
            _validator = validator
        End Sub

        Public Function ObtenerTodosAsync() As Task(Of List(Of Cliente)) Implements IClienteService.ObtenerTodosAsync
            Return _repository.GetClient()
        End Function

        Public Async Function GetById(id As Integer) As Task(Of Cliente) Implements IClienteService.GetById
            Dim cliente = Await _repository.GetClientById(id)
            If cliente Is Nothing Then
                Throw New ApiException(HttpStatusCode.NotFound, "Cliente no existe")
            End If
            Return cliente
        End Function

        Public Async Function AddClient(cliente As Cliente) As Task(Of Cliente) Implements IClienteService.AddClient
            Await ValidateClient(cliente)

            If Await _repository.EmailExists(cliente.Email) Then
                Throw New ApiException(HttpStatusCode.BadRequest, "Cliente con este email ya exitse")
            End If

            cliente.Id = 0
            Return Await _repository.AddClient(cliente)
        End Function

        Public Async Function UpdateClient(id As Integer, datos As Cliente) As Task(Of Cliente) Implements IClienteService.UpdateClient
            Dim existente = Await GetById(id)
            Await ValidateClient(datos)

            If Await _repository.EmailExists(datos.Email, id) Then
                Throw New ApiException(HttpStatusCode.BadRequest, "Cliente con este email ya exitse")
            End If

            existente.Nombre = datos.Nombre
            existente.Apellido = datos.Apellido
            existente.Email = datos.Email
            existente.Telefono = datos.Telefono

            Await _repository.UpdateClient(existente)
            Return existente
        End Function

        Public Async Function DeleteClient(id As Integer) As Task Implements IClienteService.DeleteClient
            Dim existente = Await GetById(id)
            Await _repository.DeleteClient(existente)
        End Function

        Private Async Function ValidateClient(cliente As Cliente) As Task
            Dim resultado = Await _validator.ValidateAsync(cliente)
            If Not resultado.IsValid Then
                Throw New ApiException(HttpStatusCode.BadRequest, String.Join(". ", resultado.Errors.Select(Function(e) e.ErrorMessage)))
            End If
        End Function

    End Class

End Namespace
