using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Warehouse.Models;



public class AuthService
{
    private readonly WarehouseDbContext _context;

    //The key will need to be stored in an enviroment variable later for security.
    private readonly string _jwtSecret = "WnsfTEfOwUB1Hjn3y5YN+5+Bv1IVi+2z2zSL+5b++8s=";

    public AuthService(WarehouseDbContext context)
    {
        _context = context;
    }

    public async Task<string?> AuthenticateUserAsync(string email, string password)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
        if (customer != null && VerifyPassword(password, customer.Password))
        {
            return GenerateJwtToken(email, "Customer");
        }

        var worker = await _context.Workers.FirstOrDefaultAsync(w => w.Email == email);
        if (worker != null && VerifyPassword(password, worker.Password))
        {
            return GenerateJwtToken(email, "Worker");
        }

        return null;

    }

    private string GenerateJwtToken(string email, string role)
    {   

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    //Checks the user's input against a hashed password stored in the db. 
    //Note, Hashing has not yet been implemented, so this funciton checks passwords in plaintext.
    private bool VerifyPassword(string inputPassword, string storedHash)
    {
        // Hashing not implemented yet, update this once done. 
        return inputPassword == storedHash;
    }




}