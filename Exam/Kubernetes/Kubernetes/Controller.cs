using System;
using System.Collections.Generic;
using System.Linq;

namespace Kubernetes
{
    public class Controller : IController
    {
        private Dictionary<string,Pod>podtsById=new Dictionary<string, Pod> ();
      
        public bool Contains(string podId)
        {
            if (!this.podtsById.ContainsKey(podId))
            {
                return false;
            }
            return true;
        }

        public void Deploy(Pod pod)
        {
            this.podtsById.Add(pod.Id, pod);
          
        }

        public Pod GetPod(string podId)
        {
          if(!this.podtsById.ContainsKey(podId))
          {
                throw new ArgumentException();
          }
          return this.podtsById[podId];
        }

        public IEnumerable<Pod> GetPodsBetweenPort(int lowerBound, int upperBound)
        {
            return this.podtsById.Values
                  .Where(p => p.Port >= lowerBound
                        && p.Port <= upperBound);
        }

        public IEnumerable<Pod> GetPodsInNamespace(string @namespace)
        {
           return this.podtsById.Values
                .Where(s=>s.Namespace==@namespace)
                .ToList();
        }

        public IEnumerable<Pod> GetPodsOrderedByPortThenByName()
        {
            return this.podtsById.Values.OrderByDescending(p=>p.Port)
                .ThenBy(p=>p.Namespace);
        }

        public int Size() => this.podtsById.Count;
      

        public void Uninstall(string podId)
        {
            if (!this.podtsById.ContainsKey(podId))
            {
                throw new ArgumentException();
            }
          
            this.podtsById.Remove(podId);
            
          
           
        }

        public void Upgrade(Pod pod)
        {
            if (!this.podtsById.ContainsKey(pod.Id))
            {
                return;
            }
            this.podtsById[pod.Id] = pod;
           
           
        }
    }
}