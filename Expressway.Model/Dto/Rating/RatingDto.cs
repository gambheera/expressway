using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Dto.Rating
{
    public class RatingDto
    {
        public double Rating { get; set; }
        public int Star5Count { get; set; }
        public int Star4Count { get; set; }
        public int Star3Count { get; set; }
        public int Star2Count { get; set; }
        public int Star1Count { get; set; }
    }
}
