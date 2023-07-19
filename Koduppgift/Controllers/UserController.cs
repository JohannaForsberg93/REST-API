using Koduppgift.Dtos;
using Koduppgift.Interfaces;
using Koduppgift.Models;
using Microsoft.AspNetCore.Mvc;

namespace Koduppgift.Controllers
	{
	[Route("[controller]")]
	[ApiController]
	public class UserController : ControllerBase
		{
		private readonly IUserRepository _userRepository;

		public UserController(IUserRepository userRepository)
			{
			_userRepository = userRepository;
			}

		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[HttpPost("add")]
		public async Task<IActionResult> AddNewUser([FromBody] UserCreateDto user)
			{
			var result = await _userRepository.AddNewUser(user);

			if (result == null)
				{
				return BadRequest("Något gick fel!");
				}
			return Ok(result);
			}


		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetUser(int id)
			{
			var user = await _userRepository.GetUser(id);
			if (user == null)
				{
				return NotFound("Kunde inte hitta en användare med valt id.");
				}
			return Ok(user);

			}

		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[HttpGet("role/{roleName}")]
		public async Task<IActionResult> GetUserByRoleName(string roleName)
			{
			var users = await _userRepository.GetUserByRoleName(roleName);
			if (users == null)
				{
				return NotFound("Kunde inte hitta en användare med den rollen.");
				}
			return Ok(users);
			}

		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[HttpGet("group/{groupName}")]
		public async Task<IActionResult> GetUsersByGroupName(string groupName)
			{
			var users = await _userRepository.GetUsersByGroupName(groupName);
			if (users == null)
				{
				return NotFound("Kunde inte hitta någon användare i den gruppen.");
				}
			return Ok(users);
			}

		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[HttpPut("update/{id}")]

		public async Task<IActionResult> UpdateUser([FromBody] UserDto user)
			{
			var users = await _userRepository.UpdateUser(user);
			if (users == null)
			{
				return BadRequest("Något gick fel!");
			}
			return Ok(users);
			}

		}

	}
