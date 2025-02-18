using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Warehouse.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;



public class AuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly WarehouseDbContext _context;


    //The key will need to be stored in an enviroment variable later for security.

    public AuthService(IHttpContextAccessor httpContextAccessor, WarehouseDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public async Task<bool> AuthenticateUserAsync(string email, string password)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
        if (customer != null && VerifyPassword(password, customer.Password))
        {
            await GenerateCookie(email, "Customer");
            return true;
        }

        var worker = await _context.Workers.FirstOrDefaultAsync(w => w.Email == email);
        if (worker != null && VerifyPassword(password, worker.Password))
        {
            await GenerateCookie(email, "Worker");
            return true;
        }

        return false;
    }

    private async Task GenerateCookie(string email, string role)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, email),
            new(ClaimTypes.Name, email),
            new(ClaimTypes.Role, role)
        };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties { IsPersistent = true };

        var httpContext = _httpContextAccessor.HttpContext!;
        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
    }
     
    
    public async Task LogoutUser()
    {
        var httpContext = _httpContextAccessor.HttpContext!;
        await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
    }


    //Checks the user's input against a hashed password stored in the db. 
    //Note, Hashing has not yet been implemented, so this funciton checks passwords in plaintext.
    private bool VerifyPassword(string inputPassword, string storedHash)
    {
        // Hashing not implemented yet, update this once done. 
        return inputPassword == storedHash;
    }




}