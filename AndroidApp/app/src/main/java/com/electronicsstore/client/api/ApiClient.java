package com.electronicsstore.client.api;

import okhttp3.OkHttpClient;
import okhttp3.logging.HttpLoggingInterceptor;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;
import java.util.concurrent.TimeUnit;

public class ApiClient {
    // IMPORTANTE: Cambiar esta URL por la IP de tu computadora
    // Para emulador Android: usar 10.0.2.2
    // Para dispositivo físico: usar la IP local de tu PC (ej: 192.168.1.100)
    private static final String BASE_URL = "http://10.0.2.2:5000/api/";
    
    private static Retrofit retrofit = null;
    private static ApiService apiService = null;

    public static ApiService getApiService() {
        if (apiService == null) {
            // Configurar logging
            HttpLoggingInterceptor logging = new HttpLoggingInterceptor();
            logging.setLevel(HttpLoggingInterceptor.Level.BODY);

            // Configurar cliente HTTP
            OkHttpClient client = new OkHttpClient.Builder()
                    .addInterceptor(logging)
                    .connectTimeout(30, TimeUnit.SECONDS)
                    .readTimeout(30, TimeUnit.SECONDS)
                    .writeTimeout(30, TimeUnit.SECONDS)
                    .build();

            // Configurar Retrofit
            retrofit = new Retrofit.Builder()
                    .baseUrl(BASE_URL)
                    .client(client)
                    .addConverterFactory(GsonConverterFactory.create())
                    .build();

            apiService = retrofit.create(ApiService.class);
        }
        return apiService;
    }
}
