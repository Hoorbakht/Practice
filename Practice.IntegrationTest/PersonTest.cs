using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Practice.Model;

namespace Practice.IntegrationTest;

public class PersonTest(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
	#region [Test Method(s)]

	[Fact]
	public async void GetAll_ReturnExpectedResult()
	{
		var response = await factory.CreateDefaultClient().GetAsync("/Person");

		response.StatusCode.Should().Be(HttpStatusCode.OK);

		var result = await response.Content.ReadAsStringAsync();

		var convertedResult = JsonConvert.DeserializeObject<List<Person>>(result);

		convertedResult.Should().NotBeNullOrEmpty();

		convertedResult!.SingleOrDefault(x => x.Name == "John").Should().NotBeNull();
	}

	[Theory]
	[MemberData(nameof(GetByIdData))]
	public async void GetById_ReturnExpectedResult(int id, bool expectedResult)
	{
		var response = await factory.CreateDefaultClient().GetAsync("/Person/" + id);

		var result = await response.Content.ReadAsStringAsync();

		var convertedResult = JsonConvert.DeserializeObject<Person>(result);

		if (!expectedResult)
		{
			convertedResult.Should().BeNull();
			response.StatusCode.Should().Be(HttpStatusCode.NoContent);
			return;
		}

		convertedResult.Should().NotBeNull();

		convertedResult!.Name.Should().Be("John");
	}

	[Theory]
	[MemberData(nameof(AddData))]
	public async void Add_ReturnExpectedResult(Person person, bool expectedResult)
	{
		var response = await factory.CreateDefaultClient().PostAsJsonAsync("/Person/", person);

		var result = await response.Content.ReadAsStringAsync();

		var convertedResult = JsonConvert.DeserializeObject<bool>(result);

		if (!expectedResult)
			convertedResult.Should().BeFalse();
		else
			convertedResult.Should().BeTrue();
	}

	[Theory]
	[MemberData(nameof(DeleteData))]
	public async void Delete_ReturnExpectedResult(int id, bool expectedResult)
	{
		var response = await factory.CreateDefaultClient().DeleteAsync("/Person/" + id);

		var result = await response.Content.ReadAsStringAsync();

		var convertedResult = JsonConvert.DeserializeObject<bool>(result);

		if (!expectedResult)
			convertedResult.Should().BeFalse();
		else
			convertedResult.Should().BeTrue();
	}

	[Theory]
	[MemberData(nameof(UpdateData))]
	public async void Update_ReturnExpectedResult(Person person, bool expectedResult)
	{
		var response = await factory.CreateDefaultClient().PutAsJsonAsync("/Person/", person);

		var result = await response.Content.ReadAsStringAsync();

		var convertedResult = JsonConvert.DeserializeObject<bool>(result);

		if (!expectedResult)
			convertedResult.Should().BeFalse();
		else
			convertedResult.Should().BeTrue();
	}

	#endregion

	#region [Data Method(s)]

	public static List<object[]> AddData() =>
	[
		[
			new Person
			{
				Name = "Mahyar",
				Family = "Hoorbakht"
			},
			true
		],
		[
			new Person
			{
				Name = "JohnJohnJohn",
				Family = "Doe"
			},
			false
		]
	];

	public static List<object[]> UpdateData() =>
	[
		[
			new Person
			{
				Name = "Mahyar",
				Family = "Hoorbakht"
			},
			true
		],
		[
			new Person
			{
				Name = "JohnJohnJohn",
				Family = "Doe"
			},
			false
		]
	];

	public static List<object[]> GetByIdData() =>
	[
		[
			1,
			true
		],
		[
			2,
			false
		]
	];

	public static List<object[]> DeleteData() =>
	[
		[
			1,
			true
		],
		[
			2,
			false
		]
	];

	#endregion
}