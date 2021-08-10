using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models
{
    public class MovieCardResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string PosterURL { get; set; }
    }
}
