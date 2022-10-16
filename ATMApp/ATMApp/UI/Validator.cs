using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.UI
{
    public static class Validator
    {
        public static T Convert<T>(string prompt)
        {
            bool valid = false;
            string userInput;

           while (!valid)
            {
                userInput = Utility.GetUserInput(prompt);

                try
                {
                    var Converter = TypeDescriptor.GetConverter(typeof(T));
                    if(Converter != null)
                    {
                        return (T)Converter.ConvertFromString(userInput);

                    }else
                    {
                        return default;
                    }
                
                }
                catch
                {
                    Utility.PrintMessage("\n\n═Invalid input, try again.═\n\n", false);
                }
            }
            return default;
        }
     

        
    }
}
