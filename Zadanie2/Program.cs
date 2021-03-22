using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Zadanie2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var admin = args[0]; //zapisać jako argument nazwę użytkownika PC
                var path = @"C:\Users\"+@admin+@"\dane.csv"; 
                var lines = File.ReadAllLines(path);

                HashSet<Student> students = new HashSet<Student>(new Comparator());

                //plik z niepoprawnymi danymi (log)
                StreamWriter sWriter = new StreamWriter(@"C:\Users"+ @admin + @"log.txt");

                foreach (var line in lines)
                {
                    string[] studentData = line.Split(',');
                    if (studentData.Length == 9) 
                    {

                        students.Add(new Student
                        {

                            firstName = studentData[0],
                            lastName = studentData[1],
                            name = studentData[2],
                            mode = studentData[3],
                            indexNumber = "s" + studentData[4],
                            birthDate = studentData[5],
                            email = studentData[6],
                            mothersName = studentData[7],
                            fathersName = studentData[8]

                        });
                    }
                    else 
                    {
                        sWriter.WriteLine("Student: s" + studentData[4] + " " + studentData[0] + " " + studentData[1] + " : " + "Niepoprawne dane");
                    }
                }
                sWriter.Close();

                Dictionary<string, int> courseNumbr = new Dictionary<string, int>(); 

                foreach (var student in students)
                {
                    if (!courseNumbr.ContainsKey(student.name))
                    {
                        courseNumbr.Add(student.name, 1);
                    }
                    else
                    {
                        courseNumbr[student.name]++;
                    }
                }

                

                //konwersacja na xml
                XDocument xDocument = new XDocument(new XElement("uczelnia", 
                    new XAttribute("createdAt", DateTime.Today),
                    new XAttribute("author", "Patryk Kaminski"),

                    new XElement("studenci",
                        from daneStudenta in students
                        select new XElement("student",
                            new XAttribute("indexNumber", daneStudenta.indexNumber),
                            new XElement("firstName", daneStudenta.firstName),
                            new XElement("lastName", daneStudenta.lastName),
                            new XElement("birthDate", daneStudenta.birthDate),
                            new XElement("email", daneStudenta.email),
                            new XElement("mothersName", daneStudenta.mothersName),
                            new XElement("fathersName", daneStudenta.fathersName),
                            new XElement("studies",
                                new XElement("name", daneStudenta.name),
                                new XElement("mode", daneStudenta.mode)
                            )
                        )
                    ),

                    new XElement("activeStudies",
                        from fields in courseNumbr
                        select new XElement("studies",
                            new XAttribute("name", fields.Key),
                            new XAttribute("numberOfStudents", fields.Value)
                        )
                    )
                ));

                string nazwaWynik = "result.xml";
                xDocument.Save(@"C:\Users\"+ @admin + @"\result.xml"); 
                Console.WriteLine("Pomyslnie zapisano plik " + nazwaWynik);


            }
            catch (FileNotFoundException)
            {
                string nazwaDane = "dane.csv";
                Console.WriteLine("Plik " + nazwaDane + " nie istnieje");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Podana sciezka jest niepoprawna");
            }
            
        }
    }
}
