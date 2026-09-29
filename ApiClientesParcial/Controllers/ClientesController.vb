Imports ApiClientesParcial.Models
Imports ApiClientesParcial.Services
Imports Microsoft.AspNetCore.Http
Imports Microsoft.AspNetCore.Mvc

Namespace Controllers

    <ApiController>
    <Route("api/[controller]")>
    Public Class ClientesController
        Inherits ControllerBase

        Private ReadOnly _service As IClienteService

        Public Sub New(service As IClienteService)
            _service = service
        End Sub

        <HttpGet>
        <EndpointSummary("Listar clientes")>
        Public Async Function ObtenerTodos() As Task(Of ActionResult(Of List(Of Cliente)))
            Return Ok(Await _service.ObtenerTodosAsync())
        End Function

        <HttpGet("{id:int}")>
        <EndpointSummary("Recuperar cliente por Id")>
        Public Async Function GetById(id As Integer) As Task(Of ActionResult(Of Cliente))
            Return Ok(Await _service.GetById(id))
        End Function

        <HttpPost>
        <EndpointSummary("Crear cliente")>
        Public Async Function Registrar(<FromBody> cliente As Cliente) As Task(Of ActionResult(Of Cliente))
            Dim creado = Await _service.AddClient(cliente)
            Return CreatedAtAction(NameOf(GetById), New With {.id = creado.Id}, creado)
        End Function

        <HttpPut("{id:int}")>
        <EndpointSummary("Modificar cliente")>
        Public Async Function Modificar(id As Integer, <FromBody> cliente As Cliente) As Task(Of ActionResult(Of Cliente))
            Return Ok(Await _service.UpdateClient(id, cliente))
        End Function

        <HttpDelete("{id:int}")>
        <EndpointSummary("Eliminar cliente")>
        Public Async Function Eliminar(id As Integer) As Task(Of IActionResult)
            Await _service.DeleteClient(id)
            Return NoContent()
        End Function

    End Class

End Namespace
