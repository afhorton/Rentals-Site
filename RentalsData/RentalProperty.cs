using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalsData
{
    [Table("RentalProperty")]
    public class RentalProperty
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter address")]
        [StringLength(100)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter city")]
        [StringLength(50)]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter province")]
        [StringLength(15)]
        public string Province { get; set; }

        [Required(ErrorMessage = "Please enter postal code (A1A1A1)")]
        [RegularExpression("^([a-zA-Z][0-9]){3}$", ErrorMessage = "Postal code has to 6 characters, alternating letters and digits") ]
        [StringLength(15)]
        public string PostalCode { get; set; }


        [Range(1, 10000)]
        public decimal Rent { get; set; }
        public int PropertyTypeId { get; set; }
        public int OwnerId { get; set; }
        //nullable int required if FK is nullable
        public int? RenterId { get; set; }
        //navigation properties
        public PropertyType PropertyType { get; set; }
        public Owner Owner { get; set; }
        public Renter Renter { get; set; }
    }
}
