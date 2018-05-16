using System;

namespace CreaDev.Framework.Core.Utils
{
    public class Number
    {

        private static string[] arabicOnes = {"", "واحد", "اثنان", "ثلاثة", "أربعة", "خمسة", "ستة", "سبعة", "ثمانية", "تسعة",
                "عشرة", "أحد عشر", "اثنا عشر", "ثلاثة عشر", "أربعة عشر", "خمسة عشر", "ستة عشر", "سبعة عشر", "ثمانية عشر", "تسعة عشر"};
        private static string[] arabicTens = { "عشرون", "ثلاثون", "أربعون", "خمسون", "ستون", "سبعون", "ثمانون", "تسعون" };
        private static string[] arabicHundreds = { "", "مائة", "مئتان", "ثلاثمائة", "أربعمائة", "خمسمائة", "ستمائة", "سبعمائة", "ثمانمائة", "تسعمائة" };
        private static string[] arabicAppendedTwos = { "مئتا", "ألفا", "مليونا", "مليارا", "تريليونا", "كوادريليونا", "كوينتليونا", "سكستيليونا" };
        private static string[] arabicTwos = { "مئتان", "ألفان", "مليونان", "ملياران", "تريليونان", "كوادريليونان", "كوينتليونان", "سكستيليونان" };
        private static string[] arabicGroup = { "مائة", "ألف", "مليون", "مليار", "تريليون", "كوادريليون", "كوينتليون", "سكستيليون" };
        private static string[] arabicAppendedGroup = { "", "ألفاً", "مليوناً", "ملياراً", "تريليوناً", "كوادريليوناً", "كوينتليوناً", "سكستيليوناً" };
        private static string[] arabicPluralGroups = { "", "آلاف", "ملايين", "مليارات", "تريليونات", "كوادريليونات", "كوينتليونات", "سكستيليونات" };


        /// <summary>
        /// This method is to convert latin numbers to arabic numbers
        /// </summary>
        /// <param name="input">numbers</param>
        /// <returns>Arabic numbers</returns>
        public static string ConvertToArabicNumber(int input)
        {
            var inputStr = input.ToString();
            return ConvertToArabicNumber(inputStr);

        }
        /// <summary>
        /// This method is to convert latin numbers to arabic
        /// </summary>
        /// <param name="input">numbers in string</param>
        /// <returns>Arabic numbers</returns>
        public static string ConvertToArabicNumber(string input)
        {
            input = input.Replace("0", "\u0660")
            .Replace("1", "\u0661")
            .Replace("2", "\u0662")
            .Replace("3", "\u0663")
            .Replace("4", "\u0664")
            .Replace("5", "\u0665")
            .Replace("6", "\u0666")
            .Replace("7", "\u0667")
            .Replace("8", "\u0668")
            .Replace("9", "\u0669");

            return input;
        }

        /// <summary>
        /// This method is to convert numbers to arabic words (literaly)
        /// </summary>
        /// <param name="number">numbers</param>
        /// <returns>numbers literaly in arabic</returns>
        public static string ConvertToArabicWords(int number)
        {

            var tempNumber = number;

            if (tempNumber == 0)
                return "صفر";

            var retVal = "";
            var group = 0;
            while (tempNumber >= 1)
            {
                // seperate number into groups
                var numberToProcess = tempNumber % 1000;

                tempNumber = tempNumber / 1000;

                // convert group into its text
                var groupDescription = ProcessArabicGroup(number, numberToProcess, group, tempNumber);

                if (groupDescription != "")
                { // here we add the new converted group to the previous concatenated text
                    if (group > 0)
                    {
                        if (retVal != "")
                            retVal = "و" + " " + retVal;

                        if (numberToProcess != 2)
                        {
                            if ((numberToProcess % 100) != 1)
                            {
                                if (numberToProcess >= 3 && numberToProcess <= 10) // for numbers between 3 and 9 we use plural name
                                    retVal = arabicPluralGroups[group] + " " + retVal;
                                else
                                {
                                    if (retVal != "") // use appending case
                                        retVal = arabicAppendedGroup[group] + " " + retVal;
                                    else
                                        retVal = arabicGroup[group] + " " + retVal; // use normal case
                                }
                            }
                            else
                            {
                                retVal = arabicGroup[group] + " " + retVal; // use normal case
                            }
                        }
                    }

                    retVal = groupDescription + " " + retVal;
                }

                group++;
            }

            var formattedNumber = "";
            formattedNumber += (retVal != "") ? retVal : "";

            return formattedNumber.Trim();

        }

        /// <summary>
        /// This method is to convert numbers to english words (literaly)
        /// </summary>
        /// <param name="number">numbers</param>
        /// <returns>numbers literaly in english</returns>
        public static string ConvertToEnglishWords(int number)
        {

            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + ConvertToEnglishWords(System.Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += ConvertToEnglishWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += ConvertToEnglishWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += ConvertToEnglishWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
        private static string ProcessArabicGroup(int number, int groupNumber, int groupLevel, int remainingNumber)
        {
            var tens = groupNumber % 100;

            var hundreds = groupNumber / 100;

            var retVal = "";

            if (hundreds > 0)
            {
                if (tens == 0 && hundreds == 2) // حالة المضاف
                    retVal = arabicAppendedTwos[0];
                else //  الحالة العادية
                    retVal = arabicHundreds[hundreds];
            }

            if (tens > 0)
            {
                if (tens < 20)
                { // if we are processing under 20 numbers
                    if (tens == 2 && hundreds == 0 && groupLevel > 0)
                    { // This is special case for number 2 when it comes alone in the group
                        if (number == 2000 || number == 2000000 || number == 2000000000 || number == 2000000000000 || number == 2000000000000000 || number == 2000000000000000000)
                            retVal = arabicAppendedTwos[groupLevel]; // في حالة الاضافة
                        else
                            retVal = arabicTwos[groupLevel];//  في حالة الافراد
                    }
                    else
                    { // General case
                        if (retVal != "")
                            retVal += " و ";

                        if (tens == 1 && groupLevel > 0 && hundreds == 0)
                            retVal += " ";
                        else
                            if ((tens == 1 || tens == 2) && (groupLevel == 0 || groupLevel == -1) && hundreds == 0 && remainingNumber == 0)
                            retVal += ""; // Special case for 1 and 2 numbers like: ليرة سورية و ليرتان سوريتان
                        else
                            retVal += arabicOnes[tens];// Get Feminine status for this digit
                    }
                }
                else
                {
                    var ones = tens % 10;
                    tens = (tens / 10) - 2; // 20's offset

                    if (ones > 0)
                    {
                        if (retVal != "")
                            retVal += " و ";

                        // Get Feminine status for this digit
                        retVal += arabicOnes[ones];
                    }

                    if (retVal != "")
                        retVal += " و ";

                    // Get Tens text
                    retVal += arabicTens[tens];
                }
            }

            return retVal;
        }

    }
}
