using ConferenceSolution.Controllers;
using ConferenceSolution.DTOS;
using ConferenceSolution.Erros;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceSolution.API.Test;

public class ParticipantControllerTest
{

    [SetUp]
    public async Task Setup()
    {
        
    }

    [Test]
    [OrderAttribute(1)]
    public async Task GetAll()
    {
        using var application = new WebApplicationFactory<Program>();

        using var client = application.CreateClient();
        var result = await client.GetAsync("api/Participant");

        Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.OK);
    }

    [Test]
    [OrderAttribute(2)]
    public async Task Get()
    {
        using var application = new WebApplicationFactory<Program>();

        using var client = application.CreateClient();
        var result = await client.GetAsync("api/Participant/5e7db5e2-0bcf-4bf1-b9b7-fb40915d8882");

        Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.OK);
    }

    [Test]
    [OrderAttribute(3)]
    public async Task Post()
    {
        using var application = new WebApplicationFactory<Program>();

        using var client = application.CreateClient();

        ParticipantWriteDTO participantWriteDTO = new ParticipantWriteDTO()
        {
            Name = "SomeName3",
            LastName = "SomeLastName3",
            BirthDate = new DateTime(1996, 3, 31)
        };

        string json = JsonConvert.SerializeObject(participantWriteDTO);

        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        Assert.IsTrue((await client.PostAsync("api/Participant", data)).StatusCode == System.Net.HttpStatusCode.OK);
    }

    [Test]
    [OrderAttribute(4)]
    public async Task Post_ExpectErrorCode()
    {
        using var application = new WebApplicationFactory<Program>();

        using var client = application.CreateClient();

        ParticipantWriteDTO participantWriteDTO = new ParticipantWriteDTO()
        {
            Name = "SomeName1",
            LastName = "SomeLastName1",
            BirthDate = new DateTime(1996, 3, 31)
        };

        string json = JsonConvert.SerializeObject(participantWriteDTO);

        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await client.PostAsync("api/Participant", data);

        string jsonContent = await result.Content.ReadAsStringAsync();

        ErrorInfo errorInfo = JsonConvert.DeserializeObject<ErrorInfo>(jsonContent);

        Assert.IsTrue(errorInfo.ErrorCodeType == Enums.ErrorCodeType.DuplicateParticipantWithSameFullName);

    }

    [Test]
    [OrderAttribute(5)]
    public async Task Put()
    {
        using var application = new WebApplicationFactory<Program>();

        using var client = application.CreateClient();

        ParticipantWriteDTO participantWriteDTO = new ParticipantWriteDTO()
        {
            Name = "SomeName100",
            LastName = "SomeLastName100",
            BirthDate = new DateTime(1996, 3, 31)
        };

        string json = JsonConvert.SerializeObject(participantWriteDTO);

        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await client.PutAsync("api/Participant/5e7db5e2-0bcf-4bf1-b9b7-fb40915d8882", data);

        Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.OK);
    }

    [Test]
    [OrderAttribute(6)]
    public async Task Delete()
    {
        using var application = new WebApplicationFactory<Program>();

        using var client = application.CreateClient();

        var result = await client.DeleteAsync("api/Participant/5e7db5e2-0bcf-4bf1-b9b7-fb40915d8882");

        Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.OK);
    }
}