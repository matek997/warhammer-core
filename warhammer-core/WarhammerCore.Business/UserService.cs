using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WarhammerCore.Abstract.Exceptions;
using WarhammerCore.Abstract.Interfaces;
using WarhammerCore.Abstract.Models.User;

namespace WarhammerCore.Business
{
	public class UserService : IUserService
	{
		private readonly IConfiguration _config;
		private readonly IDataRepo _repo;
		public UserService(IDataRepo repo,
				IConfiguration config
				)
		{
			_config = config;
			_repo = repo;
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public async Task<UserAuthInfo> SignInAsync(string email, string password)
		{
			var user = await _repo.GetUserByEmailAsync(email);

			if (UserInfo.IsNullOrEmpty(user)) return null;

			var auth = new UserAuthInfo(user.UserId, GenerateJSONWebToken(user));

			return auth;
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public async Task<bool> SignOutByTokenAsync(string token)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public async Task<bool> SignOutByUserIdAsync(string userId)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public async Task<string> SignUpAsync(string email, string password)
		{
			UserInfo user = await _repo.GetUserByEmailAsync(email);

			if (!UserInfo.IsNullOrEmpty(user)) throw new AppBusinessException($"User with email {email} already registered", "EmailAreadyExists");

			string userId = await _repo.CreateUserAsync(email, password);

			return userId;
		}

		private string GenerateJSONWebToken(UserInfo user)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			var claims = new List<Claim> { new Claim("email", user.Email) };
			var token = new JwtSecurityToken(_config["Jwt:Issuer"],
				_config["Jwt:Issuer"],
				claims,
				expires: DateTime.Now.AddMinutes(120),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}