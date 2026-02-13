package com.electronicsstore.client;

import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.app.AlertDialog;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;
import androidx.swiperefreshlayout.widget.SwipeRefreshLayout;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.EditText;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;
import com.electronicsstore.client.adapters.ProductosAdapter;
import com.electronicsstore.client.api.ApiClient;
import com.electronicsstore.client.api.ApiService;
import com.electronicsstore.client.models.Compra;
import com.electronicsstore.client.models.Producto;
import java.util.List;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class MainActivity extends AppCompatActivity implements ProductosAdapter.OnProductoClickListener {

    private RecyclerView recyclerView;
    private ProductosAdapter adapter;
    private SwipeRefreshLayout swipeRefresh;
    private ProgressBar progressBar;
    private TextView tvEmpty;
    private ApiService apiService;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        // Inicializar vistas
        initViews();

        // Configurar API
        apiService = ApiClient.getApiService();

        // Configurar RecyclerView
        setupRecyclerView();

        // Cargar productos
        cargarProductos();
    }

    private void initViews() {
        recyclerView = findViewById(R.id.recyclerView);
        swipeRefresh = findViewById(R.id.swipeRefresh);
        progressBar = findViewById(R.id.progressBar);
        tvEmpty = findViewById(R.id.tvEmpty);

        // Configurar swipe refresh
        swipeRefresh.setOnRefreshListener(this::cargarProductos);
        swipeRefresh.setColorSchemeResources(
                android.R.color.holo_blue_bright,
                android.R.color.holo_green_light,
                android.R.color.holo_orange_light,
                android.R.color.holo_red_light
        );
    }

    private void setupRecyclerView() {
        adapter = new ProductosAdapter(this);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        recyclerView.setAdapter(adapter);
    }

    private void cargarProductos() {
        showLoading(true);
        
        apiService.getProductosDisponibles().enqueue(new Callback<List<Producto>>() {
            @Override
            public void onResponse(Call<List<Producto>> call, Response<List<Producto>> response) {
                showLoading(false);
                swipeRefresh.setRefreshing(false);

                if (response.isSuccessful() && response.body() != null) {
                    List<Producto> productos = response.body();
                    if (productos.isEmpty()) {
                        showEmptyState(true);
                    } else {
                        showEmptyState(false);
                        adapter.setProductos(productos);
                    }
                } else {
                    Toast.makeText(MainActivity.this, 
                            "Error al cargar productos", 
                            Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(Call<List<Producto>> call, Throwable t) {
                showLoading(false);
                swipeRefresh.setRefreshing(false);
                Toast.makeText(MainActivity.this, 
                        "Error de conexión: " + t.getMessage(), 
                        Toast.LENGTH_LONG).show();
            }
        });
    }

    @Override
    public void onComprarClick(Producto producto) {
        mostrarDialogoCompra(producto);
    }

    private void mostrarDialogoCompra(Producto producto) {
        View dialogView = LayoutInflater.from(this).inflate(R.layout.dialog_compra, null);
        EditText etCantidad = dialogView.findViewById(R.id.etCantidad);
        TextView tvProducto = dialogView.findViewById(R.id.tvProductoNombre);
        TextView tvPrecioUnitario = dialogView.findViewById(R.id.tvPrecioUnitario);
        TextView tvTotal = dialogView.findViewById(R.id.tvTotal);

        tvProducto.setText(producto.getNombreArticulo());
        tvPrecioUnitario.setText(String.format("Precio unitario: $%.2f", producto.getPrecio()));

        // Calcular total dinámicamente
        etCantidad.addTextChangedListener(new android.text.TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {}

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                try {
                    if (!s.toString().isEmpty()) {
                        int cantidad = Integer.parseInt(s.toString());
                        double total = cantidad * producto.getPrecio();
                        tvTotal.setText(String.format("Total: $%.2f", total));
                    } else {
                        tvTotal.setText("Total: $0.00");
                    }
                } catch (NumberFormatException e) {
                    tvTotal.setText("Total: $0.00");
                }
            }

            @Override
            public void afterTextChanged(android.text.Editable s) {}
        });

        new AlertDialog.Builder(this)
                .setTitle("Realizar Compra")
                .setView(dialogView)
                .setPositiveButton("Comprar", (dialog, which) -> {
                    String cantidadStr = etCantidad.getText().toString().trim();
                    if (cantidadStr.isEmpty()) {
                        Toast.makeText(this, "Ingrese una cantidad", Toast.LENGTH_SHORT).show();
                        return;
                    }

                    int cantidad = Integer.parseInt(cantidadStr);
                    if (cantidad <= 0) {
                        Toast.makeText(this, "La cantidad debe ser mayor a 0", Toast.LENGTH_SHORT).show();
                        return;
                    }

                    if (cantidad > producto.getCantidad()) {
                        Toast.makeText(this, 
                                "Stock insuficiente. Disponible: " + producto.getCantidad(), 
                                Toast.LENGTH_SHORT).show();
                        return;
                    }

                    realizarCompra(producto.getId(), cantidad);
                })
                .setNegativeButton("Cancelar", null)
                .show();
    }

    private void realizarCompra(int productoId, int cantidad) {
        Compra compra = new Compra(productoId, cantidad);
        
        apiService.crearCompra(compra).enqueue(new Callback<Compra>() {
            @Override
            public void onResponse(Call<Compra> call, Response<Compra> response) {
                if (response.isSuccessful()) {
                    Toast.makeText(MainActivity.this, 
                            "¡Compra realizada exitosamente!", 
                            Toast.LENGTH_LONG).show();
                    cargarProductos(); // Recargar lista para actualizar stock
                } else {
                    Toast.makeText(MainActivity.this, 
                            "Error al realizar la compra", 
                            Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(Call<Compra> call, Throwable t) {
                Toast.makeText(MainActivity.this, 
                        "Error de conexión: " + t.getMessage(), 
                        Toast.LENGTH_LONG).show();
            }
        });
    }

    private void showLoading(boolean show) {
        progressBar.setVisibility(show ? View.VISIBLE : View.GONE);
        recyclerView.setVisibility(show ? View.GONE : View.VISIBLE);
    }

    private void showEmptyState(boolean show) {
        tvEmpty.setVisibility(show ? View.VISIBLE : View.GONE);
        recyclerView.setVisibility(show ? View.GONE : View.VISIBLE);
    }
}
