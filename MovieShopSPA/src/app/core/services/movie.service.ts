import { HttpClient} from '@angular/common/http'
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MovieCard } from 'src/app/shared/models/movieCard';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { MovieDetailsComponent } from 'src/app/movies/movie-details/movie-details.component';
import { movieDetails } from 'src/app/shared/models/movieDetails';
@Injectable({
  providedIn: 'root'
})
export class MovieService {
  
  // should have all the methods that deals with Movies, getById.
  // HttpClient to make AJAX request
  // XMLHttpRequest =>
  // private readonly,inject
  constructor(private http:HttpClient) { }

  
  getTopRevenueMovies():Observable<MovieCard[]>{
    //https://localhost:44361/api/Movies/toprated
    //model based on JSON data
    //Observables are lazy, only when u subscribe to an observable u will get the data.
    //  Youtube => channels
    //  Xbox => videos

    //localhost:53320(browser) => AJAX call to http address

    return this.http.get(`${environment.apiUrl}`+'Movies/toprevenue')
    .pipe(
      map(resp => resp as MovieCard[])
    )
      // movies/where(m => m.bugget)
  }

  getMovieDetails(id:number):Observable<movieDetails>{

    return this.http.get(`${environment.apiUrl}`+'Movies/'+id)
    .pipe(
      map(resp => resp as movieDetails)
    )
  }
}
