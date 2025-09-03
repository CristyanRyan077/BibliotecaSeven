using Auth.Core.Services;
using Biblioteca.Data.Database;
using Microsoft.AspNetCore.Mvc;
using Auth.Core.DTOs;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Data.Models;

namespace Biblioteca.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly Context _context;

        public AuthController(TokenService tokenService, Context context)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == dto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return Unauthorized("Usuário ou senha inválidos");
            }

            var token = _tokenService.GerarToken(user.Id, user.Username, "user");
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrarDto dto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Username == dto.Username))
            {
                return BadRequest("Usuario ja existe");
            }

            if (await _context.Usuarios.AnyAsync(u => u.Email == dto.Email))
            {
                return BadRequest("Usuario com esse Email ja existe");
            }

            var user = new Usuario
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuário registrado com sucesso" });
        }

    }
}
