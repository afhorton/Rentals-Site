using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RentalsData
{
    [Table("Renter")]
    public class Renter
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter first name")]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name")]
        [StringLength(40)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter phone")]
        [StringLength(25)]
        public string Phone { get; set; }
        //navigation property (one-to-one)
        public RentalProperty RentalProperty { get; set; }
    }
}
