import { Component, Input, OnInit } from '@angular/core';
import { movieDetails } from 'src/app/shared/models/movieDetails';
import { MovieService } from 'src/app/core/services/movie.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {

  @Input() moviedetails!:movieDetails;
  constructor(private movieService:MovieService,private route:ActivatedRoute) { }


  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.movieService.getMovieDetails(id).subscribe(
      m =>{ 
      this.moviedetails =m;
      console.log('inside home component init method');
      console.log(this.moviedetails);
  })
    
}

}
