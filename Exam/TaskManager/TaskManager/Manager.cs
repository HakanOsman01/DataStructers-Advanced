using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager
{
    public class Manager : IManager
    {
        private Dictionary<string,Task>tasksById= new Dictionary<string,Task>();
        public void AddDependency(string taskId, string dependentTaskId)
        {
            if(!this.tasksById.ContainsKey(taskId) || !this.tasksById.ContainsKey(dependentTaskId))
            {
                throw new ArgumentException();
            }
            var parentTask = this.tasksById[taskId];
            var depentTask = this.tasksById[dependentTaskId];
            if (depentTask==parentTask)
            {
                throw new ArgumentException();
            }
            parentTask.Childrens.Add(depentTask);
            depentTask.Parent=parentTask;
            
            
        }

        public void AddTask(Task task)
        {
            if (this.tasksById.ContainsKey(task.Id))
            {
                throw new ArgumentException();
            }
            this.tasksById.Add(task.Id, task);
        }

        public bool Contains(string taskId)
        {
            if (!tasksById.ContainsKey(taskId))
            {
                return false;
            }
            return true;
        }

        public Task Get(string taskId)
        {
            if (!this.tasksById.ContainsKey(taskId))
            {
                throw new ArgumentException();
            }
            return this.tasksById[taskId];
        }

        public IEnumerable<Task> GetDependencies(string taskId)
        {
            if (!this.tasksById.ContainsKey(taskId))
            {
                return new List<Task>();
            }
            List<Task>dependencies= new List<Task>();
            var currentTask= this.tasksById[taskId];
            Queue<Task> queue= new Queue<Task>();
            queue.Enqueue(currentTask);
            while(queue.Count!=0)
            {

                dependencies.Add(queue.Peek());
                foreach (var child in queue.Peek().Childrens)
                {
                    queue.Enqueue(child);
                }
                queue.Dequeue();
              


            }
            dependencies.RemoveAt(0);
            return dependencies;
            

        }

        public IEnumerable<Task> GetDependents(string taskId)
        {
            if (!this.tasksById.ContainsKey(taskId))
            {
                return new List<Task>();
            }
            List<Task> dependencies = new List<Task>();
            var currentTask = this.tasksById[taskId].Parent;
            while (currentTask != null)
            {
                dependencies.Add(currentTask);
                currentTask = currentTask.Parent;

            }
            return dependencies;


        }

        public void RemoveDependency(string taskId, string dependentTaskId)
        {
            if (!this.tasksById.ContainsKey(taskId) || !this.tasksById.ContainsKey(dependentTaskId))
            {
                throw new ArgumentException();
            }
            var parent = this.tasksById[taskId];
            var childParent = this.tasksById[taskId].Childrens
                .FirstOrDefault(d => d.Id == dependentTaskId);
            if(childParent == null)
            {
                throw new ArgumentException();
            }
            parent.Childrens.Remove(childParent);
            childParent.Parent = null;
            

        }

        public void RemoveTask(string taskId)
        {
            if (!this.tasksById.ContainsKey(taskId))
            {
                throw new ArgumentException();
            }

            var parent = this.tasksById[taskId].Parent;
            if (parent != null)
            {
                this.tasksById[parent.Id].Childrens.Remove(this.tasksById[taskId]);
                
                
            }
            
            this.tasksById.Remove(taskId);



        }

        public int Size() => this.tasksById.Count;
        
    }
}