import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { MovieCard } from 'src/app/shared/models/movieCard';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http:HttpClient) { }


  getUserPurchase(id:number):Observable<MovieCard[]>{

    return this.http.get(`${environment.apiUrl}`+'User/'+id+'/purchase')
    .pipe(
      map(resp => resp as MovieCard[])
    )
  }

  getUserFavorite(id:number):Observable<MovieCard[]>{
    return this.http.get(`${environment.apiUrl}`+'User/'+id+'/favorite')
    .pipe(
      map(resp => resp as MovieCard[])
    )
  }
}

