package com.electronicsstore.client.models;

import com.google.gson.annotations.SerializedName;

public class Compra {
    @SerializedName("id")
    private int id;

    @SerializedName("productoId")
    private int productoId;

    @SerializedName("cantidad")
    private int cantidad;

    @SerializedName("precioTotal")
    private double precioTotal;

    @SerializedName("fechaCompra")
    private String fechaCompra;

    @SerializedName("clienteInfo")
    private String clienteInfo;

    // Constructores
    public Compra() {
    }

    public Compra(int productoId, int cantidad) {
        this.productoId = productoId;
        this.cantidad = cantidad;
    }

    // Getters y Setters
    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getProductoId() {
        return productoId;
    }

    public void setProductoId(int productoId) {
        this.productoId = productoId;
    }

    public int getCantidad() {
        return cantidad;
    }

    public void setCantidad(int cantidad) {
        this.cantidad = cantidad;
    }

    public double getPrecioTotal() {
        return precioTotal;
    }

    public void setPrecioTotal(double precioTotal) {
        this.precioTotal = precioTotal;
    }

    public String getFechaCompra() {
        return fechaCompra;
    }

    public void setFechaCompra(String fechaCompra) {
        this.fechaCompra = fechaCompra;
    }

    public String getClienteInfo() {
        return clienteInfo;
    }

    public void setClienteInfo(String clienteInfo) {
        this.clienteInfo = clienteInfo;
    }
}
