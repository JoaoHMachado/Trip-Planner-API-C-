using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripValidator : AbstractValidator<RequestRegisterTripJson>
    {
        public RegisterTripValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty()
                .WithMessage(ResourceErrorMessages.NAME_EMPTY);

            RuleFor(request => request.StartDate.Date)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .WithMessage(ResourceErrorMessages.DATE_TRIP_MUST_BE_LATER_THAN_TODAY);

            RuleFor(request => request)
                .Must(request => request.EndDate.Date >= request.StartDate.Date) //MUST SIGNIFICA NESSE CASO QUE A DATA FINAL TEM QUE SER MAIOR OU IGUAL A DATA INICIAL
                .WithMessage(ResourceErrorMessages.END_DATE_TRIP_MUST_BE_LATER_START_DATE); // SE O MUST RETORNAR FALSE SERÁ MOSTRADA A MENSAGEM
        }
    }
}
