using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Repositories;

namespace WebApiLivraria.Application.UseCases.Usuarios.Login
{
    public class LoginUsuarioUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public LoginUsuarioUseCase(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<LoginUsuarioResponse> Executar(LoginUsuarioRequest request)
        {
            var usuario = await _usuarioRepository.ObterPorEmail(request.Email);
            if (usuario == null)
                throw new Exception("Usuário ou senha inválidos");

            if (string.IsNullOrEmpty(usuario.SenhaHash))
                throw new Exception("Este usuário não possui senha cadastrada");

            bool senhaValida = BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash);
            if (!senhaValida)
                throw new Exception("Usuário ou senha inválidos");

            var token = GerarToken(usuario);

            return new LoginUsuarioResponse
            {
                Token = token,
                UsuarioId = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email
            };
        }

        private string GerarToken(Usuario usuario)
        {
            var chaveSecreta = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("Configuração JWT:Key não encontrada");
            var issuer = _configuration["Jwt:Issuer"]
                ?? throw new InvalidOperationException("Configuração JWT:Issuer não encontrada");
            var audience = _configuration["Jwt:Audience"]
                ?? throw new InvalidOperationException("Configuração JWT:Audience não encontrada");

            var chaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
            var credenciais = new SigningCredentials(chaveSimetrica, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
        new Claim(ClaimTypes.Name, usuario.Nome),
        new Claim(ClaimTypes.Role, usuario.Role),
        new Claim("nomeUsuario", usuario.NomeUsuario ?? usuario.Nome)
    };

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: credenciais
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}