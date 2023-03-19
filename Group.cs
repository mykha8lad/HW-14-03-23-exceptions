﻿using Faker;
using System.Text.RegularExpressions;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using Faker.Resources;
using System.Xml.Linq;
using ExceptionLibrary;

namespace HW_14_03_23_exceptions
{
    public class Group
    {
        private List<Student> students = new List<Student>();
        RandomDataForGroup randomData = new RandomDataForGroup();

        private int studentsInGroup;
        private string groupName;
        private string groupSpecialization;
        private int courseNumber;

        public Group()
        {
            createGroup(randomData.groupNames[new Random().Next(randomData.groupNames.Count)], randomData.groupSpecializations[new Random().Next(randomData.groupSpecializations.Count)], randomData.coursesNumber[new Random().Next(randomData.coursesNumber.Count)]);
        }
        public Group(Group group)
        {
            setGroupName(group.getGroupName());
            setGroupSpecialization(group.getGroupSpecialization());
            setCourseNumber(group.getCourseNumber());
            copyToThisListStudents(group.getListStudents());
            group.clearGroup();
            group = null;
        }
        public Group(List<Student> oldListStudents)
        {
            setGroupName(randomData.groupNames[new Random().Next(randomData.groupNames.Count)]);
            setGroupSpecialization(randomData.groupSpecializations[new Random().Next(randomData.groupSpecializations.Count)]);
            setCourseNumber(randomData.coursesNumber[new Random().Next(randomData.coursesNumber.Count)]);
            copyToThisListStudents(oldListStudents);
            oldListStudents.Clear();
        }
        public Group(string groupName, string groupSpecialization, int courseNumber, int countStudents)
        {
            setCountStudents(countStudents);
            createGroup(groupName, groupSpecialization, courseNumber);
        }
        public Group(List<Student> oldListStudents, string groupName, string groupSpecialization, int courseNumber)
        {
            setGroupName(groupName);
            setGroupSpecialization(groupSpecialization);
            setCourseNumber(courseNumber);
            copyToThisListStudents(oldListStudents);
            oldListStudents.Clear();
        }

        private void deleteStudent(Student student) { this.students.Remove(student); }
        private void copyToThisListStudents(List<Student> oldListStudents)
        {
            foreach (Student student in oldListStudents)
            {
                this.students.Add(student);
            }
        }
        private void clearGroup()
        {
            students.Clear();
            groupName = null;
            groupSpecialization = null;
            courseNumber = 0;
        }

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

        public int getCountStudents() { return this.studentsInGroup; }
        public string getGroupName() { return this.groupName; }
        public string getGroupSpecialization() { return this.groupSpecialization; }
        public int getCourseNumber() { return this.courseNumber; }

        public void createGroup(string groupName, string groupSpecialization, int courseNumber)
        {
            setGroupName(groupName);
            setGroupSpecialization(groupSpecialization);
            setCourseNumber(courseNumber);

            for (int student = 1; student <= getCountStudents(); ++student)
            {
                string phoneRegexp = @"^\(\d{3}\)\d{3}\-\d{4}$";
                string phoneNumber;
                do
                {
                    phoneNumber = Faker.Phone.Number();
                } while (!Regex.IsMatch(phoneNumber, phoneRegexp));
                Random random = new Random();
                DateTime birthday = new DateTime(random.Next(2003, 2007), random.Next(1, 13), random.Next(1, 29));
                students.Add(new Student(Faker.Name.First(), Faker.Name.Last(), Faker.Name.Middle(), birthday, phoneNumber, Faker.Address.City(), Faker.Address.StreetName(), Faker.Address.ZipCode()));
            }
        }

        public List<Student> getListStudents() { return this.students; }
        private string getAllStudentsInfo()
        {
            students.Sort((firstStudent, secondStudent) => firstStudent.getLastname().CompareTo(secondStudent.getLastname()));
            return string.Join("\n\n", getListStudents());
        }
        public override string ToString()
        {
            return $"Group: {getGroupName()} Specialization: {getGroupSpecialization()} Course: {getCourseNumber()}\n" +
                $"--------------------------------------------------------------------------------\n" +
                $"{getAllStudentsInfo()}";
        }

        public void addStudentInGroup(Student student) { students.Add(student); }

