﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Word_Hole_API.Models.Profile
{
    public class UserPostsGet
    {
        [Required]
        public int UserID { get; set; }
    }
}
