using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.People.Base;
using WebStore.Domain.References;

namespace WebStore.Domain.People;

 public class Employee : Human
 {
    public int Age { get; set; }
    
    public int PositionId { get; set; }

    [ForeignKey(nameof(PositionId))]
    public PositionClass Position { get; set; } = null!;
   
    public DateTime DateOfEmployment { get; set; } = DateTime.Now;

}

