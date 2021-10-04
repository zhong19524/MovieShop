import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user.service';
import { MovieCard } from 'src/app/shared/models/movieCard';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-getfavorite',
  templateUrl: './getfavorite.component.html',
  styleUrls: ['./getfavorite.component.css']
})
export class GetfavoriteComponent implements OnInit {

  movies!:MovieCard[];
  constructor(private userService:UserService,
              private route:ActivatedRoute
              ) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.userService.getUserFavorite(id).subscribe(
      m =>{ 
      this.movies =m;
      console.log('inside home component init method');
      console.log(this.movies);
    })
  }
}
