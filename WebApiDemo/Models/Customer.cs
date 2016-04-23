using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDemo.Models
{
  public  class Customer
    {
        public int Id { get; set; }

        [Required,MaxLength(50)]
        //Name field is mandatory and max length is 50
        public string Name { get; set; }
        [Required, MaxLength(10)]
        //Mobile field is mandatory and max length is 10
        public string  Mobile { get; set; }

    }
}
