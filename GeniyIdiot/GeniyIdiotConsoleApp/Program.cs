using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GeniyIdiotConsoleApp
{
    class Program
    {
        static List<string> GetQuestions()
        {
            var questions = new List<string>();
            questions.Add("Сколько будет два плюс два умноженное на два?");
            questions.Add("Бревно нужо распилить на 10 частей, сколько надо сделать распилов?");
            questions.Add("На двух руках 10 пальцев. Сколько пальцев на 5 руках?");
            questions.Add("Укол делают каждые полчаса, сколько минут нужно для трех уколов?");
            questions.Add("Пять свечей горело, две потухли. Сколько свечей осталось?");
            return questions;
        }
        static List<int> GetAnswers()
        {
            var answers = new List<int>();
            answers.Add(6);
            answers.Add(9);
            answers.Add(25);
            answers.Add(60);
            answers.Add(2);
            return answers;
        }
        //коммнтарийы
        static List<string> GetDiagnoses()
        {
            var diagnoses = new List<string>();
            diagnoses.Add("Идиот");
            diagnoses.Add("Кретин");
            diagnoses.Add("Дурак");
            diagnoses.Add("Нормальный");
            diagnoses.Add("Талант");
            diagnoses.Add("Гений");
            return diagnoses;
        }
        static int[] GetRandomMass(int[] mass)
        {
            for (int i = 0; i < mass.Length; i++)
            {
                mass[i] = i;
            }
            Random random = new Random();
            for (int i = mass.Length - 1; i > 0; i--)
            {
                int randomValue = random.Next(0, i);
                int massValue = mass[randomValue];

                mass[randomValue] = mass[i];
                mass[i] = massValue;
            }
            return mass;
        }
        static void SetQuestions(string name)
        {         
            var questions = GetQuestions();

            var answers = GetAnswers();

            var diagnoses = GetDiagnoses();
            int indexQuestion = questions.Count;
            int indexAnswers = answers.Count;
            int countRightAnswers = 0;
            //int[] mass = new int[indexQuestion];
            //mass = GetRandomMass(mass);
            var random = new Random();
            for (int i = 0; i < indexQuestion; i++)
            {
                Console.WriteLine("Вопрос " + (i + 1));
                var randomQuestionsIndex = random.Next(0,questions.Count);
                //int randomQuestions = mass[i];
                Console.WriteLine(questions[randomQuestionsIndex]);
                var userAnswer = GetUserAnswer();
                int rightAnswer = answers[randomQuestionsIndex];
                questions.RemoveAt(randomQuestionsIndex);
                answers.RemoveAt(randomQuestionsIndex);
                if (Convert.ToInt32(userAnswer) == rightAnswer)
                {
                    countRightAnswers++;
                }


            }
            int PercentRightAnswers = GetPercentRightAnswers(countRightAnswers, indexQuestion, indexAnswers);
            Console.WriteLine("Колличество правильных ответов: " + countRightAnswers);
            string diagnose=diagnoses[PercentRightAnswers];
            Console.WriteLine($"{name} ваш диагноз: " + diagnose);
            SaveUserResult(name, countRightAnswers, diagnose);
            bool userChoise = GetUserShoise("Хотите посмотреть предыдущие результаты игры?");
            if(userChoise)    
            {
                ShowUserResults();
            }
        }

        private static void ShowUserResults()
        {
            using (StreamReader reader = new StreamReader("userResults.txt", Encoding.UTF8))
            {
                Console.WriteLine("{0,-10}{1,25}{2,20}", "Имя", "Кол-во прав ответов", "Диагноз");
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] value = line.Split("#");
                    string name = value[0];
                    int countRightAnswers = Convert.ToInt32(value[1]);
                    string diagnose = value[2];

                    Console.WriteLine("{0,-10}{1,25}{2,20}", name,countRightAnswers,diagnose);
                }
                
            }
        }

        private static bool GetUserShoise(string value)
        {
            Console.WriteLine(value);
            string userChoise = Console.ReadLine();
            if (userChoise.ToLower() == "да")
            {
                return true;
            }
            return false;
        }

        private static void SaveUserResult(string name, int countRightAnswers, string diagnose)
        {
            string value = $"{name}#{countRightAnswers}#{diagnose}";
            AppendToFile("userResults.txt", value);
        }
        static void AppendToFile(string filename, string value)
        {
            //StreamWriter writer = new StreamWriter(filename, true, Encoding.UTF8);
            //writer.WriteLine(value);
            //writer.Close();
            using (StreamWriter writer = new StreamWriter(filename, true, Encoding.UTF8))
            {
                writer.WriteLine(value);
            }
        }
        private static int GetPercentRightAnswers(int countRightAnswers, int questionsLength, int answersLenght)
        {
            //return countRightAnswers * 100 / questionsLength;
            double percentRightAnswers = (double)countRightAnswers / questionsLength * answersLenght;
            return Convert.ToInt32(Math.Round(percentRightAnswers));
        }

        private static int GetUserAnswer()
        {
            //int userAnswer;            
            //while (!int.TryParse(Console.ReadLine(), out userAnswer))
            //{
            //    Console.WriteLine("Введите число!");
            //}

            //return userAnswer;
            while (true)
            {
                try
                {
                    return Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введите число!");
                }
                catch(OverflowException)
                {
                    Console.WriteLine("ВВедите число от -2*10^9 до 2*10^9");
                }
            }
        }

        static void Main(string[] args)
        {
            
            Console.WriteLine("#Введите свое имя: ");
            string name = Console.ReadLine();
            SetQuestions(name);
            Console.WriteLine("Хотите продолжить тест?");
            string answerContinue = Console.ReadLine();
            while (answerContinue.ToLower() == "да")
            {                   
                SetQuestions(name);
                Console.WriteLine("Хотите продолжить тест?");
                answerContinue = Console.ReadLine();
            }
            Console.WriteLine("Спасибо!");
        }
    }
}
