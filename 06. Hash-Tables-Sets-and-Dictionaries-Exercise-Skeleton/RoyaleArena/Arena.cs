namespace RoyaleArena
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Arena : IArena
    {
        private Dictionary<int, BattleCard> battleCards 
            = new Dictionary<int, BattleCard>();
        public int Count => this.battleCards.Count;

        public void Add(BattleCard card)
        {
           bool isContaindCard=this.battleCards.ContainsKey(card.Id);
            if (isContaindCard)
            {
                this.battleCards.Add(card.Id, card);
            }
        }

        public void ChangeCardType(int id, CardType type)
        {
            if (!this.battleCards.ContainsKey(id))
            {
                throw new InvalidOperationException();
            }
            this.battleCards[id].Type = type;
        }

        public bool Contains(BattleCard card)
        {
            if (this.battleCards.ContainsKey(card.Id))
            {
                return true;
            }
            return false;
        }

        public IEnumerable<BattleCard> FindFirstLeastSwag(int n)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BattleCard> GetAllInSwagRange(double lo, double hi)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BattleCard> GetByCardType(CardType type)
        {
            var anyCard=this.battleCards.Any(b=>b.Value.Type==type);
            if (!anyCard)
            {
                throw new InvalidOperationException();
            }
            var cards=this.battleCards.Select(b=>b.Value)
                .Where(b=>b.Type==type)
                .OrderByDescending(b=>b.Damage)
                .OrderBy(b=>b.Id)
                .ToList();
            return cards;
        }

        public IEnumerable<BattleCard> GetByCardTypeAndMaximumDamage(CardType type, double damage)
        {
            throw new NotImplementedException();
        }

        public BattleCard GetById(int id)
        {
            if (!this.battleCards.ContainsKey(id))
            {
                throw new InvalidOperationException();
            }
            return this.battleCards[id];
        }

        public IEnumerable<BattleCard> GetByNameAndSwagRange(string name, double lo, double hi)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BattleCard> GetByNameOrderedBySwagDescending(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BattleCard> 
        GetByTypeAndDamageRangeOrderedByDamageThenById(CardType type, int lo, int hi)
        {
            var anyCard=this.battleCards.Any(b=>b.Value.Type==type);
            if (!anyCard)
            {
                throw new InvalidOperationException();
            }
            var cards=this.battleCards
                .Select(b=>b.Value)
                .Where(c=>c.Damage>=lo && c.Damage<=hi)
                .OrderByDescending(c=>c.Damage)
                .ThenBy(c=>c.Id)
                .ToList();
            return cards;
        }

        public IEnumerator<BattleCard> GetEnumerator()
        {
            foreach (var card in this.battleCards)
            {
                yield return card.Value;
            }
        }

        public void RemoveById(int id)
        {
            if (!this.battleCards.ContainsKey(id))
            {
                throw new InvalidOperationException();
            }
            this.battleCards.Remove(id);
        }

        IEnumerator IEnumerable.GetEnumerator()=>GetEnumerator();
       
    }
}