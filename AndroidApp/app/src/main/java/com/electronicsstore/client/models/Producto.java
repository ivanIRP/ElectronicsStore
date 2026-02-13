package com.electronicsstore.client.models;

import com.google.gson.annotations.SerializedName;
import java.io.Serializable;

public class Producto implements Serializable {
    @SerializedName("id")
    private int id;

    @SerializedName("nombreArticulo")
    private String nombreArticulo;

    @SerializedName("marca")
    private String marca;

    @SerializedName("precio")
    private double precio;

    @SerializedName("cantidad")
    private int cantidad;

    @SerializedName("descripcion")
    private String descripcion;

    @SerializedName("fechaRegistro")
    private String fechaRegistro;

    // Constructores
    public Producto() {
    }

    public Producto(int id, String nombreArticulo, String marca, double precio, int cantidad, String descripcion) {
        this.id = id;
        this.nombreArticulo = nombreArticulo;
        this.marca = marca;
        this.precio = precio;
        this.cantidad = cantidad;
        this.descripcion = descripcion;
    }

    // Getters y Setters
    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getNombreArticulo() {
        return nombreArticulo;
    }

    public void setNombreArticulo(String nombreArticulo) {
        this.nombreArticulo = nombreArticulo;
    }

    public String getMarca() {
        return marca;
    }

    public void setMarca(String marca) {
        this.marca = marca;
    }

    public double getPrecio() {
        return precio;
    }

    public void setPrecio(double precio) {
        this.precio = precio;
    }

    public int getCantidad() {
        return cantidad;
    }

    public void setCantidad(int cantidad) {
        this.cantidad = cantidad;
    }

    public String getDescripcion() {
        return descripcion;
    }

    public void setDescripcion(String descripcion) {
        this.descripcion = descripcion;
    }

    public String getFechaRegistro() {
        return fechaRegistro;
    }

    public void setFechaRegistro(String fechaRegistro) {
        this.fechaRegistro = fechaRegistro;
    }

    public boolean isDisponible() {
        return cantidad > 0;
    }
}
