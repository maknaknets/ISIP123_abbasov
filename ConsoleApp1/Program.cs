
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
    // Класс детали на складе
    public class WarehousePart
    {
        public int WarehousePartId { get; set; }
        public int PartTypeId { get; set; }
        public PartType Type { get; set; }
        public int Quantity { get; set; }

        public WarehousePart()
        {
            if (Quantity < 0)
                throw new ArgumentException("Quantity cannot be negative.");
        }

        public void AddParts(int quantity) { }
        public void RemovePart() { }
        public bool HasPart() { }
    }

    // Класс клиента
    public class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; }

        public Client()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Client name cannot be empty.");
        }
    }
    // Класс автомобиля
    public class Car
    {
        public int CarId { get; set; }
        public int ClientId { get; set; }
        public PartType BrokenPartType { get; set; }

        public Car() { }
        public PartType GetBrokenPart() { }
    }

    // Класс заказа на ремонт
    public class RepairOrder
    {
        public int RepairOrderId { get; set; }
        public int ClientId { get; set; }
        public int CarId { get; set; }
        public PartType PartType { get; set; }
        public decimal RepairCost { get; set; }
        public RepairStatus Status { get; set; }
        public decimal? CompensationCost { get; set; }

        public RepairOrder()
        {
            if (RepairCost < 0)
                throw new ArgumentException("Repair cost cannot be negative.");
        }

        public void CompleteRepair() { }
        public void FailRepair() { }
        public void DeclineRepair() { }
    }

    // Класс заказа на поставку
    public class SupplyOrder
    {
        public int SupplyOrderId { get; set; }
        public PartType PartType { get; set; }
        public int Quantity { get; set; }
        public int CarsUntilDelivery { get; set; }

        public SupplyOrder()
        {
            if (Quantity <= 0)
                throw new ArgumentException("Quantity must be positive.");
            if (CarsUntilDelivery < 0)
                throw new ArgumentException("Cars until delivery cannot be negative.");
        }

        public void UpdateDeliveryStatus() { }
        public bool IsDelivered() { }
    }