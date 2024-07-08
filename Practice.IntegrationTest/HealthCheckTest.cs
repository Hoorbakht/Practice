using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Runtime.Serialization;
using FluentAssertions;

namespace Practice.IntegrationTest;

public class HealthCheckTest(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
	[Fact]
	public async void HealthCheck_ReturnExpectedResult()
	{
		var response = await factory.CreateDefaultClient().GetAsync("/HealthChecks");

		response.StatusCode.Should().Be(HttpStatusCode.OK);
		response.Content.Headers.ContentLength.Should().BeGreaterThan(0);
		var result = await response.Content.ReadAsStringAsync();
		result.Should().NotBeNullOrWhiteSpace();
		result.Should().Be("Healthy");
	}
}