        private void ShowStudentMenu()
        {
            int userAnswer;
            bool flag = true;
            Student st = null;

            Console.WriteLine($"Enter id student this group ({getGroupName()})");
            int id = int.Parse(Console.ReadLine());

            foreach (Student student in students)
            {
                if (student.getId() == id)
                {
                    st = student;
                    break;
                }
            }

            while (flag)
            {
                Console.WriteLine("Enter menu item\n1 - Name\n2 - Lastname\n3 - Surname\n4 - Phone number < (xxx)xxx-xxxx >\n5 - Birthday < DD.MM.YYYY >\n6 - Address\n7 - EXIT");
                do
                {
                    Console.Write("> ");
                    userAnswer = int.Parse(Console.ReadLine());
                } while (userAnswer < 1 || userAnswer > 7);

                switch (userAnswer)
                {
                    case (int)StudentMenu.STUDENT_NAME:
                        Console.WriteLine("Enter name student");
                        string stName;
                        do
                        {
                            Console.Write("> ");
                            stName = Console.ReadLine();
                        } while (String.IsNullOrEmpty(stName));
                        st.setName(stName);
                        break;
                    case (int)StudentMenu.STUDENT_LASTNAME:
                        Console.WriteLine("Enter lastname student");
                        string stLastName;
                        do
                        {
                            Console.Write("> ");
                            stLastName = Console.ReadLine();
                        } while (String.IsNullOrEmpty(stLastName));
                        st.setLastname(stLastName);
                        break;
                    case (int)StudentMenu.STUDENT_SURNAME:
                        Console.WriteLine("Enter surname student");
                        string stSurname;
                        do
                        {
                            Console.Write("> ");
                            stSurname = Console.ReadLine();
                        } while (String.IsNullOrEmpty(stSurname));
                        st.setSurname(stSurname);
                        break;
                    case (int)StudentMenu.STUDENT_PHONE_NUMBER:
                        Console.WriteLine("Enter phone student (xxx)xxx-xxxx");
                        string phoneNumber;
                        do
                        {
                            Console.Write("> ");
                            phoneNumber = Console.ReadLine();
                        } while (String.IsNullOrEmpty(phoneNumber));
                        st.setPhoneNumber(phoneNumber);
                        break;
                    case (int)StudentMenu.STUDENT_BIRTHDAY:
                        Console.WriteLine("Enter birthday xx.xx.xxxx");
                        DateTime birthday;
                        do
                        {
                            Console.Write("> ");
                            birthday = DateTime.Parse(Console.ReadLine());
                        } while (String.IsNullOrEmpty(birthday.ToString("d")));
                        st.setBirthday(birthday);
                        break;
                    case (int)StudentMenu.STUDENT_ADDRESS:
                        string city;
                        string street;
                        string homeNumber;

                        Console.WriteLine("Enter address");
                        do
                        {
                            Console.Write("City > ");
                            city = Console.ReadLine();
                            Console.Write("Street > ");
                            street = Console.ReadLine();
                            Console.Write("Home Number > ");
                            homeNumber = Console.ReadLine();
                        } while (String.IsNullOrEmpty(city) & String.IsNullOrEmpty(street) & String.IsNullOrEmpty(homeNumber));
                        st.setAddress(city, street, homeNumber);
                        break;
                    case (int)StudentMenu.STUDENT_EXIT:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Wrong item");
                        break;
                }
            }
        }
        private void ShowGroupMenu()
        {
            int userAnswer;
            bool flag = true;

            while (flag)
            {
                Console.WriteLine("Enter menu item\n1 - Name group\n2 - Group Specialization\n3 - Course number\n4 - EXIT");
                do
                {
                    Console.Write("> ");
                    userAnswer = int.Parse(Console.ReadLine());
                } while (userAnswer < 1 || userAnswer > 4);

                switch (userAnswer)
                {
                    case (int)GroupMenu.GROUP_NAME:
                        Console.WriteLine("Enter group name");
                        string gName;
                        do
                        {
                            Console.Write("> ");
                            gName = Console.ReadLine();
                        } while (!randomData.groupNames.Contains(gName));
                        setGroupName(gName);
                        break;
                    case (int)GroupMenu.GROUP_SPECIALIZATION:
                        Console.WriteLine("Enter group specialization");
                        string gSpec;
                        do
                        {
                            Console.Write("> ");
                            gSpec = Console.ReadLine();
                        } while (!randomData.groupSpecializations.Contains(gSpec));
                        setGroupSpecialization(gSpec);
                        break;
                    case (int)GroupMenu.COURSE_NUMBER:
                        Console.WriteLine("Enter course number");
                        int gCourse;
                        do
                        {
                            Console.Write("> ");
                            gCourse = int.Parse(Console.ReadLine());
                        } while (!randomData.coursesNumber.Contains(gCourse));
                        setCourseNumber(gCourse);
                        break;
                    case (int)GroupMenu.GROUP_EXIT:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Wrong item");
                        break;
                }
            }
        }
        public void editData()
        {
            int userAnswer;
            bool flag = true;

            while (flag)
            {
                Console.WriteLine("Enter item menu\n1 - Edit group\n2 - Edit student info\n3 - EXIT");
                do
                {
                    Console.Write("> ");
                    userAnswer = int.Parse(Console.ReadLine());
                } while (userAnswer < 1 || userAnswer > 3);

                switch (userAnswer)
                {
                    case 1:
                        ShowGroupMenu();
                        break;
                    case 2:
                        ShowStudentMenu();
                        break;
                    case 3:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Wrong item");
                        break;
                }
            }
        }

        public void studentTransfer(Group group)
        {
            Console.WriteLine($"Enter id student this group ({getGroupName()}), for transfer in group {group.getGroupName()}");
            int id = int.Parse(Console.ReadLine());

            foreach (Student student in students)
            {
                if (student.getId() == id)
                {
                    group.students.Add(student);
                    deleteStudent(student);
                    break;
                }
            }
        }

        public void deletingAllStudentPassSession()
        {
            students.RemoveAll(s => s.getListOffsets().Any(score => score < 7));
        }
        public void deleteFailedStudent()
        {
            double minAvg = double.MaxValue;
            Student failedStudent = null;
            foreach (Student student in students)
            {
                double avg = 0;
                avg += student.getListOffsets().Average() + student.getListHometasks().Average() + student.getListExams().Average();

                if (avg < minAvg)
                {
                    minAvg = avg;
                    failedStudent = student;
                }
            }
            Console.WriteLine($"Student {failedStudent.getName()} ({failedStudent.getId()}) remove");
            deleteStudent(failedStudent);
        }
    }
}
