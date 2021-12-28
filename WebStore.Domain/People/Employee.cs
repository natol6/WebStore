using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.People.Base;
using WebStore.Domain.References;

namespace WebStore.Domain.People;

 public class Employee : Human
 {
    public int Age { get; set; }
    public PositionClass Position { get; set; }
   
    public DateTime DateOfEmployment { get; set; }

}

