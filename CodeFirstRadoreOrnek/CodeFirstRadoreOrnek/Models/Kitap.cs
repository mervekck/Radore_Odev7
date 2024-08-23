using System.ComponentModel.DataAnnotations;

namespace CodeFirstRadoreOrnek.Models
{
    public class Kitap
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string kitapAdi { get; set; }

        [Required]
        public double fiyat { get; set; }


        [Required]
        public int sayfaSayisi { get; set; }


    }
}
