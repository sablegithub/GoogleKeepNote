﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class ResetPassword
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }

        public string ConformPassword { get; set; }
    }
}
