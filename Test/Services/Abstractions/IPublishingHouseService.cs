using Test.DTOs;

namespace Test.Services.Abstractions;

public interface IPublishingHouseService
{
    public Task<List<PublishingHouseDTO>> GetAllAsync(string? country, string? city);
}