using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.GetById
{
    public class GetTripByIdUseCase
    {
        public ResponseTripJson Execute(Guid id)
        {
            var dbContext = new JourneyDbContext();
            var trip = dbContext.Trips
                                .Include(trip => trip.Activities)
                                .FirstOrDefault(t => t.Id == id);

            if (trip is null)
            {
                throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
            }

            return new ResponseTripJson
            {
                Name = trip.Name,
                Id = trip.Id,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Activities = trip.Activities.Select(x => new ResponseActivityJson
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date,
                    Status = (Communication.Enums.ActivityStatus)x.Status
                }).ToList()
            };
        }
    }
}
