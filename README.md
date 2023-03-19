# Дз от 14/03/23. Exceptions
## 1. В класс Student и в класс Group добавить по три генерации исключений.
### Класс Student
 ```cs
public void setName(string name)
{
    if (String.IsNullOrEmpty(name)) throw new ArgumentNullException();
    this.name = name;
}
public void setLastname(string lastname)
{
    if (String.IsNullOrEmpty(lastname)) throw new ArgumentNullException();
    this.lastname = lastname;
}
public void setSurname(string surname)
{
    if (String.IsNullOrEmpty(name)) throw new ArgumentNullException();
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
    if (String.IsNullOrEmpty(city)) throw new ArgumentNullException();
    this.city = city;
}
public void setStreet(string street)
{
    if (String.IsNullOrEmpty(street)) throw new ArgumentNullException();
    this.street = street;
}
public void setHomeNumber(string homeNumber)
{
    if (String.IsNullOrEmpty(homeNumber)) throw new ArgumentNullException();
    this.homeNumber = homeNumber;
}
```
____
### Класс Group
```cs
public void setCountStudents(int countStudents)
{
    if (countStudents < 5 || countStudents > 15) throw new Exception("The number of students cannot exceed 15, or or be less than 5.");
    this.studentsInGroup = countStudents;
}
public void setGroupName(string groupName)
{
    if (String.IsNullOrEmpty(groupName)) throw new ArgumentNullException();
    this.groupName = groupName;
}
public void setGroupSpecialization(string groupSpecialization)
{
    if (String.IsNullOrEmpty(groupSpecialization)) throw new ArgumentNullException();
    this.groupSpecialization = groupSpecialization;
}
public void setCourseNumber(int courseNumber)
{
    if (courseNumber < 1 || courseNumber > 5) throw new Exception("The number of courses cannot exceed 5, or or be less than 1.");
    this.courseNumber = courseNumber;
}
```
____
## 2. В main протестировать перехват этих исключений
```cs
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
```
### Результат:
[![1.jpg](https://i.postimg.cc/50KC7XZC/1.jpg)](https://postimg.cc/YGg0jC2r)
____
