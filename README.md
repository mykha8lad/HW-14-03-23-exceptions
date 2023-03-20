# Дз от 14/03/23. Exceptions
## 1. В класс Student и в класс Group добавить по три генерации исключений.
### От нечего делать имеем классы собственных исключений в отдельной библиотеке
[![solution.jpg](https://i.postimg.cc/Qdk5m3R3/solution.jpg)](https://postimg.cc/K1jRcdGq)
### Класс исключения StringException для выброса исключений всех строковых вхождений
```cs
public class StringException : Exception
{
    private readonly string strMessageException = "Value property cannot be \"null\", a space, or an empty occurrence, please refer to the detailed documentation or try again.\n";
    private string getStrMessageException() { return this.strMessageException; }

    public StringException()
    {
        Console.Write($"Exception: {getStrMessageException()}");
    }

    public override string ToString() { return $"{StackTrace}\n"; }
}
```
____
### Классы исключений CountStudentsException и CourseNumberException для обработки всех числовых вхождений, отдельно для случая с некорректным количеством студентов, и некорректным значением курса
```cs
public class IntException : Exception
{
    private readonly string intMessageException = "Range violated. ";
    private string getIntMessageException() { return this.intMessageException; }

    public IntException()
    {
        Console.Write($"Exception: {getIntMessageException()}");
    }

    public override string ToString() { return $"{StackTrace}\n"; }
}

public class CountStudentsException : IntException
{
    private readonly string CountStudentsMessageException = "The number of students cannot exceed 15, or or be less than 5.\n";
    private string getMessageException() { return this.CountStudentsMessageException; }

    public CountStudentsException()
    {
        Console.Write(getMessageException());
    }
}

public class CourseNumberException : IntException
{
    private readonly string CourseNumberMessageException = "The number of courses cannot exceed 5, or or be less than 1.\n";
    private string getMessageException() { return this.CourseNumberMessageException; }

    public CourseNumberException()
    {
        Console.Write(getMessageException());
    }
}
```
____
### Класс Student с проработанными сеттерами исключением StringException выглядит так
 ```cs
public void setName(string name)
{
    if (String.IsNullOrEmpty(name) || String.IsNullOrWhiteSpace(name)) throw new StringException();
    this.name = name;
}
public void setLastname(string lastname)
{
    if (String.IsNullOrEmpty(lastname) || String.IsNullOrWhiteSpace(lastname)) throw new StringException();
    this.lastname = lastname;
}
public void setSurname(string surname)
{
    if (String.IsNullOrEmpty(name) || String.IsNullOrWhiteSpace(surname)) throw new StringException();
    this.surname = surname;
}
public void setPhoneNumber(string phoneNumber)
{
    string phoneRegexp = @"^\(\d{3}\)\d{3}\-\d{4}$";
    if (Regex.IsMatch(phoneNumber, phoneRegexp)) this.phoneNumber = phoneNumber;
    else this.phoneNumber = "(000)000-0000";
    /// Метод Regex.IsMatch() принимает два параметра: строку для поиска совпадения и регулярное выражение.
    /// Если любой из этих параметров имеет значение null, метод Regex.IsMatch() выбрасывает исключение System.ArgumentNullException()            
}
```
____
### Класс Address
```cs
public void setCity(string city)
{
    if (String.IsNullOrEmpty(city) || String.IsNullOrWhiteSpace(city)) throw new StringException();
    this.city = city;
}
public void setStreet(string street)
{
    if (String.IsNullOrEmpty(street) || String.IsNullOrWhiteSpace(street)) throw new StringException();
    this.street = street;
}
public void setHomeNumber(string homeNumber)
{
    if (String.IsNullOrEmpty(homeNumber) || String.IsNullOrWhiteSpace(homeNumber)) throw new StringException();
    this.homeNumber = homeNumber;
}
```
____
### Сеттеры класса Group с исключениями CourseNumberException и CountStudentsException
```cs
public void setCountStudents(int countStudents)
{
    if (countStudents < 5 || countStudents > 15) throw new CountStudentsException();
    this.studentsInGroup = countStudents;
}
public void setGroupName(string groupName)
{
    if (String.IsNullOrEmpty(groupName) || String.IsNullOrWhiteSpace(groupName)) throw new StringException();
    this.groupName = groupName;
}
public void setGroupSpecialization(string groupSpecialization)
{
    if (String.IsNullOrEmpty(groupSpecialization) || String.IsNullOrWhiteSpace(groupSpecialization)) throw new StringException();
    this.groupSpecialization = groupSpecialization;
}
public void setCourseNumber(int courseNumber)
{
    if (courseNumber < 1 || courseNumber > 5) throw new CourseNumberException();
    this.courseNumber = courseNumber;
}
```
____
## 2. В main перехватываем всё это дело
```cs
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
        gr.setGroupName(null);
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
```
### И получаем такую красоту :star_struck:
[![console.jpg](https://i.postimg.cc/RFmD840L/console.jpg)](https://postimg.cc/PL6yvGYC)
[![99px-ru-animacii-12142-chernaja-obezjana-cheshet-golovu-smotrja-v-kameru.gif](https://i.postimg.cc/c4b23Bmt/99px-ru-animacii-12142-chernaja-obezjana-cheshet-golovu-smotrja-v-kameru.gif)](https://postimg.cc/4mpWktjX)
