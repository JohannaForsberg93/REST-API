using Koduppgift.Interfaces;
using Koduppgift.Models;
using Microsoft.AspNetCore.Mvc;

namespace Koduppgift.Controllers
	{
	[Route("[controller]")]
	[ApiController]
	public class GroupController : Controller
		{
		private readonly IGroupRepository _groupRepository;
		public GroupController(IGroupRepository groupRepository)
			{
			_groupRepository = groupRepository;
			}

		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[HttpPost("add")]
		public async Task<IActionResult> AddNewGroup([FromBody] Group group)
			{
			var result = await _groupRepository.AddNewGroup(group);

			if (result == null)
				{
				return BadRequest();
				}
			return Ok(result);
			}

		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetGroupById(int id)
			{
			var result = await _groupRepository.GetGroupById(id);

			if (result == null)
				{
				return NotFound();
				}
			return Ok(result);
			}

		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[HttpGet("users/{id}")]
		public async Task<IActionResult> GetUsersByGroupId(int id)

			{
			var result = await _groupRepository.GetUsersByGroupId(id);

			if (result == null)
				{
				return NotFound();
				}
			return Ok(result);
			}

		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[HttpPost("update")]
		public async Task<IActionResult> UpdateGroup([FromBody] Group group)
			{
			var result = await _groupRepository.UpdateGroup(group);

			if (result == null)
				{
				return BadRequest();
				}
			return Ok(result);
			}

		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[HttpDelete("remove")]
		public async Task<IActionResult> DeleteGroup(int id)
			{
			var result = await _groupRepository.DeleteGroup(id);
			if (result == null)
				return BadRequest();

			return Ok(result);
			}
		}
	}
