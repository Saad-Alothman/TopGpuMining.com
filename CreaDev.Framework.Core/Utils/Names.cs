using System;
using System.Collections.Generic;
using CreaDev.Framework.Core.Collections;

namespace CreaDev.Framework.Core.Utils
{
    public static class Names
    {
        static Random random = new Random();
        public static string RandomCompanyName()
        {
            return $"{companyNamePrefix.Random()} {RandomName()} {companyNamePostfix.Random()}";
        }
        public static string RandomName()
        {
            bool isMale = random.Next(1, 100) < 80;
            string firstName = RandomFirstName(isMale);
            string middleNmae = RandomMiddleName(2);
            string lastName = RandomFamilyName();

            return $"{firstName.Trim()} {middleNmae.Trim()} {lastName.Trim()}";


        }
        public static string RandomFirstName(bool isMale)
        {
            return isMale?maleNames.Random():femaleNames.Random();
        }
        public static string RandomFamilyName()
        {
            return familyNamePrefix.Random()+" "+ maleNames.Random();
        }
        public static string RandomMiddleName(int count)
        {
            string result = "";
            foreach (var middleName in maleNames.Random(count))
            {
                result += middleName+" ";
            }
            result= result.Trim();

            return result;
        }


        public static List<string> femaleNames = new List<string>()
        {
            "آية",
"أسيل",
"أمينة",
"سمية",
"اسراء",
"إيمان",
"إيناس",
"السعدية",
"بحرية",
"بشرى",
"بثينة",
"جيداء",
"حفصة",
"حليمة",
"داليا",
"رامة",
"رغد",
"رفيف",
"رقية",
"رنا",
"ريحانة",
"زينب",
"سارة",
"سعيدة",
"سلمى",
"سلوى",
"سمراء",
"سميرة",
"سها",
"شيماء",
"عائشة",
"عبير",
"غادة",
"غيداء",
"فاطمة",
"فتيحة",
"فرح",
"كنزى",
"لميس",
"لولوه",
"ليلى",
"مبروكة",
"مروة",
"مروى",
"مناصف",
"منى",
"مها",
"مهيبة",
"ميا",
"ميساء",
"ميمونة",
"نادية",
"نادين",
"ندى",
"نجلاء",
"نورا",
"هاجر",
"هند",
"هيفاء",
"يارا",
"ياسمين",

        };
        public static List<string> maleNames = new List<string>()
        {
            "أبو بكر",
"أجود",
"أحمد",
"أحنف",
"أدهم",
"أيوب",
"إياد",
"بدر",
"بلال",
"جعفر",
"حسن",
"حسين",
"حكيم",
"راكان",
"رضوان",
"زبير",
"سلام",
"صباح",
"صلاح الدين",
"طارق",
"عارف",
"عاطف",
"عباس",
"عبد الستار",
"عبد المجيد",
"عرقوب",
"عزيز",
"عفيف",
"عكرمة",
"علاء",
"علي",
"عمر",
"عمرو",
"فاروق",
"فهد",
"فيصل",
"كنان",
"لطيف",
"محمد",
"محمد علي",
"محمود",
"مروان",
"مصطفى",
"ملحم",
"مهند",
"ميثم",
"ناصيف (اسم علم)",
"نجم الدين",
"نعمان",
"نعمة الله",
"هاشم",
"هشام",
"هيثم",
"ولاء الدين",
"رال",
"حياتي",
"رجاء",
"عفلق",
"رماح",
"غدق",
"سرور",
"مكارم",
"سلام",
"نور",
"اسلام",
"تحاسين",
"نضال",
"تحرير",
"جهاد",
"تمام",
"نهاد",
"حماس",
"سهيل",
"عيد",
"رجب",
"رمضان",
"خميس",
"شعبان",
"جمعة",

        };

        public static List<string> familyNamePrefix = new List<string>() {" آل","ال","با","بن "};
        public static List<string> companyNamePrefix = new List<string>() {" شركة","مكتب","مؤسسة"};
        public static List<string> companyNamePostfix = new List<string>() {"لإدارة الأملاك","للتطوير العقاري","للعقارات"};
    }
}