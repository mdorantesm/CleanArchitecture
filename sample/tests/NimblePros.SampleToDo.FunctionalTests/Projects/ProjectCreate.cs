﻿using NimblePros.SampleToDo.Web.Projects;
using Shouldly;

namespace NimblePros.SampleToDo.FunctionalTests.Projects;

[Collection("Sequential")]
public class ProjectCreate : IClassFixture<CustomWebApplicationFactory<Program>>
{
  private readonly HttpClient _client;

  public ProjectCreate(CustomWebApplicationFactory<Program> factory)
  {
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task ReturnsOneProject()
  {
    var testName = Guid.NewGuid().ToString();
    var request = new CreateProjectRequest() { Name = testName };
    var content = StringContentHelpers.FromModelAsJson(request);

    var result = await _client.PostAndDeserializeAsync<CreateProjectResponse>(
        CreateProjectRequest.Route, content);

    result.Name.ShouldBe(testName);
    result.Id.ShouldBeGreaterThan(0);
  }
}
