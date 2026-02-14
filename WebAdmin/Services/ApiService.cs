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

        // Productos
        public async Task<List<Producto>> GetProductosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/Productos");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Producto>>(content) ?? new List<Producto>();
            }
            catch
            {
                return new List<Producto>();
            }
        }

        public async Task<Producto?> GetProductoAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/Productos/{id}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Producto>(content);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CreateProductoAsync(Producto producto)
        {
            try
            {
                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_baseUrl}/Productos", content);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateProductoAsync(Producto producto)
        {
            try
            {
                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_baseUrl}/Productos/{producto.Id}", content);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteProductoAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/Productos/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        // Compras
        public async Task<dynamic?> GetReporteVentasAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/Compras/reporte");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<dynamic>(content);
            }
            catch
            {
                return null;
            }
        }
    }
}
