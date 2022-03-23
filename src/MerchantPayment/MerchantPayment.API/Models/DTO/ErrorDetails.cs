using System.Collections.Generic;

namespace MerchantPayment.API.Models.DTO;

public class ErrorDetails
{
    public string[] Details { get; set; }

    public ErrorDetails()
    {
        Details  = Array.Empty<string>();
    }

    public ErrorDetails(string error)
    {
        Details = new string[] { error };
    }

    public ErrorDetails(IEnumerable<string> errors)
    {
        Details = errors.ToArray();
    }
};
