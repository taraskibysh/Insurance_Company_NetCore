namespace InsuranceCompany.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;  
    using InsuranceCompany.Contracts.Authentication;
    using InsuranceCompany.Application.Services.Authentication;

    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        // Реєстрація користувача
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            try
            {
                var authResult = await _authenticationService.Register(
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    request.Password);

                var response = new AuthenticationResponce(
                    authResult.Id,
                    authResult.FirstName,
                    authResult.LastName,
                    authResult.Email,
                    authResult.Token
                );

                return Ok(response); 
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message }); // Помилка через вже існуючого користувача
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        // Логін користувача
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var authResult = await _authenticationService.Login(
                    request.Email,
                    request.Password);

                var response = new AuthenticationResponce(
                    authResult.Id,
                    authResult.FirstName,
                    authResult.LastName,
                    authResult.Email,
                    authResult.Token
                );

                return Ok(response); 
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message }); // Помилка через невірний email
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message }); // Помилка через невірний пароль
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }
    }
}
