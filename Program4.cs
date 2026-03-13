using System.Globalization;

class Program3
{
    public static int StudentCounter = 0;
    public static string StudentID = "";
    public static string[] Students = new string[50];
    public static string[] FacultyDepartment = new string[50];
    public static string işlem = "";
    public static string[] studentDetails = new string[10];

    public static void Main(string[] args)
    {
        programBaşı();

        while (işlem != "5")
        {
            işlemSeçimi();
            işlem = Console.ReadLine();

            switch (işlem)
            {
                case "1":
                    KayıtOluştur();
                    break;
                case "2":
                    KayıtGörüntüle();
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

    public static void işlemSeçimi()
    {
        Console.WriteLine("Lütfen yapmak istediğiniz işlemi seçiniz:");
        Console.WriteLine("1- Yeni kayıt oluştur");
        Console.WriteLine("2- Kayıtları görüntüle");
        Console.WriteLine("3- Bilgi değiştirme");
        Console.WriteLine("4- Kayıt silme");
        Console.WriteLine("5- Çıkış");
    }

    public static void KayıtOluştur()
    {
        bool correct = true;

        Console.Clear();
        Console.WriteLine("Yeni Kayıt Oluşturma İşlemi Seçildi.");
        List<string> StudentInfo = new List<string>();

        Console.WriteLine("Please Enter your First Name:");
        string FirstName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(FirstName)) correct = false;
        StudentInfo.Add(FirstName);
        Console.Clear();

        Console.WriteLine("Please Enter your Last Name:");
        string LastName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(LastName)) correct = false;
        StudentInfo.Add(LastName);
        Console.Clear();

        Console.WriteLine("Please Enter your ID Number (11 haneli):");
        string IDNumber = Console.ReadLine();
        Console.Clear();
        while (!IDControl(IDNumber))
        {
            Console.WriteLine("Please Enter your ID Number (11 haneli):");
            IDNumber = Console.ReadLine();
        }
        StudentInfo.Add(IDNumber);
        Console.Clear();

        Console.WriteLine("Please Enter your Gender:");
        string Gender = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(Gender)) correct = false;
        StudentInfo.Add(Gender);
        Console.Clear();

        Console.WriteLine("Please Enter Date of Birth (DD/MM/YYYY):");
        string DateOfBirthInput = Console.ReadLine();
        string DateOfBirth;
        Console.Clear();
        while (!TarihGecerliMi(DateOfBirthInput, out DateOfBirth))
        {
            Console.WriteLine("Please Enter Date of Birth (DD/MM/YYYY):");
            DateOfBirthInput = Console.ReadLine();
        }
        Console.Clear();
        StudentInfo.Add(DateOfBirth);

        Console.WriteLine("Please Enter your Dtae of Enrollment (DD/MM/YYYY):");
        string YearOfEnrollment = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(YearOfEnrollment)) correct = false;
        StudentInfo.Add(YearOfEnrollment);

        while (!TarihGecerliMi(YearOfEnrollment, out YearOfEnrollment))
        {
            Console.WriteLine("Please Enter your Dtae of Enrollment (DD/MM/YYYY):");
            YearOfEnrollment = Console.ReadLine();
        }
        Console.Clear();

