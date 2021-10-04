import { Component, OnInit } from '@angular/core';
import { MovieCard } from '../../models/movieCard';
import { Input } from '@angular/core';
@Component({
  selector: 'app-movie-card',
  templateUrl: './movie-card.component.html',
  styleUrls: ['./movie-card.component.css']
})
export class MovieCardComponent implements OnInit {

  @Input() movie!:MovieCard;
  //@Input() test!:MovieCard;
  constructor() { }

  ngOnInit(): void {
  }

}
