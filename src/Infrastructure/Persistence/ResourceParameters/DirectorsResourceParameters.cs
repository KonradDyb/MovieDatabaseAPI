using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.ResourceParameters
{
    public class DirectorsResourceParameters : IDirectorsResourceParameters
    {
        public int YearOfBirth { get; set; }
        public string SearchQuery { get; set; }
    }
}
