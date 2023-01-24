using System.Collections;
using System.Collections.Generic;
using MonopolyLib.Logique.Joueurs;
using Newtonsoft.Json;

namespace MonopolySimulator
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ListJoueurs : ICollection<Joueur>
    {
        public Joueur this[int index]
        {
            get => _joueurs[index];
            set => _joueurs[index] = value;
        }

        [JsonProperty]
        private List<Joueur> _joueurs = new List<Joueur>();
        public void Add(Joueur item)
        {
            _joueurs.Add(new JoueurIa(item.Argent, item.Nom,true, item.NbProperties));
        }

        public void Clear()
        {
            _joueurs.Clear();
        }

        public bool Contains(Joueur item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(Joueur[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(Joueur item)
        {
            throw new System.NotImplementedException();
        }

        public int Count { get; }
        public bool IsReadOnly { get; }
        public IEnumerator<Joueur> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}