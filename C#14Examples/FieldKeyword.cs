using System;
using System.Collections.Generic;
using System.Text;

namespace C_14Examples
{
    public static class FieldKeyword
    {
        public static void FieldKeywordDemo()
        {
            // OLD
            var oldAddress = new AddressOld();
            oldAddress.CityName = "Ahmedabad";
            Console.WriteLine($"OLD City Name: {oldAddress.CityName}");

            // NEW
            var newAddress = new AddressNew();
            newAddress.CityName = "Surat";
            Console.WriteLine($"NEW City Name: {newAddress.CityName}");
        }
    }

    // OLD Style
    public class AddressOld
    {
        private string _cityName = string.Empty;

        public string CityName
        {
            get => _cityName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("City Name cannot be empty");

                _cityName = value;
            }
        }
    }

    // NEW C# 14 Style
    public class AddressNew
    {
        public string CityName
        {
            get;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty");

                field = value;   // C# 14
            }
        }
    }

}
