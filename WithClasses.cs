using System.Globalization;

class Program
{
    class Student
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String YearOfEnrollment { get; set; }
        public String Faculty { get; set; }
        public String Department { get; set; }
        public String StudentID { get; set; }

        public Student(String firstName, String lastName, String yearOfEnrollment, String faculty, String department, String studentID)
        {
            FirstName = firstName;
            LastName = lastName;
            YearOfEnrollment = yearOfEnrollment;
            Faculty = faculty;
            Department = department;
            StudentID = studentID;
        }
    }

    class Sistem
    {
        public List<Student> StudentList;
        string işlem = "";
        int IDCount = 0;
        string SearchID = null;
        int foundIndex = 0;
        bool found = false;
        string DeleteID = null;

        string filePath = "/Users/halilibrahim/Desktop/Code/stringParse/classDeneme/studentList";

        public Sistem()
        {
            StudentList = new List<Student>();

            programBaşı();

            while (işlem != "5")
            {
                İşlemSeçimi();
                işlem = Console.ReadLine();

                switch (işlem)
                {
                    case "1":
                        KayıtOluştur();
                        break;
                    case "2":
                        PrintStudentİnformation();
                        break;
                    case "3":
                        KayıtGüncelle();
                        break;
                    case "4":
                        KayıtSil();
                        break;
                    case "5":
                        Console.Clear();
                        Console.WriteLine("Çıkış işlemi seçildi. Program sonlandırılıyor...");
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Geçersiz işlem seçimi. Lütfen tekrar deneyiniz.");
                        break;
                }
            }
        }

        public static void İşlemSeçimi()
        {
            Console.WriteLine("Lütfen yapmak istediğiniz işlemi seçiniz:");
            Console.WriteLine("1- Yeni kayıt oluştur");
            Console.WriteLine("2- Kayıtları görüntüle");
            Console.WriteLine("3- Bilgi değiştirme");
            Console.WriteLine("4- Kayıt silme");
            Console.WriteLine("5- Çıkış");
        }

        public void KayıtOluştur()
        {
            Console.Clear();
            Console.WriteLine("Yeni Kayıt Oluşturma İşlemi Seçildi.");

            Console.WriteLine("Lütfen isminizi giriniz!");
            string FirstName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(FirstName))
            {
                Console.Clear();
                Console.WriteLine("İsim boş bırakılamaz. Tekrar giriniz:");
                FirstName = Console.ReadLine();
            }

            Console.WriteLine("Lütfen Soyisminizi giriniz:");
            string LastName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(LastName))
            {
                Console.Clear();
                Console.WriteLine("Soyisim bilgisi boş bırakılamaz. Tekrar giriniz:");
                LastName = Console.ReadLine();
            }

            Console.WriteLine("Lütfen Kayıt Tarihini Giriniz (GG/AA/YYYY):");
            string YearOfEnrollment = Console.ReadLine();
            while (!TarihGecerliMi(YearOfEnrollment, out YearOfEnrollment))
            {
                Console.WriteLine("Lütfen Kayıt Tarihini Giriniz (GG/AA/YYYY):");
                YearOfEnrollment = Console.ReadLine();
            }

            Console.WriteLine("Lütfen Fakülte Bilgisini Giriniz:");
            string Faculty = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(Faculty))
            {
                Console.Clear();
                Console.WriteLine("Lütfen Fakülte Bilgisini Giriniz:");
                Faculty = Console.ReadLine();
            }

            Console.WriteLine("Lütfen Department Bilgisini Giriniz:");
            string Department = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(Department))
            {
                Console.Clear();
                Console.WriteLine("Department boş bırakılamaz. Tekrar giriniz:");
                Department = Console.ReadLine();
            }

            Console.Clear();
            Console.WriteLine("Girilen bilgiler:");
            Console.WriteLine("First Name: " + FirstName);
            Console.WriteLine("Last Name: " + LastName);
            Console.WriteLine("Date of Enrollment: " + YearOfEnrollment);
            Console.WriteLine("Faculty: " + Faculty);
            Console.WriteLine("Department: " + Department);
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Bu bilgiler kaydedilsin mi? (Y/N)");
            string onay = Console.ReadLine().ToUpper();

            if (onay != "Y")
            {
                Console.Clear();
                Console.WriteLine("Kayıt oluşturma işlemi iptal edildi.");
                return;
            }

            string StudentID = CreateNewID(YearOfEnrollment, Faculty, Department, -1);

            Student student = new Student(FirstName, LastName, YearOfEnrollment, Faculty, Department, StudentID);
            StudentList.Add(student);
            IDCount++;

            DosyayaYaz();

            Console.Clear();
            Console.WriteLine("Yeni kayıt başarıyla oluşturuldu. Öğrenci ID: " + StudentID);
        }

        public void PrintStudentİnformation()
        {
            Console.Clear();
            if (StudentList.Count == 0)
            {
                Console.WriteLine("Henüz kayıt yok.");
                return;
            }

            for (int i = 0; i < StudentList.Count; i++)
            {
                Console.WriteLine(
                    (i + 1) + ". " +
                    $"{StudentList[i].FirstName} - {StudentList[i].LastName} - {StudentList[i].YearOfEnrollment} - {StudentList[i].Faculty} - {StudentList[i].Department} - {StudentList[i].StudentID}"
                );
            }
        }

        public void KayıtGüncelle()
        {
            Console.Clear();
            Console.WriteLine("Kayıt Güncelleme İşlemi Seçildi.");
            Console.WriteLine("Lütfen güncellemek istediğiniz Öğrenci ID'sini giriniz:");
            SearchID = Console.ReadLine();

            SearchStudent();

            if (!found)
            {
                Console.Clear();
                Console.WriteLine("Aranan ID'ye sahip öğrenci bulunamadı. Lütfen tekrar deneyiniz.");
                return;
            }

            Console.Clear();
            Console.WriteLine("Bulunan öğrenci:");
            Console.WriteLine($"{StudentList[foundIndex].FirstName} - {StudentList[foundIndex].LastName} - {StudentList[foundIndex].YearOfEnrollment} - {StudentList[foundIndex].Faculty} - {StudentList[foundIndex].Department} - {StudentList[foundIndex].StudentID}");
            Console.WriteLine("------------------------------------");

            Console.WriteLine("Değiştirmek istediğiniz bilgiyi seçiniz:");
            Console.WriteLine("1- First Name");
            Console.WriteLine("2- Last Name");
            Console.WriteLine("3- Year of Enrollment (DD/MM/YYYY)");
            Console.WriteLine("4- Faculty");
            Console.WriteLine("5- Department");
            string changeInfo = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Güncellemek istediğinize emin misiniz? (Y/N)");
            string yesorno = Console.ReadLine().ToUpper();
            if (yesorno != "Y")
            {
                Console.Clear();
                Console.WriteLine(yesorno == "N" ? "Güncelleme iptal edildi." : "Geçersiz işlem!");
                return;
            }

            Console.WriteLine("Lütfen yeni bilgiyi giriniz:");
            string newInfo = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(newInfo))
            {
                Console.Clear();
                Console.WriteLine("Boş değer girilemez. Lütfen yeni bilgiyi giriniz:");
                newInfo = Console.ReadLine();
            }

            bool idAffecting = false;

            if (changeInfo == "3")
            {
                while (!TarihGecerliMi(newInfo, out newInfo))
                {
                    Console.WriteLine("Lütfen tarihi GG/AA/YYYY formatında giriniz:");
                    newInfo = Console.ReadLine();
                }
                StudentList[foundIndex].YearOfEnrollment = newInfo;
                idAffecting = true;
            }
            else if (changeInfo == "4")
            {
                StudentList[foundIndex].Faculty = newInfo;
                idAffecting = true;
            }
            else if (changeInfo == "5")
            {
                StudentList[foundIndex].Department = newInfo;
                idAffecting = true;
            }
            else if (changeInfo == "1")
            {
                StudentList[foundIndex].FirstName = newInfo;
            }
            else if (changeInfo == "2")
            {
                StudentList[foundIndex].LastName = newInfo;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Geçersiz seçim!");
                return;
            }
            if (idAffecting)
            {
                if (StudentList[foundIndex].Faculty.Length < 1 ||
                    StudentList[foundIndex].Department.Length < 1 ||
                    StudentList[foundIndex].YearOfEnrollment.Length < 10)
                {
                    Console.Clear();
                    Console.WriteLine("Geçersiz Faculty/Department/YearOfEnrollment. Güncelleme iptal.");
                    return;
                }

                StudentList[foundIndex].StudentID =
                    CreateNewID(
                        StudentList[foundIndex].YearOfEnrollment,
                        StudentList[foundIndex].Faculty,
                        StudentList[foundIndex].Department,
                        foundIndex);
            }

            DosyayaYaz();

            Console.Clear();
            Console.WriteLine("Bilgi başarıyla güncellendi.");
            Console.WriteLine("Güncel kayıt: " +
                $"{StudentList[foundIndex].FirstName} - {StudentList[foundIndex].LastName} - {StudentList[foundIndex].YearOfEnrollment} - {StudentList[foundIndex].Faculty} - {StudentList[foundIndex].Department} - {StudentList[foundIndex].StudentID}"
            );
        }

        public void SearchStudent()
        {
            found = false;
            foundIndex = 0;

            for (int i = 0; i < StudentList.Count; i++)
            {
                if (StudentList[i].StudentID == SearchID)
                {
                    foundIndex = i;
                    found = true;
                    break;
                }
            }
        }

        public void KayıtSil()
        {
            Console.Clear();
            Console.WriteLine("Kayıt silme işlemi seçildi!");
            Console.WriteLine("Silmek istediğiniz öğrenci ID giriniz!");
            DeleteID = Console.ReadLine();

            SearchID = DeleteID;
            SearchStudent();

            if (!found)
            {
                Console.Clear();
                Console.WriteLine("Aranan ID'ye sahip öğrenci bulunamadı.");
                return;
            }

            Console.Clear();
            Console.WriteLine("Silinecek öğrenci:");
            Console.WriteLine($"{StudentList[foundIndex].FirstName} - {StudentList[foundIndex].LastName} - {StudentList[foundIndex].StudentID}");
            Console.WriteLine("Devam etmek istiyor musunuz? (Y/N)");
            string yesorno = Console.ReadLine().ToUpper();

            if (yesorno != "Y")
            {
                Console.Clear();
                Console.WriteLine(yesorno == "N" ? "Silme iptal edildi." : "Geçersiz işlem!");
                return;
            }

            string deletedId = StudentList[foundIndex].StudentID;
            StudentList.RemoveAt(foundIndex);
            IDCount--;
            DosyayaYaz();
            Console.Clear();
            Console.WriteLine(deletedId + " ID'li öğrenci başarıyla silindi");
        }
        public string CreateNewID(string yearOfEnrollment, string faculty, string department, int studentIndex)
        {
            string yearFacultyDepartment = yearOfEnrollment.Substring(6) + faculty.Substring(0, 1).ToUpper() + department.Substring(0, 1).ToUpper();

            int maxExistingNumber = 0;

            for (int i = 0; i < StudentList.Count; i++)
            {
                if (i == studentIndex)
                    continue;

                string existingId = StudentList[i].StudentID;
                if (string.IsNullOrEmpty(existingId))
                    continue;

                // Aynı yıl+fakülte/bölüm kodu ile başlayan ID'ler
                if (existingId.Length >= yearFacultyDepartment.Length + 3 && existingId.StartsWith(yearFacultyDepartment))
                {
                    string numberPartText = existingId.Substring(yearFacultyDepartment.Length, 3);

                    int numberPart;
                    if (int.TryParse(numberPartText, out numberPart))
                    {
                        if (numberPart > maxExistingNumber)
                            maxExistingNumber = numberPart;
                    }
                }
            }

            int newNumber = maxExistingNumber + 1;
            string newNumberText = newNumber.ToString("000"); // 001, 002, ...

            return yearFacultyDepartment + newNumberText;
        }

        

        public void DosyayaYaz()
        {
            StudentList.Sort((a, b) => string.Compare(a.StudentID, b.StudentID, StringComparison.Ordinal));

            List<string> lines = new List<string>();

            for (int i = 0; i < StudentList.Count; i++)
            {
                lines.Add(
                    StudentList[i].FirstName + "|" +
                    StudentList[i].LastName + "|" +
                    StudentList[i].YearOfEnrollment + "|" +
                    StudentList[i].Faculty + "|" +
                    StudentList[i].Department + "|" +
                    StudentList[i].StudentID
                );
            }

            File.WriteAllLines(filePath, lines);
        }

        public void programBaşı()
        {
            StudentList.Clear();
            IDCount = 0;

            if (!File.Exists(filePath))
                return;

            string[] lines = File.ReadAllLines(filePath);

            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                string[] cell = lines[i].Split('|');
                if (cell.Length < 6)
                    continue;

                Student student = new Student(cell[0], cell[1], cell[2], cell[3], cell[4], cell[5]);
                StudentList.Add(student);
                IDCount++;
            }

            DosyayaYaz();
        }

        public static bool TarihGecerliMi(string input, out string duzeltilmisTarih)
        {
            duzeltilmisTarih = "";

            DateTime dt;
            bool sonuc = DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);

            if (!sonuc)
            {
                Console.Clear();
                Console.WriteLine(input + " Tarihi doğru değil!");
                return false;
            }

            duzeltilmisTarih = dt.ToString("dd/MM/yyyy");
            return true;
        }
    }

    static void Main(string[] args)
    {
        new Sistem();
    }
}