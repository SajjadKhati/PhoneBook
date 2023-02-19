using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBook
{
    internal class Convertor
    {
        /// <summary>
        /// این متد چون چند بار آن هم با پیامی ثابت تکرار میشود ، بخاطر همین ، یکبار اینجا نوشتیم اما چندان متد اصولی نیست .
        /// </summary>
        /// <param name="numberString"></param>
        /// <returns></returns>
        internal static long PhoneNumberWithMessage(string numberString)
        {
            long convertedNumber = Convertor.StringToUInt64(numberString);

            string errorCaption = "خطای اعتبار سنجی";
            string errorMessage = "خطای اعتبار سنجی برای فرمت 'شماره تماس' روی داد .\n" +
                                  "لطفا شماره تماس را بصورت عددی و همچنین به شکلی صحیح وارد کنید .\n" +
                                  "تبدیل شماره تماس ، با شکست رو به رو شد .";

            if (convertedNumber == -1)
                MessageBox.Show(errorMessage, errorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            return convertedNumber;
        }


        /// <summary>
        /// در صورت عدم موفقیت ، عدد -1 را برمیگرداند .
        /// </summary>
        /// <param name="numberString"></param>
        /// <returns></returns>
        internal static long StringToUInt64(string numberString)
        {
            bool isConverted = long.TryParse(numberString, out long convertedNumber);
            if (isConverted == false)
                return -1;

            return convertedNumber;
        }


        internal static int StringToUInt32(string numberString)
        {
            bool isConverted = int.TryParse(numberString, out int convertedNumber);
            if (isConverted == false)
                return -1;

            return convertedNumber;
        }


    }
}
