using System.ComponentModel.DataAnnotations;

namespace CenturyBelongingCalculatorAPI.Features;

public class NoEventExistsException(int EventId): ValidationException($"Event with Id: {EventId} doesn't exist")
{
}
