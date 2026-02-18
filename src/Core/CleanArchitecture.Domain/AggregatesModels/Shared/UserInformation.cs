namespace CleanArchitecture.Domain.AggregatesModels.Shared;

public record UserInformation
{
    public string Name { get; private set; }
    public Phone Phone { get; private set; }
    public Address Address { get; private set; }

    protected UserInformation() { }

    public UserInformation(string name, Phone phone, Address address)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));
            
        Name = name;
        Phone = phone ?? throw new ArgumentNullException(nameof(phone));
        Address = address ?? throw new ArgumentNullException(nameof(address));
    }
}