        Console.WriteLine("Please Enter your Faculty:");
        string Faculty = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(Faculty)) correct = false;
        StudentInfo.Add(Faculty);
        Console.Clear();

        Console.WriteLine("Please Enter your Department:");
        string Department = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(Department)) correct = false;
        StudentInfo.Add(Department);
        Console.Clear();

        Console.Clear();
        Console.WriteLine("Girilen bilgiler:");
        Console.WriteLine("First Name: " + FirstName);
        Console.WriteLine("Last Name: " + LastName);
        Console.WriteLine("ID Number: " + IDNumber);
        Console.WriteLine("Gender: " + Gender);
        Console.WriteLine("Date of Birth: " + DateOfBirth);
        Console.WriteLine("Year of Enrollment: " + YearOfEnrollment);
        Console.WriteLine("Faculty: " + Faculty);
        Console.WriteLine("Department: " + Department);
        Console.WriteLine("------------------------------------");
        Console.WriteLine("Bu bilgiler kaydedilsin mi? (Y/N)");
        string onay = Console.ReadLine().ToUpper();

        if (onay != "Y" || !correct)
        {
            Console.Clear();
            Console.WriteLine("Kayıt oluşturma işlemi iptal edildi veya geçersiz bilgi girildi.");
            return;
        }
        else
        {
            int IDCount = 0;
            string facultydepartment = Faculty.Substring(0, 1).ToUpper() + Department.Substring(0, 1).ToUpper();

            FacultyDepartment[StudentCounter] = facultydepartment;

            for (int e = 0; e < FacultyDepartment.Length; e++)
            {
                if (FacultyDepartment[e] == facultydepartment)
                {
                    IDCount++;
                }
            }

            StudentID = YearOfEnrollment.Substring(6) + facultydepartment + $"{IDCount:000}";
            StudentInfo.Add(StudentID);

            Students[StudentCounter] = string.Join("|", StudentInfo);
            StudentCounter++;

            DosyayaYaz();
            Console.Clear();
            Console.WriteLine("Yeni kayıt başarıyla oluşturuldu. Öğrenci ID: " + StudentID);
        }
    }

    public static void KayıtGörüntüle()
    {
        Console.Clear();
        Console.WriteLine("Kayıtları Görüntüleme İşlemi Seçildi.");

        if (StudentCounter == 0)
        {
            Console.WriteLine(StudentCounter);
            Console.WriteLine("Henüz kayıtlı öğrenci yok!");
        }
        else
        {
            Console.WriteLine(StudentCounter);
            for (int i = 0; i < StudentCounter; i++)
            {
                if (string.IsNullOrWhiteSpace(Students[i]))
                    continue;

                Console.WriteLine("Öğrenci bilgileri: " + Students[i]);
                Console.WriteLine("-----------------------------");
            }
        }
    }

    public static void KayıtGüncelle()
    {
        string yesorno = "";
        string SearchID = "0";
        bool found = false;

        Console.Clear();
        Console.WriteLine("Kayıt Güncelleme İşlemi Seçildi.");
        Console.WriteLine("Lütfen güncellemek istediğiniz Öğrenci ID'sini giriniz:");
        SearchID = Console.ReadLine();
        Console.Clear();

        int foundIndex = -1;

        for (int i = 0; i < StudentCounter; i++)
        {
            if (string.IsNullOrWhiteSpace(Students[i]))
                continue;

            studentDetails = Students[i].Split('|');
            if (studentDetails.Length >= 9 && studentDetails[8] == SearchID)
            {
                Console.WriteLine("Öğrenci bilgileri: " + Students[i]);
                found = true;
                foundIndex = i;
                break;
            }
        }

        if (!found)
        {
            Console.Clear();
            Console.WriteLine("Aranan ID'ye sahip öğrenci bulunamadı. Lütfen tekrar deneyiniz.");
            return;
        }

        Console.WriteLine("Değiştirmek istediğiniz bilgiyi seçiniz:");
        Console.WriteLine("1- First Name");
        Console.WriteLine("2- Last Name");
        Console.WriteLine("3- ID Number");
        Console.WriteLine("4- Gender");
        Console.WriteLine("5- Date of Birth");
        Console.WriteLine("6- Year of Enrollment");
        Console.WriteLine("7- Faculty");
        Console.WriteLine("8- Department");
        string changeInfo = Console.ReadLine();
        Console.Clear();

        Console.WriteLine("Lütfen yeni bilgiyi giriniz:");
        string newInfo = Console.ReadLine();

        switch (changeInfo)
        {
            case "1":
                Console.WriteLine(SearchID + " No'lu öğrencinin First Name bilgisini güncellemek istediğinize emin misiniz? (Y/N)");
                break;
            case "2":
                Console.WriteLine(SearchID + " No'lu öğrencinin Last Name bilgisini güncellemek istediğinize emin misiniz? (Y/N)");
                break;
            case "3":
                Console.WriteLine(SearchID + " No'lu öğrencinin ID Number bilgisini güncellemek istediğinize emin misiniz? (Y/N)");
                break;
            case "4":
                Console.WriteLine(SearchID + " No'lu öğrencinin Male or Female bilgisini güncellemek istediğinize emin misiniz? (Y/N)");
                break;
            case "5":
                Console.WriteLine(SearchID + " No'lu öğrencinin Date of Birth bilgisini güncellemek istediğinize emin misiniz? (Y/N)");
                break;
            case "6":
                Console.WriteLine(SearchID + " No'lu öğrencinin Year of Enrollment bilgisini güncellemek istediğinize emin misiniz? (Y/N)");
                break;
            case "7":
                Console.WriteLine(SearchID + " No'lu öğrencinin Faculty bilgisini güncellemek istediğinize emin misiniz? (Y/N)");
                break;
            case "8":
                Console.WriteLine(SearchID + " No'lu öğrencinin Department bilgisini güncellemek istediğinize emin misiniz? (Y/N)");
                break;
            default:
                Console.WriteLine("Geçersiz seçim!");
                return;
        }

        yesorno = Console.ReadLine().ToUpper();
        Console.Clear();

        if (yesorno != "Y")
        {
            if (yesorno == "N")
            {
                Console.Clear();
                Console.WriteLine("Güncelleme işleminden vazgeçildi!");
                return;
            }

            Console.Clear();
            Console.WriteLine("Geçersiz işlem!");
            return;
        }

        if (changeInfo == "3")
        {
            if (!IDControl(newInfo))
            {
                Console.Clear();
                Console.WriteLine("Geçersiz Kimlik Numarası!");
                Console.WriteLine("Kimlik numarası 11 haneli ve sadece rakam olmalıdır.");
                return;
            }
        }

        if (changeInfo == "5")
        {
            string duzeltilmisTarih;
            if (!TarihGecerliMi(newInfo, out duzeltilmisTarih))
            {
                Console.Clear();
                Console.WriteLine("Geçersiz tarih formatı!");
                Console.WriteLine("Lütfen tarihi DD/MM/YYYY formatında giriniz.");
                return;
            }
            newInfo = duzeltilmisTarih;
        }

        string[] detaylar = Students[foundIndex].Split('|');
        detaylar[int.Parse(changeInfo) - 1] = newInfo;

        if (changeInfo == "6" || changeInfo == "7" || changeInfo == "8")
        {
            string year = detaylar[5].Substring(6); 
            string faculty = detaylar[6];
            string department = detaylar[7];
            string facultydepartment = faculty.Substring(0, 1).ToUpper() + department.Substring(0, 1).ToUpper();

            FacultyDepartment[foundIndex] = facultydepartment;

            int IDCount = 0;
            for (int e = 0; e < FacultyDepartment.Length; e++)
            {
                if (FacultyDepartment[e] == facultydepartment)
                {
                    IDCount++;
                }
            }

            string newStudentId = year + facultydepartment + $"{IDCount:000}";
            detaylar[8] = newStudentId;
        }

        Students[foundIndex] = string.Join("|", detaylar);

        Console.Clear();
        Console.WriteLine("Bilgi başarıyla güncellendi.");
        Console.WriteLine("Güncel kayıt: " + Students[foundIndex]);
        DosyayaYaz();
    }

    public static void KayıtSil()
    {
        string yesorno = "";
        Console.Clear();
        Console.WriteLine("Kayıt Silme İşlemi Seçildi.");
        Console.WriteLine("Lütfen silmek istediğiniz Öğrenci ID'sini giriniz:");
        string DeleteID = Console.ReadLine();
        Console.Clear();

        Console.WriteLine(DeleteID + " No'lu öğrenci bilgileri silinecek:");
        Console.WriteLine("Devam etmek istiyor musunuz? (Y/N)");
        yesorno = Console.ReadLine().ToUpper();

        if (yesorno != "Y")
        {
            Console.Clear();
            Console.WriteLine(yesorno == "N" ? "Silme işleminden vazgeçildi!" : "Geçersiz işlem!");
            return;
        }

        bool deleted = false;

        for (int i = 0; i < StudentCounter; i++)
        {
            if (string.IsNullOrWhiteSpace(Students[i]))
                continue;

            string[] detaylar = Students[i].Split('|');
            if (detaylar.Length >= 9 && detaylar[8] == DeleteID)
            {
                for (int k = i; k < StudentCounter - 1; k++)
                {
                    Students[k] = Students[k + 1];
                    FacultyDepartment[k] = FacultyDepartment[k + 1];
                }

                Students[StudentCounter - 1] = "";
                FacultyDepartment[StudentCounter - 1] = "";
                StudentCounter--;
                deleted = true;
                break;
            }
        }

        Console.Clear();

        if (deleted)
        {
            Console.WriteLine("Kayıt başarıyla silindi.");

            DosyayaYaz();
            programBaşı();
        }
        else
        {
            Console.WriteLine("Aranan ID'ye sahip öğrenci bulunamadı. Lütfen tekrar deneyiniz.");
        }
    }

    public static void DosyayaYaz()
    {
        string filePath = "/Users/halilibrahim/Desktop/Code/stringParse/studentList";

        List<string> list = new List<string>();

        for (int i = 0; i < StudentCounter; i++)
        {
            if (!string.IsNullOrWhiteSpace(Students[i]))
            {
                list.Add(Students[i]);
            }
        }

        File.WriteAllLines(filePath, list);
    }

    public static void programBaşı()
    {
        string filePath = "/Users/halilibrahim/Desktop/Code/stringParse/studentList";

        if (!File.Exists(filePath))
        {
            StudentCounter = 0;
            return;
        }

        string[] lines = File.ReadAllLines(filePath);

        int j = 0;
        for (int i = 0; i < lines.Length && i < Students.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(lines[i]))
            {
                string[] lineElements = lines[i].Split("|");
                if (lineElements.Length < 8)
                    continue;

                string year = lineElements[5].Substring(6);
                string facultyInitial = lineElements[6].Substring(0, 1).ToUpper();
                string departmentInitial = lineElements[7].Substring(0, 1).ToUpper();
                string facultydepartment = facultyInitial + departmentInitial;

                FacultyDepartment[j] = facultydepartment;

                int IDCount = 0;
                for (int e = 0; e <= j; e++)
                {
                    if (FacultyDepartment[e] == facultydepartment)
                    {
                        IDCount++;
                    }
                }

                string newId = year + facultydepartment + $"{IDCount:000}";

                if (lineElements.Length < 9)
                {
                    Array.Resize(ref lineElements, 9);
                }
                lineElements[^1] = newId;

                lines[i] = string.Join("|", lineElements);
                Students[j] = lines[i];
                j++;
            }
        }

        StudentCounter = j;
        File.WriteAllLines(filePath, lines);
    }

    public static bool IDControl(string idNumber)
    {
        if (string.IsNullOrWhiteSpace(idNumber))
            return false;

        if (idNumber.Length != 11)
        {
            Console.Clear();
            Console.WriteLine("ID number 11 haneli olmalı!");
            return false;
        }

        for (int i = 0; i < idNumber.Length; i++)
        {
            if (!char.IsDigit(idNumber[i]))
            {
                Console.Clear();
                Console.WriteLine("ID Number sadece rakamlardan oluşmalı");
                return false;
            }
        }

        return true;
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