package com.electronicsstore.client.adapters;

import android.graphics.Color;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;
import androidx.annotation.NonNull;
import androidx.cardview.widget.CardView;
import androidx.recyclerview.widget.RecyclerView;
import com.electronicsstore.client.R;
import com.electronicsstore.client.models.Producto;
import java.text.NumberFormat;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;

public class ProductosAdapter extends RecyclerView.Adapter<ProductosAdapter.ProductoViewHolder> {
    
    private List<Producto> productos;
    private OnProductoClickListener listener;
    private NumberFormat currencyFormat;

    public interface OnProductoClickListener {
        void onComprarClick(Producto producto);
    }

    public ProductosAdapter(OnProductoClickListener listener) {
        this.productos = new ArrayList<>();
        this.listener = listener;
        this.currencyFormat = NumberFormat.getCurrencyInstance(new Locale("es", "MX"));
    }

    @NonNull
    @Override
    public ProductoViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.item_producto, parent, false);
        return new ProductoViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull ProductoViewHolder holder, int position) {
        Producto producto = productos.get(position);
        holder.bind(producto);
    }

    @Override
    public int getItemCount() {
        return productos.size();
    }

    public void setProductos(List<Producto> productos) {
        this.productos = productos;
        notifyDataSetChanged();
    }

    class ProductoViewHolder extends RecyclerView.ViewHolder {
        private CardView cardView;
        private TextView tvNombre;
        private TextView tvMarca;
        private TextView tvPrecio;
        private TextView tvStock;
        private TextView tvDescripcion;
        private Button btnComprar;

        public ProductoViewHolder(@NonNull View itemView) {
            super(itemView);
            cardView = itemView.findViewById(R.id.cardProducto);
            tvNombre = itemView.findViewById(R.id.tvNombre);
            tvMarca = itemView.findViewById(R.id.tvMarca);
            tvPrecio = itemView.findViewById(R.id.tvPrecio);
            tvStock = itemView.findViewById(R.id.tvStock);
            tvDescripcion = itemView.findViewById(R.id.tvDescripcion);
            btnComprar = itemView.findViewById(R.id.btnComprar);
        }

        public void bind(Producto producto) {
            tvNombre.setText(producto.getNombreArticulo());
            tvMarca.setText(producto.getMarca());
            tvPrecio.setText(currencyFormat.format(producto.getPrecio()));
            
            // Configurar stock con colores
            String stockText = producto.getCantidad() + " disponibles";
            tvStock.setText(stockText);
            
            if (producto.getCantidad() > 10) {
                tvStock.setTextColor(Color.parseColor("#10b981")); // Verde
            } else if (producto.getCantidad() > 0) {
                tvStock.setTextColor(Color.parseColor("#f59e0b")); // Naranja
            } else {
                tvStock.setTextColor(Color.parseColor("#ef4444")); // Rojo
                stockText = "Agotado";
                tvStock.setText(stockText);
            }

            // Descripción
            if (producto.getDescripcion() != null && !producto.getDescripcion().isEmpty()) {
                tvDescripcion.setText(producto.getDescripcion());
                tvDescripcion.setVisibility(View.VISIBLE);
            } else {
                tvDescripcion.setVisibility(View.GONE);
            }

            // Botón comprar
            if (producto.isDisponible()) {
                btnComprar.setEnabled(true);
                btnComprar.setAlpha(1.0f);
                btnComprar.setOnClickListener(v -> {
                    if (listener != null) {
                        listener.onComprarClick(producto);
                    }
                });
            } else {
                btnComprar.setEnabled(false);
                btnComprar.setAlpha(0.5f);
                btnComprar.setText("Agotado");
            }

            // Animación al hacer click
            cardView.setOnClickListener(v -> {
                // Puedes agregar más funcionalidad aquí si lo deseas
            });
        }
    }
}
