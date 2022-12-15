package codewithcal.au.doan.data.api;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.List;

import codewithcal.au.doan.data.model.User;
import retrofit2.Call;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;
import retrofit2.http.GET;
import retrofit2.http.Query;

public interface ApiService {

    //link API https://jsonplaceholder.typicode.com/posts?userId=1
    Gson gson = new GsonBuilder()
            .setDateFormat("yyyy-MM-dd HH:mm:ss")
            .create();
    ApiService apiService = new Retrofit.Builder()
            .baseUrl("https://jsonplaceholder.typicode.com/")
            .addConverterFactory(GsonConverterFactory.create(gson))
            .build()
            .create(ApiService.class);
    @GET("posts")
    Call<List<User>> getListUsers(@Query("userId") int userId);
}
