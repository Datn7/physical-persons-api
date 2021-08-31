using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace physical_persons_api.Entities
{
    public class UserSettings
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool EmailOffers { get; set; }
        public string InterfaceStyle { get; set; }
        public string SubscriptionType { get; set; }
        public string Notes { get; set; }


    }
}
