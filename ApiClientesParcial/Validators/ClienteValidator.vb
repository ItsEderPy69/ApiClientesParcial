Imports ApiClientesParcial.Models
Imports FluentValidation

Namespace Validators
    Public Class ClienteValidator
        Inherits AbstractValidator(Of Cliente)
        Public Sub New()
            RuleFor(Function(c) c.Nombre).NotEmpty().WithMessage("El nombre es obligatorio").
                MaximumLength(100).WithMessage("El nombre no puede superar 100 caracteres")

            RuleFor(Function(c) c.Apellido).NotEmpty().WithMessage("El apellido es obligatorio").
                MaximumLength(100).WithMessage("El apellido no puede superar 100 caracteres")

            RuleFor(Function(c) c.Email).NotEmpty().WithMessage("El email es obligatorio").
                EmailAddress().WithMessage("El email no tiene un formato válido").
                MaximumLength(150).WithMessage("El email no puede superar 150 caracteres")

            RuleFor(Function(c) c.Telefono).MaximumLength(30).WithMessage("El teléfono no puede superar 30 caracteres")
        End Sub

    End Class
End Namespace
