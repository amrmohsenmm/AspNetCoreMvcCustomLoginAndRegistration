using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UsersApp.ViewModels
{
    public class empleeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string FullName { get; set; }

        public string email { get; set; }
        public string role { get; set; }
        public int phone { get; set; }
        public int salary { get; set; }

        
        public DateTime JoinDate { get; set; }
    }
}
