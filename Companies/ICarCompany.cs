using CarPrime.Controllers;
using CarPrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarPrime.Companies;

public interface ICarCompany
{
    public Task<List<FrontCar>> GetCars();
    public Task<ActionResult<FrontCar>> GetCar(int carId);
    public Task<ActionResult<OfferDisplay>> CreateOffer(int carId, Customer customer);
    public Task<ActionResult<LeaseDisplay>> AcceptOffer(int offerId, Customer customer);
    public Task<ActionResult<OfferDisplay>> GetOffer(int offerId, Customer customer);
    public Task<ActionResult<LeaseDisplay>> GetLease(int leaseId, Customer customer);
    public Task<ActionResult> EndLease(int leaseId, Customer customer);
}