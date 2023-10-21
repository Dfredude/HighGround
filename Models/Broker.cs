using MessagePack;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Broker
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public int USDOT { get; set; }

        public Broker()
        {
            ID = 0;
            Name = "";
            Phone = "";
            Email = "";
            Notes = "";
            USDOT = 0;
        }
    }
}
