using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MonopolyLib.Logique.Cards.Actionnables;
using MonopolyLib.Logique.Cards.Keepables;
using MonopolyLib.Logique.Plateaux;
using MonopolyLib.Logique.PartieComponents.Parties;

namespace MonopolyLib.Logique.Cards.CardSets
{
    public class CardSet : ICollection<Card>
    {
        public Card this[int index]
        {
            get => Get(index);
            set => Set(index, value);
        }
        
        public Card Get(int index)
        {
            if (index >= Count)
            {
                throw new IndexOutOfRangeException();
            }

            using var enumerator = GetEnumerator();
            for (int i = 0; i <= index; i++)
            {
                enumerator.MoveNext();
            }

            return enumerator.Current;
        }

        private void Set(int index, Card value)
        {
            if (index >= Count)
            {
                throw new IndexOutOfRangeException();
            }

            Card[] cards = _items.ToArray();
            cards[index] = value;
            _items = cards.ToList();
        }

        public void RemoveAll(Func<object, bool> func)
        {
            for (int i = 0; i < Count; i++)
            {
                if (func(this[i]))
                {
                    Remove(this[i]);
                }
            }
        }

        public void RemoveAt(int index)
        {
            if (index < 0)
            {
                return;
            }
            Remove(this[index]);
        }

        private ICollection<Card> _items = new List<Card>();

