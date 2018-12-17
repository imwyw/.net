﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSales.Model.PO
{
    public class SellOrder
    {
        public int SellOrderID { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int SellOrderNumber { get; set; }
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public DateTime SellOrderDate { get; set; }
    }
}