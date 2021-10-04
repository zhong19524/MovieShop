
    export interface Cast {
        id: number;
        name: string;
        gender?: any;
        tmdbUrl?: any;
        profilePath: string;
        character: string;
        movieDetailResponseModels?: any;
    }

    export interface Genre {
        id: number;
        name: string;
    }

    export interface movieDetails {
        id: number;
        title: string;
        overview: string;
        tagline?: any;
        budget: number;
        revenue: number;
        imdbUrl?: any;
        tmdbUrl?: any;
        posterUrl: string;
        backdropUrl?: any;
        originalLanguage?: any;
        releaseDate: Date;
        runTime: number;
        price?: any;
        rating: number;
        casts: Cast[];
        genres: Genre[];
    }