        public void Generate(int numberOfCards, Plateau p, Type partie)
        {
            int numberOfCardOutOfPrison = 0;
            int numberOfCardXTimes = 0;
            int numberOfCardBack = 0;
            int numberOfCardGoTo = 0;
            int numberOfCardHouseHotel = 0;
            int numberOfCardMultiplier = 0;
            int numberOfCardGoToPrison = 0;
            int numberOfCardGetXFromAll = 0;
            int numberOfCardGiveXToAll = 0;
            int numberOfCardDepart = 0;
            int numberOfCardMoney = 0;

            int cardXTimesLimit1 = 0;
            int cardXTimesLimit2 = 0;

            int cardHouseHotelLimit1 = 0;
            int cardHouseHotelLimit2 = 0;
            int cardHouseHotelLimit3 = 0;
            int cardHouseHotelLimit4 = 0;

            int cardGetXFromAllLimit1 = 0;
            int cardGetXFromAllLimit2 = 0;

            int cardGiveXToAllLimit1 = 0;
            int cardGiveXToAllLimit2 = 0;

            int cardMoneyLimit1 = 0;
            int cardMoneyLimit2 = 0;

            if (partie == typeof(PartieNormale) || partie == typeof(PartieRandom))
            {
                numberOfCardOutOfPrison = numberOfCards * 1 / 16;
                numberOfCardBack = numberOfCards * 1 / 16;
                numberOfCardGoTo = numberOfCards * 2 / 16;
                numberOfCardHouseHotel = numberOfCards * 1 / 16;
                numberOfCardGoToPrison = numberOfCards * 1 / 16;
                numberOfCardGetXFromAll = numberOfCards * 1 / 16;
                numberOfCardGiveXToAll = numberOfCards * 1 / 16;
                numberOfCardDepart = numberOfCards * 1 / 16;

                cardXTimesLimit1 = 2;
                cardXTimesLimit2 = 5;

                cardHouseHotelLimit1 = 150;
                cardHouseHotelLimit2 = 175;
                cardHouseHotelLimit3 = 215;
                cardHouseHotelLimit4 = 245;

                cardGetXFromAllLimit1 = 40;
                cardGetXFromAllLimit2 = 60;
                
                cardGiveXToAllLimit1 = 10; 
                cardGiveXToAllLimit2 = 20;

                cardMoneyLimit1 = 20;
                cardMoneyLimit2 = 100;
            }
            
            else if (partie == typeof(PartieChaos))
            {
                numberOfCardOutOfPrison = numberOfCards * 4 / 52;
                numberOfCardXTimes = numberOfCards * 4 / 52;
                numberOfCardBack = numberOfCards * 4 / 52;
                numberOfCardGoTo = numberOfCards * 4 / 52;
                numberOfCardHouseHotel = numberOfCards * 4 / 52;
                numberOfCardMultiplier = numberOfCards * 2 / 52;
                numberOfCardGoToPrison = numberOfCards * 2 / 52;
                numberOfCardGetXFromAll = numberOfCards * 2 / 52;
                numberOfCardGiveXToAll = numberOfCards * 2 / 52;
                numberOfCardDepart = numberOfCards * 2 / 52;
                
                cardXTimesLimit1 = 4;
                cardXTimesLimit2 = 8;

                cardHouseHotelLimit1 = 250;
                cardHouseHotelLimit2 = 275;
                cardHouseHotelLimit3 = 315;
                cardHouseHotelLimit4 = 345;

                cardGetXFromAllLimit1 = 80;
                cardGetXFromAllLimit2 = 120;
                
                cardGiveXToAllLimit1 = 30; 
                cardGiveXToAllLimit2 = 50;

                cardMoneyLimit1 = 100;
                cardMoneyLimit2 = 300;
            }
            
            Random rng = new Random();
            
            for (int i = 0; i < numberOfCards; i++)
            {
                int y = 0;
                if (i < y + numberOfCardOutOfPrison)
                {
                    Add(new CardOutOfPrison());
                    continue;
                }

                y += numberOfCardOutOfPrison;
                if(i < y + numberOfCardXTimes)
                {
                    Random rd = new Random();
                    Add(new CardXTimes(rd.Next(cardXTimesLimit1,cardXTimesLimit2)));
                    continue;

                }

                y += numberOfCardXTimes;
                if (i < y + numberOfCardBack)
                {
                    Add(new CardGoBack());
                    continue;
                }

                y += numberOfCardBack;
                if (i < y + numberOfCardGoTo)
                {
                    Add(new CardGoTo(p));
                    continue;
                }

                y += numberOfCardGoTo;
                if (i < y + numberOfCardHouseHotel)
                {
                    Add(new CardHouseHotels(
                        rng.Next(cardHouseHotelLimit1,cardHouseHotelLimit2), 
                        rng.Next(cardHouseHotelLimit3,cardHouseHotelLimit4)));
                    continue;
                }

                y += numberOfCardHouseHotel;

                if (i < y + numberOfCardMultiplier)
                {
                    Add(new CardMultiplyEffect());
                    continue;
                }

                y += numberOfCardMultiplier;

                if (i < y + numberOfCardGoToPrison)
                {
                    Add(new CardToPrison());
                    continue;
                }

                y += numberOfCardGoToPrison;

                if (i < y + numberOfCardGetXFromAll)
                {
                    Add(new CardGetXFromAll(rng.Next(cardGetXFromAllLimit1,cardGetXFromAllLimit2)));
                    continue;
                }

                y += numberOfCardGetXFromAll;

                if (i < y + numberOfCardGiveXToAll)
                {
                    Add(new CardGiveXToAll(rng.Next(cardGiveXToAllLimit1,cardGiveXToAllLimit2)));
                    continue;
                }

                y += numberOfCardGiveXToAll;

                if (i < y + numberOfCardDepart)
                {
                    Add(new CardGoToDepart());
                }
                
                Add(new CardMoney(cardMoneyLimit1, cardMoneyLimit2));
            }

            while (Count > numberOfCards)
            {
                RemoveAt(Count - 1);
            }
            Shuffle();
        }
        
        public void Add(Card item)
        {
            _items.Add(item);
            Count++;
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(Card item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(Card[] array, int arrayIndex)
        {
            _items.CopyTo(array,arrayIndex);
        }

        public bool Remove(Card item)
        {
            if (!_items.Remove(item))
            {
                return false;
            }
            if(!(item is IKeepable))
            {
                _items.Add(item);
            }

            Count--;
            return true;
        }

        public int Count { get; private set; }
        public bool IsReadOnly => false;

        public IEnumerator<Card> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        

        public void Shuffle()
        {
            Random rng = new Random();
            int n = _items.Count - 1;  
            while (n > 1) {  
                n--;  
                int k = rng.Next(n + 1);  
                (this[k], this[n]) = (this[n], this[k]);
            }  
        }

        public bool HasType(Type type)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].GetType() == type)
                {
                    return true;
                }
            }

            return false;
        }
        
        
    }
}