namespace RunAndHikeStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using RunAndHikeStore.Data.Common.Models;
    using RunAndHikeStore.Data.Models.Enums;
    using static RunAndHikeStore.Common.GlobalConstants.Order;

    public class Order : BaseDeletableModel<string>
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
            this.OrderDetails = new HashSet<OrderDetail>();
        }

        /// <summary>
        /// Gets or sets order Date.
        /// </summary>
        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets ship Date.
        /// </summary>
        public DateTime? ShipDate { get; set; }

        /// <summary>
        /// Gets or sets order status.
        /// </summary>
        [StringLength(OrderStatusMaxLength)]
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        /// <summary>
        /// Gets or sets payment status.
        /// </summary>
        [StringLength(PaymentStatusMaxLength)]
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.NotPaid;

        /// <summary>
        /// Gets or sets payment date.
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// Foreign Key to ApplicationUsers Table.
        /// </summary>
        [Required]
        public string CustomerId { get; set; }

        /// <summary>
        /// Navigation property to ApplicationUsers Table.
        /// </summary>
        [ForeignKey("CustomerId")]
        public ApplicationUser Customer { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// Foreign Key to Billing details for the order.
        /// </summary>
        [Required]
        public string BillingDetailsId { get; set; }

        /// <summary>
        /// Billing details for the order.
        /// </summary>
        public BillingDetails BillingDetails { get; set; }
    }
}
