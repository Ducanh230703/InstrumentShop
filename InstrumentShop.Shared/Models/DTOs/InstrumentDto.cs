﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentShop.Shared.Models.DTOs
{
    public class InstrumentDto
    {
        public int InstrumentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[] ImageData { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } // Thêm tên category vào DTO
    }
}
