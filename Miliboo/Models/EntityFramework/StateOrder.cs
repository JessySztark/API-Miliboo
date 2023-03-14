﻿using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miliboo.Models.EntityFramework
{
    [Table("t_e_stateorder_sto")]

    public class StateOrder
    {
        private int stateOrderID;
        private string? stateOrderName;

        [Key]
        [Column("sto_stateorderid")]
        public int StateOrderID
        {
            get { return stateOrderID; }
            set { stateOrderID = value; }
        }

        [Column("sto_stateordername")]
        public string? StateOrderName
        {
            get { return stateOrderName; }
            set { stateOrderName = value; }
        }

        [InverseProperty("StateOrderOrder")]
        public virtual ICollection<Order> OrderStateOrder { get; set; } = new List<Order>();
    }
}
