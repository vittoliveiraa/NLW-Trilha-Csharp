﻿using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Journey.Exception;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.Delete
{
    public class DeleteTripByIdUseCase
    {
        public void Execute(Guid id)
        {
            var dbContext = new JourneyDbContext();
            var trip = dbContext.Trips
                                .Include(trip => trip.Activities)
                                .FirstOrDefault(t => t.Id == id);

            if (trip is null)
            {
                throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
            }

            dbContext.Trips.Remove(trip);
            dbContext.SaveChanges();
        }
    }
}
