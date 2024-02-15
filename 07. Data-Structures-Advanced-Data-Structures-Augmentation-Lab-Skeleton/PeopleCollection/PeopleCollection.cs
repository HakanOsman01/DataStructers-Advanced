namespace CollectionOfPeople
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class PeopleCollection : IPeopleCollection
    {
        private Dictionary<string, Person> personByEmail;
        private Dictionary<string, SortedSet<Person>> personsByEmailDomain;
        private Dictionary<(string name,string town),SortedSet<Person>> personsByNameAndTown;
        private OrderedDictionary<int,SortedSet<Person>> personsByAge;
        private Dictionary<string, OrderedDictionary<int, SortedSet<Person>>> 
            peopleByTownAndAge;

        public PeopleCollection()
        {
            this.personByEmail = new Dictionary<string, Person>();
            this.personsByEmailDomain=new Dictionary<string, SortedSet<Person>>();
            this.personsByNameAndTown=new Dictionary<(string, string), SortedSet<Person>>();
            this.personsByAge=new OrderedDictionary<int, SortedSet<Person>>();
            this.peopleByTownAndAge = new Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>();
        }
        public int Count => personByEmail.Count;

        public bool Add(string email, string name, int age, string town)
        {
            if (this.personByEmail.ContainsKey(email))
            {
                return false;
            }
            var person = new Person(email, name, age, town);
            this.personByEmail.Add(email, person);
            this.personsByEmailDomain.AppendValueToKey(email.Split("@")[1],person);
            this.personsByNameAndTown.AppendValueToKey((name,email), person);

            this.personsByAge.AppendValueToKey(age,person);
            this.peopleByTownAndAge.EnsureKeyExists(town);
            this.peopleByTownAndAge[town].AppendValueToKey(age, person);

            //var emailDomain = email.Split("@")[1];

            //if (!this.personsByEmailDomain.ContainsKey(emailDomain))
            //{
            //    this.personsByEmailDomain.Add(emailDomain, new SortedSet<Person>());

            //}
            //this.personsByEmailDomain[email].Add(person);

            return true;
        }

        public bool Delete(string email)
        {
            var person=this.Find(email);
            if (person == null)
            {
                return false;
            }
           this.personsByEmailDomain[email.Split("@")[1]].Remove(person);
           this.personsByNameAndTown[(person.Name, person.Email)].Remove(person);
           this.personsByAge[person.Age].Remove(person);
           this.peopleByTownAndAge[person.Town][person.Age].Remove(person);
        
           return this.personByEmail.Remove(email);
        }

        public Person Find(string email)
        {
           return this.personByEmail.GetValueOrDefault(email);
               
        }

        public IEnumerable<Person> FindPeople(string emailDomain)
            => this.personsByEmailDomain.GetValuesForKey(emailDomain);
       
        public IEnumerable<Person> FindPeople(string name, string town)
            => this.personsByNameAndTown.GetValuesForKey((name, town));



        public IEnumerable<Person> FindPeople(int startAge, int endAge)
            => this.personsByAge.Range(startAge, true, endAge, true)
            .SelectMany(kvp => kvp.Value)
            .OrderBy(p=>p.Age);


        public IEnumerable<Person> FindPeople(int startAge, int endAge, string town)
        {
            if (!this.peopleByTownAndAge.ContainsKey(town))
            {
                return Enumerable.Empty<Person>();
            }
           return this.peopleByTownAndAge[town]
            .Range(startAge, true, endAge, true)
            .SelectMany(kvp => kvp.Value);
        }
       
    }
}
