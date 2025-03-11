using BL;
using UI.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace UI.ApiControlles
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly IConfiguration oClsConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginsController"/> class.
        /// </summary>
        /// <param name="oConfiguration">The configuration settings used for JWT generation.</param>
        public LoginsController(IConfiguration oConfiguration)
        {
            oClsConfiguration = oConfiguration;
        }

        

     

        /// <summary>
        /// Authenticates a user and generates a JWT if successful.
        /// </summary>
        /// <param name="User">The user credentials to authenticate.</param>
        /// <returns>An IActionResult containing the JWT token if authentication is successful.</returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] ApplicationUsers User)
        {
            IActionResult response = Unauthorized();
            var myUser = AuthenticateUser(User);
            if (myUser != null)
            {
                var token = GenerateToken(User);
                return Ok(new { token = token });
            }
            return response;
        }

        /// <summary>
        /// Updates a value (not implemented).
        /// </summary>
        /// <param name="id">The ID of the value to update.</param>
        /// <param name="value">The new value.</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

     

        /// <summary>
        /// Authenticates the user based on username and password.
        /// </summary>
        /// <param name="model">The user credentials to authenticate.</param>
        /// <returns>The authenticated user if successful, otherwise null.</returns>
        private ApplicationUsers AuthenticateUser(ApplicationUsers model)
        {
            // This is a hardcoded example. Replace with actual user validation logic.
            if (model.UserName == "Ahmed" && model.PasswordHash == "123")
            {
                return new ApplicationUsers()
                {
                    UserName = "Ahmed",
                    PasswordHash = "123"
                };
            }
            return null;
        }

        /// <summary>
        /// Generates a JWT token for the authenticated user.
        /// </summary>
        /// <param name="user">The authenticated user.</param>
        /// <returns>A JWT token as a string.</returns>
        private string GenerateToken(ApplicationUsers user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(oClsConfiguration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: oClsConfiguration["Jwt:Issuer"],
                audience: oClsConfiguration["Jwt:Audience"], // Add this if necessary
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
