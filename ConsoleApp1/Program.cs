
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoServiceGame
{
    // Перечисление для типов деталей
    public enum PartType
    {
        Engine,
        Wheel,
        Brake,
        Transmission,
        Suspension
    }

    // Перечисление для статуса заказа на ремонт
    public enum RepairStatus
    {
        Success,
        Failure,
        Declined
    }

    // Класс детали
    public class Part
    {
        public int PartTypeId { get; set; }
        public PartType Type { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal RepairPrice { get; set; }

        public Part()
        {
            if (PurchasePrice < 0 || RepairPrice < 0)
                throw new ArgumentException("Prices cannot be negative.");
        }

        public void Validate() { }
    }
