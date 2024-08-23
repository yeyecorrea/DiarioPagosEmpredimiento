using Dapper;
using DiarioPagosApp.Models;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace DiarioPagosApp.Services
{
    public class RepositoryUser : IRepositoryUser
    {
        private readonly string _connectionString;
        private readonly HttpContext _httpContext;

        public RepositoryUser(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<int> CreateUser(User user)
        {
            using var connection = new SqlConnection(_connectionString);
            var userId = await connection.QuerySingleAsync<int>(@"INSERT INTO USERS (FIRST_NAME, LAST_NAME, USERS_EMAIL, STANDARD_EMAIL, PASS_WORD_HASH)
                                                                VALUES (@FirstName, @LastName, @UserEmail, @StandardEmail, @PassWordHash);
                                                                SELECT SCOPE_IDENTITY();", user);

            return userId;
        }

        public async Task<User> SearchUserByEmail(string StandardEmail)
        {
            using var connection = new SqlConnection(_connectionString);
            var standardEmail = await connection.QuerySingleOrDefaultAsync<User>(@"SELECT USERS_ID AS UserId,
                                                                                   FIRST_NAME AS FirstName,
                                                                                   LAST_NAME AS LastName,
                                                                                   USERS_EMAIL AS UserEmail, 
                                                                                   STANDARD_EMAIL AS StandardEmail,
                                                                                   PASS_WORD_HASH AS PassWordHash
                                                                                   FROM USERS WHERE STANDARD_EMAIL = @StandardEmail", new { StandardEmail });

            return standardEmail;
        }

        public async Task<User> SearchUserByName(string FirstName)
        {
            using var connection = new SqlConnection(_connectionString);
            var userName = await connection.QuerySingleOrDefaultAsync<User>(@"SELECT * FROM USERS WHERE FIRST_NAME = @FirstName", new { FirstName });

            return userName;
        }

        public int GetUser() 
        {
            // el http context optenemos el usuario actual y validamos si esta authenticado
            if (_httpContext.User.Identity.IsAuthenticated)
            {
                // Obtenemos el id del usuario con claim
                var claimId = _httpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();

                // Parseamos el valor de tipo Claim a int
                int userId = int.Parse(claimId.Value);

                return userId;
            }
            else
            {
                throw new ApplicationException("El usuario no esta autenticado");
            } 
        }

    }
}
