using System;

namespace CMenu
{
    public class Menu
    {
        public Menu(string titulo)
        {
            title = titulo;
            options = 0;

            optionStrings = new string[10];
        }

        public Menu AddOption(string descripcion)
        {
            optionStrings[options] = descripcion;
            options++;
            return this;
        }

        public void Show()
        {
            Console.WriteLine("#----- " + title + " -----#\n");
            for (int i = 0; i < options; ++i)
            {
                Console.WriteLine((i + 1) + ". " + optionStrings[i]);
            }
        }

        public int GetOption()
        {
            int opt = -1;

            while (opt < 0 || opt >= options)
            {
                Console.Write("Selecciona una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opt))
                    opt = -1;
                else
                    opt--;
            }

            return opt;
        }

        private string title;
        private int options;
        private string[] optionStrings;
    }
}
