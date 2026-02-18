namespace CleanArchitecture.Domain.AggregatesModels.Shared;

public record Phone
{
    private const int DefaultLength = 10;
    
    public string Value { get; init; }

    public Phone(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
             throw new ArgumentException("Phone number cannot be empty", nameof(value));

        if (value.Length != DefaultLength)
             throw new ArgumentException("Phone number must be 10 digits", nameof(value));
             
        Value = value;
    }
}
