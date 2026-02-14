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
            try {
                var response = await _httpClient.GetAsync($"{_baseUrl}/Productos");
                if (!response.IsSuccessStatusCode) return new List<Producto>();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Producto>>(content) ?? new List<Producto>();
            } catch { return new List<Producto>(); }
        }

        public async Task<Producto?> GetProductoAsync(int id)
        {
            try {
                var response = await _httpClient.GetAsync($"{_baseUrl}/Productos/{id}");
                if (!response.IsSuccessStatusCode) return null;
                return JsonConvert.DeserializeObject<Producto>(await response.Content.ReadAsStringAsync());
            } catch { return null; }
        }

        public async Task<dynamic?> GetReporteVentasAsync()
        {
            try {
                var response = await _httpClient.GetAsync($"{_baseUrl}/Compras/reporte");
                if (!response.IsSuccessStatusCode) return null;
                return JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
            } catch { return null; }
        }

        public async Task<(bool success, string error, string data)> CreateProductoAsync(Producto p)
        {
            try {
                var content = new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");
                var res = await _httpClient.PostAsync($"{_baseUrl}/Productos", content);
                return (res.IsSuccessStatusCode, res.ReasonPhrase ?? "Error", await res.Content.ReadAsStringAsync());
            } catch (Exception ex) { return (false, ex.Message, ""); }
        }

        public async Task<(bool success, string error)> UpdateProductoAsync(Producto p)
        {
            try {
                var content = new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");
                var res = await _httpClient.PutAsync($"{_baseUrl}/Productos/{p.Id}", content);
                return (res.IsSuccessStatusCode, res.ReasonPhrase ?? "Error");
            } catch (Exception ex) { return (false, ex.Message); }
        }

        public async Task<(bool success, string error)> DeleteProductoAsync(int id)
        {
            try {
                var res = await _httpClient.DeleteAsync($"{_baseUrl}/Productos/{id}");
                return (res.IsSuccessStatusCode, res.ReasonPhrase ?? "Error");
            } catch (Exception ex) { return (false, ex.Message); }
        }
    }
}