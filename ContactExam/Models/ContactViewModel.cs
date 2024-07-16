namespace ContactExam.Models
{
    public class ContactViewModel
    {

        public ContactViewModel() { }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int CountryID { get; set; }
        public List<Country> countries { get; set; } = new List<Country>();
    }
}
