using System;
using System.Collections.Generic;
using System.Text;

namespace PerceptronUnicapa
{
    public class Salida
    {
        private int _SalidaEsperada;
        private int _SalidaObtenida;

        public Salida(int salidaEsperada, int salidaObtenida)
        {
            _SalidaEsperada = salidaEsperada;
            _SalidaObtenida = salidaObtenida;
        }

        public int GetError()
        {
            return _SalidaEsperada - _SalidaObtenida;
        }
    }
}
