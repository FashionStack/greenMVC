﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenMVC.Models
{
    public class DashboardCategoriesCount
    {
        public string Category { get; set; }
        public int SustainableItemsCount { get; set; }
        public int NonSustainableItemsCount { get; set; }
    }
}
