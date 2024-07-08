using Microsoft.EntityFrameworkCore;
using Practice.DataAccess;
using Practice.Model;
using System;

namespace Practice.Business;

public class PersonBusiness(PracticeContext practiceContext) : IPersonBusiness
{
	public async Task<List<Person>> LoadAllAsync(CancellationToken cancellationToken = default) =>
		await practiceContext.Persons.ToListAsync(cancellationToken);

	public async Task<Person?> LoadByIdAsync(int id, CancellationToken cancellationToken = default) =>
		await practiceContext.Persons.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

	public async Task<bool> AddAsync(Person person, CancellationToken cancellationToken = default)
	{
		try
		{
			if (person.Name!.Length > 10)
				return false;

			await practiceContext.AddAsync(person, cancellationToken);
			await practiceContext.SaveChangesAsync(cancellationToken);
			return true;
		}
		catch
		{
			return false;
		}
	}

	public async Task<bool> UpdateAsync(Person person, CancellationToken cancellationToken = default)
	{
		try
		{
			if (person.Name!.Length > 10)
				return false;

			practiceContext.Update(person);
			await practiceContext.SaveChangesAsync(cancellationToken);
			return true;
		}
		catch
		{
			return false;
		}
	}

	public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
	{
		try
		{
			var exist = await LoadByIdAsync(id, cancellationToken);

			if (exist is null)
				return false;

			practiceContext.Remove(exist);
			await practiceContext.SaveChangesAsync(cancellationToken);
			return true;
		}
		catch
		{
			return false;
		}
	}
}