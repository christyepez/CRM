using CRM.Domain.Customer360;
using Xunit;

namespace CRM.UnitTests;

public sealed class Customer360PolicyTests
{
    [Fact]
    public void ValidSnapshot_IsNormalized()
    {
        var id = Guid.NewGuid();
        var result = Customer360Policy.Validate(new(id.ToString("B"), "  Acme  ", 2, 1, 0, 4, 3, 1, 2, 1));
        Assert.True(result.Valid);
        Assert.Equal(id.ToString("D"), result.NormalizedSnapshot!.CustomerId);
        Assert.Equal("Acme", result.NormalizedSnapshot.DisplayName);
    }

    [Fact]
    public void InvalidId_IsRejected() =>
        Assert.Equal(Customer360ValidationErrorCode.InvalidCustomerId, Customer360Policy.Validate(new("bad", "Acme",0,0,0,0,0,0,0,0)).ErrorCode);

    [Fact]
    public void EmptyDisplayName_IsRejected() =>
        Assert.Equal(Customer360ValidationErrorCode.DisplayNameRequired, Customer360Policy.Validate(new(Guid.NewGuid().ToString(), "  ",0,0,0,0,0,0,0,0)).ErrorCode);

    [Fact]
    public void NegativeCount_IsRejected() =>
        Assert.Equal(Customer360ValidationErrorCode.NegativeAggregateCount, Customer360Policy.Validate(new(Guid.NewGuid().ToString(), "Acme",0,-1,0,0,0,0,0,0)).ErrorCode);
}
