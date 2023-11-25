using System;
using System.Collections.Generic;

namespace SampleTask.Models;

public partial class City
{
    public int Id { get; set; }

    public string CityName { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; } = new List<Customer>();
}
