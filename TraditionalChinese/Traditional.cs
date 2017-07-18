using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TraditionalChinese
{
    public static class Traditional
    {
        public static bool TryGetBoChar(string bopo, out IList<string> found) // Working
        {
            IList<string> allChar = new List<string>();

            using (var charBopo = new DataClasses1DataContext())
            {
                var myChars = from q in charBopo.CharBopoPinCrits
                              where q.Bopo == bopo
                              select q;

                foreach (var myChar in myChars)
                {
                    allChar.Add(myChar.Char);
                }

                found = allChar;

                return allChar.Any();
            }
        }

        public static bool TryGetBoPin(string bopo, out IList<string> found) // Working
        {
            IList<string> onePinyin = new List<string>();

            using (var pinBopo = new DataClasses1DataContext())
            {
                var pinyins = from q in pinBopo.CharBopoPinCrits
                              where q.Bopo == bopo
                              select q;

                foreach (var onepin in pinyins)
                {
                    onePinyin.Add(onepin.Pin);
                }

                found = onePinyin;

                return onePinyin.Any();
            }
        }

        public static bool TryGetCharBo(string character, out IList<string> found) // Working
        {
            var bo = new List<string>();

            using (var charBopo = new DataClasses1DataContext())
            {
                var bos = from q in charBopo.CharBopoPinCrits
                          where q.Char == character
                          select q;

                bo.AddRange(bos.Select(oneBo => oneBo.Bopo));

                found = bo;

                return bo.Any();
            }
        }

        public static bool TryGetCharSimp(string character, out IList<string> found) // Working
        {
            var simp = new List<string>();

            using (var charSimplified = new DataClasses1DataContext())
            {
                var simps = from q in charSimplified.CeDictFulls
                          where q.Traditional == character
                          select q;

                foreach (var simple in simps)
                {
                    simp.Add(simple.Simplified);
                    break;
                }

                found = simp;

                return simp.Any();
            }
        }

        public static bool TryGetCharPin(string character, out IList<string> found) // Working
        {
            IList<string> allBopo;

            var status1 = TryGetCharBo(character, out allBopo);

            if (!status1)
            {
                found = null;
                return false;
            }
            IList<string> allPin = new List<string>();

            for (var index = 0; index < allBopo.Count(); index++)
            {
                IList<string> onePin;

                var status2 = TryGetBoPin(allBopo[index], out onePin);

                if (status2)
                {
                    allPin.Add(onePin[0]);
                }
            }

            found = allPin;

            return allPin.Any();
        }

        public static bool TryGetCharCji(string character, out IList<string> found) // Working
        {
            IList<string> allCji = new List<string>();

            using (var uniHan = new DataClasses1DataContext())
            {
                var workChin = new char[1];
                workChin[0] = character[0];
                var uniHex = ExpandUnihex(workChin);

                var myCjis = from q in uniHan.UniHans
                             where q.cp == uniHex
                             select q;

                foreach (var myChar in myCjis)
                {
                    allCji.Add(myChar.kCangjie);
                }

                found = allCji;

                return allCji.Any();
            }
        }

        public static bool TryGetCharEnglish(string character, out IList<string> found) // Working
        {
            IList<string> allEnglish = new List<string>();

            using (var fullFarEast = new DataClasses1DataContext())
            {
                var myEngs = from q in fullFarEast.FullFarEasts
                             where q.Char == character
                             select q;

                foreach (var myEnglish in myEngs)
                {
                    allEnglish.Add(myEnglish.English);
                }

                found = allEnglish;

                return allEnglish.Any();
            }
        }

        public static bool TryGetCharFei(string character, out IList<string> found) //Working
        {
            IList<string> allFeIs = new List<string>();

            using (var fullFarEast = new DataClasses1DataContext())
            {
                var myFeIs = from q in fullFarEast.FullFarEasts
                             where q.Char == character
                             select q;

                foreach (var myFei in myFeIs)
                {
                    allFeIs.Add(myFei.FeNumber.ToString());
                }

                found = allFeIs;

                return allFeIs.Any();
            }
        }

        public static bool TryGetPinMyPin(string pinyin, out IList<string> found) // Working
        {
            var myPinyin = new List<string>();

            using (var myPins = new DataClasses1DataContext())
            {
                var myPinyins = from q in myPins.PinMypins
                                where q.Pin == pinyin
                                select q;

                myPinyin.AddRange(myPinyins.Select(mypin => mypin.Mypin));

                found = myPinyin;

                return myPinyin.Any();
            }
        }

        public static bool TryGetPinBo(string pinyin, out IList<string> found) // Working
        {
            var bopo = new List<string>();

            using (var pinBopo = new DataClasses1DataContext())
            {
                var myBopos = from q in pinBopo.CharBopoPinCrits
                              where q.Pin == pinyin
                              select q;

                bopo.AddRange(myBopos.Select(myBopo => myBopo.Bopo));
                if (bopo.Any()) found = bopo;
                else found = bopo;

                return bopo.Any();
            }
        }

        public static bool TryGetPinChar(string pinyin, out IList<string> found) // Working
        {
            IList<string> allChar = new List<string>();

            using (var pinChar = new DataClasses1DataContext())
            {
                var myChars = from q in pinChar.CharBopoPinCrits
                              where q.Pin == pinyin
                              select q;

                foreach (var myChar in myChars)
                {
                    allChar.Add(myChar.Char);
                }

                found = allChar;

                return allChar.Any();
            }
        }

        public static bool TryGetCjiChar(string cangjie, out IList<string> found) // Working
        {
            IList<string> allChars2 = new List<string>();

            using (var uniHan = new DataClasses1DataContext())
            {
                var myChars2 = from q in uniHan.UniHans
                               where q.kCangjie == cangjie.ToUpper()
                               select q;

                foreach (
                    var eachOne in
                        myChars2.Select(myChar => ContractUniHex(myChar.cp.ToString(CultureInfo.InvariantCulture))))
                {
                    allChars2.Add(eachOne.ToString(CultureInfo.InvariantCulture));
                }

                found = allChars2;

                return allChars2.Any();
            }
        }

        private static char ContractUniHex(string unihex) //Working
        {
            var sum = 0;
            var mult = 0;
            for (var indexer = 3; indexer >= 0; indexer--)
            {
                if (indexer == 3) mult = 1;
                if (indexer == 2) mult = 16;
                if (indexer == 1) mult = 256;
                if (indexer == 0) mult = 4096;

                var nibble = unihex.Substring(indexer, 1);
                if (nibble == "0") sum += (0*mult);
                if (nibble == "1") sum += (1*mult);
                if (nibble == "2") sum += (2*mult);
                if (nibble == "3") sum += (3*mult);
                if (nibble == "4") sum += (4*mult);
                if (nibble == "5") sum += (5*mult);
                if (nibble == "6") sum += (6*mult);
                if (nibble == "7") sum += (7*mult);
                if (nibble == "8") sum += (8*mult);
                if (nibble == "9") sum += (9*mult);
                if (nibble == "A") sum += (10*mult);
                if (nibble == "B") sum += (11*mult);
                if (nibble == "C") sum += (12*mult);
                if (nibble == "D") sum += (13*mult);
                if (nibble == "E") sum += (14*mult);
                if (nibble == "F") sum += (15*mult);
            }

            return (char) sum;
        }

        private static string ExpandUnihex(IList<char> chinchars) // Working
        {
            var nibble = new char[4];

            var charNo = (uint) chinchars[0];
            for (var x = 0; x <= 3; x++)
            {
                var uintNibble = charNo%16;
                if (uintNibble == 0) nibble[x] = '0';
                if (uintNibble == 1) nibble[x] = '1';
                if (uintNibble == 2) nibble[x] = '2';
                if (uintNibble == 3) nibble[x] = '3';
                if (uintNibble == 4) nibble[x] = '4';
                if (uintNibble == 5) nibble[x] = '5';
                if (uintNibble == 6) nibble[x] = '6';
                if (uintNibble == 7) nibble[x] = '7';
                if (uintNibble == 8) nibble[x] = '8';
                if (uintNibble == 9) nibble[x] = '9';
                if (uintNibble == 10) nibble[x] = 'A';
                if (uintNibble == 11) nibble[x] = 'B';
                if (uintNibble == 12) nibble[x] = 'C';
                if (uintNibble == 13) nibble[x] = 'D';
                if (uintNibble == 14) nibble[x] = 'E';
                if (uintNibble == 15) nibble[x] = 'F';
                charNo = charNo/16;
            }

            var uniHex = nibble[3] + nibble[2].ToString(CultureInfo.InvariantCulture) + nibble[1] + nibble[0];

            return uniHex;
        }

        public static bool TryGetPinCrit(string pinyin, out IList<string> found) // Working
        {
            var critic = new List<string>();

            using (var pinCrit = new DataClasses1DataContext())
            {
                var crits = from q in pinCrit.CharBopoPinCrits
                            where q.Pin == pinyin
                            select q;

                foreach (var crit in crits)
                {
                    critic.Add(crit.Crit);
                    break;
                }

                found = critic;

                return found.Any();
            }
        }

        public static bool TryGetCharCrit(string character, out IList<string> found) // Working
        {
            var crit = new List<string>();

            using (var charCrit = new DataClasses1DataContext())
            {
                var crits = from q in charCrit.CharBopoPinCrits
                            where q.Char == character
                            select q;

                crit.AddRange(crits.Select(oneCrit => oneCrit.Crit));

                found = crit;

                return crit.Any();
            }
        }

        public static bool TryGetCritChar(string crit, out IList<string> found) // Working
        {
            IList<string> oneChar = new List<string>();

            using (var critchar = new DataClasses1DataContext())
            {
                var chars = from q in critchar.CharBopoPinCrits
                            where q.Crit == crit
                            select q;

                foreach (var cChar in chars)
                {
                    oneChar.Add(cChar.Char.ToString(CultureInfo.InvariantCulture));
                }

                found = oneChar;

                return oneChar.Any();
            }
        }

        public static bool TryGetBopoCrit(string bopo, out string found) // Working
        {
            IList<string> oneDiacritical = new List<string>();

            using (var pinCrit = new DataClasses1DataContext())
            {
                var diacriticals = from q in pinCrit.CharBopoPinCrits
                                   where q.Bopo == bopo
                                   select q;

                foreach (var diacritical in diacriticals)
                {
                    oneDiacritical.Add(diacritical.Crit);
                    break;
                }

                found = oneDiacritical[0];
                return oneDiacritical.Any();
            }
        }

        public static bool TryGetCritBopo(string crit, out string found) // Working
        {
            IList<string> oneBopo = new List<string>();

            using (var pinCrit = new DataClasses1DataContext())
            {
                var bopos = from q in pinCrit.CharBopoPinCrits
                            where q.Crit == crit
                            select q;

                foreach (var bopo in bopos)
                {
                    oneBopo.Add(bopo.Bopo);
                    break;
                }
                if (oneBopo.Any())
                    found = oneBopo[0];
                else
                    found = "";

                return oneBopo.Any();
            }
        }
    
        public static bool TryGetCritPin(string crit, out string found) // Working
        {
            IList<string> onePin = new List<string>();

            using (var pinCrit = new DataClasses1DataContext())
            {
                var Pins = from q in pinCrit.CharBopoPinCrits
                            where q.Crit == crit
                            select q;

                foreach (var Pin in Pins)
                {
                    onePin.Add(Pin.Pin);
                    break;
                }

                found = onePin[0];
                return onePin.Any();
            }
        }
    }
}
