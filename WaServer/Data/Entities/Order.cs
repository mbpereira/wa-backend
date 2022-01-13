using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WaServer.Consts;

namespace WaServer.Data.Entities
{
    public class Order
    {
        [Key]
        public int IdOrder { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
        [ForeignKey("DeliveryTeamId")]
        public DeliveryTeam DeliveryTeam { get; set; }
        public int? DeliveryTeamId { get; set; }
        [Required]
        public IList<OrderItem> Items { get; set; }
        [Required, StringLength(255)]
        public string Street { get; set; }
        [Required]
        public int Number { get; set; }
        [Required, StringLength(10)]
        public string ZipCode { get; set; }
        [Required, StringLength(255)]
        public string AddressDistrict { get; set; }
        [Required, StringLength(255)]
        public string City { get; set; }
        [Required, StringLength(255)]
        public string Country { get; set; }
        [Required, StringLength(2)]
        public string State { get; set; }
    }
}
