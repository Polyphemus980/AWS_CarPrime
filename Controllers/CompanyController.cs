using CarPrime.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CarPrime.Controllers;

public class CompanyController(
    CompaniesService companiesService,
    ICustomerService customerService
    ) : ControllerBase
{
    
    private static readonly TimeSpan GetCarsTimeout = TimeSpan.FromSeconds(10);
    
    [HttpGet("/Company/All/Car/")]
    public async Task<IActionResult> GetCars()
    {
        var tasks = companiesService.AllCompanies.Select(company => 
            company.GetCars()
                .WaitAsync(timeout: GetCarsTimeout)
                .DefaultIfFaulted());
        var values = await Task.WhenAll(tasks);
        var cars = values.SelectMany(maybeList => maybeList ?? []).ToList();
        
        return Ok(cars);
    }

    [HttpGet("/Company/{companyId:int}/Car/{carId:int}")]
    public async Task<IActionResult> GetCar(int companyId, int carId)
    {
        var company = companiesService[companyId];
        return (await company.GetCar(carId)).ToIActionResult();
    }
    
    [HttpGet("/Company/{companyId:int}/Car/{carId:int}/offer")]
    public async Task<IActionResult> RequestOffer(int companyId, int carId)
    {
        var company = companiesService[companyId];
        var customer = await customerService.GetAuthenticatedCustomerAsync(User);
        if (customer == null)
            return Challenge();
         
        return (await company.CreateOffer(carId, customer)).ToIActionResult();
    }

    [HttpGet("/Company/{companyId:int}/Offer/{offerId:int}")]
    public async Task<IActionResult> GetOffer(int companyId, int offerId)
    {
        var company = companiesService[companyId];
        var customer = await customerService.GetAuthenticatedCustomerAsync(User);
        if (customer == null)
            return Challenge();
        
        return (await company.GetOffer(offerId, customer)).ToIActionResult();
    }

    [HttpPost("/Company/{companyId:int}/Offer/{offerId:int}/accept")]
    public async Task<IActionResult> AcceptOffer(int companyId, int offerId)
    {
        var company = companiesService[companyId];
        var customer = await customerService.GetAuthenticatedCustomerAsync(User);
        if (customer == null)
            return Challenge();
        
        return (await company.AcceptOffer(offerId, customer)).ToIActionResult();
    }

    [HttpGet("/Company/{companyId:int}/Lease/{leaseId:int}")]
    public async Task<IActionResult> GetLease(int companyId, int leaseId)
    {
        var company = companiesService[companyId];
        var customer = await customerService.GetAuthenticatedCustomerAsync(User);
        if (customer == null)
            return Challenge();
        
        return (await company.GetLease(leaseId, customer)).ToIActionResult();
    }

    [HttpDelete("/Company/{companyId:int}/Lease/{leaseId:int}")]
    public async Task<IActionResult> RequestEndLease(int companyId, int leaseId)
    {
        var company = companiesService[companyId];
        var customer = await customerService.GetAuthenticatedCustomerAsync(User);
        if (customer == null)
            return Challenge();
        
        return await company.EndLease(leaseId, customer);
    }
}

public static class TaskExtensions
{
    public static Task<T?> DefaultIfFaulted<T>(this Task<T> @this)
    {
        return @this.ContinueWith(task => task.IsCanceled || task.IsFaulted ? default : task.Result);
    }

    public static ActionResult<TResult> Map<T, TResult>(this ActionResult<T> @this, Func<T, TResult> func)
    {
        return @this.Value != null 
            ? new ActionResult<TResult>(func(@this.Value)) 
            : new ActionResult<TResult>(@this.Result!);
    }
    public static async Task<ActionResult<TResult>> Map<T, TResult>(this ActionResult<T> @this, Func<T, Task<TResult>> func)
    {
        return @this.Value != null 
            ? new ActionResult<TResult>(await func(@this.Value)) 
            : new ActionResult<TResult>(@this.Result!);
    }
    
    public static IActionResult ToIActionResult<T>(this ActionResult<T> @this) => 
        ((IConvertToActionResult)@this).Convert();
}