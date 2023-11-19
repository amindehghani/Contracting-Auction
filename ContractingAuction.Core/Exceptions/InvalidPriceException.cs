namespace ContractingAuction.Core.Exceptions;

public class InvalidPriceException : Exception
{
    public InvalidPriceException()
    {
        
    }

    public InvalidPriceException(string? message) : base(message)
    {
        
    }
}