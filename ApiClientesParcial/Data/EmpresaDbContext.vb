Imports ApiClientesParcial.Models
Imports Microsoft.EntityFrameworkCore

Namespace Data
    Public Class EmpresaDbContext
        Inherits DbContext

        Public Sub New(options As DbContextOptions(Of EmpresaDbContext))
            MyBase.New(options)
        End Sub

        Public Property Clientes As DbSet(Of Cliente)

        Protected Overrides Sub OnModelCreating(modelBuilder As ModelBuilder)
            modelBuilder.Entity(Of Cliente)(
                Sub(entity)
                    entity.ToTable("Clientes")
                    entity.HasKey(Function(c) c.Id)
                    entity.Property(Function(c) c.Id).UseIdentityColumn()
                    entity.Property(Function(c) c.Nombre).IsUnicode(False).IsRequired()
                    entity.Property(Function(c) c.Apellido).IsUnicode(False).IsRequired()
                    entity.Property(Function(c) c.Email).IsUnicode(False).IsRequired()
                    entity.Property(Function(c) c.Telefono).IsUnicode(False)
                End Sub)
        End Sub

    End Class

End Namespace
