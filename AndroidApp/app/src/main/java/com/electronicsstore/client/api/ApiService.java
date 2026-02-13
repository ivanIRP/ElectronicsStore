package com.electronicsstore.client.api;

import com.electronicsstore.client.models.Compra;
import com.electronicsstore.client.models.Producto;
import java.util.List;
import retrofit2.Call;
import retrofit2.http.*;

public interface ApiService {
    
    // Productos
    @GET("Productos")
    Call<List<Producto>> getProductos();

    @GET("Productos/disponibles")
    Call<List<Producto>> getProductosDisponibles();

    @GET("Productos/{id}")
    Call<Producto> getProducto(@Path("id") int id);

    // Compras
    @POST("Compras")
    Call<Compra> crearCompra(@Body Compra compra);

    @GET("Compras")
    Call<List<Compra>> getCompras();
}
