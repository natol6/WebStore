﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore.Domain
{
    public class ProductFilter
    {
        public int? SectionId { get; set; }
        
        public int? BrandId { get; set; }

        public int[]? Ids { get; set; }

        public int Page { get; set; }

        public int? PageSize { get; set; }
    }
}
