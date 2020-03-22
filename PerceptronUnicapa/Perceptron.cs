using System;
using System.Collections.Generic;
using System.Text;

namespace PerceptronUnicapa
{
    public class Perceptron
    {
        private int Entradas;
        private int Salidas;
        private int[,] PatronEntrada = new int[4, 4] { { 1, 0, 1, 0 }, { 0, 1, 0, 1 }, { 0, 0, 1, 1 }, { 0, 1, 0, 0 } };
        private int[,] PatronSalida = new int[4, 2] { { 1, 1 }, { 0, 1 }, { 1, 0 }, { 0, 0 } };
        private double[,] MatrizDePeso;
        private double[] MatrizUmbral;
        private Salida[] RegistroSalidas;
        bool EsBinario;

        private const int _RATADEAPRENDIZAJE = 1;

        public Perceptron(int entradas, int salidas, bool esBinario)
        {
            Entradas = entradas;
            Salidas = salidas;
            EsBinario = esBinario;

            MatrizDePeso = new double[entradas, salidas];
            MatrizUmbral = new double[salidas];
            RegistroSalidas = new Salida[10];

            LlenarMatrizDePeso();
            LlenarMatrizUmbral();            
        }

        private void LlenarMatrizDePeso()
        {
            Random random = new Random();
            for (var i = 0; i < Salidas; i++)
            {
                for (var j = 0; j < Entradas; j++)
                {
                    //TODO: SE CONTROLA PARA POSITIVOS Y NEGATIVOS EN LA MATRIZ DE PESO
                    if(random.Next(0,1) == 0)
                    {
                        MatrizDePeso[j, i] = -1 * random.NextDouble();
                    }
                    else
                    {
                        MatrizDePeso[j, i] = random.NextDouble();
                    }                    
                }
            }
        }

        private void LlenarMatrizUmbral()
        {
            Random random = new Random();
            for (var i = 0; i < Salidas; i++)
            {
                //TODO: SE CONTROLA PARA POSITIVOS Y NEGATIVOS EN LA MATRIZ DE PESO
                if (random.Next(0, 1) == 0)
                {
                    MatrizUmbral[i] = -1 * random.NextDouble();
                }
                else
                {
                    MatrizUmbral[i] = random.NextDouble();
                }
            }
        }

        public void Entrenamiento()
        {
            double operacion = 0.0;
            for (var i = 0; i < Salidas; i++)
            {
                for (var j = 0; j < Entradas; j++)
                {
                    operacion = PatronEntrada[0, j] * MatrizDePeso[j, i];
                }
                int salida = FuncionDeActivacion(operacion -= MatrizUmbral[i]);
                RegistroSalidas[i] = new Salida(salida, PatronSalida[0,i]);
                CalcularError(RegistroSalidas[i]);
                operacion = 0.0;
            }
        }

        private int FuncionDeActivacion(double salida)
        {
            if (EsBinario)
            {
                if (salida >= 0)
                {
                    return 1;
                }
                return 0;
            }
            else
            {
                if (salida >= 0)
                {
                    return 1;
                }
                return -1;
            }
        }

        private void CalcularError(Salida salida)
        {
            var error = salida.GetError();
            if (error > 0 || error < 0)
            {
                Console.WriteLine($"Actualizando pesos para error {salida.GetError()}");
                ActualizacionDePesos();
            }
            else
            {
                //TODO: RED ENTRENADA, siguiente patron.
                Console.WriteLine("red entrenada para patron 0");
            }
        }

        private void ActualizacionDePesos()
        {
            for (var i = 0; i < Salidas; i++)
            {
                for (var j = 0; j < Entradas; j++)
                {
                    MatrizDePeso[j,i] = MatrizDePeso[j, i] + (_RATADEAPRENDIZAJE * RegistroSalidas[i].GetError());
                    Console.WriteLine($"peso {j} , {i} {MatrizDePeso[j,i]}");                   
                }
                MatrizUmbral[i] = MatrizUmbral[i] + 1 * RegistroSalidas[i].GetError();
                Console.WriteLine($"umbral {i} {MatrizUmbral[i]}");
            }
            Entrenamiento();
        }
    }
}
