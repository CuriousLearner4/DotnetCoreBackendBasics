﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class Error
    {
        public string FriendlyMessage { set; get; }
        public List<string> ErrorMessages { get; set; }

    }
}
