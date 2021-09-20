﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class PurchaseResponseModel
    {
        public int UserId { get; set; }

        public Guid PurchaseNumber { get; set; }
        public decimal? TotalPrice { get; set; }

        public DateTime? PurchaseDateTime { get; set; }

        public int MovieId { get; set; }
    }
}
