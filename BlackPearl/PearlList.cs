using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackPearl
{
    public class PearlList
    {
        

        [Key]
        public int PearlListID { get; set; }

        // Listan
        List<Pearl> _pearllist = new List<Pearl>();
        public int PearlID { get; set; }

        // Här räknas totala priset för hela halsbandet ut
        public int totalPrice
        {
            get
            {
                var totalprice = 0;
                foreach (var x in _pearllist)
                {
                    totalprice += x.Price;
                }
                return totalprice;
            }
        }
        
        // Hör till uträkningen av antal pärlor
        public int Count()
        {
            return _pearllist.Count;
        }

        // Deafultsorteringen som anges i Pearl.cs public int CompareTo(IPearl other)
        public void Sort()
        {
            _pearllist.Sort();
        }

        //Hur många Saltvattenspärlor finns det
        public int Count2()
        {
            return _pearllist.Where(sv => sv.Type == "Saltvatten").Count();
        }

        //Hur många Sötvattenspärlor finns det
        public int Count3()
        {
            return _pearllist.Where(sv => sv.Type == "Sötvatten").Count();
        }

        // Hitta index för en viss pärla
        public int IndexOf(Pearl other)
        {
            return _pearllist.IndexOf(other);
        }

        //ToString måste vara med annars skrivs inget ut
        public override string ToString()
        {
            string sRet = "";
            for (int i = 0; i < _pearllist.Count; i++)
            {
                sRet += $"{_pearllist[i]}\n";
            }
            return sRet;
        }

        // Innan jag gjorde en Factory användes detta.
        //public PearlList(int NrOfMembers)
        //{
        //    for (int i = 0; i < NrOfMembers; i++)
        //    {
        //        _pearllist.Add(new Pearl());
        //    }
        //}
        
        //Nu med Factory
        internal static class Factory
        {
            internal static PearlList CreateRandomList(int NrOfPearls)
            {
                var pearlList = new PearlList();
                for (int i = 0; i < NrOfPearls; i++)
                {
                    pearlList._pearllist.Add(Pearl.Factory.CreateRandomPearl());
                }
                return pearlList;
            }
        }
    }
}
