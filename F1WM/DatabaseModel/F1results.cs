﻿using System;
using System.Collections.Generic;

namespace F1WM.DatabaseModel
{
    public class F1results
    {
        public uint Entryid { get; set; }
        public uint Raceid { get; set; }
        public string Endpos { get; set; }
        public byte Laps { get; set; }
        public string Info { get; set; }
        public byte Ord { get; set; }
        public byte? Pits { get; set; }
    }
}
