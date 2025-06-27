using AppData.Model;
using APPMVC.IService;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace APPMVC.Service
{
    public class CardService : ICardService
    {
        private readonly HttpClient _httpClient;

        public CardService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7198")
            };
        }

        public async Task Create(GioHang gioHang)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Card/them", gioHang);
            response.EnsureSuccessStatusCode(); // Ensure the request was successful
        }

        public async Task<Guid> GetCartIdByCustomerIdAsync(Guid customerId)
        {
            // Check if the customerId is valid
            if (customerId == Guid.Empty)
            {
                Console.WriteLine("Invalid customer ID provided.");
                return Guid.Empty; // Return Guid.Empty if customerId is invalid
            }

            var requestUrl = $"api/Card/getbyid?id={customerId}";

            try
            {
                // Send the HTTP GET request
                var response = await _httpClient.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode(); // Ensure we have a successful response

                // Read the response content
                var jsonString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response content: {jsonString}"); // Log the response content

                jsonString = jsonString.Trim().Trim('"'); // Trim whitespace and quotes

                // Try to parse the response directly as a GUID
                if (Guid.TryParse(jsonString, out Guid cartId))
                {
                    return cartId; // Return the parsed GUID directly
                }

                // If not a valid GUID, log an error and return Guid.Empty
                Console.WriteLine("Response content is not a valid GUID.");
                return Guid.Empty;
            }
            catch (HttpRequestException httpEx)
            {
                // Log specific HTTP request errors
                Console.WriteLine($"HTTP request error for customer ID {customerId}: {httpEx.Message}");
                return Guid.Empty; // Return Guid.Empty in case of an HTTP error
            }
            catch (Exception ex)
            {
                // Log general errors
                Console.WriteLine($"Error fetching cart ID for customer {customerId}: {ex.Message}");
                return Guid.Empty; // Return Guid.Empty in case of a general error
            }
        }
    }

    public class CartResponse
    {
        public Guid IdGioHang { get; set; }
        // Add other properties as needed based on the API response
    }
}