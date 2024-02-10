namespace PersoHashCode.Classes
{
    public class Person
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int Age { get; set; }
        public override int GetHashCode()
        {
            int firstNameNumber=this.FirstName.GetHashCode()*Age;
            int secondNameNumber=this.SecondName.GetHashCode()*Age;
            return firstNameNumber+secondNameNumber;
            
        }
    }
}
