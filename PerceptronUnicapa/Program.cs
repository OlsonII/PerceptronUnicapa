using System;

namespace PerceptronUnicapa
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Vamos a entrenar un perceptron");
            Perceptron perceptron = new Perceptron(4, 2, true);
            Console.WriteLine("Entrenando perceptron");
            perceptron.Entrenamiento();
            Console.ReadKey();
        }
    }
}
