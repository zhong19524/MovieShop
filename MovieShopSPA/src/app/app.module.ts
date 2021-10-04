import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './core/layout/header/header.component';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { TopratedComponent } from './movies/toprated/toprated.component';
import { CreateMovieComponent } from './admin/create-movie/create-movie.component';
import { MovieDetailsComponent } from './movies/movie-details/movie-details.component';
import { HttpClientModule } from '@angular/common/http';
import { MovieCardComponent } from './shared/components/movie-card/movie-card.component';
import { GetpurchaseComponent } from './user/getpurchase/getpurchase.component';
import { GetfavoriteComponent } from './user/getfavorite/getfavorite.component';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    LoginComponent,
    RegisterComponent,
    TopratedComponent,
    CreateMovieComponent,
    MovieDetailsComponent,
    MovieCardComponent,
    GetpurchaseComponent,
    GetfavoriteComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
