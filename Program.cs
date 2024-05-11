using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

//Тема: Серіалізація об'єктів. Логування
//Модуль 14. Частина 1

namespace _09._05._24_c__lab
{
    //Task_2

    public class AlbumTask2
    {
        private string title;
        private string artist;
        private int release;
        private int duration;
        private string label;

        public AlbumTask2() { }
        public AlbumTask2(string title, string artist, int release, int duration, string label)
        {
            this.title = title;
            this.artist = artist;
            this.release = release;
            this.duration = duration;
            this.label = label;
        }
        public string Title { get { return title; } set { title = value; } }
        public string Artist { get { return artist; } set { artist = value; } }
        public int Release { get { return release; } set { release = value; } }
        public int Duration { get { return duration; } set { duration = value; } }
        public string Label { get { return label; } set { label = value; } }

        public override string ToString()
        {
            return $"title:\t\t{Title};\n" +
                    $"artist:\t\t{Artist};\n" +
                    $"release:\t{Release} year;\n" +
                    $"duration:\t{Duration} min;\n" +
                    $"label:\t\t{Label};\n";
        }
        public void InputAlbumInfo()
        {
            Console.WriteLine("enter album information:");
            Console.Write("title:\t\t");
            Title = Console.ReadLine();
            Console.Write("artist:\t\t");
            Artist = Console.ReadLine();
            Console.Write("release(year):\t");
            Release = Convert.ToInt32(Console.ReadLine());
            Console.Write("duration(min):\t");
            Duration = Convert.ToInt32(Console.ReadLine());
            Console.Write("label:\t\t");
            Label = Console.ReadLine();
            Console.WriteLine();
        }

        public string ReplaceSpace()
        {
            return Title.Replace(" ", "_");
        }
        public string MakeName()
        {
            return $"{ReplaceSpace()}.txt";
        }
        public string ToSerialize()
        {
            string JsonFile = JsonConvert.SerializeObject(this);
            Console.WriteLine($"\t\tfile \'{Title}\' converted to json format\n");
            return JsonFile;
        }

        public void WriteToFile(string file)
        {
            if (File.Exists(MakeName()))
            {
                throw new Exception("file with that name already exists");
            }
            else
            {
                try
                {
                    using (StreamWriter stream_writer = new StreamWriter(MakeName(), false))
                    {
                        stream_writer.Write($"{file}");
                        Console.WriteLine($"\t\tfile \'{MakeName()}\' created\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error: {ex.Message}");
                }
            }
        }
        public string ReadFromFile()
        {
            try
            {
                using (StreamReader stream_reader = new StreamReader(MakeName()))
                {
                    return stream_reader.ReadToEnd();
                }
            }
            catch (FileNotFoundException)
            {
                return "file not found";
            }
            catch (Exception ex)
            {
                return $"error: {ex.Message}";
            }
        }
        public void ToShow()
        {
            Console.WriteLine($"album info:\n\n{this}");
        }
    }

    //Task_3

    public class Song
    {
        private string name { get; set; }
        private int duration { get; set; }
        private string style { get; set; }

        public Song() { }

        public Song(string name, int duration, string style) 
        {
            this.name = name;
            this.duration = duration;
            this.style = style;
        }
        public string Name { get { return name; } set { name = value; } }
        public int Duration { get { return duration; } set { duration = value; } }
        public string Style { get { return style; } set { style = value; } }

        public override string ToString()
        {
            return  $"name:\t\t{Name};\n" +
                    $"duration:\t{Duration} min;\n" +
                    $"style:\t\t{Style};\n";
        }
    }
    public class AlbumTask3
    {
        private string title;
        private string artist;
        private int release;
        private int duration;
        private string label;
        private List<Song> songs;

        public AlbumTask3() { }

        public AlbumTask3(string title, string artist, int release, int duration, string label, List<Song> songs)
        {
            this.title = title;
            this.artist = artist;
            this.release = release;
            this.duration = duration;
            this.label = label;
            this.songs = songs;
        }

        public string Title { get { return title; } set { title = value; } }
        public string Artist { get { return artist; } set { artist = value; } }
        public int Release { get { return release; } set { release = value; } }
        public int Duration { get { return duration; } set { duration = value; } }
        public string Label { get { return label; } set { label = value; } }
        public List<Song> Songs { get { return songs; } set { songs = value; } }

