using System.Security.Claims;
using CarPrime.Data;
using CarPrime.Models;
using Microsoft.EntityFrameworkCore;

namespace CarPrime.Services;

public class CustomerService : ICustomerService
{
    private readonly ApplicationDbContext _dbContext;

    public CustomerService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Customer?> GetCustomerByEmailAsync()
    {
        Customer customer = await _dbContext.Customers.FirstAsync();
        return customer;
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        await _dbContext.Customers.AddAsync(customer);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<Customer?> GetAuthenticatedCustomerAsync(ClaimsPrincipal currentUser)
    {
        return await GetCustomerByEmailAsync();
    }
}