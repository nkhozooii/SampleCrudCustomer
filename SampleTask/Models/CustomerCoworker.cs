using System;
using System.Collections.Generic;

namespace SampleTask.Models;

public partial class CustomerCoworker
{
    public int Id { get; set; }

    public int? FkCustomerId { get; set; }

    public int? FkCoworkerId { get; set; }

    public virtual Customer? FkCoworker { get; set; }

    public virtual Customer? FkCustomer { get; set; }
}
