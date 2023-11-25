using System;
using System.Collections.Generic;

namespace SampleTask.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Fax { get; set; }

    public int CityId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual ICollection<CustomerCoworker> CustomerCoworkerFkCoworkers { get; } = new List<CustomerCoworker>();

    public virtual ICollection<CustomerCoworker> CustomerCoworkerFkCustomers { get; } = new List<CustomerCoworker>();
}
