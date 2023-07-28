﻿using CarApp.Data;
using CarApp.Pages.Car;
using CarApp.Pages.Car.Queries;
using MediatR;

namespace CarApp.Pages.Car.Commands
{
    public record UpdateCarCommand(CarViewModel car) : IRequest<Unit>;
    public class UpdateCarHandler : IRequestHandler<UpdateCarCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly CarAppContext ctx;
        public UpdateCarHandler(IMediator mediator, CarAppContext ctx)
        {
            _mediator = mediator;
            this.ctx = ctx;
        }
        public async Task<Unit> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var existingCar = await _mediator.Send(new GetCarByIdQuery(request.car.CarId));

            if (existingCar == null)
            {

            }
            existingCar.Name = request.car.Name;
            existingCar.TransmissionId = request.car.TransmissionId;
            existingCar.Seats = request.car.Seats;
            existingCar.DailyPrice = request.car.DailyPrice;
            existingCar.Year = request.car.Year;
            existingCar.ModelName = request.car.ModelName;
            existingCar.Image = request.car.Image;
            existingCar.CarBodyTypeId = request.car.CarBodyTypeId;
            existingCar.VehicleTypeId = request.car.VehicleTypeId;
            existingCar.BrandId = (int)request.car.BrandId;
            existingCar.FuelId = (int)request.car.FuelId;
            existingCar.DriveId = (int)request.car.DriveId;

            //ctx.Entry(existingCar).State = EntityState.Modified;
            ctx.Car.Update(existingCar);
            await ctx.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
