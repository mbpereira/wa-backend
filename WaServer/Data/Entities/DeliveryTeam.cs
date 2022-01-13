using System.ComponentModel.DataAnnotations;

namespace WaServer.Data.Entities
{
    public class DeliveryTeam
    {
        [Key]
        public int IdDeliveryTeam { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        [Required, StringLength(255)]
        public string Vehicle { get; set; }
        [Required, StringLength(10)]
        public string LicensePlate { get; set; }
    }
}
