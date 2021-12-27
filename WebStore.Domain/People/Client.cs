using WebStore.Domain.People.Base;

namespace WebStore.Domain.People;

 public class Client : Human
 {
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }

}

