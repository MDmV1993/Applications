using System;
using System.Linq;

namespace GeniyIdiotConsoleApp
{
    class Program
    {
        static string[] GetQuestions(int indexQuestion)
        {
            string[] questions = new string[indexQuestion];
            questions[0] = "Сколько будет два плюс два умноженное на два?";
            questions[1] = "Бревно нужо распилить на 10 частей, сколько надо сделать распилов?";
            questions[2] = "На двух руках 10 пальцев. Сколько пальцев на 5 руках?";
            questions[3] = "Укол делают каждые полчаса, сколько минут нужно для трех уколов?";
            questions[4] = "Пять свечей горело, две потухли. Сколько свечей осталось?";
            return questions;
        }
        static int[] GetAnswers(int indexQuestion)
        {
            int[] answers = new int[indexQuestion];
            answers[0] = 6;
            answers[1] = 9;
            answers[2] = 25;
            answers[3] = 60;
            answers[4] = 2;
            return answers;
        }
        static string[] GetDiagnoses(int countDiagnoses)
        {
            string[] diagnoses = new string[countDiagnoses];
            diagnoses[0] = "Идиот";
            diagnoses[1] = "Кретин";
            diagnoses[2] = "Дурак";
            diagnoses[3] = "Нормальный";
            diagnoses[4] = "Талант";
            diagnoses[5] = "Гений";
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
            
            int indexQuestion = 5;
            string[] questions = GetQuestions(indexQuestion);

            int[] answers = GetAnswers(indexQuestion);

            string[] diagnoses = GetDiagnoses(6);

            int countRightAnswers = 0;
            int[] mass = new int[indexQuestion];
            mass = GetRandomMass(mass);
            for (int i = 0; i < indexQuestion; i++)
            {


                Console.WriteLine("Вопрос " + (i + 1));
                //int randomQuestions = mass[i];
                Console.WriteLine(questions[mass[i]]);

                int userAnswer = Convert.ToInt32(Console.ReadLine());

                int rightAnswer = answers[mass[i]];
                if (userAnswer == rightAnswer)
                {
                    countRightAnswers++;
                }

            }
            Console.WriteLine("Колличество правильных ответов: " + countRightAnswers);
            Console.WriteLine($"{name} ваш диагноз: " + diagnoses[countRightAnswers]);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Введите свое имя: ");
            string name = Console.ReadLine();
            SetQuestions(name);
            Console.WriteLine("Хотите продолжить тест?");
            string answerContinue = Console.ReadLine();
            while (answerContinue == "да")
            {                
                SetQuestions(name);
                Console.WriteLine("Хотите продолжить тест?");
                answerContinue = Console.ReadLine();
            }
            Console.WriteLine("Спасибо!");
        }
    }
}
