using Azure.Core;
using AboHalalBackend.Dtos.User;
using AboHalalBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using AboHalalBackend.Data;

namespace FirstTut.Data
{
    public class AuthRepository : IAuthRepository
    {
        
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<ServiceResponse<AuthenticatedUserDto>> Login(string email, string password)
        {
            var response = new ServiceResponse<AuthenticatedUserDto>();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower().Equals(email.ToLower()));

            if (user == null)
            {
                response.Success = false;
                response.Message = "User Does not Exist";
                return response;
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Invalid Password";
                return response;
            }
            else
            {
                var authenticatedUser = new AuthenticatedUserDto
                {
                    Id = user.Id,
                    FullName= user.FullName,
                    Email = user.Email,
                    Token = CreateToken(user)
                };
                response.Data = authenticatedUser;
                response.Success = true;
                response.Message = "Successfully Logged In";
            }

            return response;
        }


        public async Task<ServiceResponse<int>> Register(AddRegisterDto request)
        {
            var response = new ServiceResponse<int>();
            if (await UserExists(request.Email))
            {
                response.Success = false;
                response.Message = "User is Already Exist";
                return response;
            }
            var newUser = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                Phone = request.Phone
            };
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            response.Data = newUser.Id;
            response.Message = " Successfully Registered ";
            return response;
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower()))
            {
                return true;
            }

            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))

            {

                var ComputeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return ComputeHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTToken:Key"])); ///abcabcabcabcbabbabababababbabababababbaba
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
    new Claim(JwtRegisteredClaimNames.Email, user.Email),
    new Claim("DateOfJoing", "31-04-0000"),
    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
};

            // Set the token expiration to one month from the current date
            var expirationDate = DateTime.Now.AddMonths(1);

            var token = new JwtSecurityToken(
                _configuration["JWTToken:Issuer"],
                _configuration["JWTToken:Issuer"],
                claims,
                expires: expirationDate, // Set the expiration time
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}