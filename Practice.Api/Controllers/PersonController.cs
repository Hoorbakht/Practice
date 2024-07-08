using Microsoft.AspNetCore.Mvc;
using Practice.Business;
using Practice.Model;

namespace Practice.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController(IPersonBusiness personBusiness) : ControllerBase
{
	[HttpGet]
	[HttpHead]
	public async Task<IActionResult> GetAsync(CancellationToken cancellationToken) =>
		Ok(await personBusiness.LoadAllAsync(cancellationToken));

	[HttpGet]
	[Route("{id:int}")]
	public async Task<IActionResult> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken) =>
		Ok(await personBusiness.LoadByIdAsync(id, cancellationToken));

	[HttpPost]
	public async Task<IActionResult> AddAsync([FromBody] Person person, CancellationToken cancellationToken) =>
		Ok(await personBusiness.AddAsync(person, cancellationToken));

	[HttpPut]
	public async Task<IActionResult> UpdateAsync([FromBody] Person person, CancellationToken cancellationToken) =>
		Ok(await personBusiness.UpdateAsync(person, cancellationToken));

	[HttpDelete]
	[Route("{id:int}")]
	public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancellationToken) =>
		Ok(await personBusiness.DeleteAsync(id, cancellationToken));
}