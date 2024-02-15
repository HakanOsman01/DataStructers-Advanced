namespace CollectionOfPeople
{
    using System.Collections.Generic;
    using System.Linq;

    public class PeopleCollectionSlow : IPeopleCollection
    {
        private List<Person>people = new List<Person>();
        public int Count => this.people.Count;

        public bool Add(string email, string name, int age, string town)
        {

            if (this.Find(email)!=null)
            {
                return false;
            }
            Person person = new Person(email,name,age,town);
         

            people.Add(person);
            return true;
        }

        public bool Delete(string email)
        {
            var person = this.Find(email);
            if (person == null)
            {
                return false;
            }
            people.Remove(person);
            return true;
            
        }

        public Person Find(string email)
        {
            var person=people.FirstOrDefault(p=>p.Email == email);  
            return person;
        }

        public IEnumerable<Person> FindPeople(string emailDomain)
        {
           return this.people.Where(p => p.Email.Split("@")[1]==emailDomain)
                .OrderBy(p=>p.Email)
                .ToList();
        }

        public IEnumerable<Person> FindPeople(string name, string town)
        {
           return this.people.Where(p=>p.Name==name && p.Town==town)
                .OrderBy (p=>p.Email)
                .ToList();
        }

        public IEnumerable<Person> FindPeople(int startAge, int endAge)
        {
           return this.people.Where(p=>p.Age>=startAge && p.Age<=endAge)
                .OrderBy(p=>p.Age)
                .ThenBy(p=>p.Email)
                .ToList();
        }

        public IEnumerable<Person> FindPeople(int startAge, int endAge, string town)
        {
            return this.people.Where(p => p.Age >= startAge &&
            p.Age <= endAge && p.Town==town)
                .OrderBy(p=>p.Age)
                .ThenBy(p=>p.Email)
                .ToList();
        }
    }
}
