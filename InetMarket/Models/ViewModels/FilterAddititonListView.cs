using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InetMarket.Models
{
    public class FilterAddititonListView
    {
        public IEnumerable<FilterAddititon> FilterAddititons { get; set; }
        public SelectList Filters { get; set; }
    }
}
