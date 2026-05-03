using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MOVE.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	private readonly UserManager<IdentityUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly IConfiguration _configuration;

	public AuthController(
		UserManager<IdentityUser> userManager,
		RoleManager<IdentityRole> roleManager,
		IConfiguration configuration)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_configuration = configuration;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterDto dto)
	{
		var user = new IdentityUser { UserName = dto.Email, Email = dto.Email };
		var result = await _userManager.CreateAsync(user, dto.Password);

		if (!result.Succeeded)
			return BadRequest(result.Errors);

		// Skapa roller om de inte finns
		if (!await _roleManager.RoleExistsAsync("Admin"))
			await _roleManager.CreateAsync(new IdentityRole("Admin"));
		if (!await _roleManager.RoleExistsAsync("User"))
			await _roleManager.CreateAsync(new IdentityRole("User"));

		// Tilldela roll
		await _userManager.AddToRoleAsync(user, dto.Role ?? "User");

		return Ok(new { message = "User registered successfully" });
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginDto dto)
	{
		var user = await _userManager.FindByEmailAsync(dto.Email);
		if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
			return Unauthorized(new { message = "Invalid credentials" });

		var roles = await _userManager.GetRolesAsync(user);
		var token = GenerateJwtToken(user, roles);

		return Ok(new { token });
	}

	private string GenerateJwtToken(IdentityUser user, IList<string> roles)
	{
		var jwtSettings = _configuration.GetSection("JwtSettings");
		var key = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(jwtSettings["Secret"]!));

		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id),
			new Claim(ClaimTypes.Email, user.Email!),
		};

		foreach (var role in roles)
			claims.Add(new Claim(ClaimTypes.Role, role));

		var token = new JwtSecurityToken(
			issuer: jwtSettings["Issuer"],
			audience: jwtSettings["Audience"],
			claims: claims,
			expires: DateTime.UtcNow.AddMinutes(
				double.Parse(jwtSettings["ExpiryMinutes"]!)),
			signingCredentials: new SigningCredentials(
				key, SecurityAlgorithms.HmacSha256));

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}

public record RegisterDto(string Email, string Password, string? Role);
public record LoginDto(string Email, string Password);