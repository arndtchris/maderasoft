﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaderaSoft.Models.Bootstrap
{
    public class BootstrapTableModel
    {
        public List<List<string>> lesLignes { get; set; }

        public BootstrapTableModel()
        {
            lesLignes = new List<List<string>>();
        }
    }
}