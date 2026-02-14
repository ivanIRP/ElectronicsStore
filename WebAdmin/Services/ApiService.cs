using Newtonsoft.Json;
using System.Text;
using ElectronicsStoreWeb.Models;

namespace ElectronicsStoreWeb.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ApiService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5000/api";
        }

        public async Task<List<Producto>> GetProductosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/Productos");
                if (!response.IsSuccessStatusCode) return new List<Producto>();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Producto>>(content) ?? new List<Producto>();
            }
            catch { return new List<Producto>(); }
        }

        public async Task<Producto?> GetProductoAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/Productos/{id}");
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Producto>(content);
            }
            catch { return null; }
        }

        // CORRECCIÓN: Ahora devuelve una Tupla (bool, string, string)
        public async Task<(bool success, string errorMessage, string responseData)> CreateProductoAsync(Producto producto)
        {
            try
            {
                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_baseUrl}/Productos", content);
                var responseData = await response.Content.ReadAsStringAsync();
                
                return (response.IsSuccessStatusCode, response.ReasonPhrase ?? "Error", responseData);
            }
            catch (Exception ex) { return (false, ex.Message, ""); }
        }

        // CORRECCIÓN: Ahora devuelve una Tupla (bool, string)
        public async Task<(bool success, string errorMessage)> UpdateProductoAsync(Producto producto)
        {
            try
            {
                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_baseUrl}/Productos/{producto.Id}", content);
                return (response.IsSuccessStatusCode, response.ReasonPhrase ?? "Error");
            }
            catch (Exception ex) { return (false, ex.Message); }
        }

        // CORRECCIÓN: Ahora devuelve una Tupla (bool, string)
        public async Task<(bool success, string errorMessage)> DeleteProductoAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/Productos/{id}");
                return (response.IsSuccessStatusCode, response.ReasonPhrase ?? "Error");
            }
            catch (Exception ex) { return (false, ex.Message); }
        }
    }
}