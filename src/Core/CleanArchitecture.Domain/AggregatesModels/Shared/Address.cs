namespace CleanArchitecture.Domain.AggregatesModels.Shared;

public record Address
{
    public string Street { get; private set; }
    public string City { get; private set; }

    public Address(string street, string city)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Street cannot be empty", nameof(street));
            
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City cannot be empty", nameof(city));

        Street = street;
        City = city;
    }
}
