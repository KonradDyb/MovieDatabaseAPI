﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Directors
{
    public class DirectorDto
    {
        public DirectorDto()
        {

        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string PlaceOfBirth { get; set; }
    }
}
