using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StraighteningAndPaintingWorkshop.domain
{
    public class Vehicle
    {
        private string licenseNumber;
        private string color;
        private string brand;
        private string style;
        private int year;
        private int capacity;
        private float weight;
        private string chassis_number;

        public Vehicle(string licenseNumber, string color, string brand, string style, int year, int capacity, float weight, string chassis_number)
        {
            this.licenseNumber = licenseNumber;
            this.color = color;
            this.brand = brand;
            this.style = style;
            this.year = year;
            this.capacity = capacity;
            this.weight = weight;
            this.chassis_number = chassis_number;
        }

        public string LicenseNumber { get => licenseNumber; set => licenseNumber = value; }
        public string Color { get => color; set => color = value; }
        public string Brand { get => brand; set => brand = value; }
        public string Style { get => style; set => style = value; }
        public int Year { get => year; set => year = value; }
        public int Capacity { get => capacity; set => capacity = value; }
        public float Weight { get => weight; set => weight = value; }
        public string Chassis_number { get => chassis_number; set => chassis_number = value; }
    }
}
