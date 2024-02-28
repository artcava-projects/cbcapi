
//using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CenturyBelongingCalculatorAPI.Features;

public static class InternalTasks
{
    public static async Task<bool> AddCalc(string? objectId, CalcResult json)
    {
        if (string.IsNullOrWhiteSpace(objectId))
            throw new NoUserAuthenticatedException("AddCalc");

        List<CalcResult> calcs = [];

        if (!Directory.Exists("UsersData")) Directory.CreateDirectory("UsersData");

        var filename = $".\\UsersData\\{objectId}.json";
        if (File.Exists(filename))
        {
            await using FileStream sJson = File.OpenRead(filename);
            calcs = JsonSerializer.Deserialize<List<CalcResult>>(sJson) ?? ([]);
        }

        var calc = calcs.FirstOrDefault(c => c.CalcName == json.CalcName);
        if (calc == null)
        {
            calcs.Add(json);
        }
        else { 
            calc.StartDate = json.StartDate;
            calc.EventDate=json.EventDate;
            calc.EndDate=json.EndDate;
            calc.EventName=json.EventName;
            calc.EventDescription=json.EventDescription;
        }

        await using var fileStream = File.Create(filename);
        await JsonSerializer.SerializeAsync(fileStream, calcs, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

        return true;
    }
}
