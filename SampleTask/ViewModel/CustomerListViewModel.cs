namespace SampleTask.ViewModel
{
    public class CustomerListViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public int CityId { get; set; }
        public string? CityName { get; set; }
    }
    public class CoworkerListViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IscoWorker { get; set; }
    }
}
