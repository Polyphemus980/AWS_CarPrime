using CarPrime.Controllers;
using CarPrime.Models;
using CarPrime.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarPrime.Companies;

public class CarPrimeCompany(CarPrimeService service) : ICarCompany
{
    public static int CompanyId => 0;
    public async Task<List<FrontCar>> GetCars()
    {
        return await service.GetCars();
    }

    public async Task<ActionResult<FrontCar>> GetCar(int carId)
    {
        return await service.GetCarById(carId);
    }

    public async Task<ActionResult<OfferDisplay>> CreateOffer(int carId, Customer customer)
    {
        return (await service.RequestOffer(carId, customer)).Map(OfferDisplay.FromOffer);
    }

    public async Task<ActionResult<LeaseDisplay>> AcceptOffer(int offerId, Customer customer)
    {
        return (await service.AcceptOffer(offerId, customer)).Map(LeaseDisplay.FromLease);
    }

    public async Task<ActionResult<OfferDisplay>> GetOffer(int offerId, Customer customer)
    {
        return (await service.GetOffer(offerId, customer)).Map(OfferDisplay.FromOffer);
    }

    public async Task<ActionResult<LeaseDisplay>> GetLease(int leaseId, Customer customer)
    {
        return (await service.GetLease(leaseId, customer)).Map(LeaseDisplay.FromLease);
    }

    public async Task<ActionResult> EndLease(int leaseId, Customer customer)
    {
        return await service.RequestEndLease(leaseId);
    }
}