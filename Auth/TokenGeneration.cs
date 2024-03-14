namespace Auth
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using PublicWebSite;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class TokenGeneration : IAuthProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IUserProvider _userProvider;
        public TokenGeneration(
            IConfiguration configuration, IUserProvider userProvider)
        {
            _userProvider = userProvider;
            _configuration = configuration;
        }
        public UserResponse GenerateJwtToken(Login login)
        {
            var response = _userProvider.AuthenticateUser(login);

            if (response.Success == false)
            {
                return new UserResponse
                {
                    ErrorCode = ErrorCodes.FAILED_TO_AUTHENTICATE_USER,
                    Message = response.Message,
                };
            }

            var user = response.User;

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            foreach (var permission in user.Permissions)
            {
                claims.Add(new Claim("permission", permission));
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new UserResponse
            {
                Success = true,
                Data    = user,
                Token   = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

    }
}