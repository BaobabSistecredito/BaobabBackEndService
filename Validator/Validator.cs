/* PARTE 0
    May I have your attention, please?,
    May I have your attention, please?,  
    Will the real SkormJF please stand up?
    I repeat, will the real SkormJF please stand up?
    We're gonna have a problem here

    Bueno chicos, aca veremos como usar la libreria de fluent validation, entonces
    como toda libreria debemos realizar su instalacion, asi que vamos a BaobabBackEndService.csproj.
*/


/* PARTE 2 
    Una vez instalada la libreria vamos realizar el using FluentValitation y using Models 
    para saber que vamos a basar las validaciones en la estrucutra de los modelos.
    vamos a crear el namespace FluentValitation y dentro de el las clases para cada modelo, 
    recuerden que la clase va a heredar de AbstractValidator<aca va el modelo que queremos validar>
    y debe crearse una clase para cada modelo, aca abajo veran el ejemplo de cada una.
*/
using FluentValidation;
using BaobabBackEndSerice.Models;

namespace FluentValidation{
    public class CategoryValidator : AbstractValidator<CategoryRequest>{
        public CategoryValidator(){
            /* PARTE 3
                dentro de este public vamos a realizar las validaciones correspondientes
                para cada uno de los datos que existen dentro de nuestro modelo
                y los mensajes de error en caso de que no cumplan con la validacion que se realiza
                existen muchas formas de realizar las validaciones unas que implementa la libreria
                y te da la libertad de poder crear validaciones personalizadas.
                Para iniciar una validacion de un dato basta con usar el RuleFor() e
                ir concatenando con un punto cada una de las validaciones que queremos implementarle
                a ese tipo de dato en especifico, como veran a continuacion.
            */
            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Este campo es requerido.")
                .MinimumLength(3).WithMessage("Este campo debe contener minimo 3 caracteres")
                .MaximumLength(255).WithMessage("Este campo debe contener maximo 255 caracteres");
            
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Este campo es requerido.")
                .Must(x => x.Equals("Activo", StringComparison.OrdinalIgnoreCase) || x.Equals("Inactivo", StringComparison.OrdinalIgnoreCase))
                .WithMessage("Este campo solo puede contener el valor 'Activo' o 'Inactivo'");
        }
    }

    /* PARTE 4
        Una vez que hagamos nuestras reglas de validacion ahora debes hacer la inyeccion de esta dependencia 
        para que pueda ser usada, asi que vamos al program.cs
    */

    public class CouponValidator : AbstractValidator<CouponRequest>{
        public CouponValidator(){
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Este campo es requerido.")
                .MinimumLength(3).WithMessage("Este campo debe contener minimo 3 caracteres")
                .MaximumLength(255).WithMessage("Este campo debe contener maximo 255 caracteres");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Este campo es requerido.")
                .MinimumLength(3).WithMessage("Este campo debe contener minimo 3 caracteres")
                .MaximumLength(255).WithMessage("Este campo debe contener maximo 255 caracteres");
            
            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Este campo es requerido.");

            RuleFor(x => x.ExpiryDate)
                .NotEmpty().WithMessage("Este campo es requerido.");
            
            RuleFor(x => x.ValueDiscount)
                .NotEmpty().WithMessage("Este campo es requerido.")
                .GreaterThan(0).WithMessage("Este campo tiene que ser un valor mayor a 0");

            RuleFor(x => x.TypeDiscount)
                .NotEmpty().WithMessage("Este campo es requerido.")
                .Must(x => x.Equals("Porcentual", StringComparison.OrdinalIgnoreCase) || x.Equals("Neto", StringComparison.OrdinalIgnoreCase))
                .WithMessage("Este campo solo puede contener el valor 'Porcentual' o 'Neto'");

            RuleFor(x => x.NumberOfAvailableUses)
                .NotEmpty().WithMessage("Este campo es requerido.")
                .GreaterThan(0).WithMessage("Este campo tiene que ser un valor mayor a 0");
            
            RuleFor(x => x.TypeUsability)
                .NotEmpty().WithMessage("Este campo es requerido.")
                .Must(x => x.Equals("Limitada", StringComparison.OrdinalIgnoreCase) || x.Equals("Ilimitada", StringComparison.OrdinalIgnoreCase))
                .WithMessage("Este campo solo puede contener el valor 'Limitada' o 'Ilimitada'");

            RuleFor(x => x.MinPurchaseRange)
                .NotEmpty().WithMessage("Este campo es requerido.")
                .GreaterThan(0).WithMessage("Este campo tiene que ser un valor mayor a 0");

            RuleFor(x => x.MaxPurchaseRange)
                .NotEmpty().WithMessage("Este campo es requerido.")
                .GreaterThan(0).WithMessage("Este campo tiene que ser un valor mayor a 0");
            
            RuleFor(x => x.CouponCode)
                .NotEmpty().WithMessage("Este campo es requerido.")
                .MinimumLength(3).WithMessage("Este campo debe contener minimo 3 caracteres")
                .MaximumLength(255).WithMessage("Este campo debe contener maximo 255 caracteres");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Este campo es requerido.")
                .GreaterThan(0).WithMessage("Este campo tiene que ser un valor mayor a 0");

            RuleFor(x => x.MarketingUserId)
                .NotEmpty().WithMessage("Este campo es requerido.")
                .GreaterThan(0).WithMessage("Este campo tiene que ser un valor mayor a 0"); 

        }
    }
}