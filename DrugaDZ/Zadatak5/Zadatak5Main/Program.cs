using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak4Main;

namespace Zadatak5Main
{
    public class Program
    {
        public static University FER { get; set; }
        public static University EFZG { get; set; }
        public static University FSB { get; set; }
        public static University TU { get; set; }
        public static University ANU { get; set; }
        public static University[] _allUniversities;
        public static int _numOfUniversities = 5;

        public static void Main(string[] args)
        {

            Student student1 = new Student("Tomislav", "0036487452"); //FER
            Student student2 = new Student("Ivan", "0036486949"); //FER
            Student student3 = new Student("Lucija", "0036447589"); //FER
            Student student4 = new Student("Ana", "0036412345"); //EFZG
            Student student5 = new Student("Tamara", "0036486999"); //FER
            Student student6 = new Student("Lucija", "0036486888"); //EFZG
            Student student7 = new Student("Antisa", "0036477777"); //FER
            Student student8 = new Student("Zvonimir", "0036499969"); //FER
            Student student9 = new Student("Matija", "0036444444"); //FER
            Student student10 = new Student("Petar", "0036555555"); //FSB
            Student student11 = new Student("Dominik", "0036222222"); //TU
            Student student12 = new Student("Mare", "0036333333"); //ANU

            Student[] ferStudents = new Student[10];
            Student[] efzgStudents = new Student[10];
            Student[] fsbStudents = new Student[10];
            Student[] tuStudents = new Student[10];
            Student[] anuStudents = new Student[10];

            fillStudents(ferStudents, student1, student2, student3, student5, student7, student8, student9);
            fillStudents(efzgStudents, student4, student6, student1);
            fillStudents(fsbStudents, student10);
            fillStudents(tuStudents, student11);
            fillStudents(anuStudents, student12);

            FER = new University("Fakultet elektrotehnike i računarstva", ferStudents, "CRO", false);
            EFZG = new University("Ekonomski fakultet Zagreb", efzgStudents, "CRO", false);
            FSB = new University("Fakultet strojarstva i brodogradnje", fsbStudents, "CRO", true);
            TU = new University("Technische Universität Berlin", tuStudents, "GER", false);
            ANU = new University("Australian National University", anuStudents, "AUS", false);

            _allUniversities = GetAllCroatianUniversities();


            Student[] allCroatianStudent = _allUniversities.Where(item => item != null && item.Country.Equals("CRO"))
                                                           .SelectMany(item => item.Students).Distinct().ToArray();

            // Assume that in university there is not two students with the same jmbag
            Student[] croatianStudentsOnMultipleUniversities = _allUniversities.Where(item => item != null && item.Country.Equals("CRO"))
                                                                               .SelectMany(item => item.Students)
                                                                               .GroupBy(item => item)
                                                                               .SelectMany(grp => grp.Skip(1))
                                                                               .ToArray();

            Student[] studentsOnMaleOnlyUniversities = _allUniversities.Where(item => item != null && item.MaleOnly == true)
                                                                       .SelectMany(item => item.Students)
                                                                       .ToArray();
            

        }

        private static void fillStudents(Student[] students, params Student[] arguments)
        {
            for (int i = 0; i < arguments.Length; i++)
            {
                students[i] = arguments[i];
            }
        }

        private static University[] GetAllCroatianUniversities()
        {
            University[] allUniversities = new University[10];
            allUniversities[0] = FER;
            allUniversities[1] = EFZG;
            allUniversities[2] = FSB;
            allUniversities[3] = TU;
            allUniversities[4] = ANU;

            return allUniversities;
        }


    }
}