        public override string ToString()
        {
            string songsInfo = string.Join("", songs);
            return $"title:\t\t{Title};\n" +
                    $"artist:\t\t{Artist};\n" +
                    $"release:\t{Release} year;\n" +
                    $"duration:\t{Duration} min;\n" +
                    $"label:\t\t{Label};\n" +
                    $"tracklist:\n{songsInfo}\n";
        }
        public void InputAlbumInfo()
        {
            Console.WriteLine("enter album information:");
            Console.Write("title:\t\t");
            Title = Console.ReadLine();
            Console.Write("artist:\t\t");
            Artist = Console.ReadLine();
            Console.Write("release(year):\t");
            Release = Convert.ToInt32(Console.ReadLine());
            Console.Write("duration(min):\t");
            Duration = Convert.ToInt32(Console.ReadLine());
            Console.Write("label:\t\t");
            Label = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("enter tracklist (press 'q' to quit)");
            songs = new List<Song>();
            while (true)
            {
                Console.Write("name:\t\t");
                string name = Console.ReadLine();
                if (name.ToLower() == "q") break;
                Console.Write("duration(min):\t");
                int duration = Convert.ToInt32(Console.ReadLine());
                Console.Write("style:\t\t");
                string style = Console.ReadLine();
                songs.Add(new Song(name, duration, style));
                Console.WriteLine();
            }
        }
        public string ReplaceSpace()
        {
            return Title.Replace(" ", "_");
        }
        public string MakeName()
        {
            return $"{ReplaceSpace()}.txt";
        }
        public string ToSerialize()
        {
            string JsonFile = JsonConvert.SerializeObject(this);
            Console.WriteLine($"\t\tfile \'{Title}\' converted to json format\n");
            //Console.WriteLine(JsonFile);
            return JsonFile;
        }
        public void WriteToFile(string file)
        {
            if (File.Exists(MakeName()))
            {
                throw new Exception("file with that name already exists");
            }
            else
            {
                try
                {
                    using (StreamWriter stream_writer = new StreamWriter(MakeName(), false))
                    {
                        stream_writer.Write($"{file}");
                        Console.WriteLine($"\t\tfile \'{MakeName()}\' created\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error: {ex.Message}");
                }
            }
        }
        public string ReadFromFile()
        {
            try
            {
                using (StreamReader stream_reader = new StreamReader(MakeName()))
                {
                    return stream_reader.ReadToEnd();
                }
            }
            catch (FileNotFoundException)
            {
                return "file not found";
            }
            catch (Exception ex)
            {
                return $"error: {ex.Message}";
            }
        }
        public void ToShow()
        {
            Console.WriteLine($"album info:\n\n{this}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //Завдання 1:
            //Створіть програму для роботи з масивом цілих чисел з такою
            //функціональністю:

            //1.Введення масиву цілих чисел з клавіатури.
            //2.Фільтр масиву.Залежно від вибору користувача,
            //прибираємо прості числа або числа Фібоначчі.
            //3.Серіалізація масиву.
            //4.Збереження серіалізованого масиву у файл.
            //5.Завантаження серіалізованого масиву з файлу.
            //
            //Після завантаження потрібно виконати десеріалізацію.
            //Вибір певного формату серіалізації потрібно зробити вам.
            //Звертаємо вашу увагу, що вибір має бути обґрунтованим.

            Console.WriteLine($"Task 1\n");

            List<int> list_1 = new List<int>();
            List<int> sorted_list_1 = new List<int>();

            Console.Write("fill the array (by space):\t");
            string input = Console.ReadLine();
            string[] numbers = input.Split(' ');
            foreach (string number in numbers)
            {
                list_1.Add(int.Parse(number));
            }

            Console.Write("to remove prime -> 1\nto remove fibonacci -> 2:\t");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    foreach (int num in list_1)
                    {
                        if (!isPrime(num))
                        {
                            sorted_list_1.Add(num);
                        }
                    }
                    break;
                case 2:
                    foreach (int num in list_1)
                    {
                        if (!isFibonacci(num))
                        {
                            sorted_list_1.Add(num);
                        }
                    }
                    break;
            }

            string json_array_list_1 = JsonConvert.SerializeObject(sorted_list_1);
            Console.WriteLine($"json format: \t\t\t{json_array_list_1}");

            try
            {
                using (StreamWriter stream_writer_1 = new StreamWriter("task_1.txt", false))
                {
                    stream_writer_1.Write($"{json_array_list_1}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error: {ex.Message}");
            };

            if (File.Exists("task_1.txt"))
            {
                using (StreamReader stream_reader_1 = new StreamReader("task_1.txt"))
                {
                    string json_loaded_file_1 = stream_reader_1.ReadToEnd();
                    sorted_list_1 = JsonConvert.DeserializeObject<List<int>>(json_loaded_file_1);
                }
            }

            Console.Write($"console format:\t\t\t");
            foreach (int num in sorted_list_1)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine();

            Console.WriteLine("\nPress any key to continue . . .");
            Console.ReadKey();
            Console.Clear();

            //Завдання 2:
            //Створіть програму для роботи з інформацією про музичний
            //альбом, яка зберігатиме таку інформацію:

            //1.Назва альбому.
            //2.Назва виконавця.
            //3.Рік випуску.
            //4.Тривалість.
            //5.Студія звукозапису.

            //Програма має бути з такою функціональністю:

            //1.Введення інформації про альбом.
            //2.Виведення інформації про альбом.
            //3.Серіалізація альбому.
            //4.Збереження серіалізованого альбому у файл.
            //5.Завантаження серіалізованого альбому з файлу.
            //
            //Після завантаження потрібно виконати десеріалізацію альбому.
            //Вибір певного формату серіалізації потрібно зробити вам.
            //Звертаємо вашу увагу, що вибір має бути обґрунтованим.

            Console.WriteLine($"Task 2\n");

            AlbumTask2 albumTask2 = new AlbumTask2();
            albumTask2.InputAlbumInfo();
            albumTask2.ToShow();
            albumTask2.WriteToFile(albumTask2.ToSerialize());
            Console.WriteLine($"loaded data:\t{albumTask2.ReadFromFile()}");

            Console.WriteLine("\nPress any key to continue . . .");
            Console.ReadKey();
            Console.Clear();

            //Завдання 3
            //Додайте до попереднього завдання список пісень в альбомі.
            //Потрібно зберігати таку інформацію про кожну пісню:

            //1.Назва пісні.
            //2.Тривалість пісні.
            //3.Стиль пісні.

            //Змініть функціональність з попереднього завдання таким чином,
            //щоб вона враховувала перелік пісень.
            //Вибір певного формату серіалізації потрібно зробити вам.
            //Звертаємо вашу увагу, що вибір має бути обґрунтованим.

            Console.WriteLine($"Task 3\n");

            AlbumTask3 albumTask3 = new AlbumTask3();
            albumTask3.InputAlbumInfo();
            albumTask3.ToShow();
            albumTask3.WriteToFile(albumTask3.ToSerialize());
            Console.WriteLine($"loaded data:\t{albumTask3.ReadFromFile()}");

            Console.WriteLine("\nPress any key to continue . . .");
            Console.ReadKey();
            Console.Clear();

            //Завдання 4:
            //Додайте до попереднього завдання можливість створення
            //масиву альбомів.
            //Змініть функціональність з другого завдання таким чином, щоб
            //вона враховувала масив альбомів.
            //Вибір певного формату серіалізації потрібно зробити вам.
            //Звертаємо вашу увагу, що вибір має бути обґрунтованим.

            Console.WriteLine($"Task 4\n");

            while (true)
            {
                Console.WriteLine("add album (press)\t1\nshow albums (press)\t2\nexit (press)\t\t3");
                string choice_menu = Console.ReadLine();

                switch (choice_menu)
                {
                    case "1":
                        AlbumTask3 album = new AlbumTask3();
                        album.InputAlbumInfo();
                        string jsonAlbum = album.ToSerialize();
                        WriteToFile(jsonAlbum);
                        break;
                    case "2":
                        Console.WriteLine("\n\nalbum list:\n");
                        Console.WriteLine(ReadFromFile());
                        break;
                    case "3":
                        return;
                }
                Console.WriteLine("\n");
            }
        }

        static bool isPrime(int number)
        {
            bool isPrime = true;
            if (number <= 1)
            {
                isPrime = false;
            }
            else
            {
                for (int j = 2; j <= Math.Sqrt(number); j++)
                {
                    if (number % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
            }

            return isPrime;
        }
        static bool isFibonacci(int number)
        {
            int sqrt1 = (int)Math.Sqrt(5 * number * number + 4);
            int sqrt2 = (int)Math.Sqrt(5 * number * number - 4);

            return sqrt1 * sqrt1 == 5 * number * number + 4 || sqrt2 * sqrt2 == 5 * number * number - 4;
        }
        static string MakeName(string file)
        {
            return $"{file.Replace(" ", "_")}.txt";
        }
        static void WriteToFile(string file)
        {
                try
                {
                    using (StreamWriter stream_writer = new StreamWriter("album_list.txt", true))
                {
                        stream_writer.Write($"{file}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error: {ex.Message}");
                };
        }
        static string ReadFromFile()
        {
            try
            {
                using (StreamReader stream_reader = new StreamReader("album_list.txt"))
                {
                    return stream_reader.ReadToEnd();
                }
            }
            catch (FileNotFoundException)
            {
                return "file not found";
            }
            catch (Exception ex)
            {
                return $"error: {ex.Message}";
            }
        }
    }
}
