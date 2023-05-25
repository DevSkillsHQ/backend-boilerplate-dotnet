﻿using Newtonsoft.Json;

namespace backend.Models.CreditDataModels
{
    public class AggregateDataModel
    {
        public string first_name { get; set; }

        public string last_name { get; set; }

        public string address { get; set; }

        public int assessed_income { get; set; }

        public int balance_of_debt { get; set; }

        public bool complaints { get; set; }
    }
}
