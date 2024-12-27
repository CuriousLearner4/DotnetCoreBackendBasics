using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.IdentityModel.Tokens;

namespace MagicVilla_VillaAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext db;
        private IMapper mapper;
        private readonly string SECRETKEY;

        public UserRepository(AppDbContext db,IMapper mapper,IConfiguration configuration)
        {
            this.db = db;
            this.mapper = mapper;
            SECRETKEY = configuration.GetValue<string>("APISettings:Secret");
        }

        #region IsUniqueUser
        public bool IsUniqueUser(string username)
        {
            var user = db.LocalUsers.FirstOrDefault(u => u.UserName == username);
            if(user == null)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Login
        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = db.LocalUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower() && u.Password == loginRequestDTO.Password);
            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SECRETKEY);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new()
            {
                User = user,
                Token = tokenHandler.WriteToken(token)
            };
            return loginResponseDTO;
        }
        #endregion

        #region Registration
        public async Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            LocalUser user = mapper.Map<LocalUser>(registrationRequestDTO);
            db.LocalUsers.Add(user);
            await db.SaveChangesAsync();
            user.Password = "";
            return user;
        }
        #endregion

    }
}
