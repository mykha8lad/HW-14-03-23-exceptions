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
                st.setSurname("  ");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            try
            {
                st.setAddress("Reni", "Paris Commune", "");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            #endregion

            #region Проверка класса Group на выброс исключений
            Group gr = new Group();

            try
            {
                gr.setGroupName("null");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            try
            {
                gr.setCourseNumber(0);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            try
            {
                gr.setCountStudents(-1);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            #endregion
        }
    }
}