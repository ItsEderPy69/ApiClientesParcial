Imports ApiClientesParcial.Data
Imports ApiClientesParcial.Middlewares
Imports ApiClientesParcial.Models
Imports ApiClientesParcial.Repositories
Imports ApiClientesParcial.Services
Imports ApiClientesParcial.Validators
Imports FluentValidation
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting

Public Module Program

    Public Sub Main(args As String())
        Dim builder = WebApplication.CreateBuilder(args)

        Dim connectionString = builder.Configuration.GetConnectionString("EmpresaDB")
        builder.Services.AddDbContext(Of EmpresaDbContext)(
            Sub(options As DbContextOptionsBuilder)
                options.UseSqlServer(connectionString)
            End Sub)

        builder.Services.AddScoped(Of IClienteRepository, ClienteRepository)()
        builder.Services.AddScoped(Of IClienteService, ClienteService)()
        builder.Services.AddScoped(Of IValidator(Of Cliente), ClienteValidator)()
        builder.Services.AddControllers()
        builder.Services.AddEndpointsApiExplorer()
        builder.Services.AddSwaggerGen()

        Dim app = builder.Build()

        app.UseMiddleware(Of ExceptionMiddleware)()

        app.UseSwagger()
        app.UseSwaggerUI()
        app.UseHttpsRedirection()
        app.UseAuthorization()
        app.MapControllers()

        app.Run()
    End Sub

End Module
