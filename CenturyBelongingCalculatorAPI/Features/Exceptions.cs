using System.ComponentModel.DataAnnotations;

namespace CenturyBelongingCalculatorAPI.Features;

public class NoEventExistsException(int EventId): ValidationException($"Event with Id: {EventId} doesn't exist!") { }

public class NotAllowedCalcException(string EventName): ValidationException($"Calculation for event: {EventName} is not allowed!") { }

public class EndDateGreaterThenStartDateException():ValidationException("End date greater then start date is ot allowed!") { }