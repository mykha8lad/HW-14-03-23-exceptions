using System.Text.RegularExpressions;

namespace HW_14_03_23_exceptions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Проверка класса Student на выброс исключений
            Student st = new Student();

            try
            {
                st.setName(null);
                /// st.setName("");
            }
            catch (Exception exc)
            {
                Console.WriteLine("An exception has been thrown!\n" + exc.Message);
                /// An exception has been thrown!
                /// Value cannot be null.
            }

            try
            {
                st.setAddress("Reni", "Paris Commune", "");
                /// Исключение шлепнулось из-за пустой строки, проверка на это так же учтена.
                /// К сожалению она не ловится если значение равно " ".
            }
            catch (Exception exc)
            {
                Console.WriteLine("\nAn exception has been thrown!\n" + exc.Message);                
            }
            #endregion

            #region Проверка класса Group на выброс исключений
            Group gr = new Group();
            
            try
            {
                gr.setGroupName("P12");
                gr.setGroupSpecialization("C#");
                gr.setCourseNumber(2);
                gr.setCountStudents(3);
            }
            catch (Exception exc)
            {
                Console.WriteLine("\nAn exception has been thrown!\n" + exc.Message);                
            }
            #endregion
        }
    }
}