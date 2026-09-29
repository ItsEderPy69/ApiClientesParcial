Imports ApiClientesParcial.Data
Imports ApiClientesParcial.Models
Imports Microsoft.EntityFrameworkCore

Namespace Repositories

    Public Class ClienteRepository
        Implements IClienteRepository

        Private ReadOnly _context As EmpresaDbContext

        Public Sub New(context As EmpresaDbContext)
            _context = context
        End Sub

        Public Async Function GetClient() As Task(Of List(Of Cliente)) Implements IClienteRepository.GetClient
            Return Await _context.Clientes.AsNoTracking().OrderBy(Function(c) c.Id).ToListAsync()
        End Function

        Public Async Function GetClientById(id As Integer) As Task(Of Cliente) Implements IClienteRepository.GetClientById
            Return Await _context.Clientes.Where(Function(c) c.Id = id).FirstOrDefaultAsync()
        End Function

        Public Async Function EmailExists(email As String, Optional Id As Integer? = Nothing) As Task(Of Boolean) Implements IClienteRepository.EmailExists
            Dim query = _context.Clientes.Where(Function(c) c.Email = email)
            If Id.HasValue Then
                query = query.Where(Function(c) c.Id <> Id.Value)
            End If
            Return Await query.AnyAsync()
        End Function

        Public Async Function AddClient(cliente As Cliente) As Task(Of Cliente) Implements IClienteRepository.AddClient
            _context.Clientes.Add(cliente)
            Await _context.SaveChangesAsync()
            Return cliente
        End Function

        Public Async Function UpdateClient(cliente As Cliente) As Task Implements IClienteRepository.UpdateClient
            _context.Clientes.Update(cliente)
            Await _context.SaveChangesAsync()
        End Function

        Public Async Function DeleteClient(cliente As Cliente) As Task Implements IClienteRepository.DeleteClient
            _context.Clientes.Remove(cliente)
            Await _context.SaveChangesAsync()
        End Function

    End Class

End Namespace
