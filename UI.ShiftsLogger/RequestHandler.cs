using System.Net.Http.Headers;
using System.Net.Http.Json;
using UI.ShiftsLogger.Data;
using UI.ShiftsLogger.Utilities;

namespace UI.ShiftsLogger;

public static class RequestHandler
{
    private static readonly HttpClient _client = new();

    public static void InitializeClient()
    {
        _client.BaseAddress = new Uri("https://localhost:8080/");
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public static async Task<bool> CreateNewShift(Shift shift)
    {
        try
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Shifts", shift);
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception e)
        {
            DisplayUtils.DisplayMessageToUser($"Failed to add the shift to database. \nError Message: {e.Message}");
            return false;
        }
    }

    public static async Task<List<Shift>?> ViewAllShifts()
    {
        List<Shift>? shifts = null;

        try
        {
            HttpResponseMessage response = await _client.GetAsync("api/Shifts");
            response.EnsureSuccessStatusCode();
            shifts = response.Content.ReadFromJsonAsync<List<Shift>>().Result;
        }
        catch (Exception e)
        {
            DisplayUtils.DisplayMessageToUser($"Failed to retrieve shifts from database. \nError Message: {e.Message}");
            return null;
        }

        return shifts;
    }

    public static async Task<bool> UpdateShift(int id, Shift updatedShift)
    {
        try
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/Shifts/{id}", updatedShift);
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception e)
        {
            DisplayUtils.DisplayMessageToUser($"Failed to add the shift to database. \nError Message: {e.Message}");
            return false;
        }
    }

    public static async Task<bool> DeleteShift(int id)
    {
        try
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/Shifts/{id}");
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception e)
        {
            DisplayUtils.DisplayMessageToUser($"Failed to delete the shift from the database. \nError Message: {e.Message}");
            return false;
        }
    }
}
