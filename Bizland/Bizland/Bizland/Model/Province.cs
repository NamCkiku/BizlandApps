namespace Bizland.Model
{
    public class Province
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public string NameSort
        {
            get
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    return Name[0].ToString().ToUpper();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
