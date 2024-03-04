using System.Collections.Generic;

namespace TaskManager
{
    public class Task
    {
        public Task()
        {
            this.Childrens=new List<Task>();
        }
        public string Id { get; set; }

        public string Title { get; set; }

        public string Assignee { get; set; }

        public int Priority { get; set; }
        public List<Task> Childrens { get; set; }   
        public Task Parent { get; set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}