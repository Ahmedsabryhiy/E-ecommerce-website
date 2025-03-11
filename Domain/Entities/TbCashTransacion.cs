using System;
using System.Collections.Generic;

namespace  LapShop.Domain.Entities;

public partial class TbCashTransacion
{
    public int CashTransactionId { get; set; }

    public int CustomerId { get; set; }

    public DateTime CashDate { get; set; }

    public decimal CashValue { get; set; }
}
