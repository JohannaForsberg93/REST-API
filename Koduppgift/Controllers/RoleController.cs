using Koduppgift.Dtos;
using Koduppgift.Interfaces;
using Koduppgift.Models;
using Microsoft.AspNetCore.Mvc;

namespace Koduppgift.Controllers
	{
	[Route("[controller]")]
	[ApiController]
	public class RoleController : Controller
		{
		private readonly IRoleRepository _roleRepository;
		public RoleController(IRoleRepository roleRepository)
			{
			_roleRepository = roleRepository;
			}

		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[HttpPost("add")]
		public async Task<IActionResult> AddNewRole([FromBody] RoleDto role)
			{
			var result = await _roleRepository.AddNewRole(role);

			if (result == null)
				{
				return BadRequest();
				}
			return Ok(result);
			}

		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetRoleById(int id)
			{
			var result = await _roleRepository.GetRoleById(id);

			if (result == null)
				{
				return NotFound();
				}
			return Ok(result);
			}

		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[HttpGet("/users/{roleName}")]
		public async Task<IActionResult> GetUsersByRoleName(string roleName)

			{
			var result = await _roleRepository.GetUsersByRoleName(roleName);

			if (result == null)
				{
				return NotFound();
				}
			return Ok(result);
			}

		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[HttpPut("update")]
		public async Task<IActionResult> UpdateRole([FromBody] RoleDto role)
			{
			var result = await _roleRepository.UpdateRole(role);

			if (result == null)
				{
				return BadRequest();
				}
			return Ok(result);
			}

		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[HttpDelete("remove/{id}")]

		public async Task<IActionResult> DeleteRole(int id)
			{
			var result = await _roleRepository.DeleteRole(id);
			if (result == null)
				{
				return BadRequest();
				}
			return Ok(result);
			}
		}
	}
