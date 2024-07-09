using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;
using System.Data;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripValidator : AbstractValidator<RequestRegisterTripJson>
    {
        public RegisterTripValidator()
        {
            RuleFor(request => request.Name).NotEmpty()
                                            .NotNull()
                                            .WithMessage(ResourceErrorMessages.NAME_EMPTY);

            RuleFor(request => request.StartDate.Date).GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                                                      .WithMessage(ResourceErrorMessages.DATE_TRIP_MUST_BE_LATER_THAN_TODAY);

            RuleFor(request => request).Must(request => request.EndDate.Date >= request.StartDate.Date)
                                       .WithMessage(ResourceErrorMessages.END_DATE_TRIP_MUST_BE_LATER_THAN_START_DATE);

        }
    }
}
