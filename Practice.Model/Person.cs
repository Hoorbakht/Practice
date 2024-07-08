using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice.Model;

public class Person
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	[MaxLength(50)]
	public string? Name { get; set; }

	[MaxLength(100)]
	public string? Family { get; set; }

	public DateTime CreationDate { get; set; } = DateTime.Now;
}