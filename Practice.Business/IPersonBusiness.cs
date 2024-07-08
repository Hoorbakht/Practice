using Practice.Model;

namespace Practice.Business;

public interface IPersonBusiness
{
	Task<List<Person>> LoadAllAsync(CancellationToken cancellationToken);

	Task<Person?> LoadByIdAsync(int id, CancellationToken cancellationToken);

	Task<bool> AddAsync(Person person, CancellationToken cancellationToken);

	Task<bool> UpdateAsync(Person person, CancellationToken cancellationToken);

	Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